using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Rickvdbosch.Talks.Mswfy.Functions
{
    public static class HandleFile
    {
        [FunctionName("HandleFile")]
        [return: ServiceBus("process", Connection = "ServiceBusConnectionSend")]
        public static string Run(
            [BlobTrigger("to-process/{name}", Connection = "StorageConnectionString")]Stream addedBlob,
            string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {addedBlob.Length} Bytes");

            return name;
        }

        #region vNext

        //[FunctionName("HandleFile")]
        //public static async Task Run(
        //    [BlobTrigger("to-process/{name}", Connection = "StorageConnectionString")]Stream addedBlob,
        //    [ServiceBus("process", Connection = "ServiceBusConnectionSend", EntityType = EntityType.Queue)] ICollector<string> queueCollector,
        //    string name, ILogger log)
        //{
        //    log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {addedBlob.Length} Bytes");

        //    using (var reader = new StreamReader(addedBlob))
        //    {
        //        var words = (await reader.ReadToEndAsync()).Split(' ', StringSplitOptions.RemoveEmptyEntries);
        //        Parallel.ForEach(words.Distinct(), (word) =>
        //        {
        //            var rnd = new Random();
        //            queueCollector.Add(word);
        //        });
        //    }
        //}

        #endregion
    }
}