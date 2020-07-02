using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WaterServices.Models;

namespace WaterServices.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            List<WaterData> Streams = new List<WaterData>();

            using (var client = new HttpClient())
            { 
                client.BaseAddress = new Uri("https://waterservices.usgs.gov/nwis/iv/");
                client.DefaultRequestHeaders.Clear(); 
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Get data using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("?format=json&countyCd=29071&parameterCd=00060,00065,00010&siteType=ST&siteStatus=all");
                Console.WriteLine(Res.Content);
                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response 
                    var Response = Res.Content.ReadAsStringAsync();
                    Response.Wait();
                    if (Response.IsCompleted)
                    {
                        //var data = JObject.Parse(Response.Result);
                        Streams = JsonConvert.DeserializeObject<List<WaterData>>(Response.Result);
                        //Streams.FirstOrDefault().Name = Streams.Where(x => x.value. == "Picked_Up" || x.JobStatus == "Picked Up").ToList().Count();
                        Console.Write(Streams);
                    }
                        

                    //Deserializing the response  
                    // Streams = JsonConvert.DeserializeObject<List<WaterData>>(Response);

                }
                //Pass stream data to view  
                return View(Streams);
            }
        }
    }
}
