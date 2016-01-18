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

namespace EliteReporter.Utils
{
    public class ScreenAnalyzer
    {
        private Tesseract ocr;
        private Size baseSize = new Size(1280, 800);
        public Size ScreenSize { get; set; }
        
        public ScreenAnalyzer(string languageCode, Size screenSize)
        {
            ScreenSize = screenSize;
            ocr = new Tesseract("Assets\\", languageCode, OcrEngineMode.TesseractCubeCombined);
        }

        public MissionInfo findAndAnalyzeMissionSummaryPage(bool includeImages = false)
        {
            double factor = ((double)ScreenSize.Width / (double)baseSize.Width);
            var bounds = new Rectangle(0, 0, Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            using (var bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(
                        new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size
                    );
                    var tempFilePath = Path.GetTempPath() + "eliteReporterTemp.bmp";
                    if (File.Exists(tempFilePath))
                        File.Delete(tempFilePath);
                    bitmap.Save(tempFilePath, ImageFormat.Bmp);

                    Image<Bgr, byte> source = new Image<Bgr, byte>(tempFilePath); // Image B
                    Image<Bgr, byte> yearTemplate = new Image<Bgr, byte>("Assets\\3302.png"); // Image A
                    yearTemplate = yearTemplate.Resize(factor, Emgu.CV.CvEnum.Inter.Cubic);
                    Image<Bgr, byte> imageToShow = source.Copy();

                    using (Image<Gray, float> result = source.MatchTemplate(yearTemplate, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
                    {
                        double[] minValues, maxValues;
                        Point[] minLocations, maxLocations;
                        result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                        // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                        if (maxValues[0] > 0.7)
                        {
                            // This is a match. Do something with it, for example draw a rectangle around it.
                            /*Rectangle match = new Rectangle(new Point(maxLocations[0].X - template.Width - (int)(205*factor), maxLocations[0].Y - (int)(30 *factor)), 
                                              new Size((int)(190*factor), (int)(50*factor)));
                            var region = imageToShow.GetSubRect(match);
                            if (region.Width < 450)
                                region = region.Resize(400/region.Width, Emgu.CV.CvEnum.Inter.Cubic);
                            ocr.Recognize(region);
                            var words = ocr.GetText();
                            var stationName = words.Replace("\r\n", " ");
                            Trace.TraceInformation("Station name: " + stationName);*/

                            //mission name
                            var match = new Rectangle(new Point(maxLocations[0].X + yearTemplate.Width + (int)(105 * factor), maxLocations[0].Y - (int)(25 * factor)),
                                              new Size((int)(650 * factor), (int)(40 * factor)));
                            var missionRegion = imageToShow.GetSubRect(match);
                            if (missionRegion.Width < 850)
                                missionRegion = missionRegion.Resize((double)850 / missionRegion.Width, Emgu.CV.CvEnum.Inter.Cubic);
                            ocr.Recognize(missionRegion.Convert<Gray, byte>());
                            var words = ocr.GetText();
                            var missionName = words.Replace("\r\n", " ");
                            Trace.TraceInformation("Mission name: " + missionName);
                            var missionInfo =  new MissionInfo()
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
                }
            }
            return null;
        }
    }
}
