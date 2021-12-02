namespace AdventOfCode
{
    internal class Day1Logic
    {
        public static void AnalyzeDay1(string fileName)
        {
            Console.WriteLine("Start of day 1");
            int result = AnalyzeDay1A(fileName);
            Console.WriteLine(result);
            int resultB = AnalyzeDay1B(fileName);
            Console.WriteLine(resultB);
            Console.WriteLine("End of day 1");
        }


        static int AnalyzeDay1A(string fileName)
        {
            int? priorInput = null;
            int counter = 0;
            using (StreamReader streamReader = new StreamReader(fileName))
            {
                string? line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    int.TryParse(line, out int input);
                    if (priorInput == null)
                    {
                        priorInput = input;
                        continue;
                    }

                    if (input > priorInput.Value)
                    {
                        ++counter;
                    }

                    priorInput = input;
                }
                streamReader.Close();
            }

            return counter;
        }

        static int AnalyzeDay1B(string fileName)
        {
            List<int> inputs = new List<int>();

            using (StreamReader file = new StreamReader(fileName))
            {
                string? ln;

                while ((ln = file.ReadLine()) != null)
                {
                    int.TryParse(ln, out int input);
                    inputs.Add(input);
                }
                file.Close();
            }

            List<int> sums = new List<int>();

            for (int index = 0; index < inputs.Count - 2; index++)
            {
                int sum = inputs.ElementAt(index) + inputs.ElementAt(index + 1) + inputs.ElementAt(index + 2);
                sums.Add(sum);
            }

            int counter = 0;
            for (int index = 1; index < sums.Count; index++)
            {
                int priorSum = sums.ElementAt(index - 1);
                int currentSum = sums.ElementAt(index);
                if (priorSum < currentSum)
                {
                    ++counter;
                }
            }

            return counter;
        }
    }
}
