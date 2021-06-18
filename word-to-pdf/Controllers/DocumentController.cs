using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Words;
using Microsoft.AspNetCore.Mvc;
using word_to_pdf.Models;

namespace word_to_pdf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {

        [HttpPost("convert")]
        public async Task<IActionResult> ConvertToPdf([FromBody] DocumentData doc)
        {
            try
            {
                byte[] byteArray = Convert.FromBase64String(doc.body);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    memoryStream.Write(byteArray, 0, byteArray.Length);

                    Document convertedDoc = new Document(memoryStream);

                    HandleDocumentWarnings callback = new HandleDocumentWarnings();
                    convertedDoc.WarningCallback = callback;

                    using (MemoryStream dstStream = new MemoryStream())
                    {
                        convertedDoc.Save(dstStream, SaveFormat.Pdf);
                        dstStream.Position = 0;
                        byte[] pdfBytes = dstStream.ToArray();

                        var res = System.Convert.ToBase64String(pdfBytes);
                        return await Task.FromResult(Ok(res));
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
