using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.IO;
using System.Threading.Tasks;
using TPHunter.Source.Core.Configs;

namespace TPHunter.Source.ImageProcess
{
    public class Processor:IProcessor
    {
        private readonly Image<Bgr, byte> _checkerImage;

        public Processor()
        {
            using var streamReader = new StreamReader(RuntimeConfigs.DesignRejectedImageLocation);
            var checkerImageByteText = streamReader.ReadToEnd();
            using var checkerMat = new Mat();
            CvInvoke.Imdecode(Convert.FromBase64String(checkerImageByteText), Emgu.CV.CvEnum.ImreadModes.Color, checkerMat);
            _checkerImage = new Image<Bgr, byte>(checkerMat.Width, checkerMat.Height);
            _checkerImage.Bytes = Convert.FromBase64String(checkerImageByteText);
        }
       public async Task<bool> IsProductImageApproved(string byteText)
        {
            return await Task.Run(() =>
            {
                using var productMat = new Mat();
                CvInvoke.Imdecode(Convert.FromBase64String(byteText), Emgu.CV.CvEnum.ImreadModes.Color, productMat);

                using var productImage = new Image<Bgr, byte>(productMat.Width, productMat.Height);
                productImage.Bytes = Convert.FromBase64String(byteText);


                //Template Matching MatchTemplate ile uygulanmaktadır.
                using var result = productImage.MatchTemplate(_checkerImage, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed);
                result.MinMax(out _, out var maxValues, out _, out _);

                //Benzerlik eşiği. İdeal olanı siz seçeceksiniz.
                return !(maxValues[0] > 0.6);
            });
            
        }
    }
}
