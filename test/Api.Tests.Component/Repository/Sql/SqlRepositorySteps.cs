using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OwnApt.Api.AppStart;
using OwnApt.Api.Repository.Entity.Sql;
using OwnApt.Api.Repository.Sql;
using OwnApt.TestEnvironment.Environment;
using Xunit;

namespace Api.Tests.Component.Repository.Sql
{
    public abstract class SqlRepositorySteps<TModel, TEntity, TContext> : RepositorySteps<TModel, TEntity> where TEntity : SqlEntity where TContext : DbContext
    {
        #region Protected Fields

        protected readonly string collectionName;
        protected readonly string dbName;
        protected readonly IMapper mapper;
        protected TEntity[] entitiesToRead;
        protected TEntity entityToRead;
        protected TModel modelCreated;
        protected TModel modelRead;
        protected IEnumerable<TModel> modelsRead;
        protected TModel modelToCreate;
        protected SqlRepository<TModel, TEntity, TContext> repository;

        #endregion Protected Fields

        #region Protected Constructors

        protected SqlRepositorySteps()
        {
            this.mapper = OwnAptStartup.BuildMapper();
        }

        #endregion Protected Constructors

        #region Public Methods

        public override void GivenIHaveAnEntitiesToRead()
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
                entity.Id = TestRandom.Integer;
            }

            this.environment.ImportSqlDataAsync<TContext, TEntity>(this.entitiesToRead);
        }

        public override void GivenIHaveAnEntityToCreate()
        {
            var entity = Activator.CreateInstance<TEntity>();
            entity.Id = TestRandom.Integer;

            this.modelToCreate = this.mapper.Map<TModel>(entity);
        }

        public override void GivenIHaveAnEntityToDelete()
        {
            this.GivenIHaveAnEntityToRead();
        }

        public override void GivenIHaveAnEntityToRead()
        {
            this.entityToRead = Activator.CreateInstance<TEntity>();
            this.entityToRead.Id = TestRandom.Integer;

            this.environment.ImportSqlDataAsync<TContext, TEntity>(new[] { this.entityToRead });
        }

        public override void GivenIHaveAnEntityToUpdate()
        {
            this.GivenIHaveAnEntityToRead();
        }

        public override void GivenIHaveATestEnvironment()
        {
            this.environment = new OwnAptTestEnvironmentBuilder()
                            .AddSqlContext<TContext>()
                            .BuildEnvironment();
        }

        public override void ThenICanVerifyICanCreateAsync()
        {
            var entity = this.mapper.Map<TEntity>(modelToCreate);
            var createdEntity = this.mapper.Map<TEntity>(modelCreated);

            Assert.NotNull(this.modelToCreate);
            Assert.NotNull(entity);
            Assert.NotNull(this.modelCreated);
            Assert.NotNull(createdEntity);
            Assert.Equal(createdEntity.Id, entity.Id);
        }

        public override async Task ThenICanVerifyICanDeleteAsync()
        {
            await this.WhenICallReadAsync();
            Assert.Null(this.modelRead);
        }

        public override void ThenICanVerifyICanReadAllAsync()
        {
            Assert.NotNull(this.modelsRead);

            var entitiesRead = this.mapper.Map<TEntity[]>(this.modelsRead);

            foreach (var entity in entitiesRead)
            {
                Assert.NotNull(this.entitiesToRead.FirstOrDefault(e => e.Id == entity.Id));
            }
        }

        public override void ThenICanVerifyICanReadAsync()
        {
            Assert.NotNull(this.modelRead);

            var entity = this.mapper.Map<TEntity>(this.modelRead);
            Assert.Equal(this.entityToRead.Id, entity.Id);
        }

        public override async Task ThenICanVerifyICanUpdateAsync()
        {
            await this.WhenICallReadAsync();
            this.ThenICanVerifyICanReadAsync();
        }

        public override async Task WhenICallCreateAsync()
        {
            this.modelCreated = await this.repository.CreateAsync(this.modelToCreate);
        }

        public override async Task WhenICallDeleteAsync()
        {
            await this.repository.DeleteAsync(this.entityToRead.Id);
        }

        public override async Task WhenICallReadAllAsync()
        {
            this.modelsRead = await this.repository.ReadAllAsync();
        }

        public override async Task WhenICallReadAsync()
        {
            this.modelRead = await this.repository.ReadAsync(this.entityToRead.Id);
        }

        public override async Task WhenICallUpdateAsyncAction()
        {
            var model = this.mapper.Map<TModel>(this.entityToRead);
            await this.repository.UpdateAsync(model);
        }

        #endregion Public Methods
    }
}
