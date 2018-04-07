using DevTeam.EntityFrameworkExtensions.DbContext;
using DevTeam.QueryMappings.Tests.Context.RentalContext.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace DevTeam.QueryMappings.Tests.Context.RentalContext
{
    public class RentalContext : IDbContext
    {
        public IEnumerable<Building> Buildings { get; set; }
        public IEnumerable<Appartment> Appartments { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
        public IEnumerable<Person> People { get; set; }
        public IEnumerable<Review> Reviews { get; set; }

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

    public class SecondContextMock: FirstContextMock { }
}
