using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reto_intercorp.ViewModel
{
    public class KPIClientViewModel
    {

        public int avg_age { get; set; }
        public Double desvstnd { get; set; }
        public List<Data> clientsAge { get; set; }

    }

    public class Data
    {

        public int age { get; set; }
        public int count { get; set; }

    }
}
