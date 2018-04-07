using DevTeam.QueryMappings.Base;
using DevTeam.QueryMappings.Helpers;
using System.Linq;

namespace DevTeam.QueryMappings.Tests.Context.SecurityContext.Mappings
{
    public class CarMappings: IMappingsStorage
    {
        public void Setup()
        {
            MappingsList.Add<Car, CarModel>(x => new CarModel
            {
                Make = x.Make,
                Wheels = x.Wheels.Select(w => new WheelModel
                {
                    Size = w.Size
                }).ToList()
            });

            MappingsList.Add<Car, CarModel, >(x => new CarModel
            {
                Make = x.Make,
                Wheels = x.Wheels.Select(w => new WheelModel
                {
                    Size = w.Size
                }).ToList()
            });
        }
    }
}
