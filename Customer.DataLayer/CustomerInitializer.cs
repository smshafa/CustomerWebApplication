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
                new Province {ProvinceID = 1, ProvinceName = "Hamedan"},
                new Province {ProvinceID = 2, ProvinceName = "Tehran"}
            };

            provinces.ForEach(p => context.Provinces.Add(p));
            context.SaveChanges();


            var cities = new List<City>
            {
                new City {CityName = "Hamedan", Province = provinces[0]},
                new City {CityName = "Malayer", Province = provinces[0]},
                new City {CityName = "Tehran", Province = provinces[1]},
                new City {CityName = "EslamAbad", Province = provinces[1]}
            };

            cities.ForEach(c => context.Cities.Add(c));
            context.SaveChanges();


            var customers = new List<Customer>
            {
                new Customer {FirstName = "reza", LastName = "shafaei", City = cities[0]},
                new Customer {FirstName = "Ali", LastName = "Moradi", City = cities[1]},
                new Customer {FirstName = "Hamid", LastName = "shafaei", City = cities[2]}
                //create a new province in province table with only ProvinceID
                //new Customer {FirstName = "Hamid", LastName = "shafaei", Province = new Province {ProvinceID = 2}}
            };
            customers.ForEach(s => context.Customers.Add(s));
            context.SaveChanges();

        }
    }
}
