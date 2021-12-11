namespace AdventOfCode
{
    internal class Day11Logic
    {
        public static void AnalyzeDay(string fileName)
        {
            Console.WriteLine("Start of day 11");
            AnalyzeDayAB(fileName);
            Console.WriteLine("End of day 11");
        }

        static int WhenAllFlash()
        {
            while (true)
            {
                
            }
        }

        static int CalculateOneDay(int rowCount, int columnCount, ref int[,] octopus, Stack<Utility.Position> positions, out bool didAllFlash)
        {
            int flashesTotal = 0;
            for (int x = 0; x < rowCount; ++x)
            {
                for (int y = 0; y < columnCount; ++y)
                {
                    int value = octopus[x, y] + 1;
                    octopus[x, y] = value;
                    if (value == 10)
                    {
                        Utility.Position newPosition = new Utility.Position(x, y);
                        positions.Push(newPosition);
                    }
                }
            }

            Stack<Utility.Position> alreadyVisited = new Stack<Utility.Position>();
            while (positions.Count > 0)
            {
                flashesTotal++;
                var currentPosition = positions.Pop();
                alreadyVisited.Push(currentPosition);
                int x = currentPosition.x;
                int y = currentPosition.y;
                int aboveX = x - 1;
                int belowX = x + 1;
                int leftY = y - 1;
                int rightY = y + 1;
                int value = 0;
                if (aboveX > -1)
                {
                    if (leftY > -1)
                    {
                        value = octopus[aboveX, leftY] + 1;
                        if (value == 10)
                        {
                            Utility.Position newPosition = new Utility.Position(aboveX, leftY);
                            positions.Push(newPosition);
                        }
                        octopus[aboveX, leftY] = value;
                    }
                    if (rightY < columnCount)
                    {
                        value = octopus[aboveX, rightY] + 1;
                        if (value == 10)
                        {
                            Utility.Position newPosition = new Utility.Position(aboveX, rightY);
                            positions.Push(newPosition);
                        }
                        octopus[aboveX, rightY] = value;
                    }
                    value = octopus[aboveX, y] + 1;
                    if (value == 10)
                    {
                        Utility.Position newPosition = new Utility.Position(aboveX, y);
                        positions.Push(newPosition);
                    }
                    octopus[aboveX, y] = value;
                }
                if (belowX < rowCount)
                {
                    if (leftY > -1)
                    {
                        value = octopus[belowX, leftY] + 1;
                        if (value == 10)
                        {
                            Utility.Position newPosition = new Utility.Position(belowX, leftY);
                            positions.Push(newPosition);
                        }
                        octopus[belowX, leftY] = value;
                    }
                    if (rightY < columnCount)
                    {
                        value = octopus[belowX, rightY] + 1;
                        if (value == 10)
                        {
                            Utility.Position newPosition = new Utility.Position(belowX, rightY);
                            positions.Push(newPosition);
                        }
                        octopus[belowX, rightY] = value;
                    }
                    value = octopus[belowX, y] + 1;
                    if (value == 10)
                    {
                        Utility.Position newPosition = new Utility.Position(belowX, y);
                        positions.Push(newPosition);
                    }
                    octopus[belowX, y] = value;
                }
                if (leftY > -1)
                {
                    value = octopus[x, leftY] + 1;
                    if (value == 10)
                    {
                        Utility.Position newPosition = new Utility.Position(x, leftY);
                        positions.Push(newPosition);
                    }
                    octopus[x, leftY] = value;
                }
                if (rightY < columnCount)
                {
                    value = octopus[x, rightY] + 1;
                    if (value == 10)
                    {
                        Utility.Position newPosition = new Utility.Position(x, rightY);
                        positions.Push(newPosition);
                    }
                    octopus[x, rightY] = value;

                }
            }

            didAllFlash = false;
            if (alreadyVisited.Count == rowCount * columnCount)
            {
                didAllFlash = true;
            }

            foreach (var position in alreadyVisited)
            {
                int x = position.x;
                int y = position.y;
                octopus[x, y] = 0;
            }

            return flashesTotal;
        }

        static void AnalyzeDayAB(string fileName)
        {
            var lines = Utility.GetLines(fileName);
            const int rowCount = 10;
            const int columnCount = 10;
            int[,] octopus = new int[rowCount, columnCount];
            int x = 0;
            foreach (var line in lines)
            {
                int y = 0;
                foreach (var character in line)
                {
                    octopus[x, y] = Convert.ToInt32(character.ToString());
                    y++;
                }
                x++;
            }

            int numberOfDays = 100;
            Stack<Utility.Position> positions = new Stack<Utility.Position>();
            long flashesTotal = 0;
            bool didAllFlash = false;
            for (int i = 0; i < numberOfDays; ++i)
            {
                flashesTotal += CalculateOneDay(rowCount, columnCount, ref octopus, positions, out didAllFlash);
            }

            Console.WriteLine(flashesTotal);

            long counter = numberOfDays;
            while (!didAllFlash)
            {
                CalculateOneDay(rowCount, columnCount, ref octopus, positions, out didAllFlash);
                counter++;
            }

            Console.WriteLine(counter);
        }
    }
}
