namespace Menus
{
    public static class Menu
    {
        /// <summary>   
        /// Hold a last-in-first-out stack of menus.
        /// </summary>
        private static Stack<Dictionary<string, Func<uint>>> _menuStack = new Stack<Dictionary<string, Func<uint>>>();

        /// <summary>
        /// Shows a menu with a maximum of 9 items. The menu is displayed in a loop until the user selects an option or exits.
        /// </summary>
        /// <param name="menu">The menu definition to display</param>
        /// <exception cref="ArgumentException">Thrown in case there's more than 9 (nine) items in the menu</exception>
        /// <remarks>
        /// Future development includes splitting the menu into submenus, if the menu is too long.
        /// </remarks>
        public static void ShowMenu(Dictionary<string, Func<uint>> menu)
        {
            // Validity check: the menu must have at least one item, and no more than 9 items.
            if (menu == null || menu.Count == 0)
                throw new ArgumentException("Menyn kan inte vara tom.");
            if (menu.Count > 9)
                throw new ArgumentException("Menyn kan inte ha fler än 9 alternativ.");

            // Put the menu on the stack
            _menuStack.Push(menu);

            // Repeat indefinately until the user selects 0 (zero) to exit
            while (true)
            {
                // Display "Huvudmeny" or "Undermeny" depending on the menu stack depth
                Console.WriteLine((_menuStack.Count > 1) ? "Undermeny " + (_menuStack.Count - 1) : "Huvudmeny");
                Console.WriteLine(new string('=', 9));
                Console.WriteLine("Välj ett alternativ:");
                int i = 1;
                // Display the menu items
                foreach (var item in menu)
                {
                    Console.WriteLine($"{i++}. {item.Key}");
                }
                // Add an option to exit, or go back to the previous menu
                Console.WriteLine("0. " + ((_menuStack.Count > 1) ? "Tillbaka" : "Avsluta"));

                // Repeat indefinately until the user selects a valid option
                while (true)
                {
                    int choice;
                    // Ask the user for a value, parsing it to an integer, all in one go.
                    if (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 9)
                    {
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                    }
                    else
                    {
                        // Special case: 0 (zero), go back to the previous menu or exit
                        if (choice == 0)
                        {
                            if (_menuStack.Count > 1)
                            {
                                // Remove the current menu from the stack
                                _menuStack.Pop();
                                // Show the menu on the top of the stack
                                ShowMenu(_menuStack.Peek());
                            }
                            else
                                // Exit the ShowMenu method
                                return;
                        }
                        else
                        {
                            // Execute the selected menu item, ignoring result
                            _ = menu.ElementAt(choice - 1).Value();
                            // Break out of the selection check loop
                            break;
                        }
                    }
                } // while (true)
            } // while (true)
        }
    }
}
