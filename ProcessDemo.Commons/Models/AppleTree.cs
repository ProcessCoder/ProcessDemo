using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static ProcessDemo.Commons.Enums.Enums;

namespace ProcessDemo.Commons
{
    public class AppleTree
    {
        [Key]
        public int Id { get; set; }
        public int TreeNumber { get; set; }
        public double AppleYield { get; set; }
        public double WaterConsumption { get; set; }
        public Fertilizer FertilizingAgent { get; set; }
        public AppleTree()
        {

        }
    }
}
