using System.Drawing;

namespace AdventOfCode
{
    internal class Day18Logic
    {
        public static void AnalyzeDay(string fileName)
        {
            Console.WriteLine("Start of day 18");
            TestVariousData();
            //AnalyzeDayA(fileName);
            Console.WriteLine("End of day 18");
        }

        static void AnalyzeDayA(string fileName)
        {
            var lines = Utility.GetLines(fileName);
            bool isFirstNode = true;
            ShellFish topNode = new ShellFish();
            foreach (var line in lines)
            {
                foreach(var character in line)
                {
                    ShellFish shellFish = new ShellFish();
                    if (character.Equals('['))
                    {
                        if (isFirstNode)
                        {
                            isFirstNode = false;
                            //topNode.Value = $"{character}";
                        }
                    }
                }
            }
            long magniture = -1;
            Console.WriteLine(magniture);
        }

        const char Space = ' ';

        class ShellFish
        {
            public char Value = Space;
            public ShellFish? Left = null;
            public ShellFish? Right = null;
            public bool IsNumber = false;
            public int Number = -1;
            public bool IsPairDone = false;

            public void InsertNode(ref ShellFish firstNode, ShellFish shellFish)
            {
                if (firstNode.Value.Equals(Space))
                {
                    firstNode = shellFish;
                    return;
                }

                ShellFish? currentFish = firstNode;
                if (currentFish?.Left == null)
                {
                    currentFish.Left = shellFish;
                }
                else if (currentFish.Left.IsNumber)
                {
                    currentFish.Right = shellFish;
                }    
            }
        }

        const char LeftSquareBracket = '[';
        const char Comma = ',';
        const char RightSquareBracket = ']';

        static void TestVariousData()
        {
            TestData("[1,2]");
            TestData("[[1,2],3]");
            /*
             [[1,2],3]
[9,[8,7]]
[[1,9],[8,5]]
[[[[1,2],[3,4]],[[5,6],[7,8]]],9]
[[[9,[3,8]],[[0,9],6]],[[[3,7],[4,9]],3]]
[[[[1,3],[5,3]],[[1,3],[8,7]]],[[[4,9],[6,9]],[[8,2],[7,3]]]]
             */
        }

        static void TestData(string line)
        {
            ShellFish topNode = new ShellFish();
            int counter = -1;
            foreach (char c in line)
            {
                ShellFish newShellFish = new ShellFish();
                if (c.Equals(LeftSquareBracket))
                {
                    newShellFish.Value = LeftSquareBracket;
                    counter++;
                    topNode.InsertNode(ref topNode, newShellFish, counter);
                }
                else if (c.Equals(Comma))
                {
                    continue;
                }
                else if (c.Equals(RightSquareBracket))
                {
                    counter--;
                    continue;
                }
                else
                {
                    int value = Convert.ToInt32(c.ToString());
                    newShellFish.IsNumber = true;
                    newShellFish.Number = value;
                    topNode.InsertNode(ref topNode, newShellFish);
                }
            }
            Console.WriteLine($"Testing data: {line}");
        }
    }
}
