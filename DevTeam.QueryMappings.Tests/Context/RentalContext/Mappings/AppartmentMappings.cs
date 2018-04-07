using DevTeam.QueryMappings.Base;
using DevTeam.QueryMappings.Helpers;
using DevTeam.QueryMappings.Tests.Context.RentalContext.Entities;
using DevTeam.QueryMappings.Tests.Context.RentalContext.Mappings.Arguments;
using DevTeam.QueryMappings.Tests.Context.RentalContext.Models;
using System.Linq;

namespace DevTeam.QueryMappings.Tests.Context.RentalContext.Mappings
{
    public class AppartmentMappings: IMappingsStorage
    {
        public void Setup()
        {
            MappingsList.Add<Appartment, AppartmentModel, AppartmentsArguments>(MappingsNames.AppartmentsWithBuilding, args => 
            {
                return x => new AppartmentModel
                {
                    Id = x.Id,
                    Badrooms = x.Badrooms,
                    Bathrooms = x.Bathrooms,
                    Floor = x.Floor,
                    IsLodge = x.IsLodge,
                    Number = x.Number,
                    Size = x.Size.ToString() + args.UnitOfMeasure,
                    Building = new BuildingModel
                    {
                        Id = x.Building.Id,
                        Year = x.Building.Year,
                        Floors = x.Building.Floors,
                        IsLaundry = x.Building.IsLaundry,
                        IsParking = x.Building.IsParking
                    }
                };
            });

            MappingsList.Add<Appartment, AppartmentModel, AppartmentsArguments>(MappingsNames.AppartmentsWithoutBuilding, args =>
            {
                return x => new AppartmentModel
                {
                    Id = x.Id,
                    Badrooms = x.Badrooms,
                    Bathrooms = x.Bathrooms,
                    Floor = x.Floor,
                    IsLodge = x.IsLodge,
                    Number = x.Number,
                    Size = x.Size.ToString() + args.UnitOfMeasure
                };
            });

            MappingsList.Add<Appartment, AppartmentShortModel, AppartmentsArguments>(args => 
            {
                return appartment => new AppartmentShortModel
                {
                    Id = appartment.Id,
                    Floor = appartment.Floor,
                    IsLodge = appartment.IsLodge,
                    Number = appartment.Number,
                    Size = appartment.Size.ToString() + args.UnitOfMeasure
                };
            });

            MappingsList.Add<Appartment, AppartmentReviewsModel, RentalContext>((query, context) =>
                from appartment in query
                join review in context.Reviews on new { EntityId = appartment.Id, EntityTypeId = (int)EntityType.Building }
                                               equals new { EntityId = review.EntityId, EntityTypeId = review.EntityTypeId }
                                               into reviews
                select new AppartmentReviewsModel
                {
                    Id = appartment.Id,
                    Number = appartment.Number,
                    Reviews = reviews.Select(review => new ReviewModel
                    {
                        Id = review.Id,
                        EntityId = review.EntityId,
                        EntityType = (EntityType)review.EntityTypeId,
                        Rating = review.Rating,
                        Comments = review.Comments
                    }).ToList()
                });
        }
    }
}
