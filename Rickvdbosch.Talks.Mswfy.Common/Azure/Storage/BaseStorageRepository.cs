using Microsoft.WindowsAzure.Storage;

namespace Rickvdbosch.Talks.Mswfy.Common.Azure.Storage
{
    public abstract class BaseStorageRepository
    {
        #region Fields

        protected CloudStorageAccount _storageAccount;

        #endregion

        #region Constructors

        public BaseStorageRepository(string connectionString)
        {
            _storageAccount = CloudStorageAccount.Parse(connectionString);
        }

        #endregion
    }
}