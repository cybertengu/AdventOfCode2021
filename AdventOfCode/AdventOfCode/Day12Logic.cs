namespace AdventOfCode
{
    internal class Day12Logic
    {
        public static void AnalyzeDay(string fileName)
        {
            Console.WriteLine("Start of day 12");
            AnalyzeDayAB(fileName);
            Console.WriteLine("End of day 12");
        }

        const string endString = "end"; 
        const string startString = "start";

        static void AnalyzeDayAB(string fileName)
        {
            var lines = Utility.GetLines(fileName);
            Dictionary<string, List<string>> nodes = new Dictionary<string, List<string>>();
            nodes.Add(startString, new List<string>());
            Stack<string> stack = new Stack<string>();
            List<int> indexToRemove = new List<int>();
            const char minusCharacter = '-';
            for (int i = 0; i < lines.Count; ++i)
            {
                var line = lines[i];
                var twoNodes = line.Split(minusCharacter);
                var firstNode = twoNodes[0];
                var secondNode = twoNodes[1];
                if (firstNode.Equals(startString))
                {
                    nodes[startString].Add(secondNode);
                    stack.Push(secondNode);
                    indexToRemove.Add(i);
                }
                else if (secondNode.Equals(startString))
                {
                    nodes[startString].Add(firstNode);
                    stack.Push(firstNode);
                    indexToRemove.Add(i);
                }
            }

            for (int i = indexToRemove.Count - 1; i > -1; --i)
            {
                lines.RemoveAt(indexToRemove[i]);
            }

            Dictionary<string, bool> alreadyVisitedNodes = new Dictionary<string, bool>();
            alreadyVisitedNodes.Add(startString, false);
            while (stack.Count > 0)
            {
                var value = stack.Pop();
                if (nodes.ContainsKey(value) || value.Equals(endString))
                {
                    continue;
                }
                nodes.Add(value, new List<string>());
                alreadyVisitedNodes.Add(value, false);
                for (int i = 0; i < lines.Count; ++i)
                {
                    var line = lines[i];
                    var twoNodes = line.Split(minusCharacter);
                    var firstNode = twoNodes[0];
                    var secondNode = twoNodes[1];
                    if (firstNode.Equals(value))
                    {
                        nodes[value].Add(secondNode);
                        stack.Push(secondNode);
                    }
                    else if (secondNode.Equals(value))
                    {
                        nodes[value].Add(firstNode);
                        stack.Push(firstNode);
                    }
                }
            }

            Day12Logic day12Logic = new Day12Logic();
            var allPaths = day12Logic.ProceedToGetAllValidPaths(nodes);
            long pathNumbers = allPaths.Count;
            Console.WriteLine(pathNumbers);

            Day12Logic day12LogicForPart2 = new Day12Logic();
            bool doEnterSmallCaveTwiceOnce = true;
            var allPathsForPart2 = day12LogicForPart2.ProceedToGetAllValidPaths(nodes, doEnterSmallCaveTwiceOnce);
            long pathNumbersPart2 = allPathsForPart2.Count;
            Console.WriteLine(pathNumbersPart2);
        }

        List<string> ProceedToGetAllValidPaths(Dictionary<string, List<string>> nodes, bool doEnterSmallCaveTwiceOnce = false)
        {
            var validPaths = new List<string>();
            var path = string.Empty;
            var alreadyVisitedNodes = new Dictionary<string, bool>();
            foreach (var node in nodes)
            {
                alreadyVisitedNodes.Add(node.Key, false);
            }

            if (doEnterSmallCaveTwiceOnce)
            {
                bool didEnterSmallCaveTwice = false;
                return GetAllValidPath(validPaths, nodes, alreadyVisitedNodes, startString, path, didEnterSmallCaveTwice);
            }

            // Not ideal, but this approach will make the code work for both part 1 and 2, without
            // duplicating code.
            bool doNotEnterSmallCaveTwice = true;
            return GetAllValidPath(validPaths, nodes, alreadyVisitedNodes, startString, path, doNotEnterSmallCaveTwice);
        }

        List<string> GetAllValidPath(List<string> validPaths,
                                            Dictionary<string, List<string>> nodes,
                                            Dictionary<string, bool> alreadyVisited,
                                            string value, string path, bool didVisitSecondSmallCaveTwice = true)
        {
            if (value.Equals(endString))
            {
                path += endString;
                validPaths.Add(path);
                return validPaths;
            }
            else if (char.IsLower(value[0]) && alreadyVisited[value])
            {
                // Since this small cave was already visited and if it was visited twice,
                // don't proceed and return back what valid paths already found.

                // TODO: if the code is too slow, it might be due to redoing dead-end paths.
                // Note: The code is still relatively fast enough to get my results for part 2.
                // It is quick enough to handle day 2.
                if (!didVisitSecondSmallCaveTwice)
                {
                    didVisitSecondSmallCaveTwice = true;
                }
                else
                {
                    return validPaths;
                }
            }

            alreadyVisited[value] = true;
            var options = nodes[value];
            path += $"{value},";
            foreach (var option in options)
            {
                var alreadyVisitedClone = new Dictionary<string, bool>(alreadyVisited);
                validPaths = GetAllValidPath(validPaths, nodes, alreadyVisited, option, path, didVisitSecondSmallCaveTwice);
                alreadyVisited = alreadyVisitedClone;
            }

            return validPaths;
        }
    }
}
