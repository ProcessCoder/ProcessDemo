using Microsoft.EntityFrameworkCore;
using ProcessDemo.Commons.Database;
using ProcessDemo.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ProcessDemo.Commons.Helper
{
    public class FarmHelper
    {
        private AppleTreeDbContext db;

        public FarmHelper(AppleTreeDbContext context)
        {
            db = context;
        }

        public Farm GetFarmById(int id)
        {
            return db.Farms
                .Include(f=>f.AppleTrees)
                .SingleOrDefault(f => f.FarmId == id);
        }

        public IList<Farm> GetFarmAll()
        {
            return db.Farms
                .Include(f => f.AppleTrees)
                .ToList();
        }

        public Farm CreateFarm(string name, List<AppleTree> appleTrees)
        {
            var f = new Farm();
            f.Name = name;
            f.AppleTrees = appleTrees;

            //Add to Database
            db.Farms.Add(f);

            //Save
            db.SaveChanges();

            return f;
        }

        public Farm CreateFarm(Farm f)
        {
            //Save
            db.Farms.Add(f);
            db.SaveChanges();

            return f;
        }

        public IList<Farm> DeleteFarmAll()
        {
            var query = from p in db.Farms
                        select p;
            db.Farms.RemoveRange(query);
            db.SaveChanges();

            return (from p in db.Farms
                    select p).ToList();
        }


        public void DeleteFarmById(int id)
        {
            var apples = db.Farms.Where(c => c.FarmId == id);
            db.Farms.RemoveRange(apples);
            db.SaveChanges();
        }

        public void UpdateFarm(Farm updatedFarm)
        {
            var farm = db.Farms.FirstOrDefault(c => c.FarmId == updatedFarm.FarmId);
            farm.Name = updatedFarm.Name;
            farm.AppleTrees = updatedFarm.AppleTrees;
            db.SaveChanges();
        }

        public IList<Farm> FindFarms(Expression<Func<Farm, bool>> predicate)
        {
            return db.Farms
                 .Include(c=>c.AppleTrees)
                            .Where(predicate)
                            .ToList();
        }
    }
}
