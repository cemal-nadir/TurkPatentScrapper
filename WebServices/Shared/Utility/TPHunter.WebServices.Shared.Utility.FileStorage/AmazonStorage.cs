using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using TPHunter.WebServices.Shared.Utility.Core.Abstract.FileStorage;

namespace TPHunter.WebServices.Shared.Utility.FileStorage
{
    public class AmazonStorage : IFileTransferManager
    {
        private readonly IAmazonS3 _amazonClient;
        public AmazonStorage(IAmazonS3 amazonClient)
        {
            _amazonClient = amazonClient;
        }
        public async Task<string> Upload(string byteText, string extension,string bucketName, string subDirectory)
        {
          
            var bytes = Convert.FromBase64String(byteText);
            await using var newMemoryStream = new MemoryStream(bytes, 0, bytes.Length);
            var fileId = Guid.NewGuid().ToString("N");
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = newMemoryStream,
                Key = $"{fileId}.{extension}",
                BucketName = bucketName + $"/{subDirectory}",
                CannedACL = S3CannedACL.PublicRead
            };

            var fileTransferUtility = new TransferUtility(_amazonClient);
            await fileTransferUtility.UploadAsync(uploadRequest);
            return fileId;
        }

        public async Task<string> Upload(byte[] byteArray, string extension, string bucketName, string subDirectory = null)
        {
          
            await using var newMemoryStream = new MemoryStream(byteArray, 0, byteArray.Length);
            var fileId = Guid.NewGuid().ToString("N");
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = newMemoryStream,
                Key = $"{fileId}.{extension}",
                BucketName = bucketName + $"/{subDirectory}",
                CannedACL = S3CannedACL.PublicRead
            };

            var fileTransferUtility = new TransferUtility(_amazonClient);
            await fileTransferUtility.UploadAsync(uploadRequest);
            return fileId;
        }

        public async Task<string> Upload(MemoryStream memoryStream, string extension, string bucketName, string subDirectory)
        {


            var fileId = Guid.NewGuid().ToString("N");
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = memoryStream,
                Key = $"{fileId}.{extension}",
                BucketName = bucketName + $"/{subDirectory}",
                CannedACL = S3CannedACL.PublicRead
            };

            var fileTransferUtility = new TransferUtility(_amazonClient);
            await fileTransferUtility.UploadAsync(uploadRequest);
            return fileId;
        }
        public async Task<byte[]> Download(string fileId, string extension, string bucketName, string subDirectory)
        {
            MemoryStream ms = null;

            var getObjectRequest = new GetObjectRequest
            {
                BucketName = bucketName + $"/{subDirectory}",
                Key = $"{fileId}.{extension}"
            };

            using (var response = await _amazonClient.GetObjectAsync(getObjectRequest))
            {
                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    using (ms = new MemoryStream())
                    {
                        await response.ResponseStream.CopyToAsync(ms);
                    }
                }
            }

            if (ms is null || ms.ToArray().Length < 1)
                throw new FileNotFoundException($"The document '{fileId}.{extension}' is not found");

            return ms.ToArray();
        }
        public async Task DeleteFile(string fileId,string extension, string bucketName,string subDirectory)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = bucketName + $"/{subDirectory}",
                Key = $"{fileId}.{extension}"
            };
            await _amazonClient.DeleteObjectAsync(request);
        }
        public bool IsFileExists(string fileId,string extension, string bucketName,string subDirectory)
        {
            try
            {
                var request = new GetObjectMetadataRequest()
                {
                    BucketName = bucketName + $"/{subDirectory}",
                    Key = $"{fileId}.{extension}"
                };

                var getObjectMetadataResponse = _amazonClient.GetObjectMetadataAsync(request).Result;

                return getObjectMetadataResponse != null;
            }
            catch (Exception ex)
            {
                if (ex.InnerException is not AmazonS3Exception awsEx) throw;
                if (string.Equals(awsEx.ErrorCode, "NoSuchBucket"))
                    return false;

                if (string.Equals(awsEx.ErrorCode, "NotFound"))
                    return false;

                throw;
            }
        }
    }
}
