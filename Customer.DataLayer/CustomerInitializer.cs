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
            var cities = new List<City>
            {
                new City {CityID = 1, CityName = "Hamedan"},
                new City {CityID = 2, CityName = "Malayer"},
                new City {CityID = 3, CityName = "Tehran"},
                new City {CityID = 4, CityName = "EslamAbad"}
            };

            cities.ForEach(c => context.Cities.Add(c));
            context.SaveChanges();


            var provinces = new List<Province>
            {
                new Province {ProvinceID = 1, ProvinceName = "Hamedan", City = cities[0]},
                new Province {ProvinceID = 2, ProvinceName = "Tehran", City = cities[1]}
            };

            provinces.ForEach(p => context.Provinces.Add(p));
            context.SaveChanges();
            var customer = new List<Customer>
            {
                new Customer {FirstName = "reza", LastName = "shafaei", Province = provinces[0]},
                new Customer {FirstName = "Ali", LastName = "Moradi", Province  = provinces[1]},
                new Customer {FirstName = "Hamid", LastName = "shafaei", Province = provinces[0]}
                //create a new province in province table with only ProvinceID
                //new Customer {FirstName = "Hamid", LastName = "shafaei", Province = new Province {ProvinceID = 2}}
            };

            customer.ForEach(s => context.Customers.Add(s));
            context.SaveChanges();


        }
    }
}
