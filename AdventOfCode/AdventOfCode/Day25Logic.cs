using System.Drawing;

namespace AdventOfCode
{
    internal class Day25Logic
    {
        public static void AnalyzeDay(string fileName)
        {
            Console.WriteLine("Start of day 25");
            AnalyzeDayA(fileName);
            Console.WriteLine("End of day 25");
        }

        const char EAST = '>';
        const char SOUTH = 'v';

        static void AnalyzeDayA(string fileName)
        {
            var lines = Utility.GetLines(fileName);
            var eastPositions = new List<Point>();
            var southPositions = new List<Point>();
            int rowCount = lines.Count;
            int columnCount = lines[0].Length;
            for (int x = 0; x < rowCount; ++x)
            {
                var line = lines[x];
                for (int y = 0; y < columnCount; ++y)
                {
                    if (line[y].Equals(EAST))
                    {
                        eastPositions.Add(new Point(x, y));
                    }
                    else if (line[y].Equals(SOUTH))
                    {
                        southPositions.Add(new Point(x, y));
                    }
                }
            }

            int step = 0;
            //PrintOutPositions(step, rowCount, columnCount, eastPositions, southPositions);
            step++;

            bool didSomethingMove = false;
            var eastSeaCucumberMoves = new Dictionary<Point, Point>();
            var southSeaCucumberMoves = new Dictionary<Point, Point>();
            var currentPosition = new Point();
            int index = 0;
            int newX = 0;
            int newY = 0;
            do
            {
                didSomethingMove = false;
                eastSeaCucumberMoves.Clear();
                southSeaCucumberMoves.Clear();
                foreach (var eastSeaCucumber in eastPositions)
                {
                    currentPosition = eastSeaCucumber;
                    newY = currentPosition.Y + 1;
                    if (newY >= columnCount)
                    {
                        newY = 0;
                    }
                    var newPosition = new Point(currentPosition.X, newY);
                    var didSomethingExistHere = eastPositions.Contains(newPosition) || southPositions.Contains(newPosition);
                    if (!didSomethingExistHere)
                    {
                        eastSeaCucumberMoves.Add(currentPosition, newPosition);
                        didSomethingMove = true;
                    }
                }

                foreach (var eastSeaCucumber in eastSeaCucumberMoves)
                {
                    index = eastPositions.IndexOf(eastSeaCucumber.Key);
                    eastPositions.RemoveAt(index);
                    eastPositions.Add(eastSeaCucumber.Value);
                }

                foreach (var southSeaCucumber in southPositions)
                {
                    currentPosition = southSeaCucumber;
                    newX = currentPosition.X + 1;
                    if (newX >= rowCount)
                    {
                        newX = 0;
                    }
                    var newPosition = new Point(newX, currentPosition.Y);
                    var didSomethingExistHere = eastPositions.Contains(newPosition) || southPositions.Contains(newPosition);
                    if (!didSomethingExistHere)
                    {
                        southSeaCucumberMoves.Add(currentPosition, newPosition);
                        didSomethingMove = true;
                    }
                }

                foreach (var southSeaCucumber in southSeaCucumberMoves)
                {
                    index = southPositions.IndexOf(southSeaCucumber.Key);
                    southPositions.RemoveAt(index);
                    southPositions.Add(southSeaCucumber.Value);
                }

                //PrintOutPositions(step, rowCount, columnCount, eastPositions, southPositions);
                step++;
            } while (didSomethingMove);
            step--;
            Console.WriteLine(step);
        }

        static void DebugPrintOutPositions(int step, int rowCount, int columnCount, List<Point> eastPositions, List<Point> southPositions)
        {
            Console.WriteLine($"After {step} steps:");
            for (int x = 0; x < rowCount; ++x)
            {
                for (int y = 0; y < columnCount; ++y)
                {
                    var newPosition = new Point(x, y);
                    if (eastPositions.Contains(newPosition))
                    {
                        Console.Write(EAST);
                    }
                    else if (southPositions.Contains(newPosition))
                    {
                        Console.Write(SOUTH);
                    }
                    else
                    {
                        Console.Write('.');
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
