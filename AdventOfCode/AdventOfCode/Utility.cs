using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Utility
    {
        public static List<string> GetLines(string fileName)
        {
            List<string> lines = new List<string>();

            using (StreamReader streamReader = new StreamReader(fileName))
            {
                string? line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    lines.Add(line);
                }

                streamReader.Close();
            }

            return lines;
        }
    }
}
