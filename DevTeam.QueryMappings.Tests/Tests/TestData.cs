using DevTeam.QueryMappings.Tests.Context.RentalContext.Entities;
using System.Collections.Generic;

namespace DevTeam.QueryMappings.Tests.Tests
{
    public class TestData
    {
        public List<Address> Addresses => new List<Address>
        {
            new Address
            {
                Id = 1,
                BuildingId = 1,
                BuildingNumber = "149",
                Street = "Sullivan Str",
                City = "New York",
                State = "NY",
                ZipCode = "10012",
                Country = 1
            },
            new Address
            {
                Id = 2,
                BuildingId = 2,
                BuildingNumber = "618",
                Street = "Marguerita Ave",
                City = "Santa Monica",
                State = "CA",
                ZipCode = "90402",
                Country = 1
            }
        };

        public List<Building> Buildings => new List<Building>
        {
            new Building
            {
                Id = 1,
                Floors = 2,
                IsLaundry = true,
                IsParking = false,
                Year = 1985
            },
            new Building
            {
                Id = 2,
                Floors = 1,
                IsLaundry = true,
                IsParking = true,
                Year = 1997
            }
        };

        public List<Appartment> Appartments => new List<Appartment>
        {
            new Appartment
            {
                Id = 1,
                BuildingId = 1,
                Number = "#1",
                Size = 75,
                Floor = 1,
                Badrooms = 1,
                Bathrooms = 1,
                IsLodge = false
            },
            new Appartment
            {
                Id = 2,
                BuildingId = 1,
                Number = "#2",
                Size = 150,
                Floor = 1,
                Badrooms = 2,
                Bathrooms = 2,
                IsLodge = false
            },
            new Appartment
            {
                Id = 3,
                BuildingId = 1,
                Number = "#3",
                Size = 75,
                Floor = 2,
                Badrooms = 1,
                Bathrooms = 1,
                IsLodge = true
            },
            new Appartment
            {
                Id = 4,
                BuildingId = 1,
                Number = "#4",
                Size = 150,
                Floor = 2,
                Badrooms = 1,
                Bathrooms = 2,
                IsLodge = true
            },
            new Appartment
            {
                Id = 5,
                BuildingId = 2,
                Number = "apt. 1",
                Size = 75,
                Floor = 1,
                Badrooms = 1,
                Bathrooms = 1,
                IsLodge = true
            },
            new Appartment
            {
                Id = 6,
                BuildingId = 2,
                Number = "apt. 2",
                Size = 250,
                Floor = 1,
                Badrooms = 3,
                Bathrooms = 2,
                IsLodge = true
            },
            new Appartment
            {
                Id = 7,
                BuildingId = 2,
                Number = "apt. 3",
                Size = 75,
                Floor = 1,
                Badrooms = 1,
                Bathrooms = 1,
                IsLodge = false
            }
        };

        public List<Person> People => new List<Person>
        {

        }
    }
}
