using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.DataLayer
{
    public class Customer
    {
        /*public Customer(string firstName, string lastName, Province province)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Province = province;
        }*/

        public int CustomerID { set; get; }
        //public int ProvinceID { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }

        public virtual Province Province { get; set; }
    }
}
