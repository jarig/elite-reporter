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
using System.Threading;
using System.Text.RegularExpressions;

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

        public Language.LanguageType getLanguage()
        {
            return language;
        }

        public void Dispose()
        {
            ocr.Dispose();
        }

        public MissionInfo findAndAnalyzeMissionSummaryPage(string pathToBmp, bool includeImages = false)
        {
            Image<Gray, byte> source = null;
            int tries = 5;
            while (tries > 0)
            {
                try
                {
                    source = new Image<Gray, byte>(pathToBmp);
                    tries -= 1;
                    Thread.Sleep(300);
                    break;
                }
                catch (Exception ex){}
            }
            double widthFactor = ((double)source.Width / (double)baseSize.Width);
            double heightFactor = ((double)source.Height / (double)baseSize.Height);
            Image<Gray, byte> yearTemplate = new Image<Gray, byte>("Assets\\3302_" + language.Code + ".bmp"); // Image A
            yearTemplate = yearTemplate.Resize(widthFactor, Emgu.CV.CvEnum.Inter.Cubic);
            Image<Gray, byte> rewardTemplate = new Image<Gray, byte>("Assets\\reward_" + language.Code + ".bmp"); // Image A
            rewardTemplate = rewardTemplate.Resize(widthFactor, Emgu.CV.CvEnum.Inter.Cubic);
            MissionInfo missionInfo = new MissionInfo();
            try {
                using (Image<Gray, float> result = source.MatchTemplate(yearTemplate, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
                {
                    double[] minValues, maxValues;
                    Point[] minLocations, maxLocations;
                    result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                    if (maxValues[0] > 0.7)
                    {
                        //mission name
                        var match = new Rectangle(new Point(maxLocations[0].X + yearTemplate.Width + (int)(105 * widthFactor), maxLocations[0].Y - (int)(25 * heightFactor)),
                                          new Size((int)(650 * widthFactor), (int)(40 * heightFactor)));
                        if (match.X + match.Width > source.Width || match.Y + match.Height > source.Height)
                        {
                            return null;
                        }
                        var missionRegion = source.GetSubRect(match);
                        try {
                            //match = new Rectangle(new Point(maxLocations[0].X, maxLocations[0].Y), new Size(yearTemplate.Width, yearTemplate.Height));
                            //imageToShow.Draw(match, new Gray(255), 3);
                            //imageToShow.ToBitmap().Save("E:\\test.bmp");
                            if (missionRegion.Width < 850)
                                missionRegion = missionRegion.Resize((double)850 / missionRegion.Width, Emgu.CV.CvEnum.Inter.Cubic);
                            ocr.Recognize(missionRegion.Convert<Gray, byte>());
                            var words = ocr.GetText();
                            var missionName = words.Replace("\r\n", " ").Trim();
                            Trace.TraceInformation("Mission name: " + missionName);
                            missionInfo.MissionName = missionName;
                            if (includeImages)
                            {
                                missionInfo.Images.Add(missionRegion.ToBitmap());
                            }
                        }
                        finally
                        {
                            missionRegion.Dispose();
                        }
                        
                    }
                }
                source = source.GetSubRect(new Rectangle((int)(450*widthFactor), 0, source.Width - (int)(450*widthFactor) - 10, source.Height));
                using (Image<Gray, float> result = source.MatchTemplate(rewardTemplate, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
                {
                    double[] minValues, maxValues;
                    Point[] minLocations, maxLocations;
                    result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                    if (maxValues[0] > 0.65)
                    {
                        int reward = 0;
                        // reward
                        var match = new Rectangle(new Point(maxLocations[0].X + rewardTemplate.Width,
                                                        maxLocations[0].Y),
                                              new Size((int)(210 * widthFactor), rewardTemplate.Height));
                        if (match.X + match.Width > source.Width || match.Y + match.Height > source.Height)
                        {
                            return missionInfo;
                        }
                        var rewardRegion = source.GetSubRect(match);
                        try
                        {
                            if (rewardRegion.Height < 20)
                                rewardRegion = rewardRegion.Resize((double)20 / rewardRegion.Height, Emgu.CV.CvEnum.Inter.Cubic);
                            ocr.Recognize(rewardRegion.Convert<Gray, byte>());
                            var words = ocr.GetText();
                            int.TryParse(Regex.Replace(words, @"[^0-9$]", "").Trim(), out reward);
                            Trace.TraceInformation("Reward: " + reward);
                            missionInfo.Reward = reward;
                        }
                        finally
                        {
                            rewardRegion.Dispose();
                        }
                    }
                }
                source = source.GetSubRect(new Rectangle(0, 0, source.Width, source.Height - (int)(600*heightFactor)));
                // find mission type
                var missionTypePatterns = Directory.GetFiles("Assets\\mTypes", "*.bmp");
                List<Task<Tuple<string, double>>> tasks = new List<Task<Tuple<string, double>>>();
                foreach(var file in missionTypePatterns)
                {
                    tasks.Add(Task.Run(() => { return getMissionType(source, file, widthFactor, heightFactor); }));
                }
                Task.WaitAll(tasks.ToArray());
                Tuple<string, double> maxScore = null;
                foreach (var res in tasks)
                {
                    if (res.Result != null && res.Result.Item2 > 0.5 && (maxScore == null || maxScore.Item2 < res.Result.Item2))
                        maxScore = res.Result;
                }
                if (maxScore != null)
                    missionInfo.MissionType = maxScore.Item1;
                return missionInfo;
            } finally
            {
                source.Dispose();
                yearTemplate.Dispose();
                rewardTemplate.Dispose();
            }
        }

        private Tuple<string, double> getMissionType(Image<Gray, byte> sourceImage, string patternFilePath, double widthFactor, double heightFactor)
        {
            Image<Gray, byte> templateImage = new Image<Gray, byte>(patternFilePath); // Image A
            templateImage = templateImage.Resize(widthFactor, Emgu.CV.CvEnum.Inter.Cubic);
            string missionType = Path.GetFileName(patternFilePath).Replace(".bmp", "");
            using (Image<Gray, float> result = sourceImage.MatchTemplate(templateImage, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                return new Tuple<string, double>(missionType, maxValues[0]);
            }
        }
    }
}
