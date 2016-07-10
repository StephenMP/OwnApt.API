using AutoMapper;
using MongoDB.Driver;
using OwnApt.Api.Domain.Model;
using OwnApt.Api.Repository.Entity;
using OwnApt.Api.Repository.Interface;
using System;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository
{
    public class MongoPersonRepository : IPersonRepository
    {
        private readonly IMapper mapper;
        private readonly IMongoDatabase coreDatabase;

        private IMongoCollection<PersonEntity> PersonCollection => this.coreDatabase.GetCollection<PersonEntity>("Person");

        public MongoPersonRepository(IMongoClient mongoClient, IMapper mapper)
        {
            this.coreDatabase = mongoClient.GetDatabase("Core");
            this.mapper = mapper;
        }

        public async Task<PersonModel> CreateAsync(PersonModel model)
        {
            model.Id = Guid.NewGuid().ToString("N");
            var personEntity = this.mapper.Map<PersonEntity>(model);
            await this.PersonCollection.InsertOneAsync(personEntity);

            return model;
        }

        public async Task DeleteAsync(string id)
        {
            await this.PersonCollection.DeleteOneAsync(p => p.Id == id);
        }

        public async Task<PersonModel> ReadAsync(string id)
        {
            var asyncCursor = await this.PersonCollection.FindAsync(p => p.Id == id);
            var personEntity = await asyncCursor.FirstOrDefaultAsync();
            return this.mapper.Map<PersonModel>(personEntity);
        }

        public async Task UpdateAsync(PersonModel model)
        {
            var personEntity = this.mapper.Map<PersonEntity>(model);
            await this.PersonCollection.ReplaceOneAsync(p => p.Id == model.Id, personEntity);
        }
    }
}