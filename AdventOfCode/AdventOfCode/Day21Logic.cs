using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace AdventOfCode
{
    internal class Day21Logic
    {
        public static void AnalyzeDay(string fileName)
        {
            Console.WriteLine("Start of day 21");
            AnalyzeDayA(fileName);
            AnalyzeDayB(fileName);
            Console.WriteLine("End of day 21");
        }

        const char space = ' ';

        static void AnalyzeDayA(string fileName)
        {
            var lines = Utility.GetLines(fileName);
            var tokens = lines[0].Split(space);
            int lastIndex = tokens.Length - 1;
            int player1Position = Convert.ToInt32(tokens[lastIndex]) - 1;
            tokens = lines[1].Split(space);
            lastIndex = tokens.Length - 1;
            int player2Position = Convert.ToInt32(tokens[lastIndex]) - 1;
            int dice = 1;
            int[] rolls = new int[3];
            int forwardCounter;
            int newLocation;
            int score1 = 0;
            int score2 = 0;
            int rollCounter = 0;

            while (true)
            {
                rolls[0] = dice++;
                if (dice > 100)
                {
                    dice = 1;
                }
                rolls[1] = dice++;
                if (dice > 100)
                {
                    dice = 1;
                }
                rolls[2] = dice++;
                if (dice > 100)
                {
                    dice = 1;
                }
                rollCounter += 3;
                forwardCounter = rolls[0] + rolls[1] + rolls[2];
                newLocation = ((player1Position + forwardCounter) % 10) + 1;
                score1 += newLocation;
                player1Position = newLocation - 1;

                if (score1 >= 1000)
                {
                    break;
                }

                rolls[0] = dice++;
                if (dice > 100)
                {
                    dice = 1;
                }
                rolls[1] = dice++;
                if (dice > 100)
                {
                    dice = 1;
                }
                rolls[2] = dice++;
                if (dice > 100)
                {
                    dice = 1;
                }
                rollCounter += 3;
                forwardCounter = rolls[0] + rolls[1] + rolls[2];
                newLocation = ((player2Position + forwardCounter) % 10) + 1;
                score2 += newLocation;
                player2Position = newLocation - 1;

                if (score2 >= 1000)
                {
                    break;
                }
            }

            long finalResult = 0;
            if (score1 > score2)
            {
                finalResult = score2 * rollCounter;
            }
            else
            {
                finalResult = score1 * rollCounter;
            }
            Console.WriteLine(finalResult);
        }

        static void AnalyzeDayB(string fileName)
        {
            var lines = Utility.GetLines(fileName);
            var tokens = lines[0].Split(space);
            int lastIndex = tokens.Length - 1;
            int player1Position = Convert.ToInt32(tokens[lastIndex]) - 1;
            tokens = lines[1].Split(space);
            lastIndex = tokens.Length - 1;
            int player2Position = Convert.ToInt32(tokens[lastIndex]) - 1;
            int score1 = 0;
            int score2 = 0;
            CountWins = new Dictionary<PlayerState, WinState>(new PlayerState.EqualityComparer());
            var winCount = SolveDiracDice(player1Position, player2Position, score1, score2);
            //foreach (var win in CountWins)
            //{
            //    var key = win.Key;
            //    var value = win.Value;
            //    Console.WriteLine($"{key.Player1Position} {key.Score1} {key.Player2Position} {key.Score2} {value.Player1Win} {value.Player2Win}");
            //}
            //Console.WriteLine(CountWins.Count);
            long winnerTotal = (winCount.Player1Win > winCount.Player2Win) ? winCount.Player1Win : winCount.Player2Win;
            Console.WriteLine(winnerTotal);
        }

        class WinState
        {
            public long Player1Win = 0;
            public long Player2Win = 0;

            public WinState(long player1Win, long player2Win)
            {
                Player1Win = player1Win;
                Player2Win = player2Win;
            }
        }

        class PlayerState
        {
            public int Score1 = 0;
            public int Player1Position = 0;
            public int Score2 = 0;
            public int Player2Position = 0;

            public PlayerState(int player1Position, int player2Position, int score1, int score2)
            {
                Score1 = score1;
                Player1Position = player1Position;
                Score2 = score2;
                Player2Position = player2Position;
            }

            public override bool Equals(object? obj)
            {
                if (obj == null)
                {
                    return false;
                }
                else if (!(obj is PlayerState))
                {
                    return false;
                }
                PlayerState playerState = (PlayerState)obj;
                return this.Player1Position == playerState.Player1Position && 
                    this.Player2Position == playerState.Player2Position &&
                    this.Score2 == playerState.Score2 &&
                    this.Score1 == playerState.Score1;
            }

            public override int GetHashCode()
            {
                return this.Score1 ^ this.Player1Position ^ this.Score2 ^ this.Player2Position;
            }

            public class EqualityComparer : IEqualityComparer<PlayerState>
            {
                public bool Equals(PlayerState x, PlayerState y)
                {
                    return x.Score1 == y.Score1 && x.Player1Position == y.Player1Position && x.Score2 == y.Score2 && x.Player2Position == y.Player2Position;
                }

                public int GetHashCode([DisallowNull] PlayerState x)
                {
                    return (x.Score1 ^ x.Player1Position ^ x.Score2 ^ x.Player2Position);
                }
            }
        }

        static Dictionary<PlayerState, WinState> CountWins;
        const int SCORE = 21;
        static PlayerState currentState = new PlayerState(0, 0, 0, 0);

        static WinState SolveDiracDice(int player1Position, int player2Position, int score1, int score2)
        {
            if (score1 >= SCORE)
            {
                return new WinState(1, 0);
            }
            if (score2 >= SCORE)
            {
                return new WinState(0, 1);
            }

            currentState.Score1 = score1;
            currentState.Score2 = score2;
            currentState.Player2Position = player2Position;
            currentState.Player1Position = player1Position;
            if (CountWins.ContainsKey(currentState))
            {
                //Console.WriteLine("Did this happen?");
                return CountWins[currentState];
            }

            WinState result = new WinState(0, 0);
            int newPosition1 = 0;
            int newScore1 = 0;
            WinState winState;
            for (int i = 1; i <= 3; ++i)
            {
                for (int j = 1; j <= 3; ++j)
                {
                    for (int k = 1; k <= 3; ++k)
                    {
                        newPosition1 = ((player1Position + i + j + k) % 10) + 1;
                        newScore1 = newPosition1 + score1;
                        newPosition1--;
                        //Console.WriteLine($"{i} {j} {k}");// {score2} {newScore1}");
                        winState = SolveDiracDice(player2Position, newPosition1, score2, newScore1);
                        result.Player1Win += winState.Player2Win;
                        result.Player2Win += winState.Player1Win;
                    }
                }
            }

            WinState newResult = new WinState(result.Player1Win, result.Player2Win);
            PlayerState newState = new PlayerState(player1Position, player2Position, score1, score2);
            if (CountWins.ContainsKey(newState))
            {
                CountWins[newState] = newResult;
            }
            else
            {
                CountWins.Add(newState, newResult);
            }
            return result;
        }
    }
}
