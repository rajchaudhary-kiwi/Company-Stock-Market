using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Data
{
    public class PriceVariationModel
    {
        public int Hour { get; set; }
        public int Day { get; set; }
        public DayOfWeek Week { get; set; }
        public int Month { get; set; }
    }
}
