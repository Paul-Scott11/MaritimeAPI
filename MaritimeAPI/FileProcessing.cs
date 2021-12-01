using MaritimeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;

namespace MaritimeAPI
{
    public class FileProcessing
    {
        public static string[] ProccessFileData(string fileName, string fullPath, string dbPath, IFormFile file)
        {

            string[] ListOfNums = null;

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            using (var reader = new StreamReader(fullPath))
            {
                // List<decimal> listA = new List<decimal>();                      
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    ListOfNums = line.Split(',');
                }
            }


            return (ListOfNums);
        }

        public static void SaveDataToDB(MaritimeTechTestContext _context, string[] ListOfNums)
        {
            List<TblRandomNumber> NumsToAdd = new List<TblRandomNumber>();

            //Add the values from the string array to the TblRandomNumber object so it can be used directly with LINQ
            for (int i = 0; i < ListOfNums.Length; i++)
            {
                NumsToAdd.Add(new TblRandomNumber(Convert.ToDecimal(ListOfNums[i])));
            }

            //Delete all records before adding new ones
            _context.Database.ExecuteSqlRaw("Truncate table tblRandomNumbers");

            //Add new data to table
            _context.TblRandomNumbers.AddRange(NumsToAdd);
            _context.SaveChanges();

        }


    }
}
