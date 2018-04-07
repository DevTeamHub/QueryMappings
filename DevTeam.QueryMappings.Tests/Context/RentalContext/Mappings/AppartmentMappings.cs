using DevTeam.QueryMappings.Base;
using DevTeam.QueryMappings.Helpers;
using DevTeam.QueryMappings.Tests.Context.RentalContext.Entities;
using DevTeam.QueryMappings.Tests.Context.RentalContext.Models;

namespace DevTeam.QueryMappings.Tests.Context.RentalContext.Mappings
{
    public class AppartmentMappings: IMappingsStorage
    {
        public void Setup()
        {
            MappingsList.Add<Appartment, AppartmentModel>(x => new AppartmentModel
            {
                Id = x.Id,
                Badrooms = x.Badrooms,
                Bathrooms = x.Bathrooms,
                Floor = x.Floor,
                IsLodge = x.IsLodge,
                Number = x.Number,
                Size = x.Size,
                Building = new BuildingModel
                {
                    Id = x.Building.Id,
                    Year = x.Building.Year,
                    Floors = x.Building.Floors,
                    IsLaundry = x.Building.IsLaundry,
                    IsParking = x.Building.IsParking
                }
            });
        }
    }
}
