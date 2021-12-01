using MaritimeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MaritimeAPI.Controllers
{
    [ApiController]
    public class RndNumController : ControllerBase
    {
        private readonly MaritimeTechTestContext _context;

        public RndNumController(MaritimeTechTestContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("api/PostFileData")]
        public IActionResult PostFileData()
        {
            try
            {
                string[] ListOfNums = null;
                var file = Request.Form.Files[0];
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine("UploadedFiles", fileName);

                    //Process the file and return a list of random numbers
                    ListOfNums = FileProcessing.ProccessFileData(fileName, fullPath, dbPath, file);

                    //Pass the list of numbers to save to DB
                    FileProcessing.SaveDataToDB(_context, ListOfNums);

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ("Internal server error {0}", ex));
            }


        }

        [HttpGet]
        [Route("api/GetMean")]
        public IActionResult GetMean()
        {
            //Get the list of numbers from the database
            List<double> listOfNums = _context.TblRandomNumbers.Select(o => (double)o.RndNumber).ToList();

            if (listOfNums.Count < 1)
                return Ok();

            //Calculate the arithmetic mean
            double average = Calculations.CalculateMean(listOfNums);

            return Ok(average);
        }

        [HttpGet]
        [Route("api/GetStnDev")]
        public IActionResult GetStnDev()
        {
            //Get the list of numbers from the database
            List<double> listOfNums = _context.TblRandomNumbers.Select(o => (double)o.RndNumber).ToList();

            if (listOfNums.Count < 1)
                return Ok();

            //Calculate the arithmetic mean to use with standard deviation
            double average = Calculations.CalculateMean(listOfNums);

            //Calculate the standard deviation 
            double StandardDeviation = Calculations.CalculateStandardDeviation(listOfNums, average);

            return Ok(StandardDeviation);
        }


        [HttpGet]
        [Route("api/GetFreqOfNums")]
        public IEnumerable<IGrouping<int, double>> GetFromDB()
        {
            //Get the list of numbers from the database
            List<double> listOfNums = _context.TblRandomNumbers.Select(o => (double)o.RndNumber).ToList();

            int[] ranges = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };

            IEnumerable<IGrouping<int, double>> query = from rndNums in listOfNums group rndNums by ranges.Where(x => rndNums >= x).DefaultIfEmpty().Last();

            return query.OrderBy(x => x.Key);
        }

    }
}
