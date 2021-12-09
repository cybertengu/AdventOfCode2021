namespace AdventOfCode
{
    internal class Day9Logic
    {
        public static void AnalyzeDay9(string fileName)
        {
            Console.WriteLine("Start of day 8");
            var lines = Utility.GetLines(fileName);
            var lowPointPositions = AnalyzeDay8A(lines);
            AnalyzeDay9B(lines, lowPointPositions);
            Console.WriteLine("End of day 8");
        }

        static void AnalyzeDay9B(List<string> lines, Dictionary<Utility.Position, int> lowPointPositions)
        {
            
        }

        static Dictionary<Utility.Position, int> AnalyzeDay8A(List<string> lines)
        {
            Queue<string> queue = new Queue<string>();
            List<int> lowPoints = new List<int>();
            Dictionary<Utility.Position, int> lowPointPositions = new Dictionary<Utility.Position, int>();
            for (int i = 0; i < lines.Count; ++i)
            {
                queue.Enqueue(lines[i]);
                if (queue.Count >= 3)
                {
                    string firstLine = queue.Dequeue();
                    string secondLine = queue.Dequeue();
                    string thirdLine = queue.Dequeue();
                    for (int j = 0; j < firstLine.Length; ++j)
                    {
                        bool isLowestPoint = true;
                        int currentPositionValue = Convert.ToInt32(secondLine[j].ToString());
                        int below = Convert.ToInt32(thirdLine[j].ToString());
                        if (currentPositionValue >= below)
                        {
                            if (currentPositionValue >= below)
                            {
                                isLowestPoint = false;
                                continue;
                            }
                        }
                        if (j + 1 < firstLine.Length)
                        {
                            int right = Convert.ToInt32(secondLine[j + 1].ToString());
                            if (currentPositionValue >= right)
                            {
                                isLowestPoint = false;
                                continue;
                            }
                        }
                        if (j - 1 > -1)
                        {
                            int left = Convert.ToInt32(secondLine[j - 1].ToString());
                            if (currentPositionValue >= left)
                            {
                                isLowestPoint = false;
                                continue;
                            }
                        }

                        int above = Convert.ToInt32(firstLine[j].ToString());
                        if (currentPositionValue >= above)
                        {
                            isLowestPoint = false;
                            continue;
                        }

                        if (isLowestPoint)
                        {
                            lowPoints.Add(currentPositionValue);
                            Utility.Position position = new Utility.Position(i - 1, j);
                            lowPointPositions.Add(position, currentPositionValue);
                        }
                    }
                    queue.Enqueue(secondLine);
                    queue.Enqueue(thirdLine);
                }
                else if (queue.Count == 2)
                {
                    string firstLine = queue.Dequeue();
                    string secondLine = queue.Dequeue();
                    for (int j = 0; j < firstLine.Length; ++j)
                    {
                        bool isLowestPoint = true;
                        int currentPositionValue = Convert.ToInt32(firstLine[j].ToString());
                        int below = Convert.ToInt32(secondLine[j].ToString());
                        if (currentPositionValue >= below)
                        {
                            isLowestPoint = false;
                            continue;
                        }
                        if (j + 1 < firstLine.Length)
                        {
                            int right = Convert.ToInt32(firstLine[j + 1].ToString());
                            if (currentPositionValue >= right)
                            {
                                isLowestPoint = false;
                                continue;
                            }
                        }
                        if (j - 1 > -1)
                        {
                            int left = Convert.ToInt32(firstLine[j - 1].ToString());
                            if (currentPositionValue >= left)
                            {
                                isLowestPoint = false;
                                continue;
                            }
                        }
                        
                        if (isLowestPoint)
                        {
                            lowPoints.Add(currentPositionValue);
                            Utility.Position position = new Utility.Position(i - 1, j);
                            lowPointPositions.Add(position, currentPositionValue);
                        }
                    }
                    queue.Enqueue(firstLine);
                    queue.Enqueue(secondLine);
                }
            }

            if (queue.Count == 2)
            {
                string aboveLine = queue.Dequeue();
                string startingLine = queue.Dequeue();
                for (int j = 0; j < startingLine.Length; ++j)
                {
                    bool isLowestPoint = true;
                    int currentPositionValue = Convert.ToInt32(startingLine[j].ToString());
                    int above = Convert.ToInt32(aboveLine[j].ToString());
                    if (currentPositionValue >= above)
                    {
                        isLowestPoint = false;
                        continue;
                    }
                    if (j + 1 < startingLine.Length)
                    {
                        int right = Convert.ToInt32(startingLine[j + 1].ToString());
                        if (currentPositionValue >= right)
                        {
                            isLowestPoint = false;
                            continue;
                        }
                    }
                    if (j - 1 > -1)
                    {
                        int left = Convert.ToInt32(startingLine[j - 1].ToString());
                        if (currentPositionValue >= left)
                        {
                            isLowestPoint = false;
                            continue;
                        }
                    }

                    if (isLowestPoint)
                    {
                        lowPoints.Add(currentPositionValue);
                        Utility.Position position = new Utility.Position(lines.Count - 1, j);
                        lowPointPositions.Add(position, currentPositionValue);
                    }
                }
                queue.Enqueue(startingLine);
                queue.Enqueue(aboveLine);
            }

            int sum = 0;
            foreach (var value in lowPoints)
            {
                sum += value + 1;
            }
            Console.WriteLine(sum);

            return lowPointPositions;
        }
    }
}
