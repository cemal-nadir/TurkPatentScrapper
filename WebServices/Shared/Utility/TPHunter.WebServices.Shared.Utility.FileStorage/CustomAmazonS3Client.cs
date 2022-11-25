using Amazon.S3;
using TPHunter.WebServices.Shared.Utility.Core.Abstract.FileStorage;

namespace TPHunter.WebServices.Shared.Utility.FileStorage
{
    public class CustomAmazonS3Client : AmazonS3Client
    {
        public CustomAmazonS3Client(IAmazonS3Config amazonS3Config) : base(amazonS3Config.Config.AwsAccessKey, amazonS3Config.Config.AwsSecretAccessKey, Amazon.RegionEndpoint.GetBySystemName(amazonS3Config.Config.Region))
        {

        }
    }
}
