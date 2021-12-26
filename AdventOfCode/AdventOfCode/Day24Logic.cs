namespace AdventOfCode
{
    internal class Day24Logic
    {
        public static void AnalyzeDay(string fileName)
        {
            Console.WriteLine("Start of day 24");
            var lines = Utility.GetLines(fileName);
            int[] largestInputs = new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 };
            AnalyzeDayA2(lines, largestInputs);
            int[] smallestInputs = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            AnalyzeDayA2(lines, smallestInputs);
            Console.WriteLine("End of day 24");
        }

        const char w = 'w';
        const char x = 'x';
        const char y = 'y';
        const char z = 'z';
        const char space = ' ';

        class Info
        {
            public int Index;
            public int Value;

            public Info(int index, int value)
            {
                Index = index;
                Value = value;
            }
        }

        static void AnalyzeDayA2(List<string> lines, int[] modelNumber)
        {
            Stack<Info> numbers = new Stack<Info>();
            for (int i = 0; i < 14; ++i)
            {
                var tokens = lines[i * 18 + 4].Split(space);
                int divideBy = Convert.ToInt32(tokens[2]);
                tokens = lines[i * 18 + 5].Split(space);
                int check = Convert.ToInt32(tokens[2]);
                tokens = lines[i * 18 + 15].Split(space);
                int addBy = Convert.ToInt32(tokens[2]);
                if (divideBy == 1)
                {
                    Info info = new Info(i, addBy);
                    numbers.Push(info);
                }
                else if (divideBy == 26)
                {
                    Info info = numbers.Pop();
                    int j = info.Index;
                    modelNumber[i] = modelNumber[j] + info.Value + check;
                    if (modelNumber[i] > 9)
                    {
                        modelNumber[j] -= modelNumber[i] - 9;
                        modelNumber[i] = 9;
                    }
                    else if (modelNumber[i] < 1)
                    {
                        modelNumber[j] += 1 - modelNumber[i];
                        modelNumber[i] = 1;
                    }
                }
            }
            foreach (int input in modelNumber)
            {
                Console.Write(input);
            }
            Console.WriteLine();
        }
    }
}
