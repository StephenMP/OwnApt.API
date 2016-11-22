using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using OwnApt.Api.AppStart;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Api.Repository.Mongo;
using OwnApt.TestEnvironment.Environment;
using Xunit;

namespace Api.Tests.Component.Repository.Mongo
{
    public abstract class MongoRepositorySteps<TModel, TEntity> : IDisposable where TEntity : MongoEntity
    {
        #region Protected Fields

        protected readonly string collectionName;
        protected readonly string dbName;
        protected readonly IMapper mapper;
        protected IMongoCollection<TEntity> collection;
        protected IMongoDatabase database;
        protected OwnAptTestEnvironment environment;
        protected TModel modelCreated;
        protected TModel modelRead;
        protected IEnumerable<TModel> modelsRead;
        protected MongoRepository<TModel, TEntity> repository;

        #endregion Protected Fields

        #region Private Fields

        private bool disposedValue;
        private TEntity[] entitiesToRead;
        private TEntity entityToRead;
        private TModel modelToCreate;

        #endregion Private Fields

        #region Protected Constructors

        protected MongoRepositorySteps(string dbName, string collectionName)
        {
            this.mapper = OwnAptStartup.BuildMapper();
            this.dbName = dbName;
            this.collectionName = collectionName;
        }

        #endregion Protected Constructors

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void GivenIHaveAMongoTestEnvironment()
        {
            this.environment = new OwnAptTestEnvironmentBuilder()
                            .AddMongo()
                            .BuildEnvironment();

            var client = this.environment.GetMongoClient();
            this.database = client.GetDatabase(this.dbName);
            this.collection = this.database.GetCollection<TEntity>(this.collectionName);
        }

        public void GivenIHaveAnEntitiesToRead()
        {
            this.entitiesToRead = new[]
            {
                Activator.CreateInstance<TEntity>(),
                Activator.CreateInstance<TEntity>(),
                Activator.CreateInstance<TEntity>(),
                Activator.CreateInstance<TEntity>(),
                Activator.CreateInstance<TEntity>()
            };

            foreach (var entity in this.entitiesToRead)
            {
                entity.Id = TestRandom.String;
            }

            this.collection.InsertMany(this.entitiesToRead);
        }

        public void GivenIHaveAnEntityToCreate()
        {
            var entity = Activator.CreateInstance<TEntity>();
            entity.Id = TestRandom.String;

            this.modelToCreate = this.mapper.Map<TModel>(entity);
        }

        public void GivenIHaveAnEntityToDelete()
        {
            this.GivenIHaveAnEntityToRead();
        }

        public void GivenIHaveAnEntityToRead()
        {
            this.entityToRead = Activator.CreateInstance<TEntity>();
            this.entityToRead.Id = TestRandom.String;

            this.collection.InsertOne(this.entityToRead);
        }

        public void GivenIHaveAnEntityToUpdate()
        {
            this.GivenIHaveAnEntityToRead();
        }

        public abstract void GivenIHaveARepository();

        public void ThenICanVerifyICanCreateAsync()
        {
            var entity = this.mapper.Map<TEntity>(modelToCreate);
            var createdEntity = this.mapper.Map<TEntity>(modelCreated);

            Assert.NotNull(this.modelToCreate);
            Assert.NotNull(entity);
            Assert.NotNull(this.modelCreated);
            Assert.NotNull(createdEntity);
            Assert.Equal(createdEntity.Id, entity.Id);
        }

        public async Task ThenICanVerifyICanDeleteAsync()
        {
            await this.WhenICallReadAsync();
            Assert.Null(this.modelRead);
        }

        public void ThenICanVerifyICanReadAllAsync()
        {
            Assert.NotNull(this.modelsRead);

            var entitiesRead = this.mapper.Map<TEntity[]>(this.modelsRead);

            foreach (var entity in entitiesRead)
            {
                Assert.NotNull(this.entitiesToRead.FirstOrDefault(e => e.Id == entity.Id));
            }
        }

        public void ThenICanVerifyICanReadAsync()
        {
            Assert.NotNull(this.modelRead);

            var entity = this.mapper.Map<TEntity>(this.modelRead);
            Assert.Equal(this.entityToRead.Id, entity.Id);
        }

        public async Task ThenICanVerifyICanUpdateAsync()
        {
            await this.WhenICallReadAsync();
            this.ThenICanVerifyICanReadAsync();
        }

        public async Task WhenICallCreateAsync()
        {
            this.modelCreated = await this.repository.CreateAsync(this.modelToCreate);
        }

        public async Task WhenICallDeleteAsync()
        {
            await this.repository.DeleteAsync(this.entityToRead.Id);
        }

        public async Task WhenICallReadAllAsync()
        {
            this.modelsRead = await this.repository.ReadAllAsync();
        }

        public async Task WhenICallReadAsync()
        {
            this.modelRead = await this.repository.ReadAsync(this.entityToRead.Id);
        }

        public async Task WhenICallUpdateAsyncAction()
        {
            var model = this.mapper.Map<TModel>(this.entityToRead);
            await this.repository.UpdateAsync(model);
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.environment?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
