using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCreamProject.Models
{

    /**
*  Predictor for ﻿IceCream from model/6179b76999dfe70754015aff
*  Predictive model by BigML - Machine Learning Made Easy
*/
    public class Icecream
    {
        public string city { get; set; }
        public string season { get; set; }
        public double? temperature { get; set; }
        public double? humidity { get; set; }
        public string day { get; set; }

        public static string PredictIcecream(string city, string season, double? temperature, double? humidity, string day)
        {
            if (temperature == null)
            {
                return "Coffee";
            }
            if (temperature > 19)
            {
                if (day == null)
                {
                    return "Coffee";
                }
                if (day.Equals("Friday"))
                {
                    return "Lotus cookies";
                }
                if (!day.Equals("Friday"))
                {
                    if (city == null)
                    {
                        return "Coffee";
                    }
                    if (city.Equals("ירושלים"))
                    {
                        if (humidity == null)
                        {
                            return "Coffee";
                        }
                        if (humidity > 21)
                        {
                            if (day.Equals("Wednesday"))
                            {
                                return "Colourful";
                            }
                            if (!day.Equals("Wednesday"))
                            {
                                if (day.Equals("Thursday"))
                                {
                                    if (temperature > 23.5)
                                    {
                                        return "Colourful";
                                    }
                                    if (temperature <= 23.5)
                                    {
                                        return "Coffee";
                                    }
                                }
                                if (!day.Equals("Thursday"))
                                {
                                    return "Coffee";
                                }
                            }
                        }
                        if (humidity <= 21)
                        {
                            return "Coconut";
                        }
                    }
                    if (!city.Equals("ירושלים"))
                    {
                        if (humidity == null)
                        {
                            return "Oreo";
                        }
                        if (humidity > 86)
                        {
                            if (day.Equals("Tuesday"))
                            {
                                if (temperature > 31.5)
                                {
                                    return "Strawberry";
                                }
                                if (temperature <= 31.5)
                                {
                                    return "Passionflower";
                                }
                            }
                            if (!day.Equals("Tuesday"))
                            {
                                return "Berries";
                            }
                        }
                        if (humidity <= 86)
                        {
                            if (temperature > 33)
                            {
                                if (day.Equals("Sunday"))
                                {
                                    return "Coffee";
                                }
                                if (!day.Equals("Sunday"))
                                {
                                    if (temperature > 55)
                                    {
                                        return "Oreo";
                                    }
                                    if (temperature <= 55)
                                    {
                                        if (temperature > 45)
                                        {
                                            return "Marshmallow";
                                        }
                                        if (temperature <= 45)
                                        {
                                            if (temperature > 37.5)
                                            {
                                                return "Mango";
                                            }
                                            if (temperature <= 37.5)
                                            {
                                                return "Marshmallow";
                                            }
                                        }
                                    }
                                }
                            }
                            if (temperature <= 33)
                            {
                                if (city.Equals("בני ברק"))
                                {
                                    if (temperature > 27.5)
                                    {
                                        if (temperature > 29.5)
                                        {
                                            return "Peanuts";
                                        }
                                        if (temperature <= 29.5)
                                        {
                                            return "Lotus cookies";
                                        }
                                    }
                                    if (temperature <= 27.5)
                                    {
                                        if (humidity > 53)
                                        {
                                            return "Mango";
                                        }
                                        if (humidity <= 53)
                                        {
                                            if (temperature > 24.75)
                                            {
                                                return "Coffee";
                                            }
                                            if (temperature <= 24.75)
                                            {
                                                return "Coconut";
                                            }
                                        }
                                    }
                                }
                                if (!city.Equals("בני ברק"))
                                {
                                    if (city.Equals("פתח תקווה"))
                                    {
                                        return "Oreo";
                                    }
                                    if (!city.Equals("פתח תקווה"))
                                    {
                                        if (season == null)
                                        {
                                            return "Strawberry";
                                        }
                                        if (season.Equals("Spring"))
                                        {
                                            if (humidity > 21)
                                            {
                                                return "Strawberry";
                                            }
                                            if (humidity <= 21)
                                            {
                                                return "Coconut";
                                            }
                                        }
                                        if (!season.Equals("Spring"))
                                        {
                                            if (day.Equals("Thursday"))
                                            {
                                                return "Strawberry";
                                            }
                                            if (!day.Equals("Thursday"))
                                            {
                                                if (humidity > 35)
                                                {
                                                    if (day.Equals("Wednesday"))
                                                    {
                                                        return "Coffee";
                                                    }
                                                    if (!day.Equals("Wednesday"))
                                                    {
                                                        return "Oreo";
                                                    }
                                                }
                                                if (humidity <= 35)
                                                {
                                                    return "Coffee";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (temperature <= 19)
            {
                return "Seasonal fruit";
            }
            return null;
        }
    }
}
