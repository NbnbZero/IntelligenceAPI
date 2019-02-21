using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.File;

namespace Intelligent.Data.AzureFiles
{
    public class CloudFileContext
    {
        /// <summary>
        /// 
        /// </summary>
        private static CloudFileClient _client;

        /// <summary>
        /// 
        /// </summary>
        public static CloudFileContext Instance { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="accountKey"></param>
        private CloudFileContext(string name, string accountKey)
        {
            _client = new CloudStorageAccount(new StorageCredentials(name, accountKey), true)
                .CreateCloudFileClient();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="accountKey"></param>
        /// <returns></returns>
        public static CloudFileContext InitializeContext(string name, string accountKey) => Instance ?? (Instance = new CloudFileContext(name, accountKey));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<CloudFileShare> GetFileShareAsync(string name)
        {
            var share = _client.GetShareReference(name);
            await share.CreateIfNotExistsAsync();
            return share;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<CloudFileDirectory> GetShareRootDirectoryAsync(string name)
        {
            var share = await GetFileShareAsync(name);
            return share.GetRootDirectoryReference();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shareName"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<CloudFileDirectory> GetShareUserDirectoryAsync(string shareName, string user)
        {
            var root = await GetShareRootDirectoryAsync(shareName);
            var userDir = root.GetDirectoryReference(user);
            await userDir.CreateIfNotExistsAsync();
            return userDir;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shareName"></param>
        /// <param name="user"></param>
        /// <param name="sub"></param>
        /// <returns></returns>
        public async Task<CloudFileDirectory> GetShareUserSubDirectoryAsync(string shareName, string user, string sub)
        {
            var userDir = await GetShareUserDirectoryAsync(shareName, user);
            var subDir = userDir.GetDirectoryReference(sub);
            await subDir.CreateIfNotExistsAsync();
            return subDir;
        }
    }
}
