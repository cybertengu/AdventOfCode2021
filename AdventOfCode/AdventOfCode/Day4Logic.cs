namespace AdventOfCode
{
    internal class Day4Logic
    {
        public static void AnalyzeDay4(string fileName)
        {
            Console.WriteLine("Start of day 4");
            var gameA = ReadFile(fileName);
            AnalyzeDay4A(gameA);
            var gameB = ReadFile(fileName);
            AnalyzeDay4B(gameB);
            Console.WriteLine("End of day 4");
        }

        internal class BingoGame
        {
            public string? Inputs { get; set; } = string.Empty;
            public List<BingoBoard> BingoBoards { get; set; } = new List<BingoBoard>();
        }

        internal class BingoBoard
        {
            public bool[,] MatchesGrid = new bool[5, 5] 
            {
                { false, false, false, false, false },
                { false, false, false, false, false },
                { false, false, false, false, false },
                { false, false, false, false, false },
                { false, false, false, false, false }
            };
            public int[,] Board = new int[5, 5]
            {
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 }
            };
            public int MatchesCounter = 0;
        }

        static void AnalyzeDay4B(BingoGame bingoGame)
        {
            var game = bingoGame;
            var inputs = game.Inputs?.Split(',');
            var winningBoard = new BingoBoard();
            int lastInput = -1;
            for (int i = 0; i < inputs?.Length; ++i)
            {
                string? input = inputs[i];
                int currentValue = Convert.ToInt32(input);
                var boardIndexToRemove = new List<int>();

                for (int index = 0; index < game.BingoBoards.Count; ++index)
                {
                    BingoBoard bingoBoard = game.BingoBoards[index];
                    for (int x = 0; x < 5; ++x)
                    {
                        bool isMatchFound = false;
                        for (int y = 0; y < 5; ++y)
                        {
                            if (currentValue == bingoBoard.Board[x, y])
                            {
                                bingoBoard.MatchesGrid[x, y] = true;
                                bingoBoard.MatchesCounter += 1;
                                game.BingoBoards[index] = bingoBoard;
                                isMatchFound = true;
                                if (bingoBoard.MatchesCounter > 4)
                                {
                                    if (bingoBoard.MatchesGrid[x, 0] && bingoBoard.MatchesGrid[x, 1] &&
                                        bingoBoard.MatchesGrid[x, 2] && bingoBoard.MatchesGrid[x, 3] &&
                                        bingoBoard.MatchesGrid[x, 4])
                                    {
                                        winningBoard = bingoBoard;
                                        lastInput = currentValue;
                                        boardIndexToRemove.Add(index);
                                        break;
                                    }
                                    else if (bingoBoard.MatchesGrid[0, y] &&
                                        bingoBoard.MatchesGrid[1, y] &&
                                        bingoBoard.MatchesGrid[2, y] &&
                                        bingoBoard.MatchesGrid[3, y] &&
                                        bingoBoard.MatchesGrid[4, y])
                                    {
                                        winningBoard = bingoBoard;
                                        lastInput = currentValue;
                                        boardIndexToRemove.Add(index);
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        if (isMatchFound)
                        {
                            break;
                        }
                    }
                }

                for (int j = boardIndexToRemove.Count - 1; j > -1; --j)
                {
                    int index = boardIndexToRemove[j];
                    game.BingoBoards.RemoveAt(index);
                }
            }

            int sum = 0;
            for (int x = 0; x < 5; ++x)
            {
                for (int y = 0; y < 5; ++y)
                {
                    bool wasMatchedAlready = winningBoard.MatchesGrid[x, y];
                    if (!wasMatchedAlready)
                    {
                        sum += winningBoard.Board[x, y];
                    }
                }
            }

            //Console.WriteLine($"Sum {sum} Last Input {lastInput}");
            int result = sum * lastInput;
            Console.WriteLine(result);
        }

        static void AnalyzeDay4A(BingoGame bingoGame)
        {
            var game = bingoGame;
            var inputs = game.Inputs?.Split(',');
            var winningBoard = new BingoBoard();
            bool foundWinningBoard = false;
            int lastInput = -1;
            for (int i = 0; i < inputs?.Length; i++)
            {
                string? input = inputs[i];
                int currentValue = Convert.ToInt32(input);

                for (int index = 0; index < game.BingoBoards.Count; ++index)
                {
                    if (foundWinningBoard)
                    {
                        break;
                    }

                    BingoBoard bingoBoard = game.BingoBoards[index];
                    for (int x = 0; x < 5; ++x)
                    {
                        bool isMatchFound = false;
                        for (int y = 0; y < 5; ++y)
                        {
                            if (currentValue == bingoBoard.Board[x, y])
                            {
                                bingoBoard.MatchesGrid[x, y] = true;
                                bingoBoard.MatchesCounter += 1;
                                game.BingoBoards[index] = bingoBoard;
                                isMatchFound = true;
                                if (bingoBoard.MatchesCounter > 4)
                                {
                                    if (bingoBoard.MatchesGrid[x, 0] && bingoBoard.MatchesGrid[x, 1] &&
                                        bingoBoard.MatchesGrid[x, 2] && bingoBoard.MatchesGrid[x, 3] &&
                                        bingoBoard.MatchesGrid[x, 4])
                                    {
                                        winningBoard = bingoBoard;
                                        foundWinningBoard = true;
                                        lastInput = currentValue;
                                        break;
                                    }
                                    else if (bingoBoard.MatchesGrid[0, y] &&
                                        bingoBoard.MatchesGrid[1, y] &&
                                        bingoBoard.MatchesGrid[2, y] &&
                                        bingoBoard.MatchesGrid[3, y] &&
                                        bingoBoard.MatchesGrid[4, y])
                                    {
                                        winningBoard = bingoBoard;
                                        foundWinningBoard = true;
                                        lastInput = currentValue;
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        if (isMatchFound)
                        {
                            break;
                        }
                    }
                    if (foundWinningBoard)
                    {
                        break;
                    }
                }
                if (foundWinningBoard)
                {
                    break;
                }
            }

            int sum = 0;
            for (int x = 0; x < 5; ++x)
            {
                for (int y = 0; y < 5; ++y)
                {
                    bool wasMatchedAlready = winningBoard.MatchesGrid[x, y];
                    if (!wasMatchedAlready)
                    {
                        sum += winningBoard.Board[x, y];
                    }
                }
            }

            //Console.WriteLine($"Sum {sum} Last Input {lastInput}");
            int result = sum * lastInput;
            Console.WriteLine(result);
        }

        static BingoGame ReadFile(string fileName)
        {
            BingoGame game = new BingoGame();

            using (StreamReader streamReader = new StreamReader(fileName))
            {
                string? line;
                game.Inputs = streamReader?.ReadLine();

                while ((line = streamReader?.ReadLine()) != null)
                {
                    BingoBoard board = new BingoBoard();
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    for (int x = 0; x < 5; ++x)
                    {
                        var tokens = line?.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        for (int y = 0; y < 5; ++y)
                        {
                            var token = tokens?[y];
                            board.Board[x, y] = Convert.ToInt32(token);
                        }
                        line = streamReader?.ReadLine();
                    }
                    game.BingoBoards.Add(board);
                }

                streamReader?.Close();
            }

            return game;
        }
    }
}
