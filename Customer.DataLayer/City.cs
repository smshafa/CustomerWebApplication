using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.DataLayer
{
    public class City
    {
        public int CityID { set; get; }
        //public int ProvinceID { set; get; }
        public string CityName { set; get; }

        public virtual ICollection<Province> Province { set; get; }
    }
}
