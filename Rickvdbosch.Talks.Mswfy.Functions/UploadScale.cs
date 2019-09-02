using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Rickvdbosch.Talks.Mswfy.Functions
{
    public static class UploadScale
    {
        [FunctionName("UploadScale")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "scale")] HttpRequest req,
            [Blob("to-process", FileAccess.Write, Connection = "StorageConnectionString")] Stream stream,
            ILogger log)
        {
            if (req.Form.Files.Count != 1)
            {
                return new BadRequestResult();
            }

            var file = req.Form.Files[0];
            await file.CopyToAsync(stream);

            return new OkResult();
        }
    }
}
