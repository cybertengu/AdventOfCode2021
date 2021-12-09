namespace AdventOfCode
{
    internal class Day7Logic
    {
        public static void AnalyzeDay7(string fileName)
        {
            Console.WriteLine("Start of day 7");
            int[] crabPositions;
            int smallestHorizontalPosition;
            int largestHorizontalPosition;
            GatherData(fileName, out crabPositions, out smallestHorizontalPosition, out largestHorizontalPosition);
            AnalyzeDay7A(crabPositions, smallestHorizontalPosition, largestHorizontalPosition);
            AnalyzeDay7B(crabPositions, smallestHorizontalPosition, largestHorizontalPosition);
            Console.WriteLine("End of day 7");
        }

        static void GatherData(string fileName, out int[] crabPositions, out int smallestHorizontalPosition, out int largestHorizontalPosition)
        {
            var lines = Utility.GetLines(fileName);
            string crabsPositionInString = lines[0];
            var positions = crabsPositionInString.Split(',');
            smallestHorizontalPosition = int.MaxValue;
            largestHorizontalPosition = int.MinValue;
            crabPositions = new int[positions.Length];
            for (int i = 0; i < positions.Length; ++i)
            {
                int value = Convert.ToInt32(positions[i]);
                if (value < smallestHorizontalPosition)
                {
                    smallestHorizontalPosition = value;
                }
                if (value > largestHorizontalPosition)
                {
                    largestHorizontalPosition = value;
                }
                crabPositions[i] = value;
            }
        }

        static void AnalyzeDay7A(int[] crabsPosition, int smallestValue, int largestValue)
        {
            int leastAmountOfFuel = int.MaxValue;
            int positionWithLeastFuel = -1;
            for (int i = smallestValue; i < largestValue + 1; ++i)
            {
                int sum = 0;
                bool isLessFuel = true;
                for (int j = 0; j < crabsPosition.Length; ++j)
                {
                    var fuel = crabsPosition[j] - i;
                    if (fuel < 0)
                    {
                        fuel *= -1;
                    }
                    sum += fuel;
                    if (sum >= leastAmountOfFuel)
                    {
                        isLessFuel = false;
                        break;
                    }
                }
                
                if (isLessFuel)
                {
                    if (sum < leastAmountOfFuel)
                    {
                        positionWithLeastFuel = i;
                        leastAmountOfFuel = sum;
                    }
                }
            }

            Console.WriteLine($"Position with least amount of fuel: {positionWithLeastFuel}");
            Console.WriteLine(leastAmountOfFuel);
        }

        static void AnalyzeDay7B(int[] crabsPosition, int smallestValue, int largestValue)
        {
            int leastAmountOfFuel = int.MaxValue;
            int positionWithLeastFuel = -1;
            Dictionary<int, int> distanceMetBefore = new Dictionary<int, int>();
            for (int i = smallestValue; i < largestValue + 1; ++i)
            {
                int sum = 0;
                bool isLessFuel = true;
                for (int j = 0; j < crabsPosition.Length; ++j)
                {
                    var distance = crabsPosition[j] - i;
                    if (distance < 0)
                    {
                        distance *= -1;
                    }

                    int fuel = 0;
                    if (distanceMetBefore.ContainsKey(distance))
                    {
                        fuel = distanceMetBefore[distance];
                    }
                    else
                    {
                        for (int k = 1; k < distance + 1; ++k)
                        {
                            fuel += k;
                        }
                        distanceMetBefore.Add(distance, fuel);
                    }
                    sum += fuel;
                    if (sum >= leastAmountOfFuel)
                    {
                        isLessFuel = false;
                        break;
                    }
                }

                if (isLessFuel)
                {
                    if (sum < leastAmountOfFuel)
                    {
                        positionWithLeastFuel = i;
                        leastAmountOfFuel = sum;
                    }
                }
            }

            Console.WriteLine($"Position with least amount of fuel: {positionWithLeastFuel}");
            Console.WriteLine(leastAmountOfFuel);
        }
    }
}
