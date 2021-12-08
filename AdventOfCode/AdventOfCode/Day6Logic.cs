namespace AdventOfCode
{
    internal class Day6Logic
    {
        public static void AnalyzeDay6(string fileName)
        {
            Console.WriteLine("Start of day 6");
            int numberOfDays = 80;
            AnalyzeDay6AB(fileName, numberOfDays);
            numberOfDays = 256;
            AnalyzeDay6AB(fileName, numberOfDays);
            Console.WriteLine("End of day 6");
        }

        static void AnalyzeDay6AB(string fileName, int numberOfDays)
        {
            List<string> lanternfishSchool = Utility.GetLines(fileName);
            long[] dayFishCounter = new long[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            foreach (var fish in lanternfishSchool[0].Split(','))
            {
                long dayLeft = Convert.ToInt32(fish);
                dayFishCounter[dayLeft]++;
            }

            for (int i = 0; i < numberOfDays; ++i)
            {
                long newBabiesAmount = dayFishCounter[0];
                for (int j = 0; j < dayFishCounter.Length; ++j)
                {
                    bool isIndexDay6 = (j == 6);
                    bool isIndexDay8 = (j == 8);
                    if (!isIndexDay6 && !isIndexDay8)
                    {
                        dayFishCounter[j] = dayFishCounter[j + 1];
                    }
                    else if (isIndexDay6)
                    {
                        dayFishCounter[j] = dayFishCounter[j + 1] + newBabiesAmount;
                    }
                    else if (isIndexDay8)
                    {
                        dayFishCounter[j] = newBabiesAmount;
                    }
                }
            }

            long sum = 0;
            foreach (var fish in dayFishCounter)
            {
                sum += fish;
            }

            Console.WriteLine(sum);
        }
    }
}
