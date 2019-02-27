using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

using Rickvdbosch.Talks.Mswfy.Common.Azure.Storage;

namespace Rickvdbosch.Talks.Mswfy
{
    public static class UploadFile
    {
        [FunctionName("UploadFile")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "files")] HttpRequest req, ILogger log)
        {
            var connectionString = Environment.GetEnvironmentVariable("StorageConnectionString", EnvironmentVariableTarget.Process);
            var blobStorageRepository = new BlobStorageRepository(connectionString);

            if (req.Form.Files.Count != 1)
            {
                return new BadRequestResult();
            }

            using (var ms = new MemoryStream())
            {
                var file = req.Form.Files[0];
                await file.CopyToAsync(ms);
                ms.Seek(0, SeekOrigin.Begin);
                await blobStorageRepository.AddFileAsync("upload", file.FileName, ms);
            }

            return new OkResult();
        }
    }
}