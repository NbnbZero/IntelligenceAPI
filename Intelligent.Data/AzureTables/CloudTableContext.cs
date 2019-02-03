using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

namespace Intelligent.Data.AzureTables
{
    public class CloudTableContext
    {
        private static CloudTableClient _client;

        public static CloudTableContext Instance { get; private set; }

        private CloudTableContext(string name, string accountKey)
        {
            _client = new CloudStorageAccount(new StorageCredentials(name, accountKey), true)
                .CreateCloudTableClient();
        }

        public static CloudTableContext InitializeContext(string name, string accountKey) => Instance ?? (Instance = new CloudTableContext(name, accountKey));

        public async Task<CloudTable> GetTableReference(string table)
        {
            var _ref = _client.GetTableReference(table);
            await _ref.CreateIfNotExistsAsync();
            return _ref;
        }
    }
}
