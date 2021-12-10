namespace AdventOfCode
{
    internal class Day10Logic
    {
        public static void AnalyzeDay10(string fileName)
        {
            Console.WriteLine("Start of day 10");
            AnalyzeDay10A(fileName);
            Console.WriteLine("End of day 10");
        }

        static void AnalyzeDay10A(string fileName)
        {
            var lines = Utility.GetLines(fileName);
            long sum = 0;
            const int closingParenthesis = 3;
            const int closingBracket = 57;
            const int closingCurlyBracket = 1197;
            const int closingGreaterThanSymbol = 25137;
            List<string> forDebugging = new List<string>();
            foreach (var line in lines)
            {
                Stack<char> syntaxHolder = new Stack<char>();
                bool isLineCorrupt = false;
                foreach (var character in line)
                {
                    bool wasClosingCharacterValid = false;
                    switch (character)
                    {
                        case ')':
                            char top = syntaxHolder.Pop();
                            if (top != '(')
                            {
                                forDebugging.Add(line);
                                sum += closingParenthesis;
                                isLineCorrupt = true;
                            }
                            else
                            {
                                wasClosingCharacterValid = true;
                            }
                            break;
                        case ']':
                            top = syntaxHolder.Pop();
                            if (top != '[')
                            {
                                forDebugging.Add(line);
                                sum += closingBracket;
                                isLineCorrupt = true;
                            }
                            else
                            {
                                wasClosingCharacterValid = true;
                            }
                            break;
                        case '}':
                            top = syntaxHolder.Pop();
                            if (top != '{')
                            {
                                forDebugging.Add(line);
                                sum += closingCurlyBracket;
                                isLineCorrupt = true;
                            }
                            else
                            {
                                wasClosingCharacterValid = true;
                            }
                            break;
                        case '>':
                            top = syntaxHolder.Pop();
                            if (top != '<')
                            {
                                forDebugging.Add(line);
                                sum += closingGreaterThanSymbol;
                                isLineCorrupt = true;
                            }
                            else
                            {
                                wasClosingCharacterValid = true;
                            }
                            break;
                    }

                    if (isLineCorrupt)
                    {
                        break;
                    }
                    else if (!wasClosingCharacterValid)
                    {
                        syntaxHolder.Push(character);
                    }
                }
            }

            Console.WriteLine(sum);
        }
    }
}
