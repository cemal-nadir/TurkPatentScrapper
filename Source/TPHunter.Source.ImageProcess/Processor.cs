using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using TPHunter.Source.Core.Configs;

namespace TPHunter.Source.ImageProcess
{
    public class Processor:IProcessor
    {
       public async Task<bool> IsProductImageApproved(string byteText)
        {
            using var streamReader = new StreamReader(RuntimeConfigs.DesignRejectedImageLocation);
            var checkerImageByteText = await streamReader.ReadToEndAsync();

          


            using Mat checkerMat = new Mat();
            CvInvoke.Imdecode(Convert.FromBase64String(checkerImageByteText), Emgu.CV.CvEnum.ImreadModes.Color, checkerMat);
            
            using Mat productMat = new Mat();
            CvInvoke.Imdecode(Convert.FromBase64String(byteText), Emgu.CV.CvEnum.ImreadModes.Color, productMat);

            using Image<Bgr, byte> productImage = new Image<Bgr, byte>(productMat.Width,productMat.Height);
            productImage.Bytes = Convert.FromBase64String(byteText);

            using Image<Bgr, byte> checkerImage = new Image<Bgr, byte>(checkerMat.Width,checkerMat.Height);
            checkerImage.Bytes = Convert.FromBase64String(checkerImageByteText);

            //Template Matching MatchTemplate ile uygulanmaktadır.
            using (Image<Gray, float> result = productImage.MatchTemplate(checkerImage,Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                //Benzerlik eşiği. İdeal olanı siz seçeceksiniz.
                return !(maxValues[0] > 0.6);
                
            } 
        }
    }
}
