namespace AdventOfCode
{
    internal class Day13Logic
    {
        public static void AnalyzeDay(string fileName)
        {
            Console.WriteLine("Start of day 13");
            Day13Logic day13Logic = new Day13Logic();
            day13Logic.AnalyzeDayA(fileName);
            Console.WriteLine("End of day 13");
        }

        void AnalyzeDayA(string fileName)
        {
            List<string> lines = new List<string>();
            List<string> foldLines = new List<string>();

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
                        foldLines.Add(line);
                    }
                    else
                    {
                        lines.Add(line);
                    }
                }

                streamReader.Close();
            }

            List<Utility.Position> inputs = new List<Utility.Position>();
            foreach (string input in foldLines)
            {
                var tokens = input.Split(' ');
                var coordinates = tokens[2].Split('=');
                int x = Convert.ToInt32(coordinates[0]);
                int y = Convert.ToInt32(coordinates[1]);
                Utility.Position position = new Utility.Position(x, y);
                inputs.Add(position);
            }

            Dictionary<int, List<int>> positions = new Dictionary<int, List<int>>();
            foreach (var line in lines)
            {
                var tokens = line.Split(',');
                var x = Convert.ToInt32(tokens[0]);
                var y = Convert.ToInt32(tokens[1]);

                if (positions.ContainsKey(x))
                {
                    positions[x].Add(y);
                }
                else
                {
                    positions.Add(x, new List<int> { y });
                }
            }

            long numberOfDots = 0;
            Console.WriteLine(numberOfDots);
        }
    }
}
