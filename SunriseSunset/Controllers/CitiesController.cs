using SunriseSunset.Models;
using SunriseSunset.Network;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SunriseSunset.Controllers
{
    public class CitiesController : Controller
    {
        private readonly ICityRepository _cities;

        public CitiesController(ICityRepository db) => _cities = db;

        // GET: Cities
        public async Task<ActionResult> Index()
        {
            return View(await _cities.ListAsync());
        }


        // GET: Cities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Key,Name,Latitude,Longitude")] CityModel city)
        {
            if (!ModelState.IsValid) return View(city);
            await _cities.CreateAsync(city);
            return RedirectToAction("Index");

        }

        // GET: Cities/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (!id.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var city = await _cities.GetAsync(id.Value);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: Cities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Key,Name,Latitude,Longitude")] CityModel city)
        {
            if (ModelState.IsValid)
            {
                await _cities.EditAsync(city);
                return RedirectToAction("Index");
            }
            return View(city);
        }

        // GET: Cities/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var city = await _cities.GetAsync(id.Value);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _cities.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
