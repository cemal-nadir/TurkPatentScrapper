using Microsoft.Extensions.Configuration;
using TPHunter.WebServices.Shared.Utility.FileStorage;
using TPHunter.WebServices.Shared.Utility.FileStorage.Models;

namespace TPHunter.WebServices.Scrap.API.DI
{
    public static class GeneralDı
    {
        
        public static void CreateClient(IAmazonS3ClientFactory amazonS3ClientFactory,IConfiguration configuration)
        {
            if (amazonS3ClientFactory.GetClient() is null)
            {
                var amazonSettings = configuration.GetValue<AmazonConfig>("AmazonS3Config");
                amazonS3ClientFactory.CreateClient(amazonSettings.AwsAccessKey, amazonSettings.AwsSecretAccessKey, amazonSettings.Region);
            }
        }

    }
}
