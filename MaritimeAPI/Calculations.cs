using System;
using System.Collections.Generic;
using System.Linq;

namespace MaritimeAPI
{
    public class Calculations
    {
        public static double CalculateMean(List<double> list)
        {
            // double average = list.Sum(x => Convert.ToDouble(x)) / list.Count();

            double sum = 0;

            foreach (double x in list)
            {
                sum += x;
            }

            return (sum / list.Count());
        }

        public static double CalculateStandardDeviation(IEnumerable<double> values, double average)
        {
            double standardDeviation = 0;

            // Perform the Sum of (value-avg)_2_2.      
            double sum = values.Sum(d => Math.Pow(d - average, 2));

            // Put it all together.      
            standardDeviation = Math.Sqrt((sum) / (values.Count() - 1));

            return standardDeviation;
        }

    }
}
