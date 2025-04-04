using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Menus;

namespace Exercise_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Bare minimum to get the program running.
            // I actually would like to have a separate class, but the name
            // Program was already taken.
            Execute();
        }

        /// <summary>
        /// This is the "hard working" method for execution. It shows the main menu
        /// </summary>
        private static void Execute()
        {
            // Create the menu
            Dictionary<string, Func<uint>> MainMenu = new Dictionary<string, Func<uint>>
                {
                    { "Ungdom eller pensionär", YouthOrSeniorCitizen },
                    { "Gruppberäkning", PartyCalculation },
                    { "Upprepning x10", RepeatTenTimes },
                    { "Tredje ordet", ThirdWord },
                };
            // Now, show the menu
            Menu.ShowMenu(MainMenu);
        }

        /// <summary>
        /// Selection 1: This method calculates the price for a ticket based on the age of the user.
        /// </summary>
        /// <remarks>
        /// I defy the exercise, and use an uint, as no one can be less than 0 years old.
        /// </remarks>
        /// <returns>The ticket price for a person of the entered age</returns>
        private static uint YouthOrSeniorCitizen()
        {
            // I could have used an if-then-else statement, but switch-case is 
            // highly powerful in C#, so why not use it? (Actually, an if-then-else 
            // is probably faster, but speed isn't an issue here.)
            switch (GetUInt("Vad är din ålder: ")) {
                case < 5:
                case > 100:
                    return ShowAndTell(Rates.Freebies);
                case < 20:
                    return ShowAndTell(Rates.Youth);
                case > 64:
                    return ShowAndTell(Rates.SeniorCitizen);
                default:
                    return ShowAndTell(Rates.Normal);   
            }
        }

        /// <summary>
        /// This method shows the rate for a ticket, and returns the price.
        /// </summary>
        /// <param name="rate">Indication of thw age of the person</param>
        /// <returns>The ticket price based on the rate given</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws an exception if Rates is extended in 
        /// the future and this method doesn't get updated</exception>
        static uint ShowAndTell(Rates rate)
        {
            switch(rate)
            {
                case Rates.Freebies:
                    Console.Write("Gratis! ");
                    break;
                case Rates.Youth:
                    Console.Write("Ungdomspris! ");
                    break;
                case Rates.SeniorCitizen:   
                    Console.Write("Seniorpris! ");
                    break;
                case Rates.Normal:  
                    Console.Write("Normalpris! ");
                    break;
                default:
                    // This should never happen, as the enum is used to select the rate
                    throw new ArgumentOutOfRangeException(nameof(rate), rate, null);
            }
            // Display the price
            Console.WriteLine($"({(uint)rate}kr)");
            // Return the price
            return (uint)rate;
        }

        /// <summary>
        /// Selection 2: Calculate the total price for a group of people.
        /// </summary>
        /// <returns>The grand total for the whole group</returns>
        private static uint PartyCalculation()
        {
            uint sum = 0;
            uint partySize = GetUInt("Hur stort sällskap: ");

            // Loop as many times as there are people in the group,
            // decreasing the counter each time.
            for (; partySize > 0; partySize--)
            {
                // Increase the sum for each person
                sum += YouthOrSeniorCitizen();
            }
            // Display the total price...
            Console.WriteLine($"Totalt pris: {sum}kr");
            // And return it
            return sum; 
        }

        /// <summary>
        /// Selection 3: This method asks the user for a string and repeats it 10 times.
        /// </summary>
        /// <returns>Always return 0 (zero) to follow the Func<> definition in the menu</returns>
        private static uint RepeatTenTimes()
        {
            // Ask for, and retrieve, a string from the user
            Console.WriteLine("Skriv en text som ska upprepas 10 gånger:");
            string text;
            do
            {
                // For beauty, remove leading and trailing spaces with Trim()
                text = Console.ReadLine().Trim();
                // Check for the strings validity
                if (string.IsNullOrWhiteSpace(text))
                    Console.WriteLine("Texten får inte vara tom eller bara bestå av mellanslag.");
            } while (string.IsNullOrWhiteSpace(text));

            // Loop ten times, as requested, printing the text each time
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"{i + 1}. {text}" + ((i < 9) ? ", " : ""));
            }
            // A final newline for beauty
            Console.WriteLine();
            // Return a dummy value
            return 0;
        }

        /// <summary>
        /// Selection 4: This method asks the user for a string and returns the third word.
        /// </summary>
        /// <returns>Always return 0, to conform to the Func<> definition in menu</returns>
        private static uint ThirdWord()
        {
            Console.WriteLine("Skriv en valfri text med minst tre ord:");
            string[] words;
            // Create a regex to split the string at whitespace
            Regex regex = new(@"\s+");
            // Repeat until the user enters a valid string
            while (true)
            {
                // Ask for a string and trim it
                string text = Console.ReadLine().Trim();
                // Split the string at whitespace
                words = regex.Split(text);
                // Check for the strings validity
                if (words.Length < 3)
                    Console.WriteLine("Mening måste innehålla minst tre ord.");
                else
                    break;
            }
            // Display the third word
            Console.WriteLine($"Tredje ordet är: {words[2]}");
            // Return a dummy value
            return 0;
        }

        /// <summary>
        /// This method asks the user for a string and returns it as an unsigned integer.   
        /// </summary>
        /// <param name="prompt">The prompt to display to the user</param>
        /// <returns>The unsigned integer converted from the user input</returns>
        private static uint GetUInt(string prompt)
        {
            uint result = 0;
            // Repeat until the user enters a valid number
            while (true)
            {
                // Display the prompt
                Console.Write($"{prompt}: ");
                // Ask for user input, and try to convert it
                if (uint.TryParse(Console.ReadLine(), out result))
                    return result;
                else
                    Console.WriteLine("Ogiltigt nummer. Försök igen.");
            }
        }
    }
}
