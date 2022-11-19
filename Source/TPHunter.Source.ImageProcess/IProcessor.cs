using System.Threading.Tasks;

namespace TPHunter.Source.ImageProcess
{
    public interface IProcessor
    {
        public Task<bool> IsProductImageApproved(string byteText);
    }
}
