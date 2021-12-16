using System.Drawing;

namespace AdventOfCode
{
    internal class Day15Logic
    {
        public static void AnalyzeDay(string fileName)
        {
            Console.WriteLine("Start of day 15");
            var lines = Utility.GetLines(fileName);
            AnalyzeDayA(lines);
            AnalyzeDayB(lines);
            Console.WriteLine("End of day 15");
        }

        static void AnalyzeDayB(List<string> lines)
        {
            var firstLine = lines[0];
            int length = firstLine.Length;
            int linesCount = lines.Count;
            int totalNodesForOneTile = length * linesCount;
            int[] zeroTile;
            var edges = GetEdgesAndTile(lines, out zeroTile);
            int[] firstTile = new int[totalNodesForOneTile];
            for (int i = 0; i < totalNodesForOneTile; ++i)
            {
                int value = (zeroTile[i] + 1) % 10;
                if (value == 0)
                {
                    value = 1;
                }
                firstTile[i] = value;
            }

            int fiveTilesSize = length * 5 * linesCount;
            int[] secondTile = new int[totalNodesForOneTile];
            for (int i = 0; i < totalNodesForOneTile; ++i)
            {
                int value = (firstTile[i] + 1) % 10;
                if (value == 0)
                {
                    value = 1;
                }
                secondTile[i] = value;
            }

            int[] thirdTile = new int[totalNodesForOneTile];
            for (int i = 0; i < totalNodesForOneTile; ++i)
            {
                int value = (secondTile[i] + 1) % 10;
                if (value == 0)
                {
                    value = 1;
                }
                thirdTile[i] = value;
            }

            int[] fourthTile = new int[totalNodesForOneTile];
            for (int i = 0; i < totalNodesForOneTile; ++i)
            {
                int value = (thirdTile[i] + 1) % 10;
                if (value == 0)
                {
                    value = 1;
                }
                fourthTile[i] = value;
            }

            int[] fifthTile = new int[totalNodesForOneTile];
            for (int i = 0; i < totalNodesForOneTile; ++i)
            {
                int value = (fourthTile[i] + 1) % 10;
                if (value == 0)
                {
                    value = 1;
                }
                fifthTile[i] = value;
            }

            int[] sixthTile = new int[totalNodesForOneTile];
            for (int i = 0; i < totalNodesForOneTile; ++i)
            {
                int value = (fifthTile[i] + 1) % 10;
                if (value == 0)
                {
                    value = 1;
                }
                sixthTile[i] = value;
            }

            int[] seventhTile = new int[totalNodesForOneTile];
            for (int i = 0; i < totalNodesForOneTile; ++i)
            {
                int value = (sixthTile[i] + 1) % 10;
                if (value == 0)
                {
                    value = 1;
                }
                seventhTile[i] = value;
            }

            int[] eighthTile = new int[totalNodesForOneTile];
            for (int i = 0; i < totalNodesForOneTile; ++i)
            {
                int value = (seventhTile[i] + 1) % 10;
                if (value == 0)
                {
                    value = 1;
                }
                eighthTile[i] = value;
            }

            List<int[]> tiles = new List<int[]>() { zeroTile, firstTile, secondTile, thirdTile, fourthTile, fifthTile, sixthTile, seventhTile, eighthTile };

            int lengthOfRow = length * 5;
            int maxX = lengthOfRow;
            int maxY = maxX;
            int[,] tiles5x5 = new int[maxX, maxY];
            for (int i = 0; i < 5; ++i)
            {
                var first = tiles[i];
                var second = tiles[i + 1];
                var third = tiles[i + 2];
                var fourth = tiles[i + 3];
                var fifth = tiles[i + 4];
                int counter = 0;
                for (int x = 0; x < linesCount; ++x)
                {
                    for (int y = 0; y < length; ++y)
                    {
                        //Console.WriteLine(newX);
                        tiles5x5[x + i * length, y] = first[counter];
                        tiles5x5[x + i * length, y + length] = second[counter];
                        tiles5x5[x + i * length, y + length * 2] = third[counter];
                        tiles5x5[x + i * length, y + length * 3] = fourth[counter];
                        tiles5x5[x + i * length, y + length * 4] = fifth[counter];
                        counter++;
                    }
                }
            }

            string fileName = @"TestFiles\day15b.txt";
            using (StreamWriter streamWriter = new StreamWriter(fileName, false))
            {
                for (int x = 0; x < maxX; ++x)
                {
                    for (int y = 0; y < maxY; ++y)
                    {
                        if (y < maxY - 1)
                        {
                            var value = tiles5x5[x, y];
                            //Console.Write($"{value}");
                            streamWriter.Write($"{value}");
                        }
                        else
                        {
                            var value = tiles5x5[x, y];
                            //Console.Write($"{value}\n");
                            streamWriter.Write($"{value}\n");
                        }
                    }
                }
                streamWriter.Close();
            }

            List<string> lines2 = Utility.GetLines(fileName);
            int[] temp;
            var edges5x5 = GetEdgesAndTile(lines2, out temp);
            int totalNodesCount = maxX * maxY;
            Graph graph = new Graph(edges5x5, totalNodesCount);
            int source = 0, dest = totalNodesCount - 1;
            long result = FindLeastPathCost(graph, source, dest, totalNodesCount);
            Console.WriteLine(result);
            //DEBUGPrintOutGrid(temp, maxX);
        }

        static void AnalyzeDayA(List<string> lines)
        {
            var firstLine = lines[0];
            int length = firstLine.Length;
            int linesCount = lines.Count;
            int totalNodesCount = length * linesCount;
            int[] tile;
            var edges = GetEdgesAndTile(lines, out tile);
            Graph graph = new Graph(edges, totalNodesCount);
            int source = 0, dest = totalNodesCount - 1;
            long result = FindLeastPathCost(graph, source, dest, totalNodesCount);
            Console.WriteLine(result);
        }

        static HashSet<Edge> GetEdgesAndTile(List<string> lines, out int[] tile)
        {
            var firstLine = lines[0];
            int length = firstLine.Length;
            int linesCount = lines.Count;
            int nodeAmount = length * linesCount;
            tile = new int[nodeAmount];
            HashSet<Edge> edges = new HashSet<Edge>();
            for (int x = 0; x < linesCount; ++x)
            {
                var line = lines[x];
                for (int y = 0; y < length; ++y)
                {
                    int position = x * length + y;
                    // TODO: Remove this debug code when done.
                    bool debugTest = false;
                    if (x - 1 > -1)
                    {
                        int aboveXPosition = (x - 1) * length + y;
                        int weight = tile[aboveXPosition];
                        Edge newEdge = new Edge(position, aboveXPosition, weight);
                        debugTest = edges.Add(newEdge);
                        if (!debugTest)
                        {
                            Console.WriteLine("Encounter false for edge");
                        }
                    }
                    if (x + 1 < linesCount)
                    {
                        int belowXPosition = (x + 1) * length + y;
                        tile[belowXPosition] = Convert.ToInt32(lines[x + 1][y].ToString());
                        int weight = tile[belowXPosition];
                        Edge newEdge = new Edge(position, belowXPosition, weight);
                        debugTest = edges.Add(newEdge);
                        if (!debugTest)
                        {
                            Console.WriteLine("Encounter false for edge");
                        }
                    }
                    if (y - 1 > -1)
                    {
                        int leftYPosition = position - 1;
                        int weight = tile[leftYPosition];
                        Edge newEdge = new Edge(position, leftYPosition, weight);
                        debugTest = edges.Add(newEdge);
                        if (!debugTest)
                        {
                            Console.WriteLine("Encounter false for edge");
                        }
                    }
                    if (y + 1 < length)
                    {
                        int rightYPosition = position + 1;
                        tile[rightYPosition] = Convert.ToInt32(line[y + 1].ToString());
                        int weight = tile[rightYPosition];
                        Edge newEdge = new Edge(position, rightYPosition, weight);
                        debugTest = edges.Add(newEdge);
                        if (!debugTest)
                        {
                            Console.WriteLine("Encounter false for edge");
                        }
                    }
                    tile[position] = Convert.ToInt32(line[y].ToString());
                }
            }

            //DEBUGPrintOutGrid(grid);

            return edges;
        }

        static HashSet<Edge> GetEdgesFromTile(int[] tile, int rowCount, int columnLength)
        {
            HashSet<Edge> edges = new HashSet<Edge>();
            for (int i = 0; i < tile.Length; ++i)
            {
                int x = i / columnLength;
                int y = i % columnLength;
                int position = i;
                if (x - 1 > -1)
                {
                    int aboveXPosition = (x - 1) * columnLength + y;
                    int weight = tile[aboveXPosition];
                    Edge newEdge = new Edge(position, aboveXPosition, weight);
                }
                if (x + 1 < rowCount)
                {
                    int belowXPosition = (x + 1) * columnLength + y;
                    int weight = tile[belowXPosition];
                    Edge newEdge = new Edge(position, belowXPosition, weight);
                }
                if (y - 1 > -1)
                {
                    int leftYPosition = x * columnLength + y - 1;
                    int weight = tile[leftYPosition];
                    Edge newEdge = new Edge(position, leftYPosition, weight);
                }
                if (y + 1 < columnLength)
                {
                    int rightYPosition = x * columnLength + y + 1;
                    int weight = tile[rightYPosition];
                    Edge newEdge = new Edge(position, rightYPosition, weight);
                }
            }
            return edges;
        }

        static void DEBUGPrintOutGrid(int[] grid, int columnLength)
        {
            int priorX = 0;
            for (int i = 0; i < grid.Length; ++i)
            {
                int x = i / columnLength;
                int y = i % columnLength;
                if (x == priorX)
                {
                    Console.Write($"{grid[i]}");
                }
                else
                {
                    priorX = x;
                    Console.Write($"\n{grid[i]}");
                }
            }
            Console.WriteLine();
        }

        class Edge
        {
            public int Source, Dest, Weight;

            public Edge(int source, int dest, int weight)
            {
                this.Source = source;
                this.Dest = dest;
                this.Weight = weight;
            }
        }

        // Perform BFS on the graph starting from vertex source
        static long FindLeastPathCost(Graph graph, int source, int dest, int n)
        {
            // stores vertex is discovered in BFS traversal or not
            List<bool> discovered = new List<bool>();

            // `predecessor[]` stores predecessor information. It is used
            // to trace a least-cost path from the destination back to the source.
            List<int> predecessor = new List<int>();
            for (int i = 0; i < 9 * n; ++i)
            {
                discovered.Add(false);
                predecessor.Add(-1);
            }

            // mark the source vertex as discovered
            discovered[source] = true;

            // create a queue for doing BFS and enqueue source vertex
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(source);
 
            // loop till queue is empty
            while (queue.Count > 0)
            {
                // dequeue front node
                int curr = queue.Dequeue();
 
                // if destination vertex is reached
                if (curr == dest)
                {
                    int cost = -1; 
                    GetLessCostPath(predecessor, dest, ref cost, n);
                    return cost;
                }
 
                // do for every adjacent edge of the current vertex
                foreach (int v in graph.AdjacencyList[curr])
                {
                    if (!discovered[v])
                    {
                        // mark it as discovered and enqueue it
                        discovered[v] = true;
                        queue.Enqueue(v);
 
                        // set `curr` as the predecessor of vertex `v`
                        predecessor[v] = curr;
                    }
                }
            }

            return -1;
        }

        // Recursive function to print the path of a given vertex `v` from the source vertex
        static void GetLessCostPath(List<int> predecessor, int v, ref int cost, int n)
        {
            if (v < 0) 
            {
                return;
            }

            GetLessCostPath(predecessor, predecessor[v], ref cost, n);
            cost++;
        }

        class Graph
        {
            // A list of lists to represent an adjacency list
            public List<List<int>> AdjacencyList;

            // Constructor
            public Graph(HashSet<Edge> edges, int n)
            {
                AdjacencyList = new List<List<int>>(9 * n);

                for (int i = 0; i < 9 * n; i++)
                {
                    AdjacencyList.Add(new List<int>());
                }

                // add edges to the directed graph
                foreach (Edge edge in edges)
                {
                    int v = edge.Source;
                    int u = edge.Dest;
                    int weight = edge.Weight;

                    if (weight == 9)
                    {
                        AdjacencyList[v].Add(v + n);
                        AdjacencyList[v + n].Add(v + 2 * n);
                        AdjacencyList[v + 2 * n].Add(v + 3 * n);
                        AdjacencyList[v + 3 * n].Add(v + 4 * n);
                        AdjacencyList[v + 4 * n].Add(v + 5 * n);
                        AdjacencyList[v + 5 * n].Add(v + 6 * n);
                        AdjacencyList[v + 6 * n].Add(v + 7 * n);
                        AdjacencyList[v + 7 * n].Add(v + 8 * n);
                        AdjacencyList[v + 8 * n].Add(u);
                    }
                    else if (weight == 8)
                    {
                        AdjacencyList[v].Add(v + n);
                        AdjacencyList[v + n].Add(v + 2 * n);
                        AdjacencyList[v + 2 * n].Add(v + 3 * n);
                        AdjacencyList[v + 3 * n].Add(v + 4 * n);
                        AdjacencyList[v + 4 * n].Add(v + 5 * n);
                        AdjacencyList[v + 5 * n].Add(v + 6 * n);
                        AdjacencyList[v + 6 * n].Add(v + 7 * n);
                        AdjacencyList[v + 7 * n].Add(u);
                    }
                    else if (weight == 7)
                    {
                        AdjacencyList[v].Add(v + n);
                        AdjacencyList[v + n].Add(v + 2 * n);
                        AdjacencyList[v + 2 * n].Add(v + 3 * n);
                        AdjacencyList[v + 3* n].Add(v + 4 * n);
                        AdjacencyList[v + 4 * n].Add(v + 5 * n);
                        AdjacencyList[v + 5 * n].Add(v + 6 * n);
                        AdjacencyList[v + 6 * n].Add(u);
                    }
                    else if (weight == 6)
                    {
                        AdjacencyList[v].Add(v + n);
                        AdjacencyList[v + n].Add(v + 2 * n);
                        AdjacencyList[v + 2 * n].Add(v + 3 * n);
                        AdjacencyList[v + 3 * n].Add(v + 4 * n);
                        AdjacencyList[v + 4 * n].Add(v + 5 * n);
                        AdjacencyList[v + 5 * n].Add(u);
                    }
                    else if (weight == 5)
                    {
                        AdjacencyList[v].Add(v + n);
                        AdjacencyList[v + n].Add(v + 2 * n);
                        AdjacencyList[v + 2 * n].Add(v + 3 * n);
                        AdjacencyList[v + 3 * n].Add(v + 4 * n);
                        AdjacencyList[v + 4 * n].Add(u);
                    }
                    else if (weight == 4)
                    {
                        AdjacencyList[v].Add(v + n);
                        AdjacencyList[v + n].Add(v + 2 * n);
                        AdjacencyList[v + 2 * n].Add(v + 3 * n);
                        AdjacencyList[v + 3 * n].Add(u);
                    }

                    // create two new vertices, `v+n` and `v+2×n`, if the edge's weight is `3x`
                    // Also, split edge (v, u) into (v, v+n), (v+n, v+2N) and (v+2N, u),
                    // each having weight `x`.
                    else if (weight == 3)
                    {
                        AdjacencyList[v].Add(v + n);
                        AdjacencyList[v + n].Add(v + 2 * n);
                        AdjacencyList[v + 2 * n].Add(u);
                    }

                    // create one new vertex `v+n` if the weight of the edge is `2x`.
                    // Also, split edge (v, u) into (v, v+n), (v+n, u) each having weight `x`
                    else if (weight == 2)
                    {
                        AdjacencyList[v].Add(v + n);
                        AdjacencyList[v + n].Add(u);
                    }

                    // no splitting is needed if the edge weight is `1x`
                    else
                    {
                        AdjacencyList[v].Add(u);
                    }
                }
            }
        }
    }
}
