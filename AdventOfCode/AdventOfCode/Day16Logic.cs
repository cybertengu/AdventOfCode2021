using System.Text;

namespace AdventOfCode
{
    internal class Day16Logic
    {
        public const string ONE = "1";

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

            // TestDayBWithSampleInput had additional test data suggested by others
            // in reddit. For example, some input will overflow if you don't have
            // right data type.
            TestDayBWithSampleInput();
            AnalyzeDayA(lines);
            AnalyzeDayB(lines);
            Console.WriteLine("End of day 16");
        }

        static void AnalyzeDayB(List<string> lines)
        {
            // I made a foreach loop because I wanted to test various inputs despite
            // the real data to go against is only one line.
            foreach (var line in lines)
            {
                StringBuilder fullBinaryStringBuilder = new StringBuilder();
                var firstLine = line;
                foreach (var character in firstLine)
                {
                    var binary = HexadecimalToBinary[character];
                    fullBinaryStringBuilder.Append(binary);
                }

                string fullBinaryString = fullBinaryStringBuilder.ToString();
                int fullBinaryLength = fullBinaryString.Length;
                int currentPosition = 0;
                Packet packet = new Packet();
                DoAllID(fullBinaryString, currentPosition, ref packet);

                for (int i = packet.SubPackets.Count - 1; i > -1; --i)
                {
                    var currentPacket = packet.SubPackets[i];
                    int subPacketsCount = currentPacket.SubPackets.Count;
                    bool areThereMoreSubPacket = subPacketsCount > 0;
                    Stack<Packet> stack = new Stack<Packet>();
                    stack.Push(currentPacket);
                    while (stack.Count > 0)
                    {
                        currentPacket = stack.Pop();
                        if (currentPacket.SubPackets.Count > 0 && currentPacket.LiteralValue == -1)
                        {
                            bool wasAllWithValidValue = true;
                            stack.Push(currentPacket);
                            foreach (var subPacket in currentPacket.SubPackets)
                            {
                                if (subPacket.LiteralValue == -1)
                                {
                                    stack.Push(subPacket);
                                    wasAllWithValidValue = false;
                                }
                            }

                            if (wasAllWithValidValue)
                            {
                                currentPacket = stack.Pop();
                                RunThroughIDCase(ref currentPacket);
                            }
                        }
                    }
                }
                RunThroughIDCase(ref packet);
                Console.WriteLine(packet.LiteralValue);
            }
        }

        static void RunThroughIDCase(ref Packet currentPacket)
        {
            var id = currentPacket.ID;
            switch (id)
            {
                case 0:
                    long sum = 0;
                    foreach (var subPacket in currentPacket.SubPackets)
                    {
                        sum += subPacket.LiteralValue;
                    }
                    currentPacket.LiteralValue = sum;
                    break;
                case 1:
                    long product = 1;
                    foreach (var subPacket in currentPacket.SubPackets)
                    {
                        product *= subPacket.LiteralValue;
                    }
                    currentPacket.LiteralValue = product;
                    break;
                case 2:
                    List<long> values = new List<long>();
                    foreach (var subPacket in currentPacket.SubPackets)
                    {
                        values.Add(subPacket.LiteralValue);
                    }
                    values.Sort();
                    long minimum = values[0];
                    currentPacket.LiteralValue = minimum;
                    break;
                case 3:
                    values = new List<long>();
                    foreach (var subPacket in currentPacket.SubPackets)
                    {
                        values.Add(subPacket.LiteralValue);
                    }
                    values.Sort();
                    long maximum = values[values.Count - 1];
                    currentPacket.LiteralValue = maximum;
                    break;
                case 5:
                    var firstSubPacket = currentPacket.SubPackets[0];
                    long firstValue = firstSubPacket.LiteralValue;
                    var secondSubPacket = currentPacket.SubPackets[1];
                    long secondValue = secondSubPacket.LiteralValue;
                    int greaterThan = (firstValue > secondValue) ? 1 : 0;
                    currentPacket.LiteralValue = greaterThan;
                    break;
                case 6:
                    firstSubPacket = currentPacket.SubPackets[0];
                    firstValue = firstSubPacket.LiteralValue;
                    secondSubPacket = currentPacket.SubPackets[1];
                    secondValue = secondSubPacket.LiteralValue;
                    int lessThan = (firstValue < secondValue) ? 1 : 0;
                    currentPacket.LiteralValue = lessThan;
                    break;
                case 7:
                    firstSubPacket = currentPacket.SubPackets[0];
                    firstValue = firstSubPacket.LiteralValue;
                    secondSubPacket = currentPacket.SubPackets[1];
                    secondValue = secondSubPacket.LiteralValue;
                    int equalTo = (firstValue == secondValue) ? 1 : 0;
                    currentPacket.LiteralValue = equalTo;
                    break;
            }
        }

        static int DoAllID(string fullBinaryString, int currentPosition, ref Packet packet)
        {
            string versionString = fullBinaryString.Substring(currentPosition, 3);
            int version = Convert.ToInt32(versionString, 2);
            packet.Version = version;
            currentPosition += 3;
            string idString = fullBinaryString.Substring(currentPosition, 3);
            int id = Convert.ToInt32(idString, 2);
            packet.ID = id;
            currentPosition += 3;
            string leadingIdString = fullBinaryString.Substring(currentPosition++, 1);
            int leadingId = Convert.ToInt32(leadingIdString, 2);

            if (id == 4)
            {
                StringBuilder stringBuilder = new StringBuilder();
                while (leadingId != 0)
                {
                    string fourBits = fullBinaryString.Substring(currentPosition, 4);
                    stringBuilder.Append(fourBits);
                    currentPosition += 4;
                    leadingIdString = fullBinaryString.Substring(currentPosition++, 1);
                    leadingId = Convert.ToInt32(leadingIdString, 2);
                }
                string lastFourBits = fullBinaryString.Substring(currentPosition, 4);
                stringBuilder.Append(lastFourBits);
                currentPosition += 4;
                string literalValueString = stringBuilder.ToString();
                packet.LiteralValue = Convert.ToInt64(literalValueString, 2);
            }
            else
            {
                if (leadingId == 0)
                {
                    string totalLengthInBitsString = fullBinaryString.Substring(currentPosition, 15);
                    long totalLengthInBits = Convert.ToInt64(totalLengthInBitsString, 2);
                    currentPosition += 15;
                    long fullLengthAfterPosition = currentPosition + totalLengthInBits;
                    while (currentPosition < fullLengthAfterPosition)
                    {
                        currentPosition = DoSubPacket(fullBinaryString, currentPosition, ref packet);
                    }
                }
                else
                {
                    string numberOfSubPacketsString = fullBinaryString.Substring(currentPosition, 11);
                    long numberOfSubPackets = Convert.ToInt64(numberOfSubPacketsString, 2);
                    currentPosition += 11;
                    for (int i = 0; i < numberOfSubPackets; ++i)
                    {
                        currentPosition = DoSubPacket(fullBinaryString, currentPosition, ref packet);
                    }
                }
            }

            return currentPosition;
        }

        static int DoSubPacket(string fullBinaryString, int currentPosition, ref Packet packet)
        {
            Packet subPacket = new Packet();
            currentPosition = DoAllID(fullBinaryString, currentPosition, ref subPacket);
            packet.SubPackets.Add(subPacket);
            return currentPosition;
        }

        class Packet
        {
            public int Version;
            public int ID;
            public long LiteralValue = -1;
            public List<Packet> SubPackets = new List<Packet>();
        }

        public static void TestDayBWithSampleInput()
        {
            Console.WriteLine("Test data running:");
            List<string> lines = new List<string>() { "3232D42BF9400", "26008C8E2DA0191C5B400", "8A004A801A8002F478", "C200B40A82", "04005AC33890", "880086C3E88112",
            "CE00C43D881120","D8005AC2A8F0","F600BC2D8F","9C005AC2F8F0", "9C0141080250320F1802104A08"};
            AnalyzeDayB(lines);
            Console.WriteLine("End of test data");
        }

        // This solution was not designed well enough to handle part 2.
        static void AnalyzeDayA(List<string> lines)
        {
            foreach (var line in lines)
            {
                StringBuilder fullBinaryStringBuilder = new StringBuilder();
                var firstLine = line;
                foreach (var character in firstLine)
                {
                    var binary = HexadecimalToBinary[character];
                    fullBinaryStringBuilder.Append(binary);
                }

                string fullBinaryString = fullBinaryStringBuilder.ToString();
                long fullBinaryLength = fullBinaryString.Length;
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
                long value = Convert.ToInt64(next15Bits, 2);
                long counter = 0;
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
                        int newCurrentPosition = DoNotID4(fullBinaryString, currentPosition, ref header);
                        counter += newCurrentPosition - currentPosition;
                        currentPosition = newCurrentPosition;
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
                        currentPosition = DoNotID4(fullBinaryString, currentPosition, ref header);
                    }
                }
            }

            return currentPosition;
        }

        class Header
        {
            public List<int> Ids = new List<int>();
            public List<int> Versions = new List<int>();

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
