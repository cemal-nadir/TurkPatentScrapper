namespace TPHunter.Source.Core.Configs
{
    public class GeneralConfig
    {
        public AmazonConfig AmazonConfig { get; set; }
        public TpConfig TpConfig { get; set; }
        public Services Services { get; set; }
        public ServiceConfigs ServicesConfigs { get; set; }

    }
    public class TpConfig
    {
        public string TpSearchPage { get; set; }
        public string TpPatentPdfPage { get; set; }
    }
    public class AmazonConfig
    {
        public AmazonS3Config AmazonS3Config { get; set; }
    }
    public class AmazonS3Config
    {
        public string BucketName { get; set; }
        public string Region { get; set; }
        public string AwsAccessKey { get; set; }
        public string AwsSecretAccessKey { get; set; }
    }

    public class Services
    {
        public string IdentityApiUri { get; set; }
        public string ScrapApiUri { get; set; }
    }

    public class ServiceConfigs
    {
        public string ScrapApiClient { get; set; }
        public string ScrapApiSecret { get; set; }
    }
}
