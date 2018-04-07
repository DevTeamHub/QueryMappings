using DevTeam.QueryMappings.Helpers;
using DevTeam.EntityFrameworkExtensions.DbContext;
using Autofac;
using NUnit.Framework;
using DevTeam.QueryMappings.Tests.Mappings;
using System.Collections.Generic;
using DevTeam.QueryMappings.Mappings;
using System.Linq;

namespace DevTeam.QueryMappings.Tests.Tests
{
    [Category("Unit")]
    [TestFixture]
    public class MappingTests
    {
        private IContainer _container;

        [SetUp]
        public void Init()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<FirstContextMock>().As<IDbContext>().Keyed<IDbContext>(ContextType.First).SingleInstance();
            builder.RegisterType<SecondContextMock>().Keyed<IDbContext>(ContextType.Second).SingleInstance();

            _container = builder.Build();

            MappingsConfiguration.Register(typeof(PersonMappings).Assembly);

            ContextResolver<IDbContext>.RegisterResolver(type => 
            {
                if (type == null)
                {
                    return _container.Resolve<IDbContext>();
                }

                return _container.ResolveKeyed<IDbContext>(type);
            });
        }

        [Test]
        public void Mappings_Shound_Be_Loaded_Into_Storage()
        {
            var carMapping = MappingsList.Get<Car, CarModel>();
            var personMapping = MappingsList.Get<Person, PersonModel>();

            Assert.IsNotNull(carMapping);
            Assert.IsNotNull(personMapping);
        }

        [Test]
        public void Entity_Framework_Context_Should_Be_Available()
        {
            var defaultContext = _container.Resolve<IDbContext>();
            var firstContext = _container.ResolveKeyed<IDbContext>(ContextType.First);
            var secondContext = _container.ResolveKeyed<IDbContext>(ContextType.Second);

            Assert.IsNotNull(defaultContext);
            Assert.IsInstanceOf<FirstContextMock>(defaultContext);

            Assert.IsNotNull(firstContext);
            Assert.IsInstanceOf<FirstContextMock>(firstContext);

            Assert.IsNotNull(secondContext);
            Assert.IsInstanceOf<SecondContextMock>(secondContext);
        }

        [Test]
        public void Context_Resolver_Should_Work()
        {
            var defaultContext = ContextResolver<IDbContext>.Resolve();
            var firstContext = ContextResolver<IDbContext>.Resolve(ContextType.First);
            var secondContext = ContextResolver<IDbContext>.Resolve(ContextType.Second);

            Assert.IsNotNull(defaultContext);
            Assert.IsInstanceOf<FirstContextMock>(defaultContext);

            Assert.IsNotNull(firstContext);
            Assert.IsInstanceOf<FirstContextMock>(firstContext);

            Assert.IsNotNull(secondContext);
            Assert.IsInstanceOf<SecondContextMock>(secondContext);
        }

        [Test]
        public void Should_Convert_Car_Into_Model()
        {
            var car = new Car
            {
                Make = "Opel",
                Year = 2000,
                Wheels = new List<Wheel>
                {
                    new Wheel
                    {
                        Position = "Front Left",
                        Size = 21
                    },
                    new Wheel
                    {
                        Position = "Front Right",
                        Size = 21
                    },
                    new Wheel
                    {
                        Position = "Back Left",
                        Size = 21
                    },
                    new Wheel
                    {
                        Position = "Back Right",
                        Size = 21
                    }
                }
            };

            var list = new List<Car> { car };
            var query = list.AsQueryable();

            var carMapping = MappingsList.Get<Car, CarModel>();

            Assert.IsNotNull(carMapping);
            Assert.IsInstanceOf<ExpressionMapping<Car, CarModel>>(carMapping);

            var expressionMapping = (ExpressionMapping<Car, CarModel>)carMapping;

            var carModelQuery = expressionMapping.Apply(query);

            Assert.IsNotNull(carModelQuery);
            Assert.IsInstanceOf<IQueryable<CarModel>>(carModelQuery);

            var carModelList = carModelQuery.ToList();

            CollectionAssert.IsNotEmpty(carModelList);
            Assert.AreEqual(carModelList.Count, list.Count);

            var carModel = carModelList.First();

            Assert.AreEqual(carModel.Make, car.Make);
            CollectionAssert.IsNotEmpty(carModel.Wheels);
            Assert.AreEqual(carModel.Wheels.Count, car.Wheels.Count);

            for (var i = 0; i < carModel.Wheels.Count; i++)
            {
                var wheelModel = carModel.Wheels[i];
                var wheel = car.Wheels[i];

                Assert.AreEqual(wheelModel.Size, wheel.Size);
            }
        }

        [Test]
        public void Should_Convert_Car_Into_Model()
        {
            var car = new Car
            {
                Make = "Opel",
                Year = 2000,
                Wheels = new List<Wheel>
                {
                    new Wheel
                    {
                        Position = "Front Left",
                        Size = 21
                    },
                    new Wheel
                    {
                        Position = "Front Right",
                        Size = 21
                    },
                    new Wheel
                    {
                        Position = "Back Left",
                        Size = 21
                    },
                    new Wheel
                    {
                        Position = "Back Right",
                        Size = 21
                    }
                }
            };

            var list = new List<Car> { car };
            var query = list.AsQueryable();

            var carMapping = MappingsList.Get<Car, CarModel>();

            Assert.IsNotNull(carMapping);
            Assert.IsInstanceOf<ExpressionMapping<Car, CarModel>>(carMapping);

            var expressionMapping = (ExpressionMapping<Car, CarModel>)carMapping;

            var carModelQuery = expressionMapping.Apply(query);

            Assert.IsNotNull(carModelQuery);
            Assert.IsInstanceOf<IQueryable<CarModel>>(carModelQuery);

            var carModelList = carModelQuery.ToList();

            CollectionAssert.IsNotEmpty(carModelList);
            Assert.AreEqual(carModelList.Count, list.Count);

            var carModel = carModelList.First();

            Assert.AreEqual(carModel.Make, car.Make);
            CollectionAssert.IsNotEmpty(carModel.Wheels);
            Assert.AreEqual(carModel.Wheels.Count, car.Wheels.Count);

            for (var i = 0; i < carModel.Wheels.Count; i++)
            {
                var wheelModel = carModel.Wheels[i];
                var wheel = car.Wheels[i];

                Assert.AreEqual(wheelModel.Size, wheel.Size);
            }
        }
    }
}
