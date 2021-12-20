using System.Text;

namespace AdventOfCode
{
    internal class Day20Logic
    {
        public static void AnalyzeDay(string fileName)
        {
            Console.WriteLine("Start of day 20");
            AnalyzeDayA(fileName);
            Console.WriteLine("End of day 20");
        }

        static void AnalyzeDayA(string fileName)
        {
            List<string> lines = new List<string>();
            using (StreamReader streamReader = new StreamReader(fileName))
            {
                string? line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    lines.Add(line);
                }

                streamReader.Close();
            }

            int lastIndex = lines.Count - 1;
            if (string.IsNullOrWhiteSpace(lines[lastIndex]))
            {
                lines.RemoveAt(lastIndex);
            }

            var imageEnhancementAlgorithm = lines[0];
            Dictionary<string, char> alreadySeen = new Dictionary<string, char>();
            int maxX = lines.Count - 2; 
            int maxY = lines[2].Length;
            var inputImage = new char[maxX, maxY];
            var outputImage = new char[maxX, maxY];

            for (int i = 2; i < lines.Count; ++i)
            {
                var line = lines[i];
                for (int j = 0; j < line.Length; ++j)
                {
                    char currentCharacter = line[j];
                    outputImage[i - 2, j] = currentCharacter;
                    inputImage[i - 2, j] = currentCharacter;
                }
            }

            char infiniteChar = imageEnhancementAlgorithm[0];

            for (int x = 0; x < maxX; ++x)
            {
                for (int y = 0; y < maxY; ++y)
                {
                    StringBuilder build6x6 = new StringBuilder();
                    char middle = inputImage[x, y];
                    int aboveX = x - 1;
                    int belowX = x + 1;
                    int leftY = y - 1;
                    int rightY = y + 1;
                    char topLeft = '.';
                    char topMiddle = '.';
                    char topRight = '.';
                    char left = '.';
                    char right = '.';
                    char bottomLeft = '.';
                    char bottomMiddle = '.';
                    char bottomRight = '.';
                    if (aboveX < 0 || leftY < 0)
                    {
                        topLeft = infiniteChar;
                    }
                    else
                    {
                        topLeft = inputImage[aboveX, leftY];
                    }
                    if (aboveX < 0)
                    {
                        topMiddle = infiniteChar;
                    }
                    else
                    {
                        topMiddle = inputImage[aboveX, y];
                    }
                    if (leftY < 0)
                    {
                        left = infiniteChar;
                    }
                    else
                    {
                        left = inputImage[x, leftY];
                    }
                    if (belowX >= maxX)
                    {
                        bottomMiddle = infiniteChar;
                    }
                    else
                    {
                        bottomMiddle = inputImage[belowX, y];
                    }
                    if (leftY < 0 || belowX >= maxX)
                    {
                        bottomLeft = infiniteChar;
                    }
                    else
                    {
                        bottomLeft = inputImage[belowX, leftY];
                    }
                    if (rightY >= maxY || aboveX < 0)
                    {
                        topRight = infiniteChar;
                    }
                    else
                    {
                        topRight = inputImage[aboveX, rightY];
                    }
                    if (rightY >= maxY)
                    {
                        right = infiniteChar;
                    }
                    else
                    {
                        right = inputImage[x, rightY];
                    }
                    if (rightY >= maxY || belowX >= maxX)
                    {
                        bottomRight = infiniteChar;
                    }
                    else
                    {
                        bottomRight = inputImage[belowX, rightY];
                    }

                    build6x6.Append($"{topLeft}{topMiddle}{topRight}{left}{middle}{right}{bottomLeft}{bottomMiddle}{bottomRight}");
                    string smallGrid = build6x6.ToString();
                    char resultingLight;
                    if (alreadySeen.ContainsKey(smallGrid))
                    {
                        resultingLight = alreadySeen[smallGrid];
                    }
                    else
                    {
                        StringBuilder buildBinary = new StringBuilder();
                        foreach (var character in smallGrid)
                        {
                            if (character.Equals('#'))
                            {
                                buildBinary.Append('1');
                            }
                            else
                            {
                                buildBinary.Append('0');
                            }
                        }
                        string binaryString = buildBinary.ToString();
                        int decimalValue = Convert.ToInt32(binaryString, 2);
                        resultingLight = imageEnhancementAlgorithm[decimalValue];
                        alreadySeen.Add(smallGrid, resultingLight);
                    }
                    outputImage[x, y] = resultingLight;
                }
            }

            DebugPrintGrid(inputImage, maxX, maxY);
            Console.WriteLine();
            DebugPrintGrid(outputImage, maxX, maxY);

            long numberOfLight = 0;
            Console.WriteLine(numberOfLight);
        }

        static void DebugPrintGrid(char[,] grid, int maxX, int maxY)
        {
            for (int x = 0; x < maxX; ++x)
            {
                for (int y = 0; y < maxY; ++y)
                {
                    Console.Write(grid[x, y]);
                }
                Console.WriteLine();
            }
        }
    }
}
