using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterServices.Models
{
    public class WaterData
    {
        
        public string Name { get; set; }
        public int Discharge { get; set; }
        public double GageHeight { get; set; }
        public double Temp { get; set; }

    }
}
