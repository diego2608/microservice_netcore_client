using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reto_intercorp.ViewModel
{
    public class ClientViewModel
    {
        public String ClientId { get; set; }
        public String Name { get; set; }
        public String Last_Name { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime Probably_death => DateTime.Now.AddYears(new Random().Next(5, 30)).AddDays(new Random().Next(7, 30));

    }
}
