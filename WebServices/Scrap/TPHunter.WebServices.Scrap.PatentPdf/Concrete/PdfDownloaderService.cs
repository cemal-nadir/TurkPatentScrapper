using System;
using System.Net.Http;
using System.Threading.Tasks;
using TPHunter.WebServices.Scrap.PatentPdf.Abstract;

namespace TPHunter.WebServices.Scrap.PatentPdf.Concrete
{
    public class PdfDownloaderService:IPdfDownloaderService
    {
        private readonly HttpClient _httpClient;

        public PdfDownloaderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<byte[]> DownloadPdf(string pdfUrl)
        {
            try
            {
                return await _httpClient.GetByteArrayAsync(pdfUrl);
            }
            catch
            {
                return null;
            }
        }
    }
}
