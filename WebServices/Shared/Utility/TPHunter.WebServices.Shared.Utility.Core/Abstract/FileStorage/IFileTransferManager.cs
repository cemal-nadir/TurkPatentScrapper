using System.IO;
using System.Threading.Tasks;

namespace TPHunter.WebServices.Shared.Utility.Core.Abstract.FileStorage
{
    public interface IFileTransferManager
    {
        public Task<string> Upload(string byteText, string extension,string bucketName, string subDirectory = null);
        public Task<string> Upload(byte[]byteArray, string extension, string bucketName, string subDirectory = null);
        public Task<string> Upload(MemoryStream memoryStream, string extension, string bucketName, string subDirectory = null);
        public Task<byte[]> Download(string fileId, string extension, string bucketName, string subDirectory = null);
        public Task DeleteFile(string fileId, string extension, string bucketName, string subDirectory = null);
        public bool IsFileExists(string fileId, string extension, string bucketName, string subDirectory = null);
    }
}
