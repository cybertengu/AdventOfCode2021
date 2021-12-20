using System.Drawing;

namespace AdventOfCode
{
    internal class Day17Logic
    {
        public static void AnalyzeDay(string fileName)
        {
            Console.WriteLine("Start of day 17");
            //TestData();
            AnalyzeDayAB(fileName);
            Console.WriteLine("End of day 17");
        }

        static void AnalyzeDayAB(string fileName)
        {
            // Rules: Always start at 0,0. We are trying to find the best
            // trajectory for maximum height.
            // X increases by its value.
            // Y increase by its value.
            // X value decrease by one every step
            // toward zero when greater than 0. If less than 0, then increase
            // by one. 
            // Y always decrease by 1.
            // Target area is where the probe must land on.
            // Need to find all unique velocity that lands on the platform.
            var lines = Utility.GetLines(fileName);
            var line = lines[0];
            var tokens = line.Split(' ');
            var xPart = tokens[2];
            var yPart = tokens[3];
            int xLength = xPart.Length;
            var xRange = xPart.Substring(2, xLength - 3);
            char period = '.';
            var xs = xRange.Split(period, StringSplitOptions.RemoveEmptyEntries);
            int yLength = yPart.Length;
            var yRange = yPart.Substring(2, yLength - 2);
            var ys = yRange.Split(period, StringSplitOptions.RemoveEmptyEntries);
            int[] y = new int[] { Convert.ToInt32(ys[0]), Convert.ToInt32(ys[1]) };
            int[] x = new int[] { Convert.ToInt32(xs[0]), Convert.ToInt32(xs[1]) };
            int leastX, mostX;
            if (x[0] < x[1])
            {
                leastX = x[0];
                mostX = x[1];
            }
            else
            {
                leastX = x[1];
                mostX = x[0];
            }
            int leastY, mostY;
            if (y[0] < y[1])
            {
                leastY = y[0];
                mostY = y[1];
            }
            else
            {
                leastY = y[1];
                mostY = y[0];
            }

            // TODO: For now, I am using hard-coded values.
            Point closestPoint = new Point(leastX, leastY);
            Point furthestPoint = new Point(mostX, mostY); 
            List<Point> velocities = new List<Point>();
            int theHighestHeight = -1;
            for (int i = 1; i < mostX + 1; i++)
            {
                for (int j = leastY; j < Math.Abs(leastY) + 1; j++)
                {
                    Point velocity = new Point(i, j);
                    int highestHeight = 0;
                    bool didLandInTargetArea = DidLandInLandingArea(velocity, closestPoint, furthestPoint, out highestHeight);
                    if (didLandInTargetArea)
                    {
                        velocities.Add(velocity);
                        if (highestHeight > theHighestHeight)
                        {
                            theHighestHeight = highestHeight;
                        }
                    }
                }
            }

            Console.WriteLine(theHighestHeight);
            Console.WriteLine(velocities.Count);
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
