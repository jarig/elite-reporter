using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.OCR;
using System.Diagnostics;
using EliteReporter.Models;
using System.Xml;
using System.Xml.Linq;

namespace EliteReporter.Utils
{
    public class ScreenAnalyzer
    {
        private Tesseract ocr;
        private Size baseSize = new Size(1280, 800);
        private Language.LanguageType language;
        
        public ScreenAnalyzer(string languageCode)
        {
            this.language = Language.codeToLanguage(languageCode);
            ocr = new Tesseract("Assets\\", languageCode, language.OcrMode);
            //ocr.SetVariable("tessedit_char_whitelist", language.OcrWhitelist);
        }

        public MissionInfo findAndAnalyzeMissionSummaryPage(string pathToBmp, bool includeImages = false)
        {
            Image<Gray, byte> source = new Image<Gray, byte>(pathToBmp); // Image B
            double factor = ((double)source.Width / (double)baseSize.Width);
            Image<Gray, byte> yearTemplate = new Image<Gray, byte>("Assets\\3302_"+ language.Code + ".bmp"); // Image A
            yearTemplate = yearTemplate.Resize(factor, Emgu.CV.CvEnum.Inter.Cubic);

            using (Image<Gray, float> result = source.MatchTemplate(yearTemplate, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                if (maxValues[0] > 0.7)
                {
                    //mission name
                    var match = new Rectangle(new Point(maxLocations[0].X + yearTemplate.Width + (int)(105 * factor), maxLocations[0].Y - (int)(25 * factor)),
                                      new Size((int)(650 * factor), (int)(40 * factor)));
                    //match = new Rectangle(new Point(maxLocations[0].X, maxLocations[0].Y), new Size(yearTemplate.Width, yearTemplate.Height));
                    var missionRegion = source.GetSubRect(match);
                    //imageToShow.Draw(match, new Gray(255), 3);
                    //imageToShow.ToBitmap().Save("E:\\test.bmp");
                    if (missionRegion.Width < 850)
                        missionRegion = missionRegion.Resize((double)850 / missionRegion.Width, Emgu.CV.CvEnum.Inter.Cubic);
                    ocr.Recognize(missionRegion.Convert<Gray, byte>());
                    var words = ocr.GetText();
                    var missionName = words.Replace("\r\n", " ");
                    Trace.TraceInformation("Mission name: " + missionName);
                    var missionInfo = new MissionInfo()
                    {
                        MissionName = missionName
                    };
                    if (includeImages)
                    {
                        missionInfo.Images.Add(missionRegion.ToBitmap());
                    }
                    return missionInfo;
                }
            }
            return null;
        }
    }
}
