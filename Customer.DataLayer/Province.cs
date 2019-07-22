using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.DataLayer
{
    public class Province
    {
        //public Province(string city) => this.City = city;

        public int ProvinceID { set; get; }
        public string ProvinceName { set; get; }

        public virtual City City { set; get; }

    }
}
