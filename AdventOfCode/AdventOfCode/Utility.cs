namespace AdventOfCode
{
    internal class Utility
    {
        public static List<string> GetLines(string fileName)
        {
            List<string> lines = new List<string>();

            using (StreamReader streamReader = new StreamReader(fileName))
            {
                string? line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    lines.Add(line);
                }

                streamReader.Close();
            }

            return lines;
        }

        internal class Position
        {
            public int x { get; }
            public int y { get; }

            public Position(int X, int Y)
            {
                x = X;
                y = Y;
            }
        }
    }
}
