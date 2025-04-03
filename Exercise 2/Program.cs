using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Menus;

namespace Exercise_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Execute();
        }

        private static void Execute()
        {
            Dictionary<string, Func<uint>> MainMenu = new Dictionary<string, Func<uint>>
                {
                    { "Ungdom eller pensionär", YouthOrSeniorCitizen },
                    { "Gruppberäkning", PartyCalculation },
                    { "Upprepning x10", RepeatTenTimes },
                    { "Tredje ordet", ThirdWord },
                };
            Menu.ShowMenu(MainMenu);
        }

        private static uint YouthOrSeniorCitizen()
        {
            // I defy the exercise, and use an uint, as no one can be less than 0 years old.
            // 0 (zero) indicates an invalid input.
            uint age = 0;   
            do
            {
                Console.Write("Vad är din ålder: ");
                // Read input, adn convert, all in one go.
                if (!uint.TryParse(Console.ReadLine(), out age))
                    age = 0;    

            } while(age == 0);

            switch (age) {
                case < 5:
                case > 100:
                    Console.WriteLine($"Freebie! ({(uint)Rates.Freebies}kr)");
                    return (uint)Rates.Freebies;    
                case < 20:
                    Console.WriteLine($"Ungdomspris ({(uint)Rates.Youth}kr)");
                    return (uint)Rates.Youth;
                case > 64:
                    Console.WriteLine($"Seniorpris ({(uint)Rates.SeniorCitizen}kr)");
                    return (uint)Rates.SeniorCitizen;
                default:
                    Console.WriteLine($"Normalpris ({(uint)Rates.Normal}kr)");
                    return (uint)Rates.Normal;
            }
        }
        private static uint PartyCalculation()
        {
            uint sum = 0;
            uint partySize = 0;
            do
            {
                Console.Write("Hur stort sällskap: ");
                if (!uint.TryParse(Console.ReadLine(), out partySize))
                    partySize = 0;    
            }
            while (partySize == 0); 

            for (; partySize > 0; partySize--)
            {
                sum += YouthOrSeniorCitizen();
            }
            Console.WriteLine($"Totalt pris: {sum}kr");
            return sum; 
        }

        private static uint RepeatTenTimes()
        {
            Console.WriteLine("Skriv en text som ska upprepas 10 gånger:");
            string text = Console.ReadLine();   

            for (int i = 0; i < 10; i++)
            {
                Console.Write($"{i + 1}. {text}" + ((i < 9) ? ", " : ""));
            }
            Console.WriteLine();
            return 0;
        }

        private static uint ThirdWord()
        {
            Console.WriteLine("Skriv en valfri text med minst tre ord:");
            string[] words;
            while (true)
            {
                string text = Console.ReadLine();
                Regex regex = new Regex(@"\s+");
                words = regex.Split(text.Trim());
                if (words.Length < 3)
                    Console.WriteLine("Mening måste innehålla minst tre ord.");
                else
                    break;
            }
            Console.WriteLine($"Tredje ordet är: {words[2]}");
            return 0;
        }
    }
}
