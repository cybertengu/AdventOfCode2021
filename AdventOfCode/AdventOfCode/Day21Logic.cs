using System.Text;

namespace AdventOfCode
{
    internal class Day21Logic
    {
        public static void AnalyzeDay(string fileName)
        {
            Console.WriteLine("Start of day 21");
            AnalyzeDayA(fileName);
            Console.WriteLine("End of day 21");
        }

        static void AnalyzeDayA(string fileName)
        {
            var lines = Utility.GetLines(fileName);
            var tokens = lines[0].Split(' ');
            int lastIndex = tokens.Length - 1;
            int player1Position = Convert.ToInt32(tokens[lastIndex]);
            tokens = lines[1].Split(' ');
            lastIndex = tokens.Length - 1;
            int player2Position = Convert.ToInt32(tokens[lastIndex]);
            int dice = 0;
            int[] rolls = new int[3] { dice++, dice++, dice++ };
            int forwardCounter = rolls[0] + rolls[1] + rolls[2];
            int newLocation = (player1Position + forwardCounter);
            if ((int)(newLocation / 11) > 1)
            {

            }
        }
    }
}
