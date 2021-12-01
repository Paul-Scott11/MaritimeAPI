using MaritimeAPI.Controllers;
using MaritimeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaritimeTest
{
    [TestClass]
    public class UnitTest1
    {
        private MaritimeTechTestContext _context;
        private RndNumController API;

        [TestMethod()]
        public void T01Get_GetMean_ReturnsSuccessMessage()
        {
            //Arrange
            _context = new MaritimeTechTestContext(new DbContextOptionsBuilder<MaritimeTechTestContext>().UseInMemoryDatabase(databaseName: "TestMaritimeInMemoryDatabase").Options);
            API = new RndNumController(_context);

            List<TblRandomNumber> NumsToAdd = new List<TblRandomNumber>();
            string[] ListOfNums = { "1.357055324", "91.61421247", "15.85347463", "78.44536547", "20.15793072", "67.87355683", "39.12873487", "83.8166761", "8.509336082", "94.33034055" };
            //Add the values from the string array to the TblRandomNumber object so it can be used directly with LINQ
            for (int i = 0; i < ListOfNums.Length; i++)
            {
                NumsToAdd.Add(new TblRandomNumber(Convert.ToDecimal(ListOfNums[i])));
            }
            //Add new data to table
            _context.TblRandomNumbers.AddRange(NumsToAdd);
            _context.SaveChanges();

            //Act
            var result = API.GetMean() as OkObjectResult;
            _context.Dispose();

            //Assert
            Assert.AreEqual(50.1086683046, result.Value);
        }

        [TestMethod()]
        public void T02Get_GetStnDev_ReturnsSuccessMessage()
        {
            //Arrange
            _context = new MaritimeTechTestContext(new DbContextOptionsBuilder<MaritimeTechTestContext>().UseInMemoryDatabase(databaseName: "TestMaritimeInMemoryDatabase").Options);
            API = new RndNumController(_context);

            List<TblRandomNumber> NumsToAdd = new List<TblRandomNumber>();
            string[] ListOfNums = { "1.357055324", "91.61421247", "15.85347463", "78.44536547", "20.15793072", "67.87355683", "39.12873487", "83.8166761", "8.509336082", "94.33034055" };
            //Add the values from the string array to the TblRandomNumber object so it can be used directly with LINQ
            for (int i = 0; i < ListOfNums.Length; i++)
            {
                NumsToAdd.Add(new TblRandomNumber(Convert.ToDecimal(ListOfNums[i])));
            }
            //Add new data to table
            _context.TblRandomNumbers.AddRange(NumsToAdd);
            _context.SaveChanges();


            //Act
            var result = API.GetStnDev() as OkObjectResult;
            _context.Dispose();

            //Assert
            Assert.AreEqual(35.88182955508907, result.Value);
        }

        [TestMethod()]
        public void T03Get_GetFreqOfNums_ReturnsSuccessMessage()
        {
            //Arrange
            _context = new MaritimeTechTestContext(new DbContextOptionsBuilder<MaritimeTechTestContext>().UseInMemoryDatabase(databaseName: "TestMaritimeInMemoryDatabase").Options);
            API = new RndNumController(_context);

            List<TblRandomNumber> NumsToAdd = new List<TblRandomNumber>();
            string[] ListOfNums = { "1.357055324", "91.61421247", "15.85347463", "78.44536547", "20.15793072", "67.87355683", "39.12873487", "83.8166761", "48.509336082", "54.33034055" };
            //Add the values from the string array to the TblRandomNumber object so it can be used directly with LINQ
            for (int i = 0; i < ListOfNums.Length; i++)
            {
                NumsToAdd.Add(new TblRandomNumber(Convert.ToDecimal(ListOfNums[i])));
            }
            //Add new data to table
            _context.TblRandomNumbers.AddRange(NumsToAdd);
            _context.SaveChanges();

            //Act
            var result = API.GetFromDB().ToList();

            _context.Dispose();

            //Assert
            Assert.AreEqual(10, result.Count);
        }

        [TestMethod()]
        public void T04CalculateMean_ReturnsSuccessMessage()
        {
            //Arrange          
            List<double> ListOfNums = new() { 1.357055324, 91.61421247, 15.85347463, 78.44536547, 20.15793072, 67.87355683, 39.12873487, 83.8166761, 48.509336082, 54.33034055 };

            //Act
            double average = MaritimeAPI.Calculations.CalculateMean(ListOfNums);

            //Assert
            Assert.AreEqual(50.1086683046, average);
        }

        [TestMethod()]
        public void T05CalculateStandardDeviation_ReturnsSuccessMessage()
        {
            //Arrange          
            List<double> ListOfNums = new() { 1.357055324, 91.61421247, 15.85347463, 78.44536547, 20.15793072, 67.87355683, 39.12873487, 83.8166761, 48.509336082, 54.33034055 };

            //Act
            var standardDeviation = MaritimeAPI.Calculations.CalculateStandardDeviation(ListOfNums, 50.1086683046);

            //Assert
            Assert.AreEqual(30.850218293220841, standardDeviation);
        }


    }
}
