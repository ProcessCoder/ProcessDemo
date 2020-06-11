using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProcessDemo.Commons.Database;
using ProcessDemo.Commons.Helper;
using ProcessDemo.Commons.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ProcessDemo.Tests
{
    [TestClass]
    public class FarmHelperTests
    {
        //Create an instance of DbContextOptions configured to use an in memory database
        DbContextOptions<AppleTreeDbContext> options = new DbContextOptionsBuilder<AppleTreeDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

       
        [TestMethod]
        public void CreateFarmAndGetFarmAllTests()
        {
            //Instantiate a new DbContext with the in memory database
            var InMemoryContext = new AppleTreeDbContext(options);

            //Create an instance of FarmHelper and pass the in memory context as a parameter
            FarmHelper farmHelper = new FarmHelper(InMemoryContext);
            farmHelper.DeleteFarmAll();

            //Check if the database is empty
            Assert.AreEqual(InMemoryContext.Farms.Count(), 0);

            //Create an in memory farm
            var farm = farmHelper.CreateFarm("Testfarm",AppleTreeDbInitializer.InitialiseTrees());

            //Check if farm was inserted
            Assert.AreEqual(InMemoryContext.Farms.Count(), 1);

            //Check the inserted farm's properties
            Assert.AreEqual("Testfarm", InMemoryContext.Farms.FirstOrDefault().Name);
            Assert.AreEqual(10, InMemoryContext.Farms.FirstOrDefault().AppleTrees.Count);

            ObservableCollection<Farm> farms = new ObservableCollection<Farm>(farmHelper.GetFarmAll());
            Assert.AreEqual(farms.FirstOrDefault().AppleTrees.Count, 10);
        }

        [TestMethod]
        public void CreateFarmTestsWithObjectAsParameter()
        {
            //Instantiate a new DbContext with the in memory database
            var InMemoryContext = new AppleTreeDbContext(options);

            //Create an instance of FarmHelper and pass the in memory context as a parameter
            FarmHelper farmHelper = new FarmHelper(InMemoryContext);

            //Delete all farms
            farmHelper.DeleteFarmAll();

            //Check if the database is empty
            Assert.AreEqual(InMemoryContext.Farms.Count(), 0);

            

            //Create an in memory farm
            var farm = new Farm {
                Name="Second Testfarm", 
                AppleTrees = AppleTreeDbInitializer.InitialiseTrees()
            };
            farmHelper.CreateFarm(farm);

            //Check if farm was inserted
            Assert.AreEqual(InMemoryContext.Farms.Count(), 1);

            //Check the inserted farm's properties
            Assert.AreEqual("Second Testfarm", InMemoryContext.Farms.FirstOrDefault().Name);
            Assert.AreEqual(10, InMemoryContext.Farms.FirstOrDefault().AppleTrees.Count);
        }

        [TestMethod]
        public void UpdateFarmTest()
        {
            //Instantiate a new DbContext with the in memory database
            var InMemoryContext = new AppleTreeDbContext(options);

            //If there's no farm, insert one
            if (InMemoryContext.Farms.Count() == 0)
            {
                InMemoryContext.Farms.Add(new Farm { Name = "Another test farm", AppleTrees=AppleTreeDbInitializer.InitialiseTrees() });
                InMemoryContext.SaveChanges();
            }

            //Create an instance of FarmHelper and pass the in memory context as a parameter
            FarmHelper farmHelper = new FarmHelper(InMemoryContext);

            //Get the first in memory farm
            var farm = InMemoryContext.Farms.FirstOrDefault();

            //Change a property
            farm.Name = "Updated another test farm";
            //Update the farm
            farmHelper.UpdateFarm(farm);

            //Assert
            Assert.AreEqual("Updated another test farm", InMemoryContext.Farms.FirstOrDefault().Name);

        }

        [TestMethod]
        public void DeleteFarmAllTest()
        {
            //Instantiate a new DbContext with the in memory database
            var InMemoryContext = new AppleTreeDbContext(options);

            //Create an instance of FarmHelper and pass the in memory context as a parameter
            FarmHelper farmHelper = new FarmHelper(InMemoryContext);

            //If there's no farm, insert one
            if (InMemoryContext.AppleTrees.Count() == 0)
            {
                InMemoryContext.Farms.Add(new Farm { Name = "Lets delete this farm", AppleTrees = AppleTreeDbInitializer.InitialiseTrees() });
                InMemoryContext.SaveChanges();
            }

            //Check if there are farms
            Assert.IsTrue(InMemoryContext.AppleTrees.Count() > 0);

            //Delete them
            farmHelper.DeleteFarmAll();

            //Check for correct deletion
            Assert.AreEqual(InMemoryContext.Farms.Count(), 0);
        }
    }
}
