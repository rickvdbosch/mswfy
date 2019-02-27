using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Rickvdbosch.Talks.Mswfy.Functions
{
    public static class HandleUploadedFile
    {
        [FunctionName("HandleUploadedFile")]
        public static async Task Run(
            [BlobTrigger("upload/{name}", Connection = "StorageConnectionString")]Stream addedBlob,
            [Blob("copied/{name}", FileAccess.Write, Connection = "StorageConnectionString")] Stream stream,
            string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {addedBlob.Length} Bytes");
            
            await addedBlob.CopyToAsync(stream);
        }
    }
}