using BucketListAdventures.Models;
using Microsoft.Spatial;

namespace BucketListAdventures.Data
{
    public interface IApplicationRepository
    {
        public IEnumerable<WeatherStation> GetAllWeatherStations();
        public WeatherStation GetNearestWeatherStation(double latitude, double longitude);
    }
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationRepository()
        {

        }
        public ApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual IEnumerable<WeatherStation> GetAllWeatherStations()
        {
            return _context.WeatherStations.ToList();
        }
        public virtual WeatherStation GetNearestWeatherStation(double latitude, double longitude)
        {
            GeographyPoint location = GeographyPoint.Create(latitude, longitude);
            WeatherStation closest = GetAllWeatherStations()
                .OrderBy(x => GeographyOperationsExtensions.Distance(x.geography_point, location))
                .First();
            return closest;
        }
    }
}
