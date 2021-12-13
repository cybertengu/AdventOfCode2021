namespace AdventOfCode
{
    internal class Day13Logic
    {
        public static void AnalyzeDay(string fileName)
        {
            Console.WriteLine("Start of day 13");
            AnalyzeDayA(fileName);
            Console.WriteLine("End of day 13");
        }

        static void AnalyzeDayA(string fileName)
        {
            List<string> lines = new List<string>();
            List<string> foldLines = new List<string>();
            List<Utility.Position> positions = new List<Utility.Position>();

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


        }
    }
}
