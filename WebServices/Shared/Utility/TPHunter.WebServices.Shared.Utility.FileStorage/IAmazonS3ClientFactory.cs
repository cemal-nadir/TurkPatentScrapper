using Amazon.S3;

namespace TPHunter.WebServices.Shared.Utility.FileStorage
{
    public interface IAmazonS3ClientFactory
    {
        public void CreateClient(string awsAccessKey, string awsSecretKey, string awsRegion);
        public IAmazonS3 GetClient();

    }
}
