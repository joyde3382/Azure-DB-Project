using E18I4DABH4Gr4.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E18I4DABH4Gr4.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DocumentClient client;

        private string DatabaseName { get; }
        private string CollectionId { get; }

        public Repository(string databaseName, string collectionId)
        {
            client = new DocumentClient(
                // new Uri("https://localhost:8081"),
                //"C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");
                new Uri("https://e18i4dab.documents.azure.com:443"),
                "kM87VaX0sSG87AFM2x6LgtUoZ80N6YRumqvnc5TUhyOrH6yoiPHGFpjAEhYeQL1PhRCkN2nKzpNEBifo3mVthw==");
            DatabaseName = databaseName;
            CollectionId = collectionId;

            createDatabase();
            createCollection();
        }
        private void createDatabase()
        {
            client.CreateDatabaseIfNotExistsAsync(new Database { Id = DatabaseName }).Wait();
        }

        private void createCollection()
        {
            client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(DatabaseName), new DocumentCollection { Id = CollectionId }).Wait();
        }
        private Uri GetCollectionURI()
        {
            return UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionId);
        }

        protected IOrderedQueryable<TEntity> GetQuery()
        {
            var queryOptions = new FeedOptions() { MaxItemCount = -1 };
            IOrderedQueryable<TEntity> query = client.CreateDocumentQuery<TEntity>(GetCollectionURI(), queryOptions);
            return query;
        }

        async Task IRepository<TEntity>.Add(TEntity entity)
        {
            await AddHelper(entity);
        }

        private async Task AddHelper(TEntity entity)
        {
            Uri uri = GetCollectionURI();
            var document = await client.CreateDocumentAsync(uri, entity);

            string id = document.Resource.Id;
            setId(entity, id);
            
        }

        async Task IRepository<TEntity>.Set(TEntity entity)
        {
            await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseName, CollectionId, Convert.ToString(getId(entity))), entity);
        }


        Task<TEntity> IRepository<TEntity>.Get(string id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<TEntity> IRepository<TEntity>.GetAll()
        {
            return GetQuery().ToList();
        }

        async Task IRepository<TEntity>.AddRange(IEnumerable<TEntity> entities)
        {
            var tasks = entities.Select(AddHelper);
            await Task.WhenAll(tasks);
        }

        async Task IRepository<TEntity>.Remove(TEntity entity)
        {
            await RemoveHelper(entity);
        }

        private async Task RemoveHelper(TEntity entity)
        {
            await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseName, CollectionId, getId(entity)));
        }

        async Task IRepository<TEntity>.RemoveRange(IEnumerable<TEntity> entities)
        {
            var tasks = entities.Select(RemoveHelper);

            await Task.WhenAll(tasks);
        }

        protected abstract string getId(TEntity entity);
        protected abstract void setId(TEntity entity, string id);

    }
}
