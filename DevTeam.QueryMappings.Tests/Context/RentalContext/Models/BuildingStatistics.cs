namespace DevTeam.QueryMappings.Tests.Context.RentalContext.Models
{
    public class BuildingStatistics
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int AppartmentsCount { get; set; }
        public int ResidentsCount { get; set; }
        public int Size { get; set; }
        public int AverageBuildingRating { get; set; }
        public int AverageAppartmentRating { get; set; }
    }
}
