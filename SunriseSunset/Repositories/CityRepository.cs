using SunriseSunset.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace SunriseSunset.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly Context _db;

        public CityRepository(Context db) => _db = db;

        public async Task<IEnumerable<CityModel>> ListAsync()
        {
            return await _db.Cities.ToListAsync();
        }

        public async Task CreateAsync(CityModel city)
        {
            _db.Cities.Add(city);
            await SaveAsync();
        }

        public async Task EditAsync(CityModel city)
        {
            _db.Entry(city).State = EntityState.Modified;
            await SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            CityModel city = await GetAsync(id);
            _db.Cities.Remove(city);
            await SaveAsync();
        }

        public async Task<CityModel> GetAsync(int id)
        {
            return await _db.Cities.FindAsync(id);
        }

        private async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}