using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

namespace Intelligent.Data.AzureTables
{
    public class CloudTableContext
    {
        /// <summary>
        /// 
        /// </summary>
        private static CloudTableClient _client;

        /// <summary>
        /// 
        /// </summary>
        public static CloudTableContext Instance { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="accountKey"></param>
        private CloudTableContext(string name, string accountKey)
        {
            _client = new CloudStorageAccount(new StorageCredentials(name, accountKey), true)
                .CreateCloudTableClient();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="accountKey"></param>
        /// <returns></returns>
        public static CloudTableContext InitializeContext(string name, string accountKey) => Instance ?? (Instance = new CloudTableContext(name, accountKey));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public async Task<CloudTable> GetTableReferenceAsync(string table)
        {
            var _ref = _client.GetTableReference(table);
            await _ref.CreateIfNotExistsAsync();
            return _ref;
        }
    }
}
