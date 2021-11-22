using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCreamProject.Models
{
    public class Temperature
    {
        public int Id { get; set; }

        public int Day { get; set; }
        public int Month { get; set; }

        public int TempValue { get; set; }
    }
}
