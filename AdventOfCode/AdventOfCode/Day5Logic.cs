namespace AdventOfCode
{
    internal class Day5Logic
    {
        public static void AnalyzeDay5(string fileName)
        {
            Console.WriteLine("Start of day 5");
            AnalyzeDay5A(fileName);
            AnalyzeDay5B(fileName);
            Console.WriteLine("End of day 5");
        }

        static void AnalyzeDay5A(string fileName)
        {
            bool dontIncludeDiagonalLines = false;
            SolveDay5(fileName, dontIncludeDiagonalLines);
        }

        static void AnalyzeDay5B(string fileName)
        {
            bool includeDiagonalLines = true;
            SolveDay5(fileName, includeDiagonalLines);
        }

        static void SolveDay5(string fileName, bool includeDiagonalLines)
        {
            var lines = Utility.GetLines(fileName);
            Dictionary<string, int> clouds = new Dictionary<string, int>();
            foreach (string line in lines)
            {
                var positions = line.Split("->");
                var xy1Values = positions[0].Split(',');
                var x1 = Convert.ToInt32(xy1Values[0]);
                var y1 = Convert.ToInt32(xy1Values[1]);
                var xy2Values = positions[1].Split(',');
                var x2 = Convert.ToInt32(xy2Values[0]);
                var y2 = Convert.ToInt32(xy2Values[1]);

                if (x1 == x2)
                {
                    bool isY1GreaterThanY2 = y1 > y2;
                    int largerValue = (isY1GreaterThanY2) ? y1 : y2;
                    int smallerValue = (isY1GreaterThanY2) ? y2 : y1;

                    for (int i = smallerValue; i < largerValue + 1; ++i)
                    {
                        string position = $"{x1},{i}";

                        if (clouds.ContainsKey(position))
                        {
                            clouds[position]++;
                        }
                        else
                        {
                            clouds.Add(position, 1);
                        }
                    }
                }
                else if (y1 == y2)
                {
                    bool isX1GreaterThanX2 = x1 > x2;
                    int largerValue = isX1GreaterThanX2 ? x1 : x2;
                    int smallerValue = isX1GreaterThanX2 ? x2 : x1;

                    for (int i = smallerValue; i < largerValue + 1; ++i)
                    {
                        string position = $"{i},{y1}";

                        if (clouds.ContainsKey(position))
                        {
                            clouds[position]++;
                        }
                        else
                        {
                            clouds.Add(position, 1);
                        }
                    }
                }
                else if (includeDiagonalLines)
                {
                    Position firstPosition = new Position(x1, y1);
                    Position secondPosition = new Position(x2, y2);
                    GetDiagonalLines(clouds, firstPosition, secondPosition);
                }
            }

            int counter = 0;
            foreach (var match in clouds)
            {
                if (match.Value > 1)
                {
                    counter++;
                }
            }

            Console.WriteLine(counter);
        }

        internal class Position
        {
            public int x { get; }
            public int y { get; }

            public Position(int X, int Y)
            {
                x = X;
                y = Y;
            }
        }

        static Dictionary<string, int> GetDiagonalLines(Dictionary<string, int> cloudLocations, Position firstPosition, Position secondPosition)
        {
            int x1 = firstPosition.x;
            int y1 = firstPosition.y;
            int x2 = secondPosition.x;
            int y2 = secondPosition.y;
            int xAmount = x1 - x2;
            int yAmount = y1 - y2;
            int xOffset = (xAmount > 0) ? -1 : 1;
            int yOffset = (yAmount > 0) ? -1 : 1;

            if (Math.Abs(xAmount) != Math.Abs(yAmount))
            {
                Console.WriteLine($"I was not expecting this situation. {x1} {y1} : {x2} {y2}");
            }

            for (int amount = 0; amount < Math.Abs(xAmount) + 1; ++amount)
            {
                int updatedX = x1 + xOffset * amount;
                int updatedY = y1 + yOffset * amount;
                string position = $"{updatedX},{updatedY}";

                if (cloudLocations.ContainsKey(position))
                {
                    cloudLocations[position]++;
                }
                else
                {
                    cloudLocations.Add(position, 1);
                }
            }

            return cloudLocations;
        }
    }
}
