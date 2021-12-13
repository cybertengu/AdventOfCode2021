namespace AdventOfCode
{
    internal class Day13Logic
    {
        const char Comma = ',';
        const char Equal = '=';
        const char HashTag = '#';
        const char Period = '.';
        const char PipeCharacter = '|';

        public static void AnalyzeDay(string fileName)
        {
            Console.WriteLine("Start of day 13");
            Day13Logic day13Logic = new Day13Logic();
            day13Logic.AnalyzeDayA(fileName);
            day13Logic.AnalyzeDayB(fileName);
            Console.WriteLine("End of day 13");
        }

        void InitializeDay(string fileName, out List<string> inputLines, out int[,] grid, out int maxX, out int maxY)
        {
            List<string> gridLines;
            GetLines(fileName, out gridLines, out inputLines);
            grid = GetStartingGrid(gridLines, inputLines, out maxX, out maxY);
        }

        int[,] GetStartingGrid(List<string> gridLines, List<string> inputLines, out int maxX, out int maxY)
        {
            // Yes, the problem had Y as rows and X as columns. It's confusing, I agree.
            List<Utility.Position> positions = new List<Utility.Position>();
            string[] tokens;
            int biggestX = -1;
            int biggestY = -1;
            foreach (var line in gridLines)
            {
                tokens = line.Split(Comma);
                var x = Convert.ToInt32(tokens[0]);
                if (x > biggestX)
                {
                    biggestX = x;
                }
                var y = Convert.ToInt32(tokens[1]);
                if (y > biggestY)
                {
                    biggestY = y;
                }
                Utility.Position newPosition = new Utility.Position(y, x);
                positions.Add(newPosition);
            }

            // Using maxX and maxY as I normally use it instead of following the problem, which is
            // why you will see biggestY used for maxX and biggestX used for maxyY.
            maxX = biggestY + 1;
            maxY = biggestX + 1;
            int[,] grid = new int[maxX, maxY];
            foreach (var position in positions)
            {
                int x = position.x;
                int y = position.y;
                grid[x, y] = 1;
            }

            return grid;
        }

        void GetLines(string fileName, out List<string> gridLines, out List<string> inputLines)
        {
            gridLines = new List<string>();
            inputLines = new List<string>();
            using (StreamReader streamReader = new StreamReader(fileName))
            {
                string? line;
                bool isAtFoldLine = false;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        isAtFoldLine = true;
                        continue;
                    }
                    else if (isAtFoldLine)
                    {
                        inputLines.Add(line);
                    }
                    else
                    {
                        gridLines.Add(line);
                    }
                }

                streamReader.Close();
            }
        }

        void AnalyzeDayB(string fileName)
        {
            int[,] grid;
            int maxX, maxY;
            List<string> foldLines;
            InitializeDay(fileName, out foldLines, out grid, out maxX, out maxY);
            int lowestX = 0;
            int lowestY = 0;

            foreach (var fold in foldLines)
            {
                var tokens = fold.Split(' ');
                string foldDetails = tokens[tokens.Length - 1];
                string[] foldTokens = foldDetails.Split(Equal);
                string axis = foldTokens[0];
                int value = Convert.ToInt32(foldTokens[1]);

                if (axis.Equals("y"))
                {
                    int xOffset = value + 1;
                    lowestX = xOffset;
                    for (int x = value - 1; x > -1; --x, ++xOffset)
                    {
                        if (xOffset >= maxX)
                        {
                            break;
                        }
                        for (int y = 0; y < maxY; ++y)
                        {
                            int currentGridValue = grid[x, y];
                            int newGridValue = grid[xOffset, y];
                            if (currentGridValue == 0)
                            {
                                grid[x, y] = grid[xOffset, y];
                            }
                        }
                    }
                }
                else
                {
                    lowestY = value + 1;
                    for (int x = 0; x < maxX; ++x)
                    {
                        int yOffset = lowestY;
                        for (int y = value - 1; y > -1; --y, ++yOffset)
                        {
                            if (yOffset >= maxY)
                            {
                                break;
                            }
                            int currentGridValue = grid[x, y];
                            int newGridValue = grid[x, yOffset];
                            if (currentGridValue == 0)
                            {
                                grid[x, y] = grid[x, yOffset];
                            }
                        }
                    }
                }
            }

            for (int x = 0; x < lowestX; ++x)
            {
                for (int y = 0; y < lowestY; ++y)
                {
                    if (grid[x, y] == 1)
                    {
                        Console.Write(HashTag);
                    }
                    else
                    {
                        Console.Write(Period);
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("What should I do?");
        }

        void AnalyzeDayA(string fileName)
        {
            int[,] grid;
            int maxX, maxY;
            List<string> foldLines;
            InitializeDay(fileName, out foldLines, out grid, out maxX, out maxY);

            string firstFold = foldLines[0];
            var tokens = firstFold.Split(' ');
            string fold = tokens[tokens.Length - 1];
            string[] foldTokens = fold.Split(Equal);
            string axis = foldTokens[0];
            int value = Convert.ToInt32(foldTokens[1]);

            //DebugPrintOutGridWithFold(maxX, maxY, grid, value);
            
            long numberOfDots = 0;
            if (axis.Equals("y"))
            {
                int xOffset = value + 1;
                for (int x = value - 1; x > -1; --x, ++xOffset)
                {
                    if (xOffset >= maxX)
                    {
                        break;
                    }
                    for (int y = 0; y < maxY; ++y)
                    {
                        int currentGridValue = grid[x, y];
                        int newGridValue = grid[xOffset, y];
                        if (currentGridValue == 0)
                        {
                            grid[x, y] = grid[xOffset, y];
                        }
                        if (currentGridValue == 1 || newGridValue == 1)
                        {
                            ++numberOfDots;
                        }
                    }
                }
            }
            else
            {
                for (int x = 0; x < maxX; ++x)
                {
                    int yOffset = value + 1;
                    for (int y = value - 1; y > -1; --y, ++yOffset)
                    {
                        if (yOffset >= maxY)
                        {
                            break;
                        }
                        int currentGridValue = grid[x, y];
                        int newGridValue = grid[x, yOffset];
                        if (currentGridValue == 0)
                        {
                            grid[x, y] = grid[x, yOffset];
                        }
                        if (currentGridValue == 1 || newGridValue == 1)
                        {
                            ++numberOfDots;
                        }
                    }
                }
            }

            //DebugPrintOutGrid(maxX, maxY, grid);

            Console.WriteLine(numberOfDots);
        }

        void DebugPrintOutGrid(int maxX, int maxY, int[,] grid)
        {
            for (int x = 0; x < maxX; ++x)
            {
                for (int y = 0; y < maxY; ++y)
                {
                    if (grid[x, y] == 1)
                    {
                        Console.Write(HashTag);
                    }
                    else
                    {
                        Console.Write(Period);
                    }
                }
                Console.WriteLine();
            }
        }

        void DebugPrintOutGridWithFold(int maxX, int maxY, int[,] grid, int foldLine)
        {
            for (int x = 0; x < maxX; ++x)
            {
                for (int y = 0; y < maxY; ++y)
                {
                    if (grid[x, y] == 1)
                    {
                        Console.Write(HashTag);
                    }
                    else if (y == foldLine)
                    {
                        Console.Write(PipeCharacter);
                    }
                    else
                    {
                        Console.Write(Period);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
