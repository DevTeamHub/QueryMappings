using System.Collections.Generic;

namespace DevTeam.QueryMappings.Tests.Context.RentalContext.Models
{
    public class Building
    {
        public int Id { get; set; }
        public int Floors { get; set; }
        public int Year { get; set; }
        public bool IsParking { get; set; }
        public bool IsLaundry { get; set; }

        public AddressModel Address { get; set; }
        public List<AppartmentModel> Appartments { get; set; }
        public List<ReviewModel> Reviews { get; set; }

        public Building()
        {
            Appartments = new List<AppartmentModel>();
            Reviews = new List<ReviewModel>();
        }
    }
}
