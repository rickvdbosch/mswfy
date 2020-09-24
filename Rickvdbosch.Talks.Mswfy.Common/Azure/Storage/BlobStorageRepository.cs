using System.IO;
using System.Threading.Tasks;

using Azure.Storage.Blobs;

namespace Rickvdbosch.Talks.Mswfy.Common.Azure.Storage
{
    public class BlobStorageRepository
    {
        #region Fields

        private string _connectionString;

        #endregion

        #region Constants

        private const string FILES_CONTAINER = "files";
        private const string IMAGES_CONTAINER = "images";
        private const string CONTENT_CONTAINER = "content";

        #endregion

        #region Constructors

        public BlobStorageRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        #endregion

        public async Task<Stream> GetFileAsStreamAsync(string filename)
        {
            var blobClient = new BlobClient(_connectionString, FILES_CONTAINER, filename);
            var stream = new MemoryStream();
            await blobClient.DownloadToAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }

        public async Task<string> GetFileContentAsync(string filename)
        {
            var blobClient = new BlobClient(_connectionString, CONTENT_CONTAINER, filename);
            var stream = new MemoryStream();
            await blobClient.DownloadToAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(stream);

            return await reader.ReadToEndAsync();
        }

        public async Task DeleteImage(string filename)
        {
            var blobClient = new BlobClient(_connectionString, IMAGES_CONTAINER, filename);
            await blobClient.DeleteIfExistsAsync();
        }

        public async Task AddFileAsync(string containerName, string filename, Stream file)
        {
            var blobClient = new BlobClient(_connectionString, containerName, filename);
            await blobClient.UploadAsync(file);
        }
    }
}