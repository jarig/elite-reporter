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

            public override string ToString()
            {
                return Name;
            }
        }

        public static LanguageType Russian = new LanguageType()
        {
            Name = "Русский",
            Code = "rus"
        };

        public static LanguageType English = new LanguageType()
        {
            Name = "English",
            Code = "eng"
        };
    }
}
