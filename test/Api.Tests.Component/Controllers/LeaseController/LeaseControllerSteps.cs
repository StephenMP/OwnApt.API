using OwnApt.Api.AppStart;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Controllers;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Domain.Service;
using OwnApt.Api.Repository.Entity.Sql;
using OwnApt.Api.Repository.Interface;
using OwnApt.Api.Repository.Sql.Lease;
using OwnApt.TestEnvironment.Environment;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Controllers.LeaseControllerTests
{
    public class LeaseControllerSteps : ControllerSteps, IDisposable
    {
        #region Private Fields

        private string currentLeaseTermId;
        private bool disposedValue;
        private LeaseContext leaseContext;
        private LeaseController leaseController;
        private LeaseTermEntity leaseTermEntity;
        private ILeaseTermRepository leaseTermRepository;
        private ILeaseTermService leaseTermService;
        private LeaseTermModel newLeaseTermModel;
        private TestingEnvironment testEnvironment;

        #endregion Private Fields

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods

        #region Internal Methods

        internal void GivenIHaveALeaseContext()
        {
            this.leaseContext = new LeaseContext(this.testEnvironment.SqlDbContextOptions<LeaseContext>());
        }

        internal void GivenIHaveALeaseController()
        {
            this.leaseController = new LeaseController(this.leaseTermService);
        }

        internal void GivenIHaveALeaseControllerEnvironment()
        {
            this.GivenIHaveATestEnvironment();
            this.GivenIHaveALeaseContext();
            this.GivenIHaveALeaseTermToRead();
            this.GivenIHaveALeaseTermRepository();
            this.GivenIHaveALeaseTermService();
            this.GivenIHaveALeaseController();
        }

        internal void GivenIHaveALeaseTermRepository()
        {
            this.leaseTermRepository = new LeaseTermRepository(this.leaseContext, OwnAptStartup.BuildMapper());
        }

        internal void GivenIHaveALeaseTermService()
        {
            this.leaseTermService = new LeaseTermService(this.leaseTermRepository);
        }

        internal void GivenIHaveALeaseTermToCreate()
        {
            this.newLeaseTermModel = TestRandom.LeaseTermModel;
            this.currentLeaseTermId = this.newLeaseTermModel.LeaseTermId;
        }

        internal void GivenIHaveALeaseTermToRead()
        {
            this.leaseTermEntity = TestRandom.LeaseTermEntity;
            this.currentLeaseTermId = this.leaseTermEntity.LeaseTermId;
            this.leaseContext.Add(leaseTermEntity);
            this.leaseContext.SaveChanges();
        }

        internal void GivenIHaveATestEnvironment()
        {
            this.testEnvironment = new TestingEnvironment();
            this.testEnvironment.AddSqlContext<LeaseContext>();
        }

        internal void ThenICanVerifyICanCreateLeaseTerm()
        {
            var leaseTermModel = this.controllerContent as LeaseTermModel;
            Assert.Equal(this.newLeaseTermModel, leaseTermModel);
        }

        internal void ThenICanVerifyICanReadLeaseTerm()
        {
            var leaseTermModel = this.controllerContent as LeaseTermModel;

            Assert.NotNull(leaseTermModel);
            Assert.Equal(leaseTermModel.EndDate, this.leaseTermEntity.EndDate);
            Assert.Equal(leaseTermModel.PropertyId, this.leaseTermEntity.PropertyId);
            Assert.Equal(leaseTermModel.Rent, this.leaseTermEntity.Rent);
            Assert.Equal(leaseTermModel.StartDate, this.leaseTermEntity.StartDate);
            Assert.Equal(leaseTermModel.LeaseTermId, this.leaseTermEntity.LeaseTermId);
        }

        internal async Task WhenICallCreateLeaseTermAsync()
        {
            this.controllerResponse = await this.leaseController.CreateLeaseTermAsync(this.newLeaseTermModel);
        }

        internal async Task WhenICallReadLeaseTermAsync()
        {
            this.controllerResponse = await this.leaseController.ReadLeaseTermAsync(this.currentLeaseTermId);
        }

        #endregion Internal Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.testEnvironment?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }

    internal static class TestRandom
    {
        #region Private Fields

        private static Random random = new Random();

        #endregion Private Fields

        #region Public Properties

        public static int Int => random.Next();

        public static LeaseTermEntity LeaseTermEntity => new LeaseTermEntity
        {
            EndDate = DateTime.Now,
            LeaseTermId = TestRandom.String,
            PropertyId = TestRandom.String,
            Rent = TestRandom.Int,
            StartDate = DateTime.Now
        };

        public static LeaseTermModel LeaseTermModel => new LeaseTermModel
        {
            EndDate = DateTime.Now,
            LeaseTermId = TestRandom.String,
            PropertyId = TestRandom.String,
            Rent = TestRandom.Int,
            StartDate = DateTime.Now
        };

        public static string String => Guid.NewGuid().ToString("N");

        #endregion Public Properties
    }
}
