using Amazon.S3;

namespace TPHunter.WebServices.Shared.Utility.FileStorage
{
    public class AmazonS3ClientFactory : IAmazonS3ClientFactory
    {
        private AmazonS3Client _amazonS3Client;
        public void CreateClient(string awsAccessKey, string awsSecretKey, string awsRegion)
        {
            _amazonS3Client = new AmazonS3Client(awsAccessKey, awsSecretKey, Amazon.RegionEndpoint.GetBySystemName(awsRegion));
        }

        public IAmazonS3 GetClient()
        {
            return _amazonS3Client;
        }
    }
}
