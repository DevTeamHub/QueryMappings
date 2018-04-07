using System.Collections.Generic;

namespace DevTeam.QueryMappings.Tests.Context.RentalContext.Models
{
    public class AppartmentModel
    {
        public int Id { get; set; }
        public int BuildingId { get; set; }
        public string Number { get; set; }
        public int Size { get; set; }
        public int Badrooms { get; set; }
        public int Bathrooms { get; set; }
        public int Floor { get; set; }
        public bool IsLodge { get; set; }

        public List<PersonModel> Residents { get; set; }
        public List<ReviewModel> Reviews { get; set; }

        public AppartmentModel()
        {
            Residents = new List<PersonModel>();
            Reviews = new List<ReviewModel>();
        }
    }
}
