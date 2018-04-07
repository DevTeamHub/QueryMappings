using System.Collections.Generic;

namespace DevTeam.QueryMappings.Tests.Context.RentalContext.Models
{
    public class AppartmentReviewsModel
    {
        public int Id { get; set; }
        public string Number { get; set; }

        public List<ReviewModel> Reviews { get; set; }

        public AppartmentReviewsModel()
        {
            Reviews = new List<ReviewModel>();
        }
    }
}
