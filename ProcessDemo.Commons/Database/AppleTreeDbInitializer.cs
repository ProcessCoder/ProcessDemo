using ProcessDemo.Commons.Enums;
using ProcessDemo.Commons.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessDemo.Commons.Database
{
    public class AppleTreeDbInitializer
    {
        public static List<AppleTree> InitialiseTrees()
        {
            //Initalise return value
            List<AppleTree> result = new List<AppleTree>();

            //Random number generator
            Random random = new Random();

            //Loop to create 10 trees
            for (int i = 1; i <= 10; i++)
            {
                //Add tree with random properties
                AppleTree tree = new AppleTree()
                {
                    AppleYield = random.Next(50, 150),
                    WaterConsumption = random.Next(800, 1200),
                    FertilizingAgent = (Fertilizer)random.Next(Enum.GetNames(typeof(Fertilizer)).Length)
                };

                AppleTreeHelper.CreateAppleTree(tree);
                result.Add(tree);
            }

            //return the List
            return result;
        }
    }
}
