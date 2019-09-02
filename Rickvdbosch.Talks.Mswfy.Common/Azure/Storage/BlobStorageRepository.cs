using System.IO;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage.Blob;

namespace Rickvdbosch.Talks.Mswfy.Common.Azure.Storage
{
    public class BlobStorageRepository : BaseStorageRepository
    {
        #region Fields

        private CloudBlobClient _blobClient;

        #endregion

        #region Constants

        private const string FILES_CONTAINER = "files";
        private const string IMAGES_CONTAINER = "images";
        private const string CONTENT_CONTAINER = "content";

        #endregion

        #region Constructors

        public BlobStorageRepository(string connectionString) : base(connectionString)
        {
            _blobClient = _storageAccount.CreateCloudBlobClient();
        }

        #endregion

        public async Task<Stream> GetFileAsStreamAsync(string fileName)
        {
            var container = _blobClient.GetContainerReference(FILES_CONTAINER);
            var blob = container.GetBlockBlobReference(fileName);
            var stream = new MemoryStream();
            await blob.DownloadToStreamAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }

        public async Task<string> GetFileContentAsync(string fileName)
        {
            var container = _blobClient.GetContainerReference(CONTENT_CONTAINER);
            var blob = container.GetBlockBlobReference(fileName);

            return await blob.DownloadTextAsync();
        }

        public async Task DeleteImage(string filename)
        {
            var container = _blobClient.GetContainerReference(IMAGES_CONTAINER);
            var blob = container.GetBlockBlobReference(filename);

            if (await blob.ExistsAsync())
            {
                await blob.DeleteAsync();
            }
        }

        public async Task AddFileAsync(string containerName, string filename, Stream file)
        {
            var container = _blobClient.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();
            var blob = container.GetBlockBlobReference(filename);

            if (await blob.ExistsAsync())
            {
                // TODO: handle this?
            }
            else
            {
                await blob.UploadFromStreamAsync(file);
            }
        }
    }
}