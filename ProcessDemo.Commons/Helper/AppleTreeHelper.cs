using System;
using System.Collections.Generic;
using System.Text;
using static ProcessDemo.Commons.Enums.Enums;

namespace ProcessDemo.Commons.Helper
{
    public static class AppleTreeHelper
    {
        public static List<AppleTree> InitialiseTrees()
        {
            //Initalise return value
            List<AppleTree> result = new List<AppleTree>();

            //Random number generator
            Random random = new Random();

            //Loop to create 10 trees
            for(int i =1; i<=10;i++)
            {
                //Add tree with random properties
                result.Add(new AppleTree()
                            {
                                TreeNumber= i,
                                AppleYield=random.Next(50, 150),
                                WaterConsumption= random.Next(800,1200),
                                FertilizingAgent= (Fertilizer)random.Next(Enum.GetNames(typeof(Fertilizer)).Length)
                            }
                        );
            }

            //return the List
            return result;
        }
    }
}
