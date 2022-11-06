using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHunter.Source.Core.Configs
{
    public class GeneralConfig
    {
        public AmazonConfig AmazonConfig { get; set; }
        public TPConfig TPConfig { get; set; }

    }
    public class TPConfig
    {
        public string TPSearchPage { get; set; }
        public string TPPatentPdfPage { get; set; }
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
}
