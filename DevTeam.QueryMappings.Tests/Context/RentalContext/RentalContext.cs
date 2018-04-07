using DevTeam.EntityFrameworkExtensions.DbContext;
using DevTeam.QueryMappings.Tests.Context.RentalContext.Entities;
using DevTeam.QueryMappings.Tests.Tests;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

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
            throw new NotImplementedException();
        }
    }
}
