using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCreamProject.Models
{
    public class ImaggaSampleClass
    {
        public List<string> CheckImage(string ImageUrl)
        {
            List<string> result = null;
            string apiKey = "acc_7f32996113f74aa";
            string apiSecret = "4ac5da868fa47b900e6f6103483ef380";      
            string imageUrl = ImageUrl;
            string basicAuthValue = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:{1}", apiKey, apiSecret)));

            var client = new RestClient("https://api.imagga.com/v2/tags");
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);
            request.AddParameter("image_url", imageUrl);
            request.AddHeader("Authorization", String.Format("Basic {0}", basicAuthValue));

            IRestResponse response = client.Execute(request);
            result = ConvertToDictionary(response.Content);
            return result; //string of what he found
        }
        public List<string>  ConvertToDictionary(string response)
        {
            List<string> result = new List<string>();
            ImaggeRoot TheTags = JsonConvert.DeserializeObject<ImaggeRoot>(response);

            foreach (Tag item in TheTags.result.tags)
            {
                result.Add(item.tag.en);
            }
            return result;
            
            ////exeption when the image not found

        }
        public bool isIceCream(List<Tag> listResults)
        { //return true if the image contains ice with a confidence of more than fifty         
            for (int i = 0; i < listResults.Count; i++)
                if ((listResults[i].tag.en == "ice" || listResults[i].tag.en == "ice cream" || listResults[i].tag.en == "frozen dessert" || listResults[i].tag.en == "Food & Drinks")
                    && listResults[i].confidence > 50)
                    return true;
            return false;

        }
    }
}
