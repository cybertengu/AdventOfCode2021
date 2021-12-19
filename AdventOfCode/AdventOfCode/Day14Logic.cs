using System.Text;

namespace AdventOfCode
{
    internal class Day14Logic
    {
        public static void AnalyzeDay(string fileName)
        {
            Console.WriteLine("Start of day 14");
            int stepsAmountForPartA = 10;
            AnalyzeDayAB(fileName, stepsAmountForPartA);
            int stepsAmountForPartB = 40;
            AnalyzeDayAB(fileName, stepsAmountForPartB);
            Console.WriteLine("End of day 14");
        }

        static void AnalyzeDayAB(string fileName, int stepsAmount)
        {
            var lines = Utility.GetLines(fileName);
            var startingSequence = lines[0];
            lines.RemoveAt(0);
            Dictionary<char, long> letterCounters = new Dictionary<char, long>();
            Dictionary<string, List<string>> pairInsertions = new Dictionary<string, List<string>>();
            Dictionary<string, long> pairInsertionCounters = new Dictionary<string, long>();

            foreach (var line in lines)
            {
                var tokens = line.Split(' ');
                var firstPairToken = tokens[0];
                var lastToken = tokens[tokens.Length - 1];
                var firstPair = $"{firstPairToken[0]}{lastToken}";
                var secondPair = $"{lastToken}{firstPairToken[1]}";
                var pairs = new List<string>() { firstPair, secondPair };
                pairInsertions.Add(firstPairToken, pairs);

                if (!pairInsertionCounters.ContainsKey(firstPairToken))
                {
                    pairInsertionCounters.Add(firstPairToken, 0);
                }

                var lastTokenChar = lastToken[0];
                if (!letterCounters.ContainsKey(lastTokenChar))
                {
                    letterCounters.Add(lastTokenChar, 0);
                }
            }

            for (int i = 0; i < startingSequence.Length - 1; ++i)
            {
                var pair = startingSequence.Substring(i, 2);
                pairInsertionCounters[pair]++;
                letterCounters[pair[0]]++;

                if (i == startingSequence.Length - 2)
                {
                    letterCounters[pair[1]]++;
                }
            }

            for (int i = 0; i < stepsAmount; ++i)
            {
                Dictionary<string, long> testingPairs = new Dictionary<string, long>();
                Stack<string> oldPairs = new Stack<string>();
                foreach (var pair in pairInsertionCounters)
                {
                    long value = pair.Value;
                    if (value > 0)
                    {
                        var key = pair.Key;
                        var pairs = pairInsertions[key];
                        var firstPair = pairs[0];
                        var secondPair = pairs[1];
                        letterCounters[secondPair[0]] += value;
                        oldPairs.Push(key);

                        if (testingPairs.ContainsKey(firstPair))
                        {
                            testingPairs[firstPair] += value;
                        }
                        else
                        {
                            testingPairs.Add(firstPair, value);
                        }

                        if (testingPairs.ContainsKey(secondPair))
                        {
                            testingPairs[secondPair] += value;
                        }
                        else
                        {
                            testingPairs.Add(secondPair, value);
                        }
                    }
                }

                // Reset all values back to zero for old pairs. I did this
                // outside the foreach loop for pairInsertionCounters because
                // dictionary are not ordered.
                foreach (var key in oldPairs)
                {
                    pairInsertionCounters[key] = 0;
                }

                // I was having issues updating the values for a given pair,
                // so I looping through the stack to add the values.
                foreach (var pair in testingPairs)
                {
                    var key = pair.Key;
                    var value = pair.Value;
                    pairInsertionCounters[key] += value;
                }
            }

            long smallestCounter = long.MaxValue;
            long largestCounter = long.MinValue;
            foreach (var counter in letterCounters.Values)
            {
                if (counter < smallestCounter)
                {
                    smallestCounter = counter;
                }
                if (counter > largestCounter)
                {
                    largestCounter = counter;
                }
            }

            long difference = largestCounter - smallestCounter;
            Console.WriteLine(difference);
        }

        static void DebugPrintKeyPair(Dictionary<char, long> pairInsertionCounters)
        {

            // For debugging:
            foreach (var pair in pairInsertionCounters)
            {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }
            Console.WriteLine();
        }

        static void DebugPrintKeyPair(Dictionary<string, long> pairInsertionCounters)
        {

            // For debugging:
            foreach (var pair in pairInsertionCounters)
            {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }
            Console.WriteLine();
        }
    }
}
