namespace AdventOfCode
{
    internal class Day3Logic
    {
        const char One = '1';
        const char Zero = '0';

        public static void AnalyzeDay3(string fileName)
        {
            Console.WriteLine("Start of day 3");
            var lines = Utility.GetLines(fileName);
            AnalyzeDay3A(lines);
            AnalyzeDay3B(lines);
            Console.WriteLine("End of day 3");
        }

        static void AnalyzeDay3B(List<string> lines)
        {
            bool isForOxygenRating = true;
            int oxygenGeneratorRating = GetRating(lines, isForOxygenRating);
            bool isForCO2ScurbberRating = false;
            int co2ScrubberRating = GetRating(lines, isForCO2ScurbberRating);
            int result = oxygenGeneratorRating * co2ScrubberRating;
            Console.WriteLine(result);
        }

        static int GetRating(List<string> lines, bool isForOxygenRating)
        {
            int bitIndex = 0;
            while (lines.Count > 1)
            {
                int oneCounter = 0;
                int zeroCounter = 0;

                for (int i = 0; i < lines.Count; ++i)
                {
                    var line = lines[i];
                    if (line[bitIndex] == One)
                    {
                        oneCounter++;
                    }
                    else
                    {
                        zeroCounter++;
                    }
                }

                char commonValue;
                if (isForOxygenRating)
                {
                    commonValue = (oneCounter >= zeroCounter) ? One : Zero;
                }
                else
                {
                    commonValue = (oneCounter < zeroCounter) ? One : Zero;
                }

                var updatedList = new List<string>();
                for (int i = 0; i < lines.Count; ++i)
                {
                    var line = lines[i];
                    if (line[bitIndex] == commonValue)
                    {
                        updatedList.Add(line);
                    }
                }
                lines = updatedList;
                bitIndex++;
            }

            int result = Convert.ToInt32(lines[0], 2);
            return result;
        }

        static void AnalyzeDay3A(List<string> lines)
        {
            List<int> oneCounter = new List<int>();
            foreach (var character in lines[0])
            {
                if (character == '1')
                {
                    oneCounter.Add(1);
                }
                else
                {
                    oneCounter.Add(0);
                }
            }

            for (int i = 1; i < lines.Count; ++i)
            {
                var line = lines[i];
                for (int j = 0; j < line.Length; ++j)
                {
                    var character = line[j];
                    if (character == One)
                    {
                        oneCounter[j] += 1;
                    }
                }
            }

            int numberOfLines = lines.Count;
            int middleAmount = numberOfLines / 2;
            string gammaRate = string.Empty;
            string epsilonRate = string.Empty;

            for (int i = 0; i < oneCounter.Count; ++i)
            {
                if (oneCounter[i] > middleAmount)
                {
                    gammaRate += One;
                    epsilonRate += Zero;
                }
                else
                {
                    gammaRate += Zero;
                    epsilonRate += One;
                }
            }

            var gammaRateNumber = Convert.ToInt32(gammaRate, 2);
            var epsilonRateNumber = Convert.ToInt32(epsilonRate, 2);
            var result = gammaRateNumber * epsilonRateNumber;

            Console.WriteLine(result);
        }
    }
}
