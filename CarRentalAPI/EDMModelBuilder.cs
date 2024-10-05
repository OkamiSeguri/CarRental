using BusinessObject;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingAPI
{
    public class EDMModelBuilder
    {
        public static IEdmModel GetEDMModel()
        {
            var modelBuilder = new ODataConventionModelBuilder();

            modelBuilder.EntitySet<Car>("Cars");
            modelBuilder.EntitySet<Rental>("Rentals");

            var rentalEntity = modelBuilder.EntityType<Rental>();

            rentalEntity.HasRequired(r => r.Car);

            return modelBuilder.GetEdmModel();
        }
    }
}
