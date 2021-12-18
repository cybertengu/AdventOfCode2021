using System.Text;

namespace AdventOfCode
{
    internal class Day16Logic
    {

        public static Dictionary<char, string> HexadecimalToBinary = new Dictionary<char, string>()
            {
                { '0', "0000" },
                { '1', "0001" },
                { '2', "0010" },
                { '3', "0011" },
                { '4', "0100" },
                { '5', "0101" },
                { '6', "0110" },
                { '7', "0111" },
                { '8', "1000" },
                { '9', "1001" },
                { 'A', "1010" },
                { 'B', "1011" },
                { 'C', "1100" },
                { 'D', "1101" },
                { 'E', "1110" },
                { 'F', "1111" }
            };

        public static void AnalyzeDay(string fileName)
        {
            Console.WriteLine("Start of day 16");
            var lines = Utility.GetLines(fileName);
            AnalyzeDayA(lines);
            AnalyzeDayB(lines);
            Console.WriteLine("End of day 16");
        }

        static void AnalyzeDayB(List<string> lines)
        {
            foreach (var line in lines)
            {
                StringBuilder fullBinaryStringBuilder = new StringBuilder();
                var firstLine = line;
                Console.WriteLine(firstLine);
                foreach (var character in firstLine)
                {
                    var binary = HexadecimalToBinary[character];
                    fullBinaryStringBuilder.Append(binary);
                }

                string fullBinaryString = fullBinaryStringBuilder.ToString();
                int fullBinaryLength = fullBinaryString.Length;
                //Console.WriteLine(fullBinaryStringBuilder.ToString());
                Header header = new Header();
                int currentPosition = header.AddHeader(fullBinaryString, 0);
                int lastIndex = header.Ids.Count - 1;
                var idValue = header.Ids[lastIndex];

                if (idValue == 4)
                {
                    currentPosition = DoID4(fullBinaryString, currentPosition, ref header);
                }
                else
                {
                    currentPosition = DoNotID4(fullBinaryString, currentPosition, ref header);
                    header.PackageIndex++;
                }

                /* 
                    Packets with type ID 0 are sum packets - their value is the sum of 
                the values of their sub-packets. If they only have a single sub-packet, 
                their value is the value of the sub-packet.
                    Packets with type ID 1 are product packets - their value is the 
                result of multiplying together the values of their sub-packets. If they
                only have a single sub-packet, their value is the value of the 
                sub-packet.
                    Packets with type ID 2 are minimum packets - their value is the 
                minimum of the values of their sub-packets.
                    Packets with type ID 3 are maximum packets - their value is the 
                maximum of the values of their sub-packets.
                    Packets with type ID 5 are greater than packets - their value is 1 
                if the value of the first sub-packet is greater than the value of the 
                second sub-packet; otherwise, their value is 0. These packets always 
                have exactly two sub-packets.
                    Packets with type ID 6 are less than packets - their value is 1 if 
                the value of the first sub-packet is less than the value of the second 
                sub-packet; otherwise, their value is 0. These packets always have 
                exactly two sub-packets.
                  Packets with type ID 7 are equal to packets - their value is 1 if 
                the value of the first sub-packet is equal to the value of the second 
                sub-packet; otherwise, their value is 0. These packets always have 
                exactly two sub-packets.
                */
                Stack<int> stack = new Stack<int>();
                //foreach (var id in header.Ids)
                //{
                //    stack.Push(id);
                //}
                stack.Push(header.Ids[0]);
                int idsCounter = 1;

                Stack<long> valueResult = new Stack<long>();
                while (stack.Count > 0)
                {
                    int id = stack.Pop();
                    if (id == 4)
                    {
                        List<int> gather4s = new List<int>();
                        while (id == 4)
                        {
                            gather4s.Add(id);
                            id = stack.Pop();
                        }

                        // Do rule for this new id found. TODO still.
                        switch (id)
                        {
                            case 0:
                                // Do rule 0:
                                long sum = 0;
                                foreach (int currentId in gather4s)
                                {
                                    lastIndex = header.SubPackagesValues.Count - 1;
                                    sum += header.SubPackagesValues[lastIndex];
                                    header.SubPackagesValues.RemoveAt(lastIndex);
                                }
                                valueResult.Push(sum);
                                break;
                            case 1:
                                // Do rule 1:
                                long product = 1;
                                foreach (int currentId in gather4s)
                                {
                                    lastIndex = header.SubPackagesValues.Count - 1;
                                    product *= header.SubPackagesValues[lastIndex];
                                    header.SubPackagesValues.RemoveAt(lastIndex);
                                }
                                valueResult.Push(product);
                                break;
                            case 2:
                                // Do rule 2:
                                List<long> newList = new List<long>();
                                foreach (int currentId in gather4s)
                                {
                                    lastIndex = header.SubPackagesValues.Count - 1;
                                    newList.Add(header.SubPackagesValues[lastIndex]);
                                    header.SubPackagesValues.RemoveAt(lastIndex);
                                }
                                newList.Sort();
                                long minimum = newList[0];
                                valueResult.Push(minimum);
                                break;
                            case 3:
                                // Do rule 3:
                                newList = new List<long>();
                                foreach (int currentId in gather4s)
                                {
                                    lastIndex = header.SubPackagesValues.Count - 1;
                                    newList.Add(header.SubPackagesValues[lastIndex]);
                                    header.SubPackagesValues.RemoveAt(lastIndex);
                                }
                                newList.Sort();
                                long maximum = newList[newList.Count - 1];
                                valueResult.Push(maximum);
                                break;
                            case 4:
                                // Do rule 4:
                                // Shouldn't happen, but be safe right?
                                foreach (int currentId in gather4s)
                                {
                                    lastIndex = header.SubPackagesValues.Count - 1;
                                    valueResult.Push(header.SubPackagesValues[lastIndex]);
                                    header.SubPackagesValues.RemoveAt(lastIndex);
                                }
                                break;
                            case 5:
                                // Do rule 5:
                                lastIndex = header.SubPackagesValues.Count - 1;
                                long firstValue = 0, secondValue = 0;
                                int counter = 0;
                                foreach (int currentId in gather4s)
                                {
                                    if (counter == 0)
                                    {
                                        firstValue = header.SubPackagesValues[lastIndex];
                                        header.SubPackagesValues.RemoveAt(lastIndex--);
                                    }
                                    else if (counter == 1)
                                    {
                                        secondValue = header.SubPackagesValues[lastIndex];
                                        header.SubPackagesValues.RemoveAt(lastIndex--);
                                    }
                                    counter++;
                                }
                                int greaterThanResult = (firstValue > secondValue) ? 1 : 0;
                                valueResult.Push(greaterThanResult);
                                break;
                            case 6:
                                // Do rule 6:
                                lastIndex = header.SubPackagesValues.Count - 1;
                                firstValue = header.SubPackagesValues[lastIndex];
                                secondValue = header.SubPackagesValues[lastIndex - 1];
                                header.SubPackagesValues.RemoveAt(lastIndex);
                                header.SubPackagesValues.RemoveAt(lastIndex - 1);
                                int lessThanResult = (firstValue < secondValue) ? 1 : 0;
                                valueResult.Push(lessThanResult);
                                break;
                            case 7:
                                // Do rule 7:
                                lastIndex = header.SubPackagesValues.Count - 1;
                                firstValue = header.SubPackagesValues[lastIndex];
                                secondValue = header.SubPackagesValues[lastIndex - 1];
                                header.SubPackagesValues.RemoveAt(lastIndex);
                                header.SubPackagesValues.RemoveAt(lastIndex - 1);
                                int equalToResult = (firstValue == secondValue) ? 1 : 0;
                                valueResult.Push(equalToResult);
                                break;
                        }
                    }
                    else
                    {
                        switch (idValue)
                        {
                            case 0:
                                long sum = 0;
                                int amount = valueResult.Count;
                                for (int i = 0; i < amount; ++i)
                                {
                                    sum += valueResult.Pop();
                                }
                                valueResult.Push(sum);
                                break;
                            case 1:
                                long product = 1;
                                amount = valueResult.Count;
                                for (int i = 0; i < amount; ++i)
                                {
                                    product *= valueResult.Pop();
                                }
                                valueResult.Push(product);
                                break;
                            case 2:
                                List<long> newList = new List<long>();
                                amount = valueResult.Count;
                                for (int i = 0; i < amount; ++i)
                                {
                                    newList.Add(valueResult.Pop());
                                }
                                newList.Sort();
                                long minimum = newList[0];
                                valueResult.Push(minimum);
                                break;
                            case 3:
                                newList = new List<long>();
                                amount = valueResult.Count;
                                for (int i = 0; i < amount; ++i)
                                {
                                    newList.Add(valueResult.Pop());
                                }
                                newList.Sort();
                                lastIndex = newList.Count - 1;
                                long maximum = newList[lastIndex];
                                valueResult.Push(maximum);
                                break;
                            case 5:
                                long firstValue = valueResult.Pop();
                                long secondValue = valueResult.Pop();
                                long greaterThan = (firstValue > secondValue) ? 1 : 0;
                                valueResult.Push(greaterThan);
                                break;
                            case 6:
                                firstValue = valueResult.Pop();
                                secondValue = valueResult.Pop();
                                long lessThan = (firstValue < secondValue) ? 1 : 0;
                                valueResult.Push(lessThan);
                                break;
                            case 7:
                                firstValue = valueResult.Pop();
                                secondValue = valueResult.Pop();
                                long equalTo = (firstValue == secondValue) ? 1 : 0;
                                valueResult.Push(equalTo);
                                break;
                        }
                    }
                }

                Console.WriteLine(valueResult.Pop());
                if (valueResult.Count > 0)
                {
                    Console.WriteLine("There is still a bug somewhere!");
                }

                //int firstId = header.Ids[0];
                //switch (firstId)
                //{
                //    case 0:
                //        long sum = 0;
                //        int index = 1;
                //        int counter = 0;
                //        while (index < header.Ids.Count && header.Ids[index] == 4)
                //        {
                //            sum += header.SubPackagesValues[header.PackageIndex - 1][counter];
                //            counter++;
                //            index++;
                //        }
                //        Console.WriteLine($"Sum: {sum}");
                //        break;
                //    case 1:
                //        long product = 1;
                //        index = 1;
                //        counter = 0;
                //        while (index < header.Ids.Count && header.Ids[index] == 4)
                //        {
                //            product *= header.SubPackagesValues[header.PackageIndex - 1][counter];
                //            index++;
                //            counter++;
                //        }
                //        Console.WriteLine($"Product: {product}");
                //        break;
                //    case 2:
                //        index = 1;
                //        List<long> allValues = new List<long>();
                //        counter = 0;
                //        while (index < header.Ids.Count && header.Ids[index] == 4)
                //        {
                //            allValues.Add(header.SubPackagesValues[header.PackageIndex - 1][counter]);
                //            counter++;
                //            index++;
                //        }
                //        allValues.Sort();
                //        long minimum = allValues[0];
                //        Console.WriteLine($"Minimum: {minimum}");
                //        break;
                //    case 3:
                //        index = 1;
                //        allValues = new List<long>();
                //        counter = 0;
                //        while (index < header.Ids.Count && header.Ids[index] == 4)
                //        {
                //            allValues.Add(header.SubPackagesValues[header.PackageIndex - 1][counter]);
                //            counter++;
                //            index++;
                //        }
                //        allValues.Sort();
                //        long maximum = allValues[allValues.Count - 1];
                //        Console.WriteLine($"Maximum: {maximum}");
                //        break;
                //    case 4:
                //        long literalValue = header.SubPackagesValues[0][0];
                //        Console.WriteLine($"Didn't think this will happen but literal value is this: {literalValue}");
                //        break;
                //    case 5:
                //        int packageIndex = header.PackageIndex;
                //        var values = header.SubPackagesValues[packageIndex - 1];
                //        long firstValue = values[0];
                //        long secondValue = values[1];
                //        int result = (firstValue > secondValue) ? 1 : 0;
                //        Console.WriteLine($"Greater Than Result: {result}");
                //        break;
                //    case 6:
                //        packageIndex = header.PackageIndex;
                //        values = header.SubPackagesValues[packageIndex - 1];
                //        firstValue = values[0];
                //        secondValue = values[1];
                //        result = (firstValue < secondValue) ? 1 : 0;
                //        Console.WriteLine($"Less Than Result: {result}");
                //        break;
                //    case 7:
                //        packageIndex = header.PackageIndex - 1;
                //        if (packageIndex > 1)
                //        {

                //        }
                //        values = header.SubPackagesValues[packageIndex - 1];
                //        firstValue = values[0];
                //        secondValue = values[1];
                //        result = (firstValue == secondValue) ? 1 : 0;
                //        Console.WriteLine($"Equal To Result: {result}");
                //        break;
                //}

                if (fullBinaryLength - currentPosition > 6)
                {
                    Console.WriteLine("Oh dear, there are more to do on this string then.");
                }
            }
        }

        public static void TestDayBWithSampleInput()
        {
            List<string> lines = new List<string>() { "C200B40A82", "04005AC33890", "880086C3E88112",
            "CE00C43D881120","D8005AC2A8F0","F600BC2D8F","9C005AC2F8F0", "9C0141080250320F1802104A08"};
            AnalyzeDayB(lines);
        }

        static void AnalyzeDayA(List<string> lines)
        {
            foreach (var line in lines)
            {
                StringBuilder fullBinaryStringBuilder = new StringBuilder();
                var firstLine = line;
                Console.WriteLine(firstLine);
                foreach (var character in firstLine)
                {
                    var binary = HexadecimalToBinary[character];
                    fullBinaryStringBuilder.Append(binary);
                }

                string fullBinaryString = fullBinaryStringBuilder.ToString();
                int fullBinaryLength = fullBinaryString.Length;
                //Console.WriteLine(fullBinaryStringBuilder.ToString());
                Header header = new Header();
                int currentPosition = header.AddHeader(fullBinaryString, 0);
                int lastIndex = header.Ids.Count - 1;
                var idValue = header.Ids[lastIndex];

                if (idValue == 4)
                {
                    currentPosition = DoID4(fullBinaryString, currentPosition, ref header);
                }
                else
                {
                    currentPosition = DoNotID4(fullBinaryString, currentPosition, ref header);
                }

                if (fullBinaryLength - currentPosition > 6)
                {
                    Console.WriteLine("Oh dear, there are more to do on this string then.");
                }

                long sum = 0;
                foreach (var version in header.Versions)
                {
                    sum += version;
                }
                Console.WriteLine(sum);
            }
        }

        static int DoID4(string fullBinaryString, int currentPosition, ref Header header)
        {
            char leadingBit = fullBinaryString[currentPosition];
            int offset = currentPosition + 1;
            StringBuilder literalValueStringBuilder = new StringBuilder();
            while (leadingBit == '1')
            {
                literalValueStringBuilder.Append(fullBinaryString.Substring(offset, 4));
                leadingBit = fullBinaryString[4 + offset];
                offset += 5;
            }
            literalValueStringBuilder.Append(fullBinaryString.Substring(offset, 4));
            string literalValueString = literalValueStringBuilder.ToString();
            long literalValue = Convert.ToInt64(literalValueString, 2);
            //Console.WriteLine(literalValue);
            int packageIndex = header.PackageIndex;
            header.SubPackagesValues.Add(literalValue);
            currentPosition = offset + 4;

            return currentPosition;
        }

        static int DoNotID4(string fullBinaryString, int currentPosition, ref Header header)
        {
            char leadingBit = fullBinaryString[currentPosition];
            currentPosition++;
            if (leadingBit == '0')
            {
                string next15Bits = fullBinaryString.Substring(currentPosition, 15);
                currentPosition += 15;
                int value = Convert.ToInt32(next15Bits, 2);
                int counter = 0;
                while (counter < value)
                {
                    currentPosition = header.AddHeader(fullBinaryString, currentPosition);
                    int lastIndex = header.Ids.Count - 1;
                    int idValue = header.Ids[lastIndex];
                    counter += 6;
                    if (idValue == 4)
                    {
                        int newCurrentPosition = DoID4(fullBinaryString, currentPosition, ref header);
                        counter += newCurrentPosition - currentPosition;
                        currentPosition = newCurrentPosition;
                    }
                    else
                    {
                        // TODO: I am guessing this part is potentially recursive.
                        //Console.WriteLine("Does this get called A");
                        int newCurrentPosition = DoNotID4(fullBinaryString, currentPosition, ref header);
                        counter += newCurrentPosition - currentPosition;
                        currentPosition = newCurrentPosition;
                        header.PackageIndex++;
                    }
                }
            }
            else
            {
                string next11Bits = fullBinaryString.Substring(currentPosition, 11);
                int packagesAmount = Convert.ToInt32(next11Bits, 2);
                currentPosition += 11;
                for (int i = 0; i < packagesAmount; ++i)
                {
                    currentPosition = header.AddHeader(fullBinaryString, currentPosition);
                    int lastIndex = header.Ids.Count - 1;
                    int idValue = header.Ids[lastIndex];
                    if (idValue == 4)
                    {
                        currentPosition = DoID4(fullBinaryString, currentPosition, ref header);
                    }
                    else
                    {
                        // TODO: I am guessing this part is potentially recursive.
                        //Console.WriteLine("Does this get called B");
                        currentPosition = DoNotID4(fullBinaryString, currentPosition, ref header);
                        header.PackageIndex++;
                    }
                }
            }

            return currentPosition;
        }

        class Header
        {
            public List<int> Ids = new List<int>();
            public List<int> Versions = new List<int>();
            //public List<long> SubPackagesValues = new List<long>();
            public int PackageIndex = 0;
            public List<long> SubPackagesValues = new List<long>();

            public int AddHeader(string fullBinaryString, int position)
            {
                var version = fullBinaryString.Substring(position, 3);
                position += 3;
                var id = fullBinaryString.Substring(position, 3);
                position += 3;
                int idValue = Convert.ToInt32(id, 2);
                Ids.Add(idValue);
                int versionValue = Convert.ToInt32(version, 2);
                Versions.Add(versionValue);

                return position;
            }
        }
    }
}
