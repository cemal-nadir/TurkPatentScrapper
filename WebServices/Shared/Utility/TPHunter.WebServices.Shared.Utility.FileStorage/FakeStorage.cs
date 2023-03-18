using System;
using System.IO;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.Utility.Core.Abstract.FileStorage;

namespace TPHunter.WebServices.Shared.Utility.FileStorage
{
    public class FakeStorage : IFileTransferManager
    {

        private const string Location = "D:\\CustomProjects\\SistemPatent\\FakeStorage";

        private static void CheckAndCreateDirectory(string bucketName, string subDirectory = null)
        {
            if (!Directory.Exists($"{Location}\\{bucketName}"))
                Directory.CreateDirectory($"{Location}\\{bucketName}");

            if (subDirectory != null && !Directory.Exists($"{Location}\\{bucketName}\\{subDirectory}"))
                Directory.CreateDirectory($"{Location}\\{bucketName}\\{subDirectory}");
        }

        private static string GetFileName(string fileId, string extension, string bucketName, string subDirectory = null)
        {
            return subDirectory != null
                ? $"{Location}\\{bucketName}\\{subDirectory}\\{fileId}.{extension}"
                : $"{Location}\\{bucketName}\\{fileId}.{extension}";
        }
       
        public async Task<string> Upload(string byteText, string extension, string bucketName, string subDirectory = null)
        {
            CheckAndCreateDirectory(bucketName, subDirectory);
            if (byteText == null) return null;
            var bytes = Convert.FromBase64String(byteText);
            await using var newMemoryStream = new MemoryStream(bytes, 0, bytes.Length);
            var fileId = Guid.NewGuid().ToString("N");
            var file = GetFileName(fileId, extension, bucketName, subDirectory);

            await using var fileStream = new FileStream(file, FileMode.Create);
            newMemoryStream.WriteTo(fileStream);
            return fileId;
        }

        public async Task<string> Upload(byte[] byteArray, string extension, string bucketName, string subDirectory = null)
        {
            CheckAndCreateDirectory(bucketName, subDirectory);

            if (byteArray == null) return null;
            await using var newMemoryStream = new MemoryStream(byteArray, 0, byteArray.Length);
            var fileId = Guid.NewGuid().ToString("N");
            var file = GetFileName(fileId, extension, bucketName, subDirectory);

            await using var fileStream = new FileStream(file, FileMode.Create);
            newMemoryStream.WriteTo(fileStream);
            return fileId;
        }

        public async Task<string> Upload(MemoryStream memoryStream, string extension, string bucketName, string subDirectory = null)
        {
            CheckAndCreateDirectory(bucketName, subDirectory);

          
            var fileId = Guid.NewGuid().ToString("N");
            var file = GetFileName(fileId, extension, bucketName, subDirectory);

            await using var fileStream = new FileStream(file, FileMode.Create);
            memoryStream.WriteTo(fileStream);
            return fileId;
        }

        public Task<byte[]> Download(string fileId, string extension, string bucketName, string subDirectory = null)
        {
            CheckAndCreateDirectory(bucketName, subDirectory);

            var file = GetFileName(fileId, extension, bucketName, subDirectory);

          return File.ReadAllBytesAsync(file);
        }

        public Task DeleteFile(string fileId, string extension, string bucketName, string subDirectory = null)
        {
            CheckAndCreateDirectory(bucketName, subDirectory);

            var file = GetFileName(fileId, extension, bucketName, subDirectory);

            File.Delete(file);

            return Task.CompletedTask;
        }

        public bool IsFileExists(string fileId, string extension, string bucketName, string subDirectory = null)
        {
            CheckAndCreateDirectory(bucketName, subDirectory);

            var file = GetFileName(fileId, extension, bucketName, subDirectory);

            return File.Exists(file);
        }
    }
}
