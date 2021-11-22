using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCreamProject.Models
{
    public class Orders
    {
        public int Id { get; set; } //order id
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        // public int MyProperty { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public string Flavor { get; set; }
        public string Season { get; set; }
      //  public string Day { get; set; }
        public float Temperature { get; set; }
        public DateTime Date { get; set; }
        public float Humidity { get; set; }
        public float Pressure { get; set; }
      //  public float Price { get; set; }
    }
}
