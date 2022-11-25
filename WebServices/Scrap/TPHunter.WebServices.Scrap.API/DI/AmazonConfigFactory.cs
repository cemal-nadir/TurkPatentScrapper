using Microsoft.Extensions.Options;
using TPHunter.WebServices.Shared.Utility.Core.Abstract.FileStorage;
using TPHunter.WebServices.Shared.Utility.Core.Models.FileStorage;

namespace TPHunter.WebServices.Scrap.API.DI
{
    public class AmazonConfigFactory:IAmazonS3Config
    {
        public AmazonConfigFactory(IOptions<AmazonS3Config>amazonS3Config)
        {
            Config = new AmazonS3Config()
            {
                AwsAccessKey = amazonS3Config.Value.AwsAccessKey,
                AwsSecretAccessKey = amazonS3Config.Value.AwsSecretAccessKey,
                Region = amazonS3Config.Value.Region
            };
        }

        public AmazonS3Config Config { get; }
    }
}
