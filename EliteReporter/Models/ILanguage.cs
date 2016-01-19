using Emgu.CV.OCR;

namespace EliteReporter.Models
{
    public interface ILanguage
    {
        string Code { get; set; }
        string Name { get; set; }
        OcrEngineMode OcrMode { get; set; }
        string OcrWhitelist { get; set; }
    }
}