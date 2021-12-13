using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Day8Logic
    {
        const char PipeCharacter = '|';
        const char Space = ' ';

        public static void AnalyzeDay8(string fileName)
        {
            Console.WriteLine("Start of day 8");
            AnalyzeDay8A(fileName);
            AnalyzeDay8B(fileName);
            Console.WriteLine("End of day 8");
        }

        static void AnalyzeDay8B(string fileName)
        {
            var lines = Utility.GetLines(fileName);
            long sum = 0;
            foreach (var line in lines)
            { 
                string twoLength = string.Empty, threeLength = string.Empty, fourLength = string.Empty, sevenLength = string.Empty;
                string[] sixLength = new string[3];
                string[] fiveLength = new string[3];
                var patterns = line.Split(PipeCharacter);
                var signalPatterns = patterns[0].Split(Space);
                int fiveLengthCounter = 0;
                int sixLengthCounter = 0;
                foreach (string pattern in signalPatterns)
                {
                    int length = pattern.Length;
                    switch (length)
                    {
                        case 2:
                            twoLength = pattern;
                            break;
                        case 3:
                            threeLength = pattern;
                            break;
                        case 4:
                            fourLength = pattern;
                            break;
                        case 5:
                            fiveLength[fiveLengthCounter++] = pattern;
                            break;
                        case 6:
                            sixLength[sixLengthCounter++] = pattern;
                            break;
                        case 7:
                            sevenLength = pattern;
                            break;
                    }
                }
                /*
                 *   -  0
                 *  | | 1 2
                 *   _  3
                 *  | | 4 5
                 *   -  6
                 */
                /*
                 * Look for two in its length. That'll be number 1. Store position 2 and 5 with those two characters.
                 * Look for three in its length. That'll be number 7. Look at result for number 1 and remaining character is at position 0.
                 * Look for four in its length. That'll be number 4. Look at result for 1 and remove those two characters. Remaining two characters are for positions 1 and 3.
                 * Look for seven in its length. That'll be number 8. Look at 4 in length and see what characters are not a match. Those characters are for positions 4 and 6.
                 */
                string[] keys = new string[7];
                keys[2] = twoLength;
                keys[5] = twoLength;
                foreach (char c in threeLength)
                {
                    if (!twoLength.Contains(c))
                    {
                        keys[0] = $"{c}";
                        break;
                    }
                }
                string remainingCharacters = string.Empty;
                string position2String = keys[2];
                foreach (char c in fourLength)
                {
                    if (!position2String.Contains(c))
                    {
                        remainingCharacters += $"{c}";
                    }
                }
                keys[1] = remainingCharacters;
                keys[3] = remainingCharacters;
                string alreadyFoundCharacters = $"{keys[0]}{fourLength}";
                remainingCharacters = string.Empty;
                foreach (char c in sevenLength)
                {
                    if (!alreadyFoundCharacters.Contains(c))
                    {
                        remainingCharacters += $"{c}";
                    }
                }
                keys[4] = remainingCharacters;
                keys[6] = remainingCharacters;

                /*
                * Grab three strings with five in its length(A, B, C). Grab any two strings(A, B). Compare them to see what 
                * characters don't match (should be two characters). Compare two strings (A, C) to see two characters don't 
                * match. Compare two strings(B, C) to see two characters don't match.
                * Compare the three results to see which character shows up twice. Compare to string in position 1. Whichever 
                * character matches, then that character is in position 1 and position 3 remove the character in position 1.
                * Look at position 4 to see the string.Look through the three pairs and see which has a character matching. 
                * That matching character is put into position 4 and position 6 is updated to remove that character.
                */
                string compareAandB = string.Empty;
                string compareAandC = string.Empty;
                string A = fiveLength[0];
                string B = fiveLength[1];
                string C = fiveLength[2];
                foreach (char c in A)
                {
                    if (!B.Contains(c))
                    {
                        compareAandB += $"{c}";
                    }
                    if (!C.Contains(c))
                    {
                        compareAandC += $"{c}";
                    }
                }
                string compareBandC = string.Empty;
                foreach (char c in B)
                {
                    if (!C.Contains(c))
                    {
                        compareBandC += $"{c}";
                    }
                }

                // Compare the three results to see which character shows up twice. Compare to string in position 1.
                // Whichever character matches, then that character is in position 1 and position 3 remove the
                // character in position 1.
                string uniqueLetter = string.Empty;
                foreach (char c in A)
                {
                    if (!B.Contains(c) && !C.Contains(c))
                    {
                        uniqueLetter += $"{c}";
                    }
                }
                foreach (char c in B)
                {
                    if (!A.Contains(c) && !C.Contains(c))
                    {
                        uniqueLetter += $"{c}";
                    }
                }
                foreach (char c in C)
                {
                    if (!A.Contains(c) && !B.Contains(c))
                    {
                        uniqueLetter += $"{c}";
                    }
                }
                foreach (char c in keys[1])
                {
                    if (uniqueLetter.Contains(c))
                    {
                        keys[1] = $"{c}";
                        break;
                    }
                }
                foreach (char c in keys[3])
                {
                    if (!uniqueLetter.Contains(c))
                    {
                        keys[3] = $"{c}";
                        break;
                    }
                }

                // Look at position 4 to see the string. Look through the three pairs and
                // see which has a character matching. That matching character is put into
                // position 4 and position 6 is updated to remove that character.
                foreach (char c in keys[4])
                {
                    if (uniqueLetter.Contains(c))
                    {
                        keys[4] = $"{c}";
                        break;
                    }
                }
                string removeThisChar = keys[4];
                foreach (char c in keys[6])
                {
                    if (!removeThisChar.Contains(c))
                    {
                        keys[6] = $"{c}";
                        break;
                    }
                }

                /*
                 * Grab three strings with length 6 and any one of these without a character in position 3, then remove that 
                 * string from next step.
                 * Two remaining strings are compared to see which characters don't match (two characters should be found). 
                 */
                string thirdPositionPattern = keys[3];
                string[] twoPatterns = new string[2];
                int counter = 0;
                foreach (string pattern in sixLength)
                {
                    if (pattern.Contains(thirdPositionPattern))
                    {
                        twoPatterns[counter++] = pattern;
                    }
                }
                string secondPattern = twoPatterns[1];
                string unmatchedCharacter = string.Empty;
                foreach (char c in twoPatterns[0])
                {
                    if (!secondPattern.Contains(c))
                    {
                        unmatchedCharacter += $"{c}";
                    }
                }
                string firstPattern = twoPatterns[0];
                foreach (char c in twoPatterns[1])
                {
                    if (!firstPattern.Contains(c))
                    {
                        unmatchedCharacter += $"{c}";
                    }
                }

                /*
                 * The one string having a character to match at position 4 means position 5 should have a character matching 
                 * be there. Remove this character from position 2.
                 * Now you are left with something (keys) that can help get you any value out of the combination in the output.
                 */
                foreach (char c in keys[5])
                {
                    if (unmatchedCharacter.Contains(c))
                    {
                        keys[2] = $"{c}";
                        break;
                    }
                }
                var atPosition5 = keys[2];
                foreach (char c in keys[5])
                {
                    if (!atPosition5.Contains(c))
                    {
                        keys[5] = $"{c}";
                        break;
                    }
                }
                Dictionary<char, Position> GetPositionByKey = new Dictionary<char, Position>();
                for (int i = 0; i < 7; ++i)
                {
                    switch (i)
                    {
                        case 0:
                            GetPositionByKey.Add(keys[i][0], Position.Top);
                            break;
                        case 1:
                            GetPositionByKey.Add(keys[i][0], Position.TopLeft);
                            break;
                        case 2:
                            GetPositionByKey.Add(keys[i][0], Position.TopRight);
                            break;
                        case 3:
                            GetPositionByKey.Add(keys[i][0], Position.Middle);
                            break;
                        case 4:
                            GetPositionByKey.Add(keys[i][0], Position.BottomLeft);
                            break;
                        case 5:
                            GetPositionByKey.Add(keys[i][0], Position.BottomRight);
                            break;
                        case 6:
                            GetPositionByKey.Add(keys[i][0], Position.Bottom);
                            break;
                    }
                }

                // Decode the output with the keys I have created.
                /*
                   0:      1:      2:      3:      4:
                     aaaa    ....    aaaa    aaaa    ....
                    b    c  .    c  .    c  .    c  b    c
                    b    c  .    c  .    c  .    c  b    c
                     ....    ....    dddd    dddd    dddd
                    e    f  .    f  e    .  .    f  .    f
                    e    f  .    f  e    .  .    f  .    f
                     gggg    ....    gggg    gggg    ....

                      5:      6:      7:      8:      9:
                     aaaa    aaaa    aaaa    aaaa    aaaa
                    b    .  b    .  .    c  b    c  b    c
                    b    .  b    .  .    c  b    c  b    c
                     dddd    dddd    ....    dddd    dddd
                    .    f  e    f  .    f  e    f  .    f
                    .    f  e    f  .    f  e    f  .    f
                     gggg    gggg    ....    gggg    gggg
                 */
                var output = patterns[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                const Position zero = Position.Top | Position.TopRight | Position.TopLeft | Position.BottomRight | Position.BottomLeft | Position.Bottom;
                const Position one = Position.TopRight | Position.BottomRight;
                const Position two = Position.Top | Position.TopRight | Position.Middle | Position.BottomLeft | Position.Bottom;
                const Position three = Position.Top | Position.TopRight | Position.Middle | Position.BottomRight | Position.Bottom;
                const Position four = Position.TopLeft | Position.TopRight | Position.Middle | Position.BottomRight;
                const Position five = Position.Top | Position.TopLeft | Position.Middle | Position.BottomRight | Position.Bottom;
                const Position six = Position.Top | Position.Middle | Position.TopLeft | Position.BottomRight | Position.BottomLeft | Position.Bottom;
                const Position seven = Position.Top | Position.TopRight | Position.BottomRight;
                const Position eight = Position.Top | Position.TopRight | Position.TopLeft | Position.Middle | Position.BottomRight | Position.BottomLeft | Position.Bottom;
                const Position nine = Position.Top | Position.Middle | Position.TopLeft | Position.BottomRight | Position.TopRight | Position.Bottom;
                string outputValue = string.Empty;
                foreach (string pattern in output)
                {
                    Position currentPosition = Position.None;
                    string value = string.Empty;
                    foreach (char c in pattern)
                    {
                        currentPosition |= GetPositionByKey[c];
                    }
                    switch (currentPosition)
                    {
                        case zero:
                            value += "0";
                            break;
                        case one:
                            value += "1";
                            break;
                        case two:
                            value += "2";
                            break;
                        case three:
                            value += "3";
                            break;
                        case four:
                            value += "4";
                            break;
                        case five:
                            value += "5";
                            break;
                        case six:
                            value += "6";
                            break;
                        case seven:
                            value += "7";
                            break;
                        case eight:
                            value += "8";
                            break;
                        case nine:
                            value += "9";
                            break;
                    }

                    outputValue += value;
                }

                sum += Convert.ToInt64(outputValue);
            }
            Console.WriteLine(sum);
        }

        [Flags]
        enum Position
        {
            None = 0,
            Top = 1,
            TopLeft = 2,
            TopRight = 4,
            Middle = 8,
            BottomLeft = 16,
            BottomRight = 32,
            Bottom = 64
        }

        static void AnalyzeDay8A(string fileName)
        {
            var lines = Utility.GetLines(fileName);
            const int oneSegmentLine = 2;
            const int fourSegmentLine = 4;
            const int sevenSegmentLine = 3;
            const int eightSegmentLine = 7;
            Dictionary<int, int> segmentsFound = new Dictionary<int, int>();
            segmentsFound.Add(oneSegmentLine, 0);
            segmentsFound.Add(fourSegmentLine, 0);
            segmentsFound.Add(sevenSegmentLine, 0);
            segmentsFound.Add(eightSegmentLine, 0);

            foreach (string line in lines)
            {
                var outputValues = line.Split(PipeCharacter);
                var values = outputValues[1].Split(Space);
                foreach (var value in values)
                {
                    int numberOfSegmentLine = value.Length;
                    switch (numberOfSegmentLine)
                    {
                        case oneSegmentLine:
                            segmentsFound[oneSegmentLine] += 1;
                            break;
                        case fourSegmentLine:
                            segmentsFound[fourSegmentLine] += 1;
                            break;
                        case sevenSegmentLine:
                            segmentsFound[sevenSegmentLine] += 1;
                            break;
                        case eightSegmentLine:
                            segmentsFound[eightSegmentLine] += 1;
                            break;
                    }
                }
            }

            int sum = 0;
            foreach (var segment in segmentsFound)
            {
                sum += segment.Value;
            }

            Console.WriteLine(sum);
        }
    }
}
