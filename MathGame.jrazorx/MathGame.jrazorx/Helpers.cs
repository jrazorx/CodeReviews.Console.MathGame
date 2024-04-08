using System.Text;

namespace MathGame.jrazorx
{
    internal class Helpers
    {
        internal static List<string> gamesHistory = new();

        internal static void Greetings()
        {
            Console.WriteLine("Welcome to the Math Game !\n");
        }

        /* Display the List containing the history of previous games played
        Example :
        #   | GAME           | CHALLENGE | ANSWER     | RESULT
        99  | Multiplication | 100 x 100 |      10000 | WIN
        100 | Addition       | 1 + 2     | 2000000000 | LOSE
        */
        internal static void DisplayGameHistory(List<string> gamesHistory)
        {
            Console.Clear();
            Console.WriteLine("#   | GAME           | CHALLENGE | ANSWER     | RESULT | TIME");

            foreach (string game in gamesHistory)
            {
                Console.WriteLine(game);
            }
        }

        private static string GameHistoryFormatLine(Game currentGame)
        {
            StringBuilder gamesHistoryLine = new();

            gamesHistoryLine.AppendFormat("{0, -3} | ", Game.CurrentGameNumber);
            gamesHistoryLine.AppendFormat("{0, -14} | ", currentGame.Mode);
            gamesHistoryLine.AppendFormat("{0, -9} | ", currentGame.Operation);
            gamesHistoryLine.AppendFormat("{0, 10} | ", currentGame.PlayerAnswer);
            gamesHistoryLine.AppendFormat("{0, -6} | ", currentGame.IsWin ? "WIN" : "LOSE");
            gamesHistoryLine.Append(currentGame.TimeTakenToAnswer.TotalMilliseconds < 1000 ? "< 1 second" : "> 1 second");

            return gamesHistoryLine.ToString();
        }

        internal static void SaveGame(Game currentGame)
        {
            try
            {
                gamesHistory.Add(GameHistoryFormatLine(currentGame));
            }
            catch (OverflowException)
            {
                Console.WriteLine("You have reached the limit of games that can be saved in one session. From now on, games will not be saved in history");
            }
        }
    }
}
