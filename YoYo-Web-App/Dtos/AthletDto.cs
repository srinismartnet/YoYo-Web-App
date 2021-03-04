using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoYo_Web_App.Dtos
{
    public class AthletDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool warn { get; set; }
        public bool stop { get; set; }
        public int levelNumber { get; set; }
        public int shuttleNumber { get; set; }
    }
}
