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

        static bool wasPositionVisited(List<Utility.Position> positions, Utility.Position positionToCompare)
        {
            foreach (Utility.Position position in positions)
            {
                if (position.x == positionToCompare.x && position.y == positionToCompare.y)
                {
                    return true;
                }
            }
            return false;
        }

        static void AnalyzeDay9B(List<string> lines, Dictionary<Utility.Position, int> lowPointPositions)
        {
            Stack<Utility.Position> positionStack = new Stack<Utility.Position>();
            int oneLineLength = lines[0].Length;
            List<int> basinSize = new List<int>();
            foreach (var position in lowPointPositions)
            {
                List<Utility.Position> alreadyVisited = new List<Utility.Position>() { position.Key };
                positionStack.Push(position.Key);
                int counter = 1;
                while (positionStack.Count > 0)
                {
                    Utility.Position currentPosition = positionStack.Pop();
                    int x = currentPosition.x;
                    int y = currentPosition.y;
                    int leftIndex = y - 1;
                    if (leftIndex > -1)
                    {
                        int left = Convert.ToInt32(lines[x][leftIndex].ToString());
                        if (left != 9)
                        {
                            Utility.Position newPosition = new Utility.Position(x, leftIndex);
                            if (!wasPositionVisited(alreadyVisited, newPosition))
                            {
                                positionStack.Push(new Utility.Position(x, leftIndex));
                                counter++;
                                alreadyVisited.Add(newPosition);
                            }
                        }
                    }
                    int downIndex = x + 1;
                    if (downIndex < lines.Count)
                    {
                        int down = Convert.ToInt32(lines[downIndex][y].ToString()); 
                        if (down != 9)
                        {
                            Utility.Position newPosition = new Utility.Position(downIndex, y);
                            if (!wasPositionVisited(alreadyVisited, newPosition))
                            {
                                positionStack.Push(new Utility.Position(downIndex, y));
                                counter++;
                                alreadyVisited.Add(newPosition);
                            }
                        }
                    }
                    int rightIndex = y + 1;
                    if (rightIndex < oneLineLength)
                    {
                        int right = Convert.ToInt32(lines[x][rightIndex].ToString());
                        if (right != 9)
                        {
                            Utility.Position newPosition = new Utility.Position(x, rightIndex);
                            if (!wasPositionVisited(alreadyVisited, newPosition))
                            {
                                positionStack.Push(new Utility.Position(x, rightIndex));
                                counter++;
                                alreadyVisited.Add(newPosition);
                            }
                        }
                    }
                    int upIndex = x - 1;
                    if (upIndex > -1)
                    {
                        int up = Convert.ToInt32(lines[upIndex][y].ToString());
                        if (up != 9)
                        {
                            Utility.Position newPosition = new Utility.Position(upIndex, y);
                            if (!wasPositionVisited(alreadyVisited, newPosition))
                            {
                                positionStack.Push(new Utility.Position(upIndex, y));
                                counter++;
                                alreadyVisited.Add(newPosition);
                            }
                        }
                    }
                }

                basinSize.Add(counter);
            }

            int firstLargestValue = -1;
            int secondLargestValue = -1;
            int thirdLargetValue = -1;
            foreach (var size in basinSize)
            {
                if (size > firstLargestValue)
                {
                    // This order of assignment is important.
                    thirdLargetValue = secondLargestValue;
                    secondLargestValue = firstLargestValue;
                    firstLargestValue = size;
                }
                else if (size > secondLargestValue)
                {
                    thirdLargetValue = secondLargestValue;
                    secondLargestValue = size;
                }
                else if (size > thirdLargetValue)
                {
                    thirdLargetValue = size;
                }
            }

            long product = (long)firstLargestValue * (long)secondLargestValue * (long)thirdLargetValue;
            Console.WriteLine(product);
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
