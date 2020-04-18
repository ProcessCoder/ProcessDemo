using System;
using System.Collections.Generic;
using System.Text;
using static ProcessDemo.Commons.Enums.Enums;

namespace ProcessDemo.Commons
{
    public class AppleTree
    {
        public int TreeNumber { get; set; }
        public double AppleYield { get; set; }
        public double WaterConsumption { get; set; }
        public Fertilizer FertilizingAgent { get; set; }
        public AppleTree()
        {

        }
        public AppleTree(int treenumber, double appleyield, double waterconsumption, Fertilizer fertilizingagent)
        {
            this.TreeNumber = treenumber;
            this.AppleYield = appleyield;
            this.WaterConsumption = waterconsumption;
            this.FertilizingAgent = fertilizingagent;
        }
    }
}
