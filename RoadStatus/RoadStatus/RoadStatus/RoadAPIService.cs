using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Linq;
using Newtonsoft.Json;
using RoadStatusModel;

namespace RoadStatus
{
    public class RoadAPIService : IRoadAPIService
    {
        public KeyValueConfigurationCollection AppSettings { get; set; }
        public RoadAPIService(KeyValueConfigurationCollection appSettings)
        {
            AppSettings = appSettings;
        }

        public async Task<object> GetRoadStatus(string roadName)
        {
            Object roadResult = null;
            using (var client = new HttpClient())
            {
                string url = AppSettings["RoadAPIUrl"].Value.Replace("{RoadName}", roadName).Replace("{Key}", AppSettings["Key"].Value).Replace("{AppId}", AppSettings["AppId"].Value);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = await client.GetAsync(url);
                if (responseMessage.StatusCode == HttpStatusCode.OK)
                {
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    roadResult = JsonConvert.DeserializeObject<List<Road>>(result).FirstOrDefault();
                }
                if (responseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    roadResult = JsonConvert.DeserializeObject<RoadError>(result);
                }
            }
            return roadResult;
        }
    }
}
