using ProcessDemo.Commons.Database;
using ProcessDemo.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessDemo.Commons.Helper
{
    public static class AppleTreeHelper
    {
        /// <summary>
        /// Returns a single AppleTree by Id
        /// </summary>
        /// <returns></returns>
        public static AppleTree GetAppleTreeById(int id)
        {
            using (var db = new AppleTreeDbContext())
            {
                return db.AppleTrees.Where(f => f.Id == id).SingleOrDefault();
            }

        }

        /// <summary>
        /// Returns all AppleTrees as a List
        /// </summary>
        /// <returns></returns>
        public static IList<AppleTree> GetAppleTreeAll()
        {
            using (var db = new AppleTreeDbContext())
            {
                var query = db.AppleTrees;
               
                return query.ToList();
            }
        }

        /// <summary>
        /// Creates a new Apple Tree from its properties
        /// </summary>
        /// <returns></returns>
        public static AppleTree CreateAppleTree(double appleYield, double waterConsumption, Fertilizer fertilizingAgent)
        {
            using (var db = new AppleTreeDbContext())
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
        }

        /// <summary>
        /// Creates a new Apple Tree from an object
        /// </summary>
        /// <returns></returns>
        public static AppleTree CreateAppleTree(AppleTree f)
        {

            using (var db = new AppleTreeDbContext())
            {
                //Save
               
                    db.AppleTrees.Add(f);
                    db.SaveChanges();
               
                return f;
            }
        }

        /// <summary>
        /// Deletes all Apple Trees from the database
        /// </summary>
        /// <returns></returns>
        public static IList<AppleTree> DeleteAppleTreeAll()
        {
            using (var db = new AppleTreeDbContext())
            {
                var query = (from p in db.AppleTrees
                             select p);
                db.AppleTrees.RemoveRange(query);

                db.SaveChanges();

                return (from p in db.AppleTrees
                        select p).ToList();
            }
        }


        /// <summary>
        /// Deletes a tree by Id
        /// </summary>
        /// <returns></returns>
        public static void DeleteAppleTreeById(int id)
        {
            using (var db = new AppleTreeDbContext())
            {
                var apples = db.AppleTrees.Where(c => c.Id == id);
                db.AppleTrees.RemoveRange(apples);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Updates a tree
        /// </summary>
        /// <returns></returns>
        public static void UpdateTree(AppleTree appletree)
        {
            using (var db = new AppleTreeDbContext())
            {
                var tree = db.AppleTrees.FirstOrDefault(c => c.Id == appletree.Id);
                tree.AppleYield = appletree.AppleYield;
                tree.FertilizingAgent = appletree.FertilizingAgent;
                tree.WaterConsumption = appletree.WaterConsumption;
                db.SaveChanges();
            }
        }
    }
}
