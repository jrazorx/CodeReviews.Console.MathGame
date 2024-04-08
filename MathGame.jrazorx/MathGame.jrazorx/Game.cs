using System.Diagnostics;
using static MathGame.jrazorx.Menu;

namespace MathGame.jrazorx
{
    internal class Game
    {
        private static Random random = new();

        internal static int CurrentGameNumber { get; private set; } = 0;

        internal GameMode Mode { get; }

        // The random numbers used for the game
        internal int FirstNumber { get; }
        internal int SecondNumber { get; }

        internal int CorrectAnswer { get; }

        internal string Operation { get; }

        internal int PlayerAnswer { get; private set; }

        internal bool IsWin { get; private set; }

        internal TimeSpan TimeTakenToAnswer { get; private set; }

        internal Game(GameMode gameMode)
        {
            if (!Enum.IsDefined(typeof(GameMode), gameMode))
            {
                throw new ArgumentException("Invalid game mode");
            }
            Mode = gameMode;
            CurrentGameNumber++;

            switch (gameMode)
            {
                case GameMode.Addition:
                    FirstNumber = random.Next(0, 10);
                    SecondNumber = random.Next(0, 10);
                    CorrectAnswer = FirstNumber + SecondNumber;
                    Operation = $"{FirstNumber} + {SecondNumber}";
                    break;

                case GameMode.Subtraction:
                    FirstNumber = random.Next(0, 10);
                    SecondNumber = random.Next(0, 10);
                    CorrectAnswer = FirstNumber - SecondNumber;
                    Operation = $"{FirstNumber} - {SecondNumber}";
                    break;

                case GameMode.Multiplication:
                    FirstNumber = random.Next(0, 10);
                    SecondNumber = random.Next(0, 10);
                    CorrectAnswer = FirstNumber * SecondNumber;
                    Operation = $"{FirstNumber} x {SecondNumber}";
                    break;

                case GameMode.Division:
                    do
                    {
                        FirstNumber = random.Next(0, 101);
                        SecondNumber = random.Next(1, 101);
                    } while (FirstNumber % SecondNumber != 0);
                    CorrectAnswer = FirstNumber / SecondNumber;
                    Operation = $"{FirstNumber} / {SecondNumber}";
                    break;

                default:
                    throw new ArgumentException("Invalid game mode letter input");
            }
        }

        internal void Play()
        {
            Console.Clear();
            Console.WriteLine(@$"{Mode} Game
--------------------");
            Console.WriteLine($"{Operation} = ?");

            PlayerAnswer = GetPlayerAnswerInput();

            WinOrLose(PlayerAnswer);
        }

        // Get the player input for the answer of the game until it is a valid number
        private int GetPlayerAnswerInput()
        {
            string? readResult;

            Stopwatch stopwatch = new ();
            stopwatch.Start();

            while (true)
            {
                readResult = Console.ReadLine();
                if (readResult == null)
                {
                    Console.WriteLine("Please enter a valid number");
                    continue;
                }
                try
                {
                    int playerAnswerInput = int.Parse((readResult ?? "").Trim());

                    stopwatch.Stop();
                    TimeTakenToAnswer = stopwatch.Elapsed;

                    return playerAnswerInput;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Invalid number : too big ! Please enter a valid number");
                }
                catch
                {
                    Console.WriteLine("Please enter a valid number");
                }
            }
        }

        private void WinOrLose(int playerAnswer)
        {
            if (playerAnswer == CorrectAnswer)
            {
                Console.WriteLine("You win !");
                IsWin = true;
            }
            else
            {
                Console.WriteLine("You lose ...");
                Console.WriteLine($"The answer was {CorrectAnswer}");
                IsWin = false;
            }
            PrintTimeTakenToAnswer(TimeTakenToAnswer);
        }

        private static void PrintTimeTakenToAnswer(TimeSpan timeTakenToAnswer)
        {
            string result = "Time taken to answer: ";

            if (timeTakenToAnswer.TotalMilliseconds < 1000)
                result += $"{timeTakenToAnswer.Milliseconds} milliseconds";
            else if (timeTakenToAnswer.TotalSeconds < 60)
                result += $"{timeTakenToAnswer.TotalSeconds:N3} seconds";
            else
                result += "more than a minute";

            Console.WriteLine(result);
        }

        internal enum GameMode
        {
            Addition        = 'A',
            Multiplication  = 'M',
            Division        = 'D',
            Subtraction     = 'S'
        }
    }
}