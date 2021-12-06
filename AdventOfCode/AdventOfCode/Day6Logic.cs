﻿namespace AdventOfCode
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

        // Old approach. Too slow to solve part B.
        static void AnalyzeDay6A(string fileName, int numberOfDays)
        {
            List<string> lanternfishSchool = Utility.GetLines(fileName);
            string[] priorDaySequence = new string[7];
            priorDaySequence[0] = lanternfishSchool[0];

            for (int day = 0; day < numberOfDays; ++day)
            {
                string[] fishes = lanternfishSchool[day].Split(',');
                string nextDayFish = string.Empty;
                string appendNewFish = string.Empty;

                for (int index = 0; index < fishes.Length; ++index)
                {
                    string fish = fishes[index];
                    int number = Convert.ToInt32(fish);
                    number--;

                    if (number < 0)
                    {
                        number = 6;
                        appendNewFish += ",8";
                    }

                    if (index < fishes.Length - 1)
                    {
                        nextDayFish += $"{number},";
                    }
                    else
                    {
                        nextDayFish += $"{number}";
                    }
                }

                nextDayFish += appendNewFish;
                lanternfishSchool.Add(nextDayFish);
                //Console.WriteLine(nextDayFish);
            }

            string fishOnLastDay = lanternfishSchool[numberOfDays];
            string[] fishAmounts = fishOnLastDay.Split(',');
            Console.WriteLine(fishAmounts.Length);
        }

        static void AnalyzeDay6AB(string fileName, int numberOfDays)
        {
            List<string> lanternfishSchool = Utility.GetLines(fileName);

            long[] dayCounter = new long[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            foreach (var fish in lanternfishSchool[0].Split(','))
            {
                long dayLeft = Convert.ToInt32(fish);
                dayCounter[dayLeft]++;
            }
            
            for (int i = 0; i < numberOfDays; ++i)
            {
                long newBabiesAmount = dayCounter[0];
                dayCounter[0] = dayCounter[1];
                dayCounter[1] = dayCounter[2];
                dayCounter[2] = dayCounter[3];
                dayCounter[3] = dayCounter[4];
                dayCounter[4] = dayCounter[5];
                dayCounter[5] = dayCounter[6];
                dayCounter[6] = dayCounter[7] + newBabiesAmount;
                dayCounter[7] = dayCounter[8];
                dayCounter[8] = newBabiesAmount;
            }

            long sum = 0;
            foreach (var fish in dayCounter)
            {
                sum += fish;
            }

            Console.WriteLine(sum);
        }
    }
}
