using Microsoft.EntityFrameworkCore.Internal;
using ProcessDemo.Commons.Enums;
using ProcessDemo.Commons.Helper;
using ProcessDemo.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessDemo.Commons.Database
{
    public class AppleTreeDbInitializer
    {
        public static List<AppleTree> InitialiseTrees()
        {
            AppleTreeHelper appleTreeHelper = new AppleTreeHelper(new AppleTreeDbContext());
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

                result.Add(tree);
            }

            //return the List
            return result;
        }

       public static List<Farm> InitialiseFarms()
       {
            AppleTreeDbContext con = new AppleTreeDbContext();

            if(con.Farms.Count()==0)
            {
                FarmHelper helper = new FarmHelper(con);

                List<Farm> farms = new List<Farm>
                {
                    new Farm
                    {
                        Name="First Farm",
                        AppleTrees= InitialiseTrees()
                    },
                    new Farm
                    {
                        Name="Second Farm",
                        AppleTrees= InitialiseTrees()
                    },
                    new Farm
                    {
                        Name="Third Farm",
                        AppleTrees= InitialiseTrees()
                    }
                };

                foreach (Farm farm in farms)
                {
                    helper.CreateFarm(farm);
                }
                return farms;
            }
            return null;
       }
    }
}
