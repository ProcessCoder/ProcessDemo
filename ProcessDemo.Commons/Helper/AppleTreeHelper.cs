using Microsoft.EntityFrameworkCore;
using ProcessDemo.Commons.Database;
using ProcessDemo.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessDemo.Commons.Helper
{
    public class AppleTreeHelper
    {
        private AppleTreeDbContext db;

        public AppleTreeHelper(AppleTreeDbContext context)
        {
            db = context;
        }

        public AppleTree GetAppleTreeById(int id)
        {
           return db.AppleTrees
                .Include(b=>b.Farm)
                .SingleOrDefault(f => f.Id == id);
        }

        public IList<AppleTree> GetAppleTreeAll()
        {
            return db.AppleTrees
                .Include(b => b.Farm)
                .ToList();
        }

        public AppleTree CreateAppleTree(double appleYield, double waterConsumption, Fertilizer fertilizingAgent)
        {
                var f = new AppleTree();
                f.AppleYield = appleYield;
                f.WaterConsumption = waterConsumption;
                f.FertilizingAgent = fertilizingAgent;

                //Add to Database
                db.AppleTrees.Add(f);

                //Save
                db.SaveChanges();

                return f;
        }

        public AppleTree CreateAppleTree(AppleTree f)
        {
                //Save
                db.AppleTrees.Add(f);
                db.SaveChanges();
               
                return f;
        }

        public IList<AppleTree> DeleteAppleTreeAll()
        {
                var query = from p in db.AppleTrees
                             select p;
                db.AppleTrees.RemoveRange(query);
                db.SaveChanges();

                return (from p in db.AppleTrees
                        select p).ToList();
        }


        public void DeleteAppleTreeById(int id)
        {
            var apples = db.AppleTrees.Where(c => c.Id == id);
            db.AppleTrees.RemoveRange(apples);
            db.SaveChanges();
        }

        public void UpdateTree(AppleTree appletree)
        {
            var tree = db.AppleTrees.FirstOrDefault(c => c.Id == appletree.Id);
            tree.AppleYield = appletree.AppleYield;
            tree.FertilizingAgent = appletree.FertilizingAgent;
            tree.WaterConsumption = appletree.WaterConsumption;
            db.SaveChanges();
        }
    }
}
