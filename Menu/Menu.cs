namespace Menus
{
    public static class Menu
    {
        private static Stack<Dictionary<string, Func<uint>>> _menuStack = new Stack<Dictionary<string, Func<uint>>>();
        public static void ShowMenu(Dictionary<string, Func<uint>> menu)
        {
            if (menu.Count > 9)
                throw new ArgumentException("Menyn kan inte ha fler än 9 alternativ.");

            _menuStack.Push(menu);

            while (true)
            {
                Console.WriteLine((_menuStack.Count > 1) ? "Undermeny " + (_menuStack.Count - 1) : "Huvudmeny");
                Console.WriteLine(new string('=', 9));
                Console.WriteLine("Välj ett alternativ:");
                int i = 1;
                foreach (var item in menu)
                {
                    Console.WriteLine($"{i++}. {item.Key}");
                }
                Console.WriteLine("0. " + ((_menuStack.Count > 1) ? "Tillbaka" : "Avsluta"));

                while (true)
                {
                    int choice;
                    if (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 9)
                    {
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                    }
                    else
                    {
                        if (choice == 0)
                        {
                            if (_menuStack.Count > 1)
                            {
                                // Remove the current menu from the stack
                                _menuStack.Pop();
                                // 
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
