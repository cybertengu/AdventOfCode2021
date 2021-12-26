using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Day24Logic
    {
        public static void AnalyzeDay(string fileName)
        {
            Console.WriteLine("Start of day 24");
            //TestCode(fileName);
            //AnalyzeDayA(fileName);
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

        class Section
        {
            public int Id = 0;
            public List<string> InputLines = new List<string>();
            public Dictionary<char, long> Variables = new Dictionary<char, long>();

            public Section(int id)
            {
                Id = id;
                Variables.Add(w, 0);
                Variables.Add(x, 0);
                Variables.Add(y, 0);
                Variables.Add(z, 0);
            }
        }

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

        static void AnalyzeDayA(string fileName)
        {
            var lines = Utility.GetLines(fileName);
            int counter = 0;
            List<Section> sections = new List<Section>();
            Section section = new Section(counter++);
            section.InputLines.Add(lines[0]);
            for (int i = 1; i < lines.Count; ++i)
            {
                var line = lines[i];
                if (!line.StartsWith("inp"))
                {
                    section.InputLines.Add(line);
                }
                else
                {
                    sections.Add(section);
                    section = new Section(counter++);
                    section.InputLines.Add(line);
                }
            }
            sections.Add(section);
            //foreach (var section1 in sections)
            //{
            //    foreach (var input in section1.InputLines)
            //    {
            //        Console.WriteLine(input);
            //    }
            //}

            Dictionary<char, long> variables = new Dictionary<char, long>();
            variables.Add(w, 0);
            variables.Add(x, 0);
            variables.Add(y, 0);
            variables.Add(z, 0);
            int[] inputs = new int[14] { 2, 1, 4, 7, 1, 5, 1, 1, 2, 1, 1, 1, 1, 2 };
            long result = -1;
            //21471511211112
            int[] zeroInputs = new int[] { 2, 3, 1 };
            int[] firstInputs = new int[] { 5, 4, 1 };
            int[] secondInputs = new int[] { 3, 4, 1 };
            int[] thirdInputs = new int[] { 7, 6, 1 };
            int[] fourthInputs = new int[] { 1, 2, 9 };
            int[] fifthInputs = new int[] { 5, 8, 9, 1 };
            int[] sixthInputs = new int[] { 1, 4, 5, 1};
            int[] seventhInputs = new int[] { 4, 1 };
            int[] eigthInputs = new int[] { 2, 1 };
            int[] ninethInputs = new int[] { 3, 1 };
            int[] tenthInputs = new int[] { 6, 1 };
            int[] eleventhInputs = new int[] { 5, 1 };
            int[] twelfthInputs = new int[] { 1 };
            int[] thirteenthInputs = new int[] { 2 };
            int index = 0;
            foreach (int zeroInput in zeroInputs)
            {
                sections[0] = CalculateZForSection(zeroInput, sections[0], sections[0].Variables);
                foreach(int firstInput in firstInputs)
                {
                    sections[1] = CalculateZForSection(firstInput, sections[1], sections[0].Variables);
                    foreach (int secondInput in secondInputs)
                    {
                        index = 2;
                        sections[index] = CalculateZForSection(secondInput, sections[index], sections[index - 1].Variables);
                        foreach (int thirdInput in thirdInputs)
                        {
                            index = 3;
                            sections[index] = CalculateZForSection(thirdInput, sections[index], sections[index - 1].Variables);
                            foreach (int fourthInput in fourthInputs)
                            {
                                index = 4;
                                sections[index] = CalculateZForSection(fourthInput, sections[index], sections[index - 1].Variables);
                                foreach (int fifthInput in fifthInputs)
                                {
                                    index = 5;
                                    sections[index] = CalculateZForSection(fifthInput, sections[index], sections[index - 1].Variables);
                                    foreach (int sixthInput in sixthInputs)
                                    {
                                        index = 6;
                                        sections[index] = CalculateZForSection(sixthInput, sections[index], sections[index - 1].Variables);
                                        foreach (int seventhInput in seventhInputs)
                                        {
                                            index = 7;
                                            sections[index] = CalculateZForSection(seventhInput, sections[index], sections[index - 1].Variables);
                                            foreach (int eigthInput in eigthInputs)
                                            {
                                                index = 8;
                                                sections[index] = CalculateZForSection(eigthInput, sections[index], sections[index - 1].Variables);
                                                foreach (int ninethInput in ninethInputs)
                                                {
                                                    index = 9;
                                                    sections[index] = CalculateZForSection(ninethInput, sections[index], sections[index - 1].Variables);
                                                    foreach (int tenthInput in tenthInputs)
                                                    {
                                                        index = 10;
                                                        sections[index] = CalculateZForSection(tenthInput, sections[index], sections[index - 1].Variables);
                                                        foreach (int eleventhInput in eleventhInputs)
                                                        {
                                                            index = 11;
                                                            sections[index] = CalculateZForSection(eleventhInput, sections[index], sections[index - 1].Variables);
                                                            foreach (int twelfthInput in twelfthInputs)
                                                            {
                                                                index = 12;
                                                                sections[index] = CalculateZForSection(twelfthInput, sections[index], sections[index - 1].Variables);
                                                                foreach (int thirtheethInput in thirteenthInputs)
                                                                {
                                                                    index = 13;
                                                                    sections[index] = CalculateZForSection(twelfthInput, sections[index], sections[index - 1].Variables);
                                                                    //Console.WriteLine(sections[index].Variables[z]);
                                                                    if (sections[index].Variables[z] == 0)
                                                                    {
                                                                        Console.WriteLine($"{zeroInput}{firstInput}{secondInput}{thirdInput}{fourthInput}{fifthInput}{sixthInput}{seventhInput}{eigthInput}{ninethInput}{tenthInput}{eleventhInput}{twelfthInput}{thirtheethInput}");
                                                                    }
                                                                    sections[index].Variables[w] = 0;
                                                                    sections[index].Variables[x] = 0;
                                                                    sections[index].Variables[y] = 0;
                                                                    sections[index].Variables[z] = 0;
                                                                }
                                                                index = 12;
                                                                sections[index].Variables[w] = 0;
                                                                sections[index].Variables[x] = 0;
                                                                sections[index].Variables[y] = 0;
                                                                sections[index].Variables[z] = 0;
                                                            }
                                                            index = 11;
                                                            sections[index].Variables[w] = 0;
                                                            sections[index].Variables[x] = 0;
                                                            sections[index].Variables[y] = 0;
                                                            sections[index].Variables[z] = 0;
                                                        }
                                                        index = 10;
                                                        sections[index].Variables[w] = 0;
                                                        sections[index].Variables[x] = 0;
                                                        sections[index].Variables[y] = 0;
                                                        sections[index].Variables[z] = 0;
                                                    }
                                                    index = 9;
                                                    sections[index].Variables[w] = 0;
                                                    sections[index].Variables[x] = 0;
                                                    sections[index].Variables[y] = 0;
                                                    sections[index].Variables[z] = 0;
                                                }
                                                index = 8;
                                                sections[index].Variables[w] = 0;
                                                sections[index].Variables[x] = 0;
                                                sections[index].Variables[y] = 0;
                                                sections[index].Variables[z] = 0;
                                            }
                                            index = 7;
                                            sections[index].Variables[w] = 0;
                                            sections[index].Variables[x] = 0;
                                            sections[index].Variables[y] = 0;
                                            sections[index].Variables[z] = 0;
                                        }
                                        index = 6;
                                        sections[index].Variables[w] = 0;
                                        sections[index].Variables[x] = 0;
                                        sections[index].Variables[y] = 0;
                                        sections[index].Variables[z] = 0;
                                    }
                                    index = 5;
                                    sections[index].Variables[w] = 0;
                                    sections[index].Variables[x] = 0;
                                    sections[index].Variables[y] = 0;
                                    sections[index].Variables[z] = 0;
                                }
                                index = 4;
                                sections[index].Variables[w] = 0;
                                sections[index].Variables[x] = 0;
                                sections[index].Variables[y] = 0;
                                sections[index].Variables[z] = 0;
                            }
                            index = 3;
                            sections[index].Variables[w] = 0;
                            sections[index].Variables[x] = 0;
                            sections[index].Variables[y] = 0;
                            sections[index].Variables[z] = 0;
                        }
                        index = 2;
                        sections[index].Variables[w] = 0;
                        sections[index].Variables[x] = 0;
                        sections[index].Variables[y] = 0;
                        sections[index].Variables[z] = 0;
                    }
                    sections[1].Variables[w] = 0;
                    sections[1].Variables[x] = 0;
                    sections[1].Variables[y] = 0;
                    sections[1].Variables[z] = 0;
                }
                sections[0].Variables[w] = 0;
                sections[0].Variables[x] = 0;
                sections[0].Variables[y] = 0;
                sections[0].Variables[z] = 0;
            }
            //for (int i = 0; i < 14; ++i)
            //{
            //    int priorVariableIndex = ((i - 1) > -1) ? i - 1 : 0;
            //    sections[i] = CalculateZForSection(inputs[i], sections[i], sections[priorVariableIndex].Variables);
            //    Console.WriteLine(sections[i].Variables[z]);
            //}
            //for (int a = 9; a > 0; --a)
            //{
            //    inputs[0] = a;
            //    sections[0] = CalculateZForSection(a, sections[0], sections[0].Variables);
            //    if (sections[0].ExceptionThrown)
            //    {
            //        sections[0].ExceptionThrown = false;
            //        continue;
            //    }
            //    for (int b = 9; b > 0; --b)
            //    {
            //        inputs[1] = b;
            //        sections[1] = CalculateZForSection(b, sections[1], sections[0].Variables);
            //        if (sections[1].ExceptionThrown)
            //        {
            //            sections[1].ExceptionThrown = false;
            //            continue;
            //        }
            //        for (int c = 9; c > 0; --c)
            //        {
            //            inputs[2] = c;
            //            sections[2] = CalculateZForSection(c, sections[2], sections[1].Variables);
            //            if (sections[2].ExceptionThrown)
            //            {
            //                sections[2].ExceptionThrown = false;
            //                continue;
            //            }
            //            for (int d = 9; d > 0; --d)
            //            {
            //                inputs[3] = d;
            //                sections[3] = CalculateZForSection(d, sections[3], sections[2].Variables);
            //                if (sections[3].ExceptionThrown)
            //                {
            //                    sections[3].ExceptionThrown = false;
            //                    continue;
            //                }
            //                for (int e = 9; e > 0; --e)
            //                {
            //                    inputs[4] = e;
            //                    sections[4] = CalculateZForSection(e, sections[4], sections[3].Variables);
            //                    if (sections[4].ExceptionThrown)
            //                    {
            //                        sections[4].ExceptionThrown = false;
            //                        continue;
            //                    }
            //                    for (int f = 9; f > 0; --f)
            //                    {
            //                        inputs[5] = f;
            //                        sections[5] = CalculateZForSection(f, sections[5], sections[4].Variables);
            //                        if (sections[5].ExceptionThrown)
            //                        {
            //                            sections[5].ExceptionThrown = false;
            //                            continue;
            //                        }
            //                        for (int g = 9; g > 0; --g)
            //                        {
            //                            inputs[6] = g;
            //                            sections[6] = CalculateZForSection(g, sections[6], sections[5].Variables);
            //                            if (sections[6].ExceptionThrown)
            //                            {
            //                                sections[6].ExceptionThrown = false;
            //                                continue;
            //                            }
            //                            for (int h = 9; h > 0; --h)
            //                            {
            //                                inputs[7] = h;
            //                                sections[7] = CalculateZForSection(h, sections[7], sections[6].Variables);
            //                                if (sections[7].ExceptionThrown)
            //                                {
            //                                    sections[7].ExceptionThrown = false;
            //                                    continue;
            //                                }
            //                                for (int i = 9; i > 0; --i)
            //                                {
            //                                    inputs[8] = i;
            //                                    sections[8] = CalculateZForSection(i, sections[8], sections[7].Variables);
            //                                    if (sections[8].ExceptionThrown)
            //                                    {
            //                                        sections[8].ExceptionThrown = false;
            //                                        continue;
            //                                    }
            //                                    for (int j = 9; j > 0; --j)
            //                                    {
            //                                        inputs[9] = j;
            //                                        sections[9] = CalculateZForSection(j, sections[9], sections[8].Variables);
            //                                        if (sections[9].ExceptionThrown)
            //                                        {
            //                                            sections[9].ExceptionThrown = false;
            //                                            continue;
            //                                        }
            //                                        for (int k = 9; k > 0; --k)
            //                                        {
            //                                            inputs[10] = k;
            //                                            sections[10] = CalculateZForSection(k, sections[10], sections[9].Variables);
            //                                            for (int l = 9; l > 0; --l)
            //                                            {
            //                                                inputs[11] = l;
            //                                                sections[11] = CalculateZForSection(l, sections[11], sections[10].Variables);
            //                                                if (sections[11].ExceptionThrown)
            //                                                {
            //                                                    sections[11].ExceptionThrown = false;
            //                                                    continue;
            //                                                }
            //                                                for (int m = 9; m > 0; --m)
            //                                                {
            //                                                    inputs[12] = m;
            //                                                    sections[12] = CalculateZForSection(m, sections[12], sections[11].Variables);
            //                                                    if (sections[12].ExceptionThrown)
            //                                                    {
            //                                                        sections[12].ExceptionThrown = false;
            //                                                        continue;
            //                                                    }
            //                                                    for (int n = 9; n > 0; --n)
            //                                                    {
            //                                                        inputs[13] = n;
            //                                                        sections[13] = CalculateZForSection(n, sections[13], sections[12].Variables);
            //                                                        if (sections[13].ExceptionThrown)
            //                                                        {
            //                                                            sections[13].ExceptionThrown = false;
            //                                                            continue;
            //                                                        }
            //                                                        //result = CalculateZ(variables, lines, inputs);
            //                                                        if (result == 0)
            //                                                        {
            //                                                            foreach (var input in inputs)
            //                                                            {
            //                                                                Console.Write(input);
            //                                                            }
            //                                                            return;
            //                                                        }
            //                                                    }
            //                                                }
            //                                            }
            //                                        }
            //                                    }
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }

        const char space = ' ';

        static void TestCode(string fileName)
        {
            var lines = Utility.GetLines(fileName);
            int counter = 0;
            List<Section> sections = new List<Section>();
            Section section = new Section(counter++);
            section.InputLines.Add(lines[0]);
            for (int index = 1; index < lines.Count; ++index)
            {
                var line = lines[index];
                if (!line.StartsWith("inp"))
                {
                    section.InputLines.Add(line);
                }
                else
                {
                    sections.Add(section);
                    section = new Section(counter++);
                    section.InputLines.Add(line);
                }
            }

            sections.Add(section);
            int[] inputs = new int[14];
            int[] twoInputs = new int[2];
            twoInputs = DoEachPairSection(sections[1], sections[sections.Count - 1]);
            inputs[1] = twoInputs[0];
            inputs[sections.Count - 1] = twoInputs[1];
            twoInputs = DoEachPairSection(sections[6], sections[7]);
            inputs[6] = twoInputs[0];
            inputs[7] = twoInputs[1];
            twoInputs = DoEachPairSection(sections[2], sections[sections.Count - 4]);
            inputs[2] = twoInputs[0];
            inputs[sections.Count - 4] = twoInputs[1];
            twoInputs = DoEachPairSection(sections[5], sections[sections.Count - 3]);
            inputs[5] = twoInputs[0];
            inputs[sections.Count - 3] = twoInputs[1];
            twoInputs = DoEachPairSection(sections[3], sections[4]);
            inputs[3] = twoInputs[0];
            inputs[4] = twoInputs[1];
            twoInputs = DoEachPairSection(sections[8], sections[12]);
            inputs[8] = twoInputs[0];
            inputs[12] = twoInputs[1];

            twoInputs = DoEachPairSection(sections[0], sections[9]);
            inputs[0] = twoInputs[0];
            inputs[9] = twoInputs[1];

            Console.WriteLine("Result:");
            for (int i = 0; i < 14; ++i)
            {
                //Console.WriteLine($"{i} {inputs[i]}");
                Console.Write(inputs[i]);
            }
            Console.WriteLine();

            for (int i = 0; i < 13; ++i)
            {
                for (int j = i + 1; j < 14; ++j)
                {
                    twoInputs = DoEachPairSection(sections[i], sections[j]);
                    if (!(twoInputs[0] == 0 && twoInputs[1] == 0))
                    {
                        Console.WriteLine($"Section Id: {i} {j} Input: {twoInputs[0]} {twoInputs[1]} Z: {sections[i].Variables[z]} {sections[j].Variables[z]}");
                    }
                }
            }
        }

        static int[] DoEachPairSection(Section section, Section secondSection)
        {
            int[] inputs = new int[2];
            bool isDone = false;
            for (int i = 1; i < 10; ++i)
            {
                section = CalculateZForSection(i, section, section.Variables);
                for (int j = 1; j < 10; ++j)
                {
                    secondSection = CalculateZForSection(j, secondSection, section.Variables);
                    if (secondSection.Variables[z] == 0)
                    {
                        inputs[0] = i;
                        inputs[1] = j;
                        //Console.WriteLine($"Result: {i} {j}");
                        isDone = true;
                        break;
                    }
                }
                section.Variables[w] = 0;
                section.Variables[x] = 0;
                section.Variables[y] = 0;
                section.Variables[z] = 0;
                secondSection.Variables[w] = 0;
                secondSection.Variables[x] = 0;
                secondSection.Variables[y] = 0;
                secondSection.Variables[z] = 0;
                if (isDone)
                {
                    break;
                }
            }

            return inputs;
        }

        static Section CalculateZForSection(int input, Section section, Dictionary<char, long> variables)
        {
            foreach (var instructionLine in section.InputLines)
            {
                var tokens = instructionLine.Split(space);
                var instruction = tokens[0];
                char firstVariable = tokens[1][0];
                long secondValue = 0;
                switch (instruction)
                {
                    case "inp":
                        variables[firstVariable] = input;
                        break;
                    case "add":
                        var secondVariable = tokens[2][0];
                        if (char.IsLetter(secondVariable))
                        {
                            variables[firstVariable] += variables[secondVariable];
                        }
                        else
                        {
                            secondValue = Convert.ToInt64(tokens[2].ToString());
                            variables[firstVariable] += secondValue;
                        }
                        break;
                    case "mul":
                        secondVariable = tokens[2][0];
                        if (char.IsLetter(secondVariable))
                        {
                            variables[firstVariable] *= variables[secondVariable];
                        }
                        else
                        {
                            secondValue = Convert.ToInt64(tokens[2].ToString());
                            variables[firstVariable] *= secondValue;
                        }
                        break;
                    case "div":
                        secondVariable = tokens[2][0];
                        if (char.IsLetter(secondVariable))
                        {
                            secondValue = variables[secondVariable];
                        }
                        else
                        {
                            secondValue = Convert.ToInt64(tokens[2].ToString());
                        }
                        if (secondValue == 0)
                        {
                            //Console.WriteLine("Something went wrong with div");
                            break;
                        }
                        variables[firstVariable] /= secondValue;
                        break;
                    case "mod":
                        secondVariable = tokens[2][0];
                        if (char.IsLetter(secondVariable))
                        {
                            secondValue = variables[secondVariable];
                        }
                        else
                        {
                            secondValue = Convert.ToInt64(tokens[2].ToString());
                        }
                        if (variables[firstVariable] < 0 || secondValue <= 0)
                        {
                            //Console.WriteLine("Something went wrong with mod");
                            break;
                        }
                        variables[firstVariable] %= secondValue;
                        break;
                    case "eql":
                        secondVariable = tokens[2][0];
                        if (char.IsLetter(secondVariable))
                        {
                            secondValue = variables[secondVariable];
                        }
                        else
                        {
                            secondValue = Convert.ToInt64(tokens[2].ToString());
                        }
                        variables[firstVariable] = (variables[firstVariable] == secondValue) ? 1 : 0;
                        break;
                }
                //Console.WriteLine($"({instructionLine}) ({variables[w]} {variables[x]} {variables[y]} {variables[z]}) {secondValue}");
            }

            section.Variables = variables;

            return section;
        }
    }
}
