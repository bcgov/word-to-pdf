using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Words;
using Aspose.Words.Fonts;
using Microsoft.AspNetCore.Mvc;
using word_to_pdf.Models;

namespace word_to_pdf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {

        private string getFontsDir()
        {
            return Environment.CurrentDirectory + "/Fonts";
        }

        [HttpPost("convert")]
        public async Task<IActionResult> ConvertToPdf([FromBody] DocumentData doc)
        {
            try
            {
                FontSettings fontSettings = new FontSettings();
                fontSettings.SetFontsFolder(getFontsDir(), true);

                byte[] byteArray = Convert.FromBase64String(doc.body);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    memoryStream.Write(byteArray, 0, byteArray.Length);

                    Document convertedDoc = new Document(memoryStream);
                    convertedDoc.FontSettings = fontSettings;

                    HandleDocumentWarnings callback = new HandleDocumentWarnings();
                    convertedDoc.WarningCallback = callback;

                    using (MemoryStream dstStream = new MemoryStream())
                    {
                        convertedDoc.Save(dstStream, SaveFormat.Pdf);
                        dstStream.Position = 0;
                        byte[] pdfBytes = dstStream.ToArray();

                        var res = System.Convert.ToBase64String(pdfBytes);
                        return await Task.FromResult(Ok(new { pdfString = res }));
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }

    public class HandleDocumentWarnings : IWarningCallback
    {
        public void Warning(WarningInfo info)
        {
            Console.WriteLine(info.WarningType + " :: " + info.Description.ToString());
        }
    }
}
