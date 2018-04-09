using DevTeam.EntityFrameworkExtensions.DbContext;
using DevTeam.QueryMappings.Tests.Context.RentalContext.Entities;
using DevTeam.QueryMappings.Tests.Tests;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace DevTeam.QueryMappings.Tests.Context.RentalContext
{
    public class RentalContext : IDbContext
    {
        public IEnumerable<Building> Buildings => TestData.Buildings;
        public IEnumerable<Appartment> Appartments => TestData.Appartments;
        public IEnumerable<Address> Addresses => TestData.Addresses;
        public IEnumerable<Person> People => TestData.People;
        public IEnumerable<Review> Reviews => TestData.Reviews;

        public Database Database => throw new NotImplementedException();

        public DbChangeTracker ChangeTracker => throw new NotImplementedException();

        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity: class
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public DbSet<TEntity> Set<TEntity>()
            where TEntity: class
        {
            List<TEntity> list = null;

            if (typeof(TEntity) == typeof(Review))
            {
                list = Reviews.Cast<TEntity>().ToList();
            }
            else if (typeof(TEntity) == typeof(Building))
            {
                list = Buildings.Cast<TEntity>().ToList();
            }
            else if (typeof(TEntity) == typeof(Address))
            {
                list = Addresses.Cast<TEntity>().ToList();
            }
            else if (typeof(TEntity) == typeof(Appartment))
            {
                list = Appartments.Cast<TEntity>().ToList();
            }
            else if (typeof(TEntity) == typeof(Person))
            {
                list = People.Cast<TEntity>().ToList();
            }

            return CreateSet(list).Object;
        }

        public static Mock<DbSet<TEntity>> CreateSet<TEntity>(List<TEntity> set)
            where TEntity : class
        {
            var queryable = set.AsQueryable();

            var dbSet = new Mock<DbSet<TEntity>>();

            dbSet.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            return dbSet;
        }
    }
}
