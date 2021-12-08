using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Day7Logic
    {
        public static void AnalyzeDay7(string fileName)
        {
            Console.WriteLine("Start of day 7");
            AnalyzeDay7A(fileName);
            Console.WriteLine("End of day 7");
        }

        static void AnalyzeDay7A(string fileName)
        {
            var lines = Utility.GetLines(fileName);
            const int oneSegmentLine = 2;
            const int fourSegmentLine = 4;
            const int sevenSegmentLine = 3;
            const int eightSegmentLine = 7;
            Dictionary<int, int> segmentsFound = new Dictionary<int, int>();
            segmentsFound.Add(oneSegmentLine, 0);
            segmentsFound.Add(fourSegmentLine, 0);
            segmentsFound.Add(sevenSegmentLine, 0);
            segmentsFound.Add(eightSegmentLine, 0);

            foreach(string line in lines)
            {
                var outputValues = line.Split('|');
                var values = outputValues[1].Split(' ');
                foreach (var value in values)
                {
                    int numberOfSegmentLine = value.Length;
                    switch (numberOfSegmentLine)
                    {
                        case oneSegmentLine:
                            segmentsFound[oneSegmentLine] += 1;
                            break;
                        case fourSegmentLine:
                            segmentsFound[fourSegmentLine] += 1;
                            break;
                        case sevenSegmentLine:
                            segmentsFound[sevenSegmentLine] += 1;
                            break;
                        case eightSegmentLine:
                            segmentsFound[eightSegmentLine] += 1;
                            break;
                    }
                }
            }

            int sum = 0;
            foreach (var segment in segmentsFound)
            {
                sum += segment.Value;
            }

            Console.WriteLine(sum);
        }
    }
}
