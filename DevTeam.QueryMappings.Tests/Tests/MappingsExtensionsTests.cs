using DevTeam.QueryMappings.Helpers;
using DevTeam.EntityFrameworkExtensions.DbContext;
using Autofac;
using NUnit.Framework;
using DevTeam.QueryMappings.Tests.Mappings;
using DevTeam.QueryMappings.Tests.Context;
using DevTeam.QueryMappings.Tests.Context.RentalContext;
using DevTeam.QueryMappings.Tests.Context.RentalContext.Mappings;
using DevTeam.QueryMappings.Tests.Context.RentalContext.Entities;
using DevTeam.QueryMappings.Tests.Context.RentalContext.Models;
using DevTeam.QueryMappings.Base;
using DevTeam.QueryMappings.Tests.Context.RentalContext.Mappings.Arguments;
using System.Linq;
using DevTeam.QueryMappings.Properties;

namespace DevTeam.QueryMappings.Tests.Tests
{
    [Category("MappingsExtensions")]
    [TestOf(typeof(MappingsExtensions))]
    [TestFixture]
    public class MappingsExtensionsTests
    {
        private IContainer _container;
        private RentalContext _context;

        [OneTimeSetUp]
        public void Init()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<RentalContext>().As<IDbContext>().Keyed<IDbContext>(ContextType.Rental).SingleInstance();
            builder.RegisterType<SecurityContext>().Keyed<IDbContext>(ContextType.Security).SingleInstance();

            _container = builder.Build();

            _context = (RentalContext) _container.Resolve<IDbContext>();

            MappingsConfiguration.Register(typeof(AddressMappings).Assembly);

            ContextResolver<IDbContext>.RegisterResolver(type => 
            {
                if (type == null)
                {
                    return _container.Resolve<IDbContext>();
                }

                return _container.ResolveKeyed<IDbContext>(type);
            });
        }

        [OneTimeTearDown]
        public void Clear()
        {
            _container = null;
            MappingsList.Clear();
        }

        [Test]
        public void Should_Convert_Address_Into_Model()
        {
            var entities = _context.Addresses.ToList();
            var query = entities.AsQueryable();

            var modelsQuery = query.AsQuery<Address, AddressModel>();

            Assert.IsNotNull(modelsQuery);
            Assert.IsInstanceOf<IQueryable<AddressModel>>(modelsQuery);

            var models = modelsQuery.ToList();

            Assert.AreEqual(entities.Count, models.Count);

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                var model = models[i];

                Assert.AreEqual(model.Id, entity.Id);
                Assert.AreEqual(model.State, entity.State);
                Assert.AreEqual(model.Street, entity.Street);
                Assert.AreEqual(model.ZipCode, entity.ZipCode);
                Assert.AreEqual(model.Country, (Countries) entity.Country);
                Assert.AreEqual(model.BuildingNumber, entity.BuildingNumber);
                Assert.AreEqual(model.City, entity.City);
            }
        }

        [Test]
        public void Should_Convert_Address_Into_Short_Model_Using_Named_Mappings()
        {
            var entities = _context.Addresses.ToList();
            var query = entities.AsQueryable();

            var shortModelsQuery = query.AsQuery<Address, AddressSummaryModel>(MappingsNames.ShortAddressFormat);

            Assert.IsNotNull(shortModelsQuery);
            Assert.IsInstanceOf<IQueryable<AddressSummaryModel>>(shortModelsQuery);

            var extendedModelsQuery = query.AsQuery<Address, AddressSummaryModel>(MappingsNames.ExtendedAddressFormat);

            Assert.IsNotNull(extendedModelsQuery);
            Assert.IsInstanceOf<IQueryable<AddressSummaryModel>>(extendedModelsQuery);

            var shortModels = shortModelsQuery.ToList();

            Assert.AreEqual(entities.Count, shortModels.Count);

            var extendedModels = extendedModelsQuery.ToList();

            Assert.AreEqual(entities.Count, extendedModels.Count);

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                var shortModel = shortModels[i];
                var extendedModel = extendedModels[i];

                Assert.AreEqual(shortModel.Id, entity.Id);
                Assert.AreEqual(shortModel.Address, entity.BuildingNumber + " " + entity.Street + ", " + entity.City);

                Assert.AreEqual(extendedModel.Id, entity.Id);
                Assert.AreEqual(extendedModel.Address, entity.BuildingNumber + " " + entity.Street + ", " + entity.City + ", " + entity.State + ", " + entity.Country + ", " + entity.ZipCode);
            }
        }

        [Test]
        public void Should_Convert_Appartments_Into_Model_And_Apply_Arguments()
        {
            var entities = _context.Appartments.ToList();
            var query = entities.AsQueryable();

            var arguments = new AppartmentsArguments { UnitOfMeasure = "sq ft" };
            var modelsQuery = query.AsQuery<Appartment, AppartmentShortModel, AppartmentsArguments>(arguments);

            Assert.IsNotNull(modelsQuery);
            Assert.IsInstanceOf<IQueryable<AppartmentShortModel>>(modelsQuery);

            var models = modelsQuery.ToList();

            Assert.AreEqual(entities.Count, models.Count);

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                var model = models[i];

                Assert.AreEqual(model.Id, entity.Id);
                Assert.AreEqual(model.Floor, entity.Floor);
                Assert.AreEqual(model.IsLodge, entity.IsLodge);
                Assert.AreEqual(model.Size, entity.Size.ToString() + arguments.UnitOfMeasure);
                Assert.AreEqual(model.Number, entity.Number);
            }
        }

        [Test]
        public void Should_Convert_Appartments_Into_Model_And_Apply_Arguments_Using_Named_Mapping()
        {
            var entities = _context.Appartments.ToList();
            var query = entities.AsQueryable();

            var arguments = new AppartmentsArguments { UnitOfMeasure = "sq meters" };
            var modelsQuery = query.AsQuery<Appartment, AppartmentModel, AppartmentsArguments>(arguments, MappingsNames.AppartmentsWithBuilding);

            Assert.IsNotNull(modelsQuery);
            Assert.IsInstanceOf<IQueryable<AppartmentModel>>(modelsQuery);

            var models = modelsQuery.ToList();

            Assert.AreEqual(entities.Count, models.Count);

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                var model = models[i];

                Assert.AreEqual(model.Id, entity.Id);
                Assert.AreEqual(model.Floor, entity.Floor);
                Assert.AreEqual(model.IsLodge, entity.IsLodge);
                Assert.AreEqual(model.Size, entity.Size.ToString() + arguments.UnitOfMeasure);
                Assert.AreEqual(model.Number, entity.Number);
                Assert.AreEqual(model.Badrooms, entity.Badrooms);
                Assert.AreEqual(model.Bathrooms, entity.Bathrooms);

                Assert.IsNotNull(model.Building);

                var building = _context.Buildings.FirstOrDefault(x => x.Id == model.Building.Id);

                Assert.IsNotNull(building);
                Assert.AreEqual(model.Building.Id, building.Id);
                Assert.AreEqual(model.Building.Year, building.Year);
                Assert.AreEqual(model.Building.Floors, building.Floors);
                Assert.AreEqual(model.Building.IsLaundry, building.IsLaundry);
                Assert.AreEqual(model.Building.IsParking, building.IsParking);
            }
        }

        [Test]
        public void Should_Convert_Appartments_Into_Model_And_Attach_Reviews_Info_Using_EF_Context()
        {
            var entities = _context.Appartments.ToList();
            var query = entities.AsQueryable();

            var modelsQuery = query.AsQuery<Appartment, AppartmentReviewsModel, IDbContext>(ContextType.Rental);

            Assert.IsNotNull(modelsQuery);
            Assert.IsInstanceOf<IQueryable<AppartmentReviewsModel>>(modelsQuery);

            var models = modelsQuery.ToList();

            Assert.AreEqual(entities.Count, models.Count);

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                var model = models[i];

                Assert.AreEqual(model.Id, entity.Id);
                Assert.AreEqual(model.Number, entity.Number);

                Assert.IsNotNull(model.Reviews);

                var reviews = _context.Reviews.Where(x => x.EntityId == entity.Id && x.EntityTypeId == (int)EntityType.Appartment).ToList();

                Assert.AreEqual(model.Reviews.Count, reviews.Count);

                for (var j = 0; j < model.Reviews.Count; j++)
                {
                    var review = reviews[j];
                    var reviewModel = model.Reviews[j];

                    Assert.AreEqual(reviewModel.Id, review.Id);
                    Assert.AreEqual(reviewModel.EntityId, review.EntityId);
                    Assert.AreEqual(reviewModel.EntityType, (EntityType) review.EntityTypeId);
                    Assert.AreEqual(reviewModel.Rating, review.Rating);
                    Assert.AreEqual(reviewModel.Comments, review.Comments);
                }
            }
        }

        [Test]
        public void Should_Convert_Building_Into_Model_Using_Named_Mapping_And_Attach_Reviews_Info_Using_EF_Context()
        {
            var entities = _context.Buildings.ToList();
            var query = entities.AsQueryable();

            var modelsQuery = query.AsQuery<Building, BuildingModel, IDbContext>(ContextType.Rental, MappingsNames.BuildingWithReviews);

            Assert.IsNotNull(modelsQuery);
            Assert.IsInstanceOf<IQueryable<BuildingModel>>(modelsQuery);

            var models = modelsQuery.ToList();

            Assert.AreEqual(entities.Count, models.Count);

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                var model = models[i];

                Assert.AreEqual(model.Id, entity.Id);
                Assert.AreEqual(model.Year, entity.Year);
                Assert.AreEqual(model.Floors, entity.Floors);
                Assert.AreEqual(model.IsLaundry, entity.IsLaundry);
                Assert.AreEqual(model.IsParking, entity.IsParking);

                Assert.IsNotNull(model.Reviews);
                Assert.IsNotNull(model.Address);
                Assert.IsNotNull(model.Appartments);

                var address = _context.Addresses.FirstOrDefault(x => x.Id == model.Address.Id);

                Assert.IsNotNull(address);

                Assert.AreEqual(model.Address.Id, address.Id);
                Assert.AreEqual(model.Address.State, address.State);
                Assert.AreEqual(model.Address.Street, address.Street);
                Assert.AreEqual(model.Address.ZipCode, address.ZipCode);
                Assert.AreEqual(model.Address.Country, (Countries)address.Country);
                Assert.AreEqual(model.Address.BuildingNumber, address.BuildingNumber);
                Assert.AreEqual(model.Address.City, address.City);

                var appartments = _context.Appartments.Where(x => x.BuildingId == model.Id).ToList();

                Assert.IsNotNull(appartments);
                Assert.AreEqual(appartments.Count, model.Appartments.Count);

                for (var k = 0; k < appartments.Count; k++)
                {
                    var appartment = appartments[k];
                    var appartmentModel = model.Appartments[k];

                    Assert.AreEqual(appartmentModel.Id, appartment.Id);
                    Assert.AreEqual(appartmentModel.Floor, appartment.Floor);
                    Assert.AreEqual(appartmentModel.IsLodge, appartment.IsLodge);
                    Assert.AreEqual(appartmentModel.Size, appartment.Size.ToString());
                    Assert.AreEqual(appartmentModel.Number, appartment.Number);
                }

                var reviews = _context.Reviews.Where(x => x.EntityId == entity.Id && x.EntityTypeId == (int)EntityType.Building).ToList();

                Assert.AreEqual(model.Reviews.Count, reviews.Count);

                for (var j = 0; j < model.Reviews.Count; j++)
                {
                    var review = reviews[j];
                    var reviewModel = model.Reviews[j];

                    Assert.AreEqual(reviewModel.Id, review.Id);
                    Assert.AreEqual(reviewModel.EntityId, review.EntityId);
                    Assert.AreEqual(reviewModel.EntityType, (EntityType)review.EntityTypeId);
                    Assert.AreEqual(reviewModel.Rating, review.Rating);
                    Assert.AreEqual(reviewModel.Comments, review.Comments);
                }
            }
        }

        [Test]
        public void Should_Get_Budilding_Statistics_Using_Arguments_And_EF_Context()
        {
            var entities = _context.Buildings.ToList();
            var query = entities.AsQueryable();

            var arguments = new BuildingArguments { TargetResidentsAge = 18 };
            var modelsQuery = query.AsQuery<Building, BuildingStatisticsModel, BuildingArguments, IDbContext>(arguments, ContextType.Rental);

            Assert.IsNotNull(modelsQuery);
            Assert.IsInstanceOf<IQueryable<BuildingStatisticsModel>>(modelsQuery);

            var models = modelsQuery.ToList();

            Assert.AreEqual(entities.Count, models.Count);

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                var model = models[i];

                var reviews = _context.Reviews.Where(x => x.EntityId == entity.Id && x.EntityTypeId == (int)EntityType.Building).ToList();

                Assert.AreEqual(model.Id, entity.Id);
                Assert.AreEqual(model.Address, entity.Address.BuildingNumber + ", " + entity.Address.Street + ", " + entity.Address.City);
                Assert.AreEqual(model.AppartmentsCount, entity.Appartments.Count());
                Assert.AreEqual(model.Size, entity.Appartments.Sum(app => app.Size));
                Assert.AreEqual(model.ResidentsCount, entity.Appartments.SelectMany(app => app.Residents).Where(r => r.Age > arguments.TargetResidentsAge).Count());
                Assert.AreEqual(model.AverageBuildingRating, reviews.Average(r => r.Rating));
            }
        }

        [Test]
        public void Should_Throw_Exception_If_Use_Exception_Mapping_Where_Should_Be_Parameterized_Mapping()
        {
            var query = _context.Appartments.AsQueryable();
            var method = new TestDelegate(delegate { query.AsQuery<Appartment, AppartmentShortModel>(); });
            var exceptionMessage = string.Format(Resources.InvalidCastMappingException, typeof(Appartment).Name, typeof(AppartmentShortModel).Name, "with", "without", "without", "without");

            var exception = Assert.Throws<MappingException>(method);
            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        [Test]
        public void Should_Throw_Exception_If_Use_Exception_Mapping_Where_Should_Be_Query_Mapping()
        {
            var query = _context.Appartments.AsQueryable();
            var method = new TestDelegate(delegate { query.AsQuery<Appartment, AppartmentReviewsModel>(); });
            var exceptionMessage = string.Format(Resources.InvalidCastMappingException, typeof(Appartment).Name, typeof(AppartmentReviewsModel).Name, "without", "with", "without", "without");

            var exception = Assert.Throws<MappingException>(method);
            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        [Test]
        public void Should_Throw_Exception_If_Use_Exception_Mapping_Where_Should_Be_Parameterized_Query_Mapping()
        {
            var query = _context.Buildings.AsQueryable();
            var method = new TestDelegate(delegate { query.AsQuery<Building, BuildingStatisticsModel>(); });
            var exceptionMessage = string.Format(Resources.InvalidCastMappingException, typeof(Building).Name, typeof(BuildingStatisticsModel).Name, "with", "with", "without", "without");

            var exception = Assert.Throws<MappingException>(method);
            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        [Test]
        public void Should_Throw_Exception_If_Use_Parameterized_Mapping_Where_Should_Be_Expression_Mapping()
        {
            var query = _context.Addresses.AsQueryable();
            var method = new TestDelegate(delegate { query.AsQuery<Address, AddressModel, AppartmentsArguments>(new AppartmentsArguments { UnitOfMeasure = "sq ft" }); });
            var exceptionMessage = string.Format(Resources.InvalidCastMappingException, typeof(Address).Name, typeof(AddressModel).Name, "without", "without", "with", "without");

            var exception = Assert.Throws<MappingException>(method);
            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        [Test]
        public void Should_Throw_Exception_If_Use_Parameterized_Mapping_Where_Should_Be_Query_Mapping()
        {
            var query = _context.Appartments.AsQueryable();
            var method = new TestDelegate(delegate { query.AsQuery<Appartment, AppartmentReviewsModel, AppartmentsArguments>(new AppartmentsArguments { UnitOfMeasure = "sq ft" }); });
            var exceptionMessage = string.Format(Resources.InvalidCastMappingException, typeof(Appartment).Name, typeof(AppartmentReviewsModel).Name, "without", "with", "with", "without");

            var exception = Assert.Throws<MappingException>(method);
            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        [Test]
        public void Should_Throw_Exception_If_Use_Parameterized_Mapping_Where_Should_Be_Parameterized_Query_Mapping()
        {
            var query = _context.Buildings.AsQueryable();
            var method = new TestDelegate(delegate { query.AsQuery<Building, BuildingStatisticsModel, BuildingArguments>(new BuildingArguments { TargetResidentsAge = 18 }); });
            var exceptionMessage = string.Format(Resources.InvalidCastMappingException, typeof(Building).Name, typeof(BuildingStatisticsModel).Name, "with", "with", "with", "without");

            var exception = Assert.Throws<MappingException>(method);
            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        [Test]
        public void Should_Throw_Exception_If_Use_Query_Mapping_Where_Should_Be_Expression_Mapping()
        {
            var query = _context.Addresses.AsQueryable();
            var method = new TestDelegate(delegate { query.AsQuery<Address, AddressModel, IDbContext>(ContextType.Rental); });
            var exceptionMessage = string.Format(Resources.InvalidCastMappingException, typeof(Address).Name, typeof(AddressModel).Name, "without", "without", "without", "with");

            var exception = Assert.Throws<MappingException>(method);
            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        [Test]
        public void Should_Throw_Exception_If_Use_Query_Mapping_Where_Should_Be_Parameterized_Mapping()
        {
            var query = _context.Appartments.AsQueryable();
            var method = new TestDelegate(delegate { query.AsQuery<Appartment, AppartmentShortModel, IDbContext>(ContextType.Rental); });
            var exceptionMessage = string.Format(Resources.InvalidCastMappingException, typeof(Appartment).Name, typeof(AppartmentShortModel).Name, "with", "without", "without", "with");

            var exception = Assert.Throws<MappingException>(method);
            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        [Test]
        public void Should_Throw_Exception_If_Use_Query_Mapping_Where_Should_Be_Parameterized_Query_Mapping()
        {
            var query = _context.Buildings.AsQueryable();
            var method = new TestDelegate(delegate { query.AsQuery<Building, BuildingStatisticsModel, IDbContext>(ContextType.Rental); });
            var exceptionMessage = string.Format(Resources.InvalidCastMappingException, typeof(Building).Name, typeof(BuildingStatisticsModel).Name, "with", "with", "without", "with");

            var exception = Assert.Throws<MappingException>(method);
            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        [Test]
        public void Should_Throw_Exception_If_Use_Parameterized_Query_Mapping_Where_Should_Be_Expression_Mapping()
        {
            var query = _context.Addresses.AsQueryable();
            var method = new TestDelegate(delegate { query.AsQuery<Address, AddressModel, AppartmentsArguments, IDbContext>(new AppartmentsArguments { UnitOfMeasure = "sq ft" }, ContextType.Rental); });
            var exceptionMessage = string.Format(Resources.InvalidCastMappingException, typeof(Address).Name, typeof(AddressModel).Name, "without", "without", "with", "with");

            var exception = Assert.Throws<MappingException>(method);
            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        [Test]
        public void Should_Throw_Exception_If_Use_Parameterized_Query_Mapping_Where_Should_Be_Parameterized_Mapping()
        {
            var query = _context.Appartments.AsQueryable();
            var method = new TestDelegate(delegate { query.AsQuery<Appartment, AppartmentShortModel, AppartmentsArguments, IDbContext>(new AppartmentsArguments { UnitOfMeasure = "sq ft" }, ContextType.Rental); });
            var exceptionMessage = string.Format(Resources.InvalidCastMappingException, typeof(Appartment).Name, typeof(AppartmentShortModel).Name, "with", "without", "with", "with");

            var exception = Assert.Throws<MappingException>(method);
            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        [Test]
        public void Should_Throw_Exception_If_Use_Parameterized_Query_Mapping_Where_Should_Be_Query_Mapping()
        {
            var query = _context.Appartments.AsQueryable();
            var method = new TestDelegate(delegate { query.AsQuery<Appartment, AppartmentReviewsModel, AppartmentsArguments, IDbContext>(new AppartmentsArguments { UnitOfMeasure = "sq ft" }, ContextType.Rental); });
            var exceptionMessage = string.Format(Resources.InvalidCastMappingException, typeof(Appartment).Name, typeof(AppartmentReviewsModel).Name, "without", "with", "with", "with");

            var exception = Assert.Throws<MappingException>(method);
            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        [Test]
        public void Should_Throw_Exception_If_Args_Arent_Passed_Into_Method()
        {
            var query = _context.Appartments.AsQueryable();

            var methodWithoutContext = new TestDelegate(delegate { query.AsQuery<Appartment, AppartmentReviewsModel, AppartmentsArguments>(null); });
            var methodWithContext = new TestDelegate(delegate { query.AsQuery<Appartment, AppartmentReviewsModel, AppartmentsArguments, IDbContext>(null, ContextType.Rental); });

            var exception1 = Assert.Throws<MappingException>(methodWithContext);
            Assert.AreEqual(exception1.Message, Resources.ArgumentsAreRequiredException);

            var exception2 = Assert.Throws<MappingException>(methodWithoutContext);
            Assert.AreEqual(exception2.Message, Resources.ArgumentsAreRequiredException);
        }

        [Test]
        public void Should_Throw_Exception_If_Context_Key_Isnt_Passed()
        {
            var query = _context.Appartments.AsQueryable();

            var arguments = new AppartmentsArguments { UnitOfMeasure = "sq ft" };
            var methodWithArgs = new TestDelegate(delegate { query.AsQuery<Appartment, AppartmentReviewsModel, AppartmentsArguments, IDbContext>(arguments, null); });

            var exception2 = Assert.Throws<MappingException>(methodWithArgs);
            Assert.AreEqual(exception2.Message, Resources.ContextKeyIsRequredException);
        }
    }
}
