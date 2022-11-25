using TPHunter.WebServices.Shared.Utility.Core.Models.FileStorage;

namespace TPHunter.WebServices.Shared.Utility.Core.Abstract.FileStorage
{
    public interface IAmazonS3Config
    {
        public AmazonS3Config Config { get; }
    }
}
