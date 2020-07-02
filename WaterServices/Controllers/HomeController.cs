using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WaterServices.Models;

namespace WaterServices.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            WaterData RiverData = new WaterData();

            using (var client = new HttpClient())
            { 
                client.BaseAddress = new Uri("https://waterservices.usgs.gov/nwis/iv/");
                client.DefaultRequestHeaders.Clear(); 
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Get data using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("?format=json&countyCd=29071&parameterCd=00060,00065,00010&siteType=ST&siteStatus=all");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response 
                    var Response = Res.Content.ReadAsStringAsync();
                    Response.Wait();
                    if (Response.IsCompleted)
                    {
                        var data = JObject.Parse(Response.Result)["value"]["timeSeries"];
                        RiverData.Name = data[0]["sourceInfo"]["siteName"].ToString();
                        RiverData.Discharge = (int)data[0]["values"][0]["value"][0]["value"];
                        RiverData.GageHeight = (double)data[1]["values"][0]["value"][0]["value"];
                        RiverData.Temp = (double)data[2]["values"][0]["value"][0]["value"];
                    }
                }
                //Pass stream data to view  
                return View(RiverData);
            }
        }
    }
}
