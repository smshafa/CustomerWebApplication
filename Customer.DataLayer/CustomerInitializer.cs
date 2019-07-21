using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.DataLayer
{
    public class CustomerInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CustomerContext>
    {
        protected override void Seed(CustomerContext context)
        {
            var provinces = new List<Province>
            {
                new Province{ProvinceID = 1, ProvinceName="Hamedan"},
                new Province{ProvinceID = 2, ProvinceName="Tehran"}
            };

            provinces.ForEach(p => context.Provinces.Add(p));
            context.SaveChanges();

            var cities = new List<City>
            {
                new City {CityID = 1, CityName = "Hamedan", ProvinceID = 1},
                new City {CityID = 2, CityName = "Malayer", ProvinceID = 1},
                new City {CityID = 3, CityName = "Tehran", ProvinceID = 2},
                new City {CityID = 4, CityName = "EslamAbad", ProvinceID = 2}
            };

            cities.ForEach(c => context.Cities.Add(c));
            context.SaveChanges();


            var customer = new List<Customer>
            {
                new Customer{FirstName="reza", LastName="shafaei", ProvinceID = 1},
                new Customer{FirstName="Ali", LastName="Moradi", ProvinceID = 2},
                new Customer{FirstName="Hamid", LastName="shafaei", ProvinceID=1}
            };

            customer.ForEach(s => context.Customers.Add(s));
            context.SaveChanges();


        }
    }
}
