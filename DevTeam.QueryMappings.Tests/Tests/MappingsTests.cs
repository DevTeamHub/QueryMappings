using DevTeam.QueryMappings.Helpers;
using DevTeam.EntityFrameworkExtensions.DbContext;
using Autofac;
using NUnit.Framework;
using DevTeam.QueryMappings.Tests.Mappings;
using DevTeam.QueryMappings.Mappings;
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
    [Category("Unit")]
    [TestFixture]
    public class MappingTests
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
        public void Entity_Framework_Context_Should_Be_Available()
        {
            var defaultContext = _container.Resolve<IDbContext>();
            var firstContext = _container.ResolveKeyed<IDbContext>(ContextType.Rental);
            var secondContext = _container.ResolveKeyed<IDbContext>(ContextType.Security);

            Assert.IsNotNull(defaultContext);
            Assert.IsInstanceOf<RentalContext>(defaultContext);

            Assert.IsNotNull(firstContext);
            Assert.IsInstanceOf<RentalContext>(firstContext);

            Assert.IsNotNull(secondContext);
            Assert.IsInstanceOf<SecurityContext>(secondContext);
        }

        [Test]
        public void Context_Resolver_Should_Work()
        {
            var defaultContext = ContextResolver<IDbContext>.Resolve();
            var firstContext = ContextResolver<IDbContext>.Resolve(ContextType.Rental);
            var secondContext = ContextResolver<IDbContext>.Resolve(ContextType.Security);

            Assert.IsNotNull(defaultContext);
            Assert.IsInstanceOf<RentalContext>(defaultContext);

            Assert.IsNotNull(firstContext);
            Assert.IsInstanceOf<RentalContext>(firstContext);

            Assert.IsNotNull(secondContext);
            Assert.IsInstanceOf<SecurityContext>(secondContext);
        }

        [Test]
        public void Shound_Find_Address_Expression_Mapping_In_Storage()
        {
            var mapping = MappingsList.Get<Address, AddressModel>();

            Assert.IsNotNull(mapping);
            Assert.IsInstanceOf<ExpressionMapping<Address, AddressModel>>(mapping);
            Assert.AreEqual(mapping.From, typeof(Address));
            Assert.AreEqual(mapping.To, typeof(AddressModel));
            Assert.IsNull(mapping.Name);
        }

        [Test]
        public void Shound_Throw_Exception_If_Mapping_Doesnt_Exist()
        {
            var method = new TestDelegate(delegate { MappingsList.Get<Address, BuildingModel>(); });
            var exceptionMessage = string.Format(Resources.MappingNotFoundException, typeof(Address).Name, typeof(BuildingModel).Name);

            var exception = Assert.Throws<MappingException>(method);
            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        [Test]
        public void Shound_Throw_Exception_If_We_Try_To_Find_Named_Mapping_Without_Explicit_Name_Argument()
        {
            var method = new TestDelegate(delegate { MappingsList.Get<Address, AddressSummaryModel>(); });
            var exceptionMessage = string.Format(Resources.NameIsNullWhenSearchForNamedMappingException, typeof(Address).Name, typeof(AddressSummaryModel).Name);

            var exception = Assert.Throws<MappingException>(method);
            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        [Test]
        public void Shound_Throw_Exception_If_The_Same_Not_Named_Mapping_Registered_Twice()
        {
            var method = new TestDelegate(delegate { MappingsList.Get<Address, InvalidAddressMapping>(); });
            var exceptionMessage = string.Format(Resources.MoreThanOneMappingFoundException, typeof(Address).Name, typeof(InvalidAddressMapping).Name);

            var exception = Assert.Throws<MappingException>(method);
            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        [Test]
        public void Shound_Throw_Exception_If_Name_For_Named_Mapping_Is_Doesnt_Exist()
        {
            var method = new TestDelegate(delegate { MappingsList.Get<Address, AddressSummaryModel>("SomeInvalidName"); });
            var exceptionMessage = string.Format(Resources.MappingNotFoundException, typeof(Address).Name, typeof(AddressSummaryModel).Name);

            var exception = Assert.Throws<MappingException>(method);
            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        [Test]
        public void Shound_Find_Expression_Named_Mapping_By_Name()
        {
            var mappingName = MappingsNames.ExtendedAddressFormat;

            var namedMapping = MappingsList.Get<Address, AddressSummaryModel>(mappingName);

            Assert.IsNotNull(namedMapping);
            Assert.IsInstanceOf<ExpressionMapping<Address, AddressSummaryModel>>(namedMapping);
            Assert.AreEqual(namedMapping.From, typeof(Address));
            Assert.AreEqual(namedMapping.To, typeof(AddressSummaryModel));
            Assert.AreEqual(namedMapping.Name, mappingName);
        }

        [Test]
        public void Shound_Find_Parameterized_Mapping_Without_Name()
        {
            var parameterizedMapping = MappingsList.Get<Appartment, AppartmentShortModel>();

            Assert.IsNotNull(parameterizedMapping);
            Assert.IsInstanceOf<ParameterizedMapping<Appartment, AppartmentShortModel, AppartmentsArguments>>(parameterizedMapping);
            Assert.AreEqual(parameterizedMapping.From, typeof(Appartment));
            Assert.AreEqual(parameterizedMapping.To, typeof(AppartmentShortModel));
            Assert.IsNull(parameterizedMapping.Name);
        }

        [Test]
        public void Shound_Find_Parameterized_Mapping_By_Name()
        {
            var mappingName = MappingsNames.AppartmentsWithoutBuilding;

            var parameterizedMapping = MappingsList.Get<Appartment, AppartmentModel>(mappingName);

            Assert.IsNotNull(parameterizedMapping);
            Assert.IsInstanceOf<ParameterizedMapping<Appartment, AppartmentModel, AppartmentsArguments>>(parameterizedMapping);
            Assert.AreEqual(parameterizedMapping.From, typeof(Appartment));
            Assert.AreEqual(parameterizedMapping.To, typeof(AppartmentModel));
            Assert.AreEqual(parameterizedMapping.Name, mappingName);
        }

        [Test]
        public void Shound_Find_Query_Mapping_Without_Name()
        {
            var queryMapping = MappingsList.Get<Appartment, AppartmentReviewsModel>();

            Assert.IsNotNull(queryMapping);
            Assert.IsInstanceOf<QueryMapping<Appartment, AppartmentReviewsModel, RentalContext>>(queryMapping);
            Assert.AreEqual(queryMapping.From, typeof(Appartment));
            Assert.AreEqual(queryMapping.To, typeof(AppartmentReviewsModel));
            Assert.IsNull(queryMapping.Name);
        }

        [Test]
        public void Shound_Find_Query_Mapping_By_Name()
        {
            var mappingName = MappingsNames.BuildingWithReviews;

            var queryMapping = MappingsList.Get<Building, BuildingModel>(mappingName);

            Assert.IsNotNull(queryMapping);
            Assert.IsInstanceOf<QueryMapping<Building, BuildingModel, RentalContext>>(queryMapping);
            Assert.AreEqual(queryMapping.From, typeof(Building));
            Assert.AreEqual(queryMapping.To, typeof(BuildingModel));
            Assert.AreEqual(queryMapping.Name, mappingName);
        }

        [Test]
        public void Shound_Find_Parameterized_Query_Mapping()
        {
            var mapping = MappingsList.Get<Building, BuildingStatisticsModel>();

            Assert.IsNotNull(mapping);
            Assert.IsInstanceOf<ParameterizedQueryMapping<Building, BuildingStatisticsModel, BuildingArguments, RentalContext>>(mapping);
            Assert.AreEqual(mapping.From, typeof(Building));
            Assert.AreEqual(mapping.To, typeof(BuildingStatisticsModel));
            Assert.IsNull(mapping.Name);
        }

        [Test]
        public void Should_Throw_Exception_If_Apply_()
        {
            var query = _context.Appartments.AsQueryable();

            var appartments = query.AsQuery<Appartment, AppartmentShortModel>();

            Assert.IsNotNull(appartments);
            Assert.IsInstanceOf<IQueryable<AppartmentShortModel>>(appartments);
        }
    }
}
