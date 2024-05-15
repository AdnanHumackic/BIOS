using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.Reflection.PortableExecutable;
using System.Text;

namespace PCShop_api.Helper
{
    public class PdfToWord
    {
        public static string ConvertToString(string fileSifra, IWebHostEnvironment webHostEnvironment)
        {
            var fileUrl = GetFile(fileSifra, webHostEnvironment);
            var text = "";

            var pageText = new StringBuilder();
            using (iText.Kernel.Pdf.PdfDocument pdfDocument = new iText.Kernel.Pdf.PdfDocument(new PdfReader(fileUrl)))
            {
                var pageNumbers = pdfDocument.GetNumberOfPages();
                for (int i = 1; i < pageNumbers; i++)
                {
                    LocationTextExtractionStrategy strategy = new LocationTextExtractionStrategy();
                    PdfCanvasProcessor parser = new PdfCanvasProcessor(strategy);
                    parser.ProcessPageContent(pdfDocument.GetPage(i));
                    pageText.AppendLine(strategy.GetResultantText());
                }
            }

            text = pageText.ToString().Replace("\r", "").Replace("\n", "");
            return text;

        }
        public static string GetFile(string path, IWebHostEnvironment env)
        {
            string imageUrl = string.Empty;
            string HostUrl = "https://localhost:7201";
            string filepath = GetFilePath(path, env);
            return filepath;

            if (!System.IO.File.Exists(filepath))
            {
                imageUrl = "";
            }
            else
            {
                imageUrl = HostUrl + "/Fajlovi/Dokumenti/" + path;
            }

            return imageUrl;
        }
        private static string GetFilePath(string productCode, IWebHostEnvironment env)
        {
            return env.WebRootPath + "\\Fajlovi\\Dokumenti\\" + productCode;
        }
    }
  
}
