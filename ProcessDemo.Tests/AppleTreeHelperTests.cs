using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProcessDemo.Commons;
using ProcessDemo.Commons.Database;
using ProcessDemo.Commons.Enums;
using ProcessDemo.Commons.Helper;
using System.Linq;

namespace ProcessDemo.Tests
{
    [TestClass]
    public class AppleTreeHelperTests
    {
        //Create an instance of DbContextOptions configured to use an in memory database
        DbContextOptions<AppleTreeDbContext> options = new DbContextOptionsBuilder<AppleTreeDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        [TestMethod]
        public void CreateAppleTreeTests()
        {
            //Instantiate a new DbContext with the in memory database
            var InMemoryContext = new AppleTreeDbContext(options);

            //Check if the database is empty
            Assert.AreEqual(InMemoryContext.AppleTrees.Count(), 0);

            //Create an instance of AppleTreeHelper and pass the in memory context as a parameter
            AppleTreeHelper appleTreeHelper = new AppleTreeHelper(InMemoryContext);

            //Create an in memory apple tree
            var appletree = appleTreeHelper.CreateAppleTree(100, 110, Fertilizer.Medium);

            //Check if tree was inserted
            Assert.AreEqual(InMemoryContext.AppleTrees.Count(), 1);

            //Check the inserted tree's properties
            Assert.AreEqual(100, InMemoryContext.AppleTrees.FirstOrDefault().AppleYield);
            Assert.AreEqual(110, InMemoryContext.AppleTrees.FirstOrDefault().WaterConsumption);
            Assert.AreEqual(Fertilizer.Medium, InMemoryContext.AppleTrees.FirstOrDefault().FertilizingAgent);


        }

        [TestMethod]
        public void UpdateAppleTreeTest()
        {
            //Instantiate a new DbContext with the in memory database
            var InMemoryContext = new AppleTreeDbContext(options);

            //If there's no tree, insert one
            if(InMemoryContext.AppleTrees.Count() == 0)
            {
                InMemoryContext.AppleTrees.Add(new AppleTree { WaterConsumption = 100, AppleYield = 200, FertilizingAgent = Fertilizer.Medium });
                InMemoryContext.SaveChanges();
            }

            //Create an instance of AppleTreeHelper and pass the in memory context as a parameter
            AppleTreeHelper appleTreeHelper = new AppleTreeHelper(InMemoryContext);

            //Get the first in memory apple tree
            var appletree = InMemoryContext.AppleTrees.FirstOrDefault();

            //Change a property
            appletree.WaterConsumption = 200;
            //Update the tree
            appleTreeHelper.UpdateTree(appletree);

            //Assert
            Assert.AreEqual(200, InMemoryContext.AppleTrees.FirstOrDefault().WaterConsumption);

        }

        [TestMethod]
        public void DeleteAppleTreeAllTest()
        {
            //Instantiate a new DbContext with the in memory database
            var InMemoryContext = new AppleTreeDbContext(options);

            //Create an instance of AppleTreeHelper and pass the in memory context as a parameter
            AppleTreeHelper appleTreeHelper = new AppleTreeHelper(InMemoryContext);

            //If there's no tree, insert one
            if (InMemoryContext.AppleTrees.Count()==0)
            {
                InMemoryContext.AppleTrees.Add(new Commons.AppleTree { WaterConsumption = 100, AppleYield = 200, FertilizingAgent = Fertilizer.Medium });
                InMemoryContext.SaveChanges();
            }

            //Check if there are trees
            Assert.IsTrue(InMemoryContext.AppleTrees.Count()> 0);

            //Delete them
            appleTreeHelper.DeleteAppleTreeAll();

            //Check for correct deletion
            Assert.AreEqual(InMemoryContext.AppleTrees.Count(), 0);
        }
    }
}
