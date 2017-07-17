using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoIntegration
{
    public class TemporaryDatabase : IMongoDatabase, IDisposable
    {
        public TemporaryDatabase(IMongoClient client, string name)
        {
            this.Client = client;
            this.DatabaseName = GenerateName(name);
            this.Database = client.GetDatabase(this.DatabaseName);
        }

        public string DatabaseName { get; private set; }
        public IMongoDatabase Database { get; private set; }

        private string GenerateName(string name)
        {
            return name + "_" + Guid.NewGuid().ToString().Replace("-", string.Empty);
        }

        public IMongoClient Client { get; private set; }

        public DatabaseNamespace DatabaseNamespace
        {
            get
            {
                return this.Database.DatabaseNamespace;
            }
        }

        public MongoDatabaseSettings Settings
        {
            get
            {
                return this.Database.Settings;
            }
        }

        public void CreateCollection(string name, CreateCollectionOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            this.Database.CreateCollection(name, options, cancellationToken);
        }

        public Task CreateCollectionAsync(string name, CreateCollectionOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Database.CreateCollectionAsync(name, options, cancellationToken);
        }

        public void CreateView<TDocument, TResult>(string viewName, string viewOn, PipelineDefinition<TDocument, TResult> pipeline, CreateViewOptions<TDocument> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            this.Database.CreateView(viewName, viewOn, pipeline, options, cancellationToken);
        }

        public Task CreateViewAsync<TDocument, TResult>(string viewName, string viewOn, PipelineDefinition<TDocument, TResult> pipeline, CreateViewOptions<TDocument> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Database.CreateViewAsync(viewName, viewOn, pipeline, options, cancellationToken);
        }

        public void DropCollection(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            this.Database.DropCollection(name, cancellationToken);
        }

        public Task DropCollectionAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Database.DropCollectionAsync(name, cancellationToken);
        }

        public IMongoCollection<TDocument> GetCollection<TDocument>(string name, MongoCollectionSettings settings = null)
        {
            return this.Database.GetCollection<TDocument>(name, settings);
        }

        public IAsyncCursor<BsonDocument> ListCollections(ListCollectionsOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Database.ListCollections(options, cancellationToken);
        }

        public Task<IAsyncCursor<BsonDocument>> ListCollectionsAsync(ListCollectionsOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Database.ListCollectionsAsync(options, cancellationToken);
        }

        public void RenameCollection(string oldName, string newName, RenameCollectionOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            this.Database.RenameCollection(oldName, newName, options, cancellationToken);
        }

        public Task RenameCollectionAsync(string oldName, string newName, RenameCollectionOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Database.RenameCollectionAsync(oldName, newName, options, cancellationToken);
        }

        public TResult RunCommand<TResult>(Command<TResult> command, ReadPreference readPreference = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Database.RunCommand<TResult>(command, readPreference, cancellationToken);
        }

        public Task<TResult> RunCommandAsync<TResult>(Command<TResult> command, ReadPreference readPreference = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Database.RunCommandAsync(command, readPreference, cancellationToken);
        }

        public IMongoDatabase WithReadConcern(ReadConcern readConcern)
        {
            return this.Database.WithReadConcern(readConcern);
        }

        public IMongoDatabase WithReadPreference(ReadPreference readPreference)
        {
            return this.Database.WithReadPreference(readPreference);
        }

        public IMongoDatabase WithWriteConcern(WriteConcern writeConcern)
        {
            return this.Database.WithWriteConcern(writeConcern);
        }

        void IDisposable.Dispose()
        {
            this.Client.DropDatabase(this.DatabaseName);
        }
    }
}
