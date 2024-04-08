using static MathGame.jrazorx.Game;

namespace MathGame.jrazorx
{

    internal class Menu
    {
        internal MenuItem MenuItemChosen { get; set; }

        internal static void ShowMenu()
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

        internal void ChooseMenuItem()
        {
            string? readResult;

            while (true)
            {
                readResult = Console.ReadLine();

                if (string.IsNullOrEmpty(readResult) || readResult.Trim().Length != 1)
                {
                    Console.WriteLine("Please enter a letter corresponding to a menu item.");
                    continue;
                }

                char userInputMenuItem = readResult.Trim().ToUpper()[0];
                if (Enum.IsDefined(typeof(MenuItem), (int)userInputMenuItem))
                {
                    MenuItemChosen = (MenuItem)userInputMenuItem;
                    break; // Sortir de la boucle si l'entrée est valide
                }
                else
                {
                    Console.WriteLine("Invalid option, please try again.");
                }
            }
        }

        internal static void MenuItemAction(MenuItem menuItemChosen)
        {
            switch (menuItemChosen)
            {
                case MenuItem.Addition:
                case MenuItem.Multiplication:
                case MenuItem.Division:
                case MenuItem.Subtraction:
                    Game currentGame = new((GameMode)menuItemChosen);
                    currentGame.Play();
                    Helpers.SaveGame(currentGame);
                    break;

                case MenuItem.Historique:
                    Helpers.DisplayGameHistory(Helpers.gamesHistory);
                    break;

                default:
                    Console.WriteLine("Goodbye");
                    Environment.Exit(0);
                    break;

            }
        }

        internal enum MenuItem
        {
            Addition        = 'A',
            Multiplication  = 'M',
            Division        = 'D',
            Subtraction     = 'S',
            Quit            = 'Q',
            Historique      = 'H'
        }
    }
}