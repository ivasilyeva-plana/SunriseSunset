using SunriseSunset.Entities;
using SunriseSunset.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SunriseSunset.Network
{
    public class CityRepository : ICityRepository
    {
        private readonly Context _db;

        public CityRepository(Context db) => _db = db;

        public async Task <IEnumerable<CityModel>> ListAsync()
        {
            return await _db.Cities
                .Select(x => new CityModel
                {
                    Id = x.Id,
                    Key = x.Key,
                    Name = x.Name,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude
                })
                .ToListAsync();
        }

        public async Task CreateAsync(CityModel city)
        {
            _db.Cities.Add(new City
            {
                Id = city.Id,
                Key = city.Key,
                Name = city.Name,
                Latitude = city.Latitude,
                Longitude = city.Longitude
            });
            await SaveAsync();
        }

        public async Task EditAsync(CityModel city)
        {
            var entity = await _db.Cities.FirstOrDefaultAsync(x => x.Id == city.Id);
            if (entity is null) return;

            entity.Id = city.Id;
            entity.Key = city.Key;
            entity.Name = city.Name;
            entity.Latitude = city.Latitude;
            entity.Longitude = city.Longitude;
            await SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Cities.FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null) return;

            _db.Cities.Remove(entity);
            await SaveAsync();
        }

        public async Task<CityModel> GetAsync(int id)
        {
            return await _db.Cities
                .Select(x => new CityModel
                {
                    Id = x.Id,
                    Key = x.Key,
                    Name = x.Name,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        private async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}