using DevTeam.EntityFrameworkExtensions.DbContext;
using DevTeam.QueryMappings.Base;
using DevTeam.QueryMappings.Helpers;
using System.Linq;

namespace DevTeam.QueryMappings.Tests.Context.RentalContext.Mappings
{
    public class PersonMappings : IMappingsStorage
    {
        public void Setup()
        {
            MappingsList.Add<Person, PersonModel>(x => new PersonModel
            {
                PersonEmail = x.Email,
                PersonName = x.Name
            });

            MappingsList.Add<Person, PersonModel, PersonArgs>((args) =>
            {
                return x => new PersonModel
                {
                    PersonEmail = x.Email,
                    PersonName = x.Name + args.Prefix
                };
            });

            MappingsList.Add<Person, PersonModel, IDbContext>((query, context) =>
                from person in query
                join address in context.Set<Address>().AsQueryable() on person.AddressId equals address.Id
                select new PersonModel
                {
                    PersonEmail = person.Email,
                    PersonName = person.Name
                }
            );

            MappingsList.Add<Person, PersonModel, PersonArgs, IDbContext>((args) =>
            {
                return (query, context) => from person in query
                                           join address in context.Set<Address>().AsQueryable() on person.AddressId equals address.Id
                                           select new PersonModel
                                           {
                                               PersonEmail = person.Email,
                                               PersonName = person.Name + args.Prefix
                                           };
            });
        }
    }
}
