namespace AdventOfCode
{
    internal class Day10Logic
    {
        public static void AnalyzeDay10(string fileName)
        {
            Console.WriteLine("Start of day 10");
            var incompleteLines = AnalyzeDay10A(fileName);
            AnalyzeDay10B(incompleteLines);
            Console.WriteLine("End of day 10");
        }

        static void AnalyzeDay10B(List<string> incompleteLines)
        {
            const int closingParenthesis = 1;
            const int closingBracket = 2;
            const int closingCurlyBrace = 3;
            const int closingGreaterThanSymbol = 4;
            List <long> totalPoints = new List<long>();
            foreach (var line in incompleteLines)
            {
                Stack<char> syntaxHolder = new Stack<char>();
                foreach (var character in line)
                {
                    bool didRemoveCharacter = false;
                    switch (character)
                    {
                        case ')':
                            syntaxHolder.Pop();
                            didRemoveCharacter = true;
                            break;
                        case ']':
                            syntaxHolder.Pop();
                            didRemoveCharacter = true;
                            break;
                        case '}':
                            syntaxHolder.Pop();
                            didRemoveCharacter = true;
                            break;
                        case '>':
                            syntaxHolder.Pop();
                            didRemoveCharacter = true;
                            break;
                    }
                    if (!didRemoveCharacter)
                    {
                        syntaxHolder.Push(character);
                    }
                }
                long total = 0;
                string legalEnd = string.Empty;
                foreach (var character in syntaxHolder)
                {
                    switch (character)
                    {
                        case '(':
                            total = total * 5 + closingParenthesis;
                            legalEnd += ")";
                            break;
                        case '[':
                            total = total * 5 + closingBracket;
                            legalEnd += "]";
                            break;
                        case '{':
                            total = total * 5 + closingCurlyBrace;
                            legalEnd += "}";
                            break;
                        case '<':
                            total = total * 5 + closingGreaterThanSymbol;
                            legalEnd += ">";
                            break;
                    }
                }
                totalPoints.Add(total);
            }

            totalPoints.Sort();
            int middleIndex = (totalPoints.Count() - 1) / 2;
            var result = totalPoints[middleIndex];
            Console.WriteLine(result);
        }

        static List<string> AnalyzeDay10A(string fileName)
        {
            var lines = Utility.GetLines(fileName);
            long sum = 0;
            const int closingParenthesis = 3;
            const int closingBracket = 57;
            const int closingCurlyBrace = 1197;
            const int closingGreaterThanSymbol = 25137;
            List<string> incompleteLines = new List<string>();
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
                                sum += closingCurlyBrace;
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
                if (!isLineCorrupt)
                {
                    incompleteLines.Add(line);
                }
            }

            Console.WriteLine(sum);

            return incompleteLines;
        }
    }
}
