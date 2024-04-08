using MathGame.jrazorx;

// --------------------------------------------------
// VARIABLES
// --------------------------------------------------

Menu currentMenu = new();

// --------------------------------------------------
// EXECUTION
// --------------------------------------------------

Helpers.Greetings();
while (true)
{
    Menu.ShowMenu();
    currentMenu.ChooseMenuItem();
    Menu.MenuItemAction(currentMenu.MenuItemChosen);

    Console.WriteLine("Press any key to return to the menu...");
    Console.ReadKey();
    Console.Clear();
}