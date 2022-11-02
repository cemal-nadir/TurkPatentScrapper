using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHunter.Source.FileStorage
{
    public interface IFileTransferManager<Config> where Config : class
    {
        public Config FileStorageConfig { get; set; }
        public Task<string> Upload(string byteText, string extension, string subDirectory = null);
        public Task<string> Upload(MemoryStream memoryStream, string extension, string subDirectory = null);
        public Task<byte[]> Download(string fileId, string extension, string subDirectory = null);
        public Task DeleteFile(string fileId, string extension, string subDirectory = null);
        public bool IsFileExists(string fileId, string extension, string subDirectory = null);
    }
}
