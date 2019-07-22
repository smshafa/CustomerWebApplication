using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.DataLayer
{
    public class City
    {
        public int CityID { set; get; }
        public string CityName { set; get; }

   
        // many side for province side
        //it is not compulsory, I put it to initiate in CustomerInitializer
        public virtual Province Province { get; set; }

        //one side for many customer
        public virtual ICollection<Customer> Customer { set; get; }

        //https://www.entityframeworktutorial.net/code-first/configure-one-to-one-relationship-in-code-first.aspx
    }
}
