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

        /// <summary>
        /// Returns a single AppleTree by Id
        /// </summary>
        /// <returns></returns>
        public AppleTree GetAppleTreeById(int id)
        {
           return db.AppleTrees.SingleOrDefault(f => f.Id == id);
        }

        /// <summary>
        /// Returns all AppleTrees as a List
        /// </summary>
        /// <returns></returns>
        public IList<AppleTree> GetAppleTreeAll()
        {
            return db.AppleTrees.ToList();
        }

        /// <summary>
        /// Creates a new Apple Tree from its properties
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Creates a new Apple Tree from an object
        /// </summary>
        /// <returns></returns>
        public AppleTree CreateAppleTree(AppleTree f)
        {
                //Save
                db.AppleTrees.Add(f);
                db.SaveChanges();
               
                return f;
        }

        /// <summary>
        /// Deletes all Apple Trees from the database
        /// </summary>
        /// <returns></returns>
        public IList<AppleTree> DeleteAppleTreeAll()
        {
                var query = from p in db.AppleTrees
                             select p;
                db.AppleTrees.RemoveRange(query);
                db.SaveChanges();

                return (from p in db.AppleTrees
                        select p).ToList();
        }


        /// <summary>
        /// Deletes a tree by Id
        /// </summary>
        /// <returns></returns>
        public void DeleteAppleTreeById(int id)
        {
            var apples = db.AppleTrees.Where(c => c.Id == id);
            db.AppleTrees.RemoveRange(apples);
            db.SaveChanges();
        }

        /// <summary>
        /// Updates a tree
        /// </summary>
        /// <returns></returns>
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
