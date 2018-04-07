using DevTeam.QueryMappings.Base;
using DevTeam.QueryMappings.Helpers;
using DevTeam.QueryMappings.Tests.Context.RentalContext.Entities;
using DevTeam.QueryMappings.Tests.Context.RentalContext.Models;
using System.Linq;

namespace DevTeam.QueryMappings.Tests.Context.RentalContext.Mappings
{
    public class BuildingMappings : IMappingsStorage
    {
        public void Setup()
        {
            MappingsList.Add<Building, BuildingModel>(x => new BuildingModel
            {
                Id = x.Id,
                Year = x.Year,
                Floors = x.Floors,
                IsLaundry = x.IsLaundry,
                IsParking = x.IsParking,
                Address = new AddressModel
                {
                    Id = x.Address.Id,
                    BuildingNumber = x.Address.BuildingNumber,
                    City = x.Address.City,
                    Country = (Countries)x.Address.Country,
                    State = x.Address.State,
                    Street = x.Address.Street,
                    ZipCode = x.Address.ZipCode
                },
                Appartments = x.Appartments.Select(a => new AppartmentModel
                {
                    Id = a.Id,
                    Badrooms = a.Badrooms,
                    Bathrooms = a.Bathrooms,
                    Floor = a.Floor,
                    IsLodge = a.IsLodge,
                    Number = a.Number,
                    Size = a.Size
                }).ToList()
            });
        }
    }
}
