using Emgu.CV.OCR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteReporter.Models
{
    public class Language
    {
        public class LanguageType : ILanguage
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public OcrEngineMode OcrMode { get; set; }
            public string OcrWhitelist { get; set; }
            public override string ToString()
            {
                return Name;
            }
        }

        public static LanguageType codeToLanguage(string code)
        {
            if (Language.English.Code == code)
                return Language.English;
            if (Language.Russian.Code == code)
                return Language.Russian;

            return Language.English;
        }

        public static LanguageType Russian = new LanguageType()
        {
            Name = "Русский",
            Code = "rus",
            OcrMode = OcrEngineMode.CubeOnly,
            OcrWhitelist = "0123456789абвгдежзийклмнопрстуфхцчшщъыьэюяАБВГДЕЖЗИЙКЛМОПРСТУФХЦЧШЩЭЮЯ-_'\""
        };

        public static LanguageType English = new LanguageType()
        {
            Name = "English",
            Code = "eng",
            OcrMode = OcrEngineMode.TesseractCubeCombined,
            OcrWhitelist = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-_'\""
        };
    }
}
