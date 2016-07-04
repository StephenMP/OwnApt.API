using MongoDB.Driver;
using Moq;
using OwnApt.Api;
using OwnApt.Api.Domain.Model;
using OwnApt.Api.Repository.Entity;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using OwnApt.Api.Repository.Interface;
using OwnApt.Api.Repository;
using OwnApt.Api.Domain.Service;
using OwnApt.Api.Controllers;
using OwnApt.Api.Domain.Interface;
using Microsoft.AspNet.Mvc;
using System.Net;
using Xunit;
using System.Reflection;

namespace Api.Tests.Component.Controllers.PropertyControllerTests
{
    internal class PropertyControllerSteps
    {
        private string propertyId;
        private PropertyModel propertyModel;
        private Random random = new Random();
        private PropertyEntity propertyEntity;

        internal void GivenIHaveAMockedMongoCoreDatabase()
        {
            var mockedMongoCoreDatabase = new Mock<IMongoDatabase>();
            mockedMongoCoreDatabase.Setup(s => s.GetCollection<PropertyEntity>("Property", null)).Returns(this.propertyCollection);

            this.mongoCoreDatabase = mockedMongoCoreDatabase.Object;
        }

        internal void ThenICanVerifyIReadProperty()
        {
            var propertyModel = this.controllerContent as PropertyModel;

            Assert.NotNull(propertyModel);
            Assert.Equal(this.propertyId, propertyModel.Id);
        }

        internal void ThenICanVerifyIReceived<T>(HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.OK)
            {
                if (typeof(T) != typeof(Missing))
                {
                    var content = Assert.IsType<HttpOkObjectResult>(this.controllerIActionResult);
                    this.controllerContent = content.Value;
                    Assert.Equal((int)statusCode, content.StatusCode.Value);
                }
            }
        }

        internal async Task WhenICallReadProperty()
        {
            this.controllerIActionResult = await this.propertyController.ReadProperty(this.propertyId);
        }

        internal void GivenIHaveAPropertyController()
        {
            this.propertyController = new PropertyController(this.propertyService);
        }

        internal void GivenIHaveAPropertyService()
        {
            this.propertyService = new PropertyService(this.propertyRepository);
        }

        internal void GivenIHaveAPropertyRepository()
        {
            this.propertyRepository = new MongoPropertyRepository(this.mongoClient, this.mapper);
        }

        internal void GivenIHaveAnAutoMapper()
        {
            this.mapper = StartupExtensions.BuildMapper();
        }

        internal void GivenIHaveAMockedMongoClient()
        {
            var mockedMongoClient = new Mock<IMongoClient>();
            mockedMongoClient.Setup(s => s.GetDatabase("Core", null)).Returns(this.mongoCoreDatabase);

            this.mongoClient = mockedMongoClient.Object;
        }

        private IMongoCollection<PropertyEntity> propertyCollection;
        private IMongoDatabase mongoCoreDatabase;
        private IMongoClient mongoClient;
        private IMapper mapper;
        private IPropertyRepository propertyRepository;
        private IPropertyService propertyService;
        private PropertyController propertyController;
        private IActionResult controllerIActionResult;
        private object controllerContent;

        internal void GivenIHaveAPropertyId()
        {
            this.propertyId = random.Next().ToString();
        }

        internal void GivenIHaveAMockedPropertyCollection()
        {
            var mockedAsyncCursor = new Mock<IAsyncCursor<PropertyEntity>>();

            // Can't mock it this way :( Need to figure out how to mock mongo driver
            mockedAsyncCursor.Setup(s => s.FirstOrDefaultAsync(default(CancellationToken))).Returns(Task.FromResult(this.propertyEntity));

            var mockedPropertyCollection = new Mock<IMongoCollection<PropertyEntity>>();
            mockedPropertyCollection.Setup(s => s.FindAsync(It.IsAny<Expression<Func<PropertyEntity, bool>>>(), null, default(CancellationToken))).Returns(Task.FromResult(mockedAsyncCursor.Object));

            this.propertyCollection = mockedPropertyCollection.Object;
        }

        internal void GivenIHaveAPropertyEntity()
        {
            this.propertyEntity = new PropertyEntity
            {
                Id = this.propertyId
            };
        }
    }
}