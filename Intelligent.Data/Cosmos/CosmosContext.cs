using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace Intelligent.Data.Cosmos
{
    public class CosmosContext
    {
        private static string _database;
        private static DocumentClient _client;

        public static CosmosContext Instance { get; private set; }

        private CosmosContext(string database, string endpoint, string key)
        {
            _client = new DocumentClient(
                new Uri(endpoint),
                key,
                new ConnectionPolicy()
                {
                    EnableEndpointDiscovery = false
                }
            );
            _database = database;

            CreateDatabaseIfNotExists(database).Wait();
        }

        private static async Task CreateDatabaseIfNotExists(string database)
        {
            try
            {
                await _client.ReadDatabaseAsync(
                    databaseUri: UriFactory.CreateDatabaseUri(database)
                );
            }
            catch (DocumentClientException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    await _client.CreateDatabaseAsync(
                        database: new Database() { Id = database }
                    );
                }
                else
                {
                    throw;
                }
            }
        }

        public static CosmosContext InitializeContext(string database, string endpoint, string key) => Instance ?? (Instance = new CosmosContext(database, endpoint, key));

        public async Task<T> GetDocumentAsync<T>(string partition, string id) where T : Document
        {
            try
            {
                return (T)(dynamic)await _client.ReadDocumentAsync(
                    documentUri: UriFactory.CreateDocumentUri(_database, partition, id)
                );
            }
            catch (DocumentClientException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<IList<T>> GetDocumentsAsync<T>(string partition, Expression<Func<T, bool>> predicate = null)
        {
            IDocumentQuery<T> query;

            if (predicate == null)
            {
                query = _client
                    .CreateDocumentQuery<T>(UriFactory.CreateDocumentCollectionUri(_database, partition), new FeedOptions { MaxItemCount = -1 })
                    .AsDocumentQuery();
            }
            else
            {
                query = _client
                    .CreateDocumentQuery<T>(UriFactory.CreateDocumentCollectionUri(_database, partition), new FeedOptions { MaxItemCount = -1 })
                    .Where(predicate)
                    .AsDocumentQuery();
            }

            var results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }

        public async Task<T> CreateDocumentAsync<T>(T entity, string partition) where T : Document
        {
            return (T)(dynamic) await _client.CreateDocumentAsync(
                documentCollectionUri: UriFactory.CreateDocumentCollectionUri(_database, partition),
                document: entity
            );
        }

        public async Task<T> UpdateDocumentAsync<T>(T entity, string partition, string id) where T : Document
        {
            return (T)(dynamic)await _client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_database, partition, id), entity);
        }

        public async Task<T> DeleteItemAsync<T>(string id, string partition) where T : Document
        {
            return (T)(dynamic)await _client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(_database, partition, id));
        }
    }
}
