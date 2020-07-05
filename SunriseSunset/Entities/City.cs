using System.ComponentModel.DataAnnotations;

namespace SunriseSunset.Entities
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(150)]
        [Required]
        public string Key { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}