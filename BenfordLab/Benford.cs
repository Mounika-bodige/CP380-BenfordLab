using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace BenfordLab
{
    public class BenfordData
    {
        public int Digit { get; set; }
        public int Count { get; set; }

        public BenfordData()
        { }
    }

    public class Benford
    {
        public static IEnumerable<BenfordData> D2 { get; private set; }

        public static BenfordData[] CalculateBenford(string csvFilePath)
        {
            // load the data
            var data = File.ReadAllLines(csvFilePath)
                .Skip(1) // For header
                .Select(s => Regex.Match(s, @"^(.?),(.?)$"))
                .Select(data => new
                {
                    Country = data.Groups[1].Value,
                    Population = int.Parse(data.Groups[2].Value)
                    
                });
            int arrlen=0;
            int val = 0;
            foreach (var len in data)
            {
                arrlen++;
            }

            int[] arrdata = new int[arrlen];
            
            foreach (var len in data)
            {
                arrdata[val]=FirstDigit.getFirstDigit(len.Population);
                val++;    
            }
            List<BenfordData> Data = new List<BenfordData>();
            for (int i = 1; i < 10; i++)
            {
                int incr = 0;
                for(int j=0;j<arrdata.Length;j++)
                {
                    if( i ==arrdata[j])
                    {
                        incr++;
                    }
                }
                Data.Add(new BenfordData
                {
                    Digit = i,
                    Count = incr
                });
                Data.Concat(Data);
            }
            var listdata = Data;

            return listdata.ToArray();
        }
    }
}
