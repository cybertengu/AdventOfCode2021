using System.Drawing;

namespace AdventOfCode
{
    internal class Day18Logic
    {
        public static void AnalyzeDay(string fileName)
        {
            Console.WriteLine("Start of day 18");
            //TestData();
            AnalyzeDayA(fileName);
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
                            topNode.Value = $"{character}";
                        }
                    }
                }
            }
            long magniture = -1;
            Console.WriteLine(magniture);
        }

        class ShellFish
        {
            public string Value = string.Empty;
            public ShellFish? Left = null;
            public ShellFish? Right = null;

            public void InsertNode(ShellFish firstNode, ShellFish shellFish)
            {
                if (firstNode == null)
                {
                    Console.WriteLine("Something went horribly wrong. Top-most node is null");
                }

                bool isInserted = false;
                ShellFish nextShellFish;
                ShellFish currentFish = firstNode;
                while (!isInserted)
                {
                    if (currentFish.Left != null)
                    {

                    }
                    nextShellFish = firstNode.Left;
                }    
            }
        }

        static void TestData()
        {
            Point closestPoint = new Point(20, -10);
            Point furthestPoint = new Point(30, -5);
            List<Point> velocities = new List<Point>();
            velocities.Add(new Point(7, 2));
            velocities.Add(new Point(6, 3));
            velocities.Add(new Point(9, 0));
            velocities.Add(new Point(17, -4));
            foreach (var velocity in velocities)
            {
                int highestHeight = 0;
                bool didLandInLandingArea = DidLandInLandingArea(velocity, closestPoint, furthestPoint, out highestHeight);
                Console.WriteLine($"Result: {didLandInLandingArea} for ({velocity.X}, {velocity.Y})");
            }
        }

        static bool DidLandInLandingArea(Point velocity, Point closestPoint, Point furthestPoint, out int highestPoint)
        {
            highestPoint = 0;
            bool isPassTargetArea = false;
            Point currentLocation = new Point(0, 0);
            int leastX = closestPoint.X;
            int leastY = closestPoint.Y;
            int mostX = furthestPoint.X;
            int mostY = furthestPoint.Y;

            while (!isPassTargetArea)
            {
                currentLocation.X += velocity.X;
                currentLocation.Y += velocity.Y--;
                if (currentLocation.Y > highestPoint)
                {
                    highestPoint = currentLocation.Y;
                }
                
                if (velocity.X < 0)
                {
                    velocity.X++;
                }
                // I thought about doing absolute value and not worry about negative
                // numbers, but this solution was fast enough for the problem, so 
                // I left this in.
                else if (velocity.X > 0)
                {
                    velocity.X--;
                }
                if (currentLocation.Y < leastY)
                {
                    isPassTargetArea = true;
                }
                else if (currentLocation.X >= leastX && currentLocation.X <= mostX &&
                    currentLocation.Y >= leastY && currentLocation.Y <= mostY)
                {
                    // Landed on the target platform.
                    break;
                }
            }
            return !isPassTargetArea;
        }
    }
}
