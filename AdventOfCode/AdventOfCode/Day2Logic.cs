namespace AdventOfCode
{
    internal class Day2Logic
    {
        public static void AnalyzeDay2(string fileName)
        {
            Console.WriteLine("Start of day 2");
            List<string> lines = Utility.GetLines(fileName);

            AnalyzeDay2A(lines);
            AnalyzeDay2B(lines);
            Console.WriteLine("End of day 2");
        }

        static void AnalyzeDay2A(List<string> lines)
        {
            int x = 0;
            int y = 0;

            foreach (string line in lines)
            {
                GetCommandAndAmount(line, out var command, out var amount);

                switch (command)
                {
                    case "forward":
                    { 
                        x += int.Parse(amount);
                        break;
                    }
                    case "up":
                    {
                        y -= int.Parse(amount);
                        break;
                    }
                    case "down":
                    { 
                        y += int.Parse(amount);
                        break;
                    }
                }
            }

            var result = x * y;
            Console.WriteLine(result);
        }

        static void AnalyzeDay2B(List<string> lines)
        {
            int horizontalPosition = 0;
            int depth = 0;
            int aim = 0;

            foreach (var line in lines)
            {
                GetCommandAndAmount(line, out var command, out var amount);

                switch (command)
                {
                    case "forward":
                    {
                        var forwardAmount = int.Parse(amount);
                        horizontalPosition += forwardAmount;

                        if (aim != 0)
                        {
                            depth = depth + forwardAmount * aim;
                        }
                        break;
                    }
                    case "up":
                    {
                        aim -= int.Parse(amount);
                        break;
                    }
                    case "down":
                    {
                        aim += int.Parse(amount);
                        break;
                    }
                }
            }

            var result = horizontalPosition * depth;
            Console.WriteLine(result);
        }

        static void GetCommandAndAmount(string line, out string command, out string amount)
        {

            var tokens = line.Split(' ');
            command = tokens[0];
            amount = tokens[1];
        }
    }
}
