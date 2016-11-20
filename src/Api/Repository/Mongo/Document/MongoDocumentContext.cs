using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using OwnApt.Api.Repository.Entity.Mongo;

namespace OwnApt.Api.Repository.Mongo.Document
{
    public interface IMongoDocumentContext
    {
        IMongoCollection<ManagementAgreementEntity> ManagementAgreementCollection { get; }
    }

    public class MongoDocumentContext : IMongoDocumentContext
    {
        #region Private Fields

        private readonly IMongoDatabase agreementDatabase;

        #endregion Private Fields

        #region Public Constructors

        public MongoDocumentContext(IMongoClient mongoClient)
        {
            this.agreementDatabase = mongoClient.GetDatabase("ManagementAgreement");
        }

        #endregion Public Constructors

        #region Public Properties

        public IMongoCollection<ManagementAgreementEntity> ManagementAgreementCollection => this.agreementDatabase.GetCollection<ManagementAgreementEntity>("ManagementAgreement");

        #endregion Public Properties
    }
}
