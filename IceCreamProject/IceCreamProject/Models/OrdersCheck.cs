using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCreamProject.Models
{
    public class OrdersCheck
    {
        public Orders Check(Orders orders)
        {
            AddressChecking addressChecking = new AddressChecking();
            //Boolean result = addressChecking.CheckAddress(City, Street);
            //bool flag = checkStreet(orders.City, orders.Street);
            if (addressChecking.CheckAddress(orders.City, orders.Street))
            {
                orders.Date = DateTime.Now;
                if (orders.Date.Month >= 12 && orders.Date.Month < 3)
                    orders.Season = "Winter";
                if (orders.Date.Month >= 3 && orders.Date.Month < 6)
                    orders.Season = "Spring";
                if (orders.Date.Month >= 6 && orders.Date.Month < 9)
                    orders.Season = "Summer";
                if (orders.Date.Month >= 9 && orders.Date.Month < 12)
                    orders.Season = "Fall";
                WeatherClass weather = new WeatherClass();
                Main result = weather.CheckWeather(orders.City);
                orders.Pressure = result.pressure;
                orders.Humidity = result.humidity;
                orders.Temperature = (float)result.temp;
                return orders;
            }
            else
            {
                return null;
            }
        }
    }
}
