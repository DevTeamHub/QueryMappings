using DevTeam.EntityFrameworkExtensions.DbContext;
using DevTeam.QueryMappings.Tests.Context.SecurityContext.Entities;
using DevTeam.QueryMappings.Tests.Tests;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace DevTeam.QueryMappings.Tests.Mappings
{
    public class SecurityContext : IDbContext
    {
        public IEnumerable<User> Users => TestData.Users;

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
