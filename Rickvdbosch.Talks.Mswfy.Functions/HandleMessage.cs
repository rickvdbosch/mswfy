using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Rickvdbosch.Talks.Mswfy.Common.Azure.Storage;

namespace Rickvdbosch.Talks.Mswfy.Functions
{
    public static class HandleMessage
    {
        [FunctionName("HandleMessage")]
        public static async Task Run(
            [ServiceBusTrigger("process", Connection = "ServiceBusConnectionListen")]string queueMessage,
            ILogger log)
        {
            var connectionString = Environment.GetEnvironmentVariable("StorageConnectionString", EnvironmentVariableTarget.Process);
            var blobStorageRepository = new BlobStorageRepository(connectionString);

            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(queueMessage));

            var rnd = new Random();
            await Task.Delay(rnd.Next(12000, 36000));

            await blobStorageRepository.AddFileAsync("scaled", queueMessage, memoryStream);
        }
    }
}