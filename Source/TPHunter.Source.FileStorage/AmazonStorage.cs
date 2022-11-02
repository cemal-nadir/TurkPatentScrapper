using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace TPHunter.Source.FileStorage
{
    public class AmazonStorage : IFileTransferManager<Core.Configs.AmazonS3Config>
    {
        private Core.Configs.AmazonS3Config _amazonConfig;
        private IAmazonS3 _amazonS3Client;

        public Core.Configs.AmazonS3Config FileStorageConfig
        {
            set
            {
                _amazonConfig = value;
                _amazonS3Client = new AmazonS3Client(value.AwsAccessKey, value.AwsSecretAccessKey, Amazon.RegionEndpoint.GetBySystemName(value.Region));
            }
            get
            {
                return _amazonConfig;
            }
        }
        public async Task<string> Upload(string byteText, string extension, string subDirectory)
        {
            byte[] imageBytes = Convert.FromBase64String(byteText);
            await using var newMemoryStream = new MemoryStream(imageBytes, 0, imageBytes.Length);
            var fileId = Guid.NewGuid().ToString("N");
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = newMemoryStream,
                Key = $"{fileId}.{extension}",
                BucketName = _amazonConfig.BucketName + $"/{subDirectory}",
                CannedACL = S3CannedACL.PublicRead,
            };

            var fileTransferUtility = new TransferUtility(_amazonS3Client);
            await fileTransferUtility.UploadAsync(uploadRequest);
            return fileId;
        }
        public async Task<string> Upload(MemoryStream memoryStream, string extension, string subDirectory)
        {


            var fileId = Guid.NewGuid().ToString("N");
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = memoryStream,
                Key = $"{fileId}.{extension}",
                BucketName = _amazonConfig.BucketName + $"/{subDirectory}",
                CannedACL = S3CannedACL.PublicRead
            };

            var fileTransferUtility = new TransferUtility(_amazonS3Client);
            await fileTransferUtility.UploadAsync(uploadRequest);
            return fileId;
        }
        public async Task<byte[]> Download(string fileId, string extension, string subDirectory)
        {
            MemoryStream ms = null;

            try
            {
                GetObjectRequest getObjectRequest = new GetObjectRequest
                {
                    BucketName = _amazonConfig.BucketName + $"/{subDirectory}",
                    Key = $"{fileId}.{extension}"
                };

                using (var response = await _amazonS3Client.GetObjectAsync(getObjectRequest))
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
                    throw new FileNotFoundException(string.Format("The document '{0}' is not found", $"{fileId}.{extension}"));

                return ms.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task DeleteFile(string fileId,string extension,string subDirectory)
        {
            DeleteObjectRequest request = new DeleteObjectRequest
            {
                BucketName = _amazonConfig.BucketName + $"/{subDirectory}",
                Key = $"{fileId}.{extension}"
            };
            await _amazonS3Client.DeleteObjectAsync(request);
        }
        public bool IsFileExists(string fileId,string extension,string subDirectory)
        {
            try
            {
                GetObjectMetadataRequest request = new GetObjectMetadataRequest()
                {
                    BucketName = _amazonConfig.BucketName + $"/{subDirectory}",
                    Key = $"{fileId}.{extension}",
                };

                var response = _amazonS3Client.GetObjectMetadataAsync(request).Result;

                return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException is AmazonS3Exception awsEx)
                {
                    if (string.Equals(awsEx.ErrorCode, "NoSuchBucket"))
                        return false;

                    else if (string.Equals(awsEx.ErrorCode, "NotFound"))
                        return false;
                }

                throw;
            }
        }
    }
}
