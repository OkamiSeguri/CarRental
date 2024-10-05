using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject
{
    public class Car
    {
        public int CarId { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public string? Type { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
        [NotMapped]
        public string? Image { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }


        public string GetCarInfo()
        {
            return $"{Type} {Model} ({Year}) - ${Price}/day";
        }
    }
}
