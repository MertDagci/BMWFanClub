using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMWFanClub.InAndOutModel
{
    public class MFiltreFormBase
    {
        public string? Search { get; set; }
        public int? Offset { get; set; }
        public int? Take { get; set; }
        public FSort? Sort { get; set; } = new FSort();

        public class FSort
        {
            public string Column { get; set; } = "";
            public string Type { get; set; } = "DESC";
        }
    }
}
