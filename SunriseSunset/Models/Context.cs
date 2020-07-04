using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SunriseSunset.Models
{
    public class Context : DbContext
    {
        public DbSet<City> Cities { get; set; }
    }
}