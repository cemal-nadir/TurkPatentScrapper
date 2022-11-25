using System.Threading.Tasks;

namespace TPHunter.WebServices.Scrap.PatentPdf.Abstract
{
    public interface IPdfDownloaderService
    {
        public Task<byte[]> DownloadPdf(string pdfUrl);
    }
}
