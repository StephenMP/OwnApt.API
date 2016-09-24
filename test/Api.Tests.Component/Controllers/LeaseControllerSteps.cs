using OwnApt.Api.AppStart;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Controllers;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Domain.Service;
using OwnApt.Api.Repository.Entity.Sql;
using OwnApt.Api.Repository.Interface;
using OwnApt.Api.Repository.Sql.Lease;
using OwnApt.Common.Enum;
using OwnApt.TestEnvironment.Environment;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Controllers
{
    public class LeaseControllerSteps : ControllerSteps, IDisposable
    {
        #region Private Fields

        private int currentLeaseTermId;
        private string currentPropertyId;
        private bool disposedValue;
        private LeaseContext leaseContext;
        private LeaseController leaseController;
        private LeaseTermEntity leaseTermEntity;
        private ILeaseTermRepository leaseTermRepository;
        private ILeaseTermService leaseTermService;
        private LeaseTermModel newLeaseTermModel;
        private OwnAptTestEnvironment testEnvironment;

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
            this.leaseContext = new LeaseContext(this.testEnvironment.GetSqlDbContextOptions<LeaseContext>());
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
            this.newLeaseTermModel = LeaseControllerRandom.LeaseTermModel;
            this.currentLeaseTermId = this.newLeaseTermModel.LeaseTermId;
            this.currentPropertyId = this.newLeaseTermModel.PropertyId;
        }

        internal void GivenIHaveALeaseTermToRead()
        {
            this.leaseTermEntity = LeaseControllerRandom.LeaseTermEntity;
            var leasePeriodClosedEntity = LeaseControllerRandom.LeasePeriodEntity(leaseTermEntity.LeaseTermId, LeasePeriodStatus.Closed);
            var leasePeriodPaymentDueEntity = LeaseControllerRandom.LeasePeriodEntity(leaseTermEntity.LeaseTermId, LeasePeriodStatus.PaymentDue);
            var leasePeriodPaymentReceivedEntity = LeaseControllerRandom.LeasePeriodEntity(leaseTermEntity.LeaseTermId, LeasePeriodStatus.PaymentReceived);
            this.currentLeaseTermId = this.leaseTermEntity.LeaseTermId;
            this.currentPropertyId = this.leaseTermEntity.PropertyId;
            this.leaseContext.Add(leaseTermEntity);
            this.leaseContext.Add(leasePeriodClosedEntity);
            this.leaseContext.Add(leasePeriodPaymentDueEntity);
            this.leaseContext.Add(leasePeriodPaymentReceivedEntity);
            this.leaseContext.SaveChanges();
        }

        internal void GivenIHaveATestEnvironment()
        {
            this.testEnvironment = OwnAptTestEnvironment
                                        .CreateEnvironment()
                                        .UseSql<LeaseContext>();
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
            Assert.Equal(leaseTermModel.Active, this.leaseTermEntity.Active);

            foreach(var periodModel in leaseTermModel.LeasePeriods)
            {
                var periodEntity = this.leaseTermEntity.LeasePeriods.FirstOrDefault(e => e.LeasePeriodId == periodModel.LeasePeriodId);
                Assert.NotNull(periodEntity);
                Assert.Equal(periodModel.LeasePeriodId, periodEntity.LeasePeriodId);
                Assert.Equal(periodModel.LeasePeriodStatus, (LeasePeriodStatus)periodEntity.LeasePeriodStatusId);
                Assert.Equal(periodModel.LeaseTermId, periodEntity.LeaseTermId);
                Assert.Equal(periodModel.Period, periodEntity.Period);
            }
        }

        internal async Task WhenICallCreateLeaseTermAsync()
        {
            this.controllerResponse = await this.leaseController.CreateLeaseTermAsync(this.newLeaseTermModel);
        }

        internal async Task WhenICallReadLeaseTermAsync()
        {
            this.controllerResponse = await this.leaseController.ReadLeaseTermAsync(this.currentLeaseTermId);
        }

        internal async Task WhenICallReadLeaseTermByPropertyIdAsync()
        {
            this.controllerResponse = await this.leaseController.ReadLeaseTermByPropertyAsync(this.currentPropertyId);
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

    internal static class LeaseControllerRandom
    {
        #region Private Fields

        private static Random random = new Random();

        #endregion Private Fields

        #region Public Properties

        public static int Int => random.Next();

        public static LeaseTermEntity LeaseTermEntity => new LeaseTermEntity
        {
            EndDate = DateTime.UtcNow.AddDays(1),
            LeaseTermId = TestRandom.Integer,
            PropertyId = TestRandom.String,
            Rent = TestRandom.Integer,
            StartDate = DateTime.UtcNow.AddDays(-1),
            Active = TestRandom.Boolean
        };

        public static LeasePeriodEntity LeasePeriodEntity(int leaseTermId, LeasePeriodStatus status) => new LeasePeriodEntity
        {
            LeasePeriodId = TestRandom.Integer,
            LeasePeriodStatusId = (int)status,
            LeaseTermId = leaseTermId,
            Period = TestRandom.String
        };

        public static LeaseTermModel LeaseTermModel => new LeaseTermModel
        {
            EndDate = DateTime.UtcNow.AddDays(1),
            LeaseTermId = TestRandom.Integer,
            PropertyId = LeaseControllerRandom.String,
            Rent = TestRandom.Integer,
            StartDate = DateTime.UtcNow.AddDays(-1)
        };

        public static string String => Guid.NewGuid().ToString("N");

        #endregion Public Properties
    }
}
