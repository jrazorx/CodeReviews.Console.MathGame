using System.Text;
using MathGame.jrazorx;

// --------------------------------------------------
// CONSTANTS
// --------------------------------------------------

const string validMenuItems = "ASMDHQ";

// --------------------------------------------------
// VARIABLES
// --------------------------------------------------

string? readResult;

char menuItemChosen;

int gameOccurence = 0;
List<string> gamesHistory = new ();

Game currentGame;

// --------------------------------------------------
// EXECUTION
// --------------------------------------------------

Greetings();
while (true)
{
    DisplayMenu();
    menuItemChosen = ChooseMenuItem();
    MenuItemAction(menuItemChosen);

    Console.WriteLine("Press any key to return to the menu...");
    Console.ReadKey();
    Console.Clear();
}

// --------------------------------------------------
// METHODS
// --------------------------------------------------

void Greetings()
{
    Console.WriteLine("Welcome to the Math Game !\n");
}

void DisplayMenu()
{
    Console.WriteLine(@"Menu
--------------------
A - Addition Game
S - Subtraction Game
M - Multiplication Game
D - Division Game
H - History of previous games
Q - Quit");
}

char ChooseMenuItem()
{
    while (true)
    {
        readResult = Console.ReadLine();

        if (string.IsNullOrEmpty(readResult) || readResult.Trim().Length != 1)
        {
            Console.WriteLine("Please enter a letter corresponding to a menu item.");
            continue;
        }

        char menuItem = readResult.Trim().ToUpper()[0];
        if (validMenuItems.Contains(menuItem))
            return menuItem;
        else
            Console.WriteLine("Invalid option, please try again.");
    }
}

void MenuItemAction(char menuItemChosen)
{
    switch (menuItemChosen)
    {
        case 'A':
        case 'S':
        case 'M':
        case 'D':
            currentGame = new Game(menuItemChosen);
            currentGame.Play();
            SaveGame(currentGame);
            break;

        case 'H':
            DisplayGameHistory(gamesHistory);
            break;

        default:
            Console.WriteLine("Goodbye");
            Environment.Exit(0);
            break;

    }
}

/* Display the List containing the history of previous games played
Example :
#   | GAME           | CHALLENGE | ANSWER     | RESULT
99  | Multiplication | 100 x 100 |      10000 | WIN
100 | Addition       | 1 + 2     | 2000000000 | LOSE
*/
void DisplayGameHistory(List<string> gamesHistory)
{
    Console.Clear();
    Console.WriteLine("#   | GAME           | CHALLENGE | ANSWER     | RESULT | TIME");

    foreach (string game in gamesHistory)
    {
        Console.WriteLine(game);
    }
}

string GameHistoryFormatLine(int gameOccurence, Game currentGame)
{
    StringBuilder gamesHistoryLine = new StringBuilder();

    gamesHistoryLine.AppendFormat("{0, -3} | ", gameOccurence);
    gamesHistoryLine.AppendFormat("{0, -14} | ", currentGame.Mode);
    gamesHistoryLine.AppendFormat("{0, -9} | ", currentGame.Operation);
    gamesHistoryLine.AppendFormat("{0, 10} | ", currentGame.PlayerAnswer);
    gamesHistoryLine.AppendFormat("{0, -6} | ", currentGame.IsWin ? "WIN" : "LOSE");
    gamesHistoryLine.Append(currentGame.TimeTakenToAnswer.TotalMilliseconds < 1000 ? "< 1 second" : "> 1 second");

    return gamesHistoryLine.ToString();
}

void SaveGame(Game currentGame)
{
    try
    {
        gameOccurence++;

        gamesHistory.Add(GameHistoryFormatLine(gameOccurence, currentGame));
    }
    catch (OverflowException)
    {
        Console.WriteLine("You have reached the limit of games that can be saved in one session. From now on, games will not be saved in history");
    }
}