namespace TPHunter.Source.Core.Configs
{
    public class AmazonS3Config
    {
        public string BucketName { get; set; }
        public string Region { get; set; }
        public string AwsAccessKey { get; set; }
        public string AwsSecretAccessKey { get; set; }
    }
}