using ProcessDemo.Commons.Database;
using ProcessDemo.Commons.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessDemo.WPF.DataAccess
{
    public class Instances
    {
        //Our global DbContext
        private static Lazy<AppleTreeDbContext> dbContext = null;

        //Getter for our global DbContext
        public static Lazy<AppleTreeDbContext> GetDbContext
        {
            get
            {
                if (dbContext == null)
                {
                    dbContext = new Lazy<AppleTreeDbContext>();
                }
                return dbContext;
            }
        }

        //Our global AppleTreeHelper
        private static Lazy<AppleTreeHelper> appleTreeHelper = null;

        //Getter for our global AppleTreeHelper
        public static Lazy<AppleTreeHelper> GetAppleTreeHelper
        {
            get
            {
                if (appleTreeHelper == null)
                {
                    appleTreeHelper = new Lazy<AppleTreeHelper>(() => new AppleTreeHelper(GetDbContext.Value));
                }
                return appleTreeHelper;
            }
        }

        //Our global FarmHelper
        private static Lazy<FarmHelper> farmHelper = null;

        //Getter for our global FarmHelper
        public static Lazy<FarmHelper> GetFarmHelper
        {
            get
            {
                if (farmHelper == null)
                {
                    farmHelper = new Lazy<FarmHelper>(() => new FarmHelper(GetDbContext.Value));
                }
                return farmHelper;
            }
        }
    }
}
