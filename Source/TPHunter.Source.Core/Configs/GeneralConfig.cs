namespace TPHunter.Source.Core.Configs
{
    public class GeneralConfig
    {
        public TpConfig TpConfig { get; set; }
        public Services Services { get; set; }
        public ServiceConfigs ServicesConfigs { get; set; }
        public MainConfig MainConfig { get; set; }

    }

    public class MainConfig
    {
        public int BrowserCount { get; set; }
        public int MaxUploadPerBrowser { get; set; }
    }
    public class TpConfig
    {
        public string TpSearchPage { get; set; }
        public string TpPatentPdfPage { get; set; }
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
