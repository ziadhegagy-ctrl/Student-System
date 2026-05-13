using System;
namespace StudentProject
{
    internal class ValidationInput
    {
        public static int ReadIntNumberBetween(int min, int max, string Message= "Enter a number between: ")
        {
            int number;
            while (true)
            {
                Console.Write($"{Message} : [{min} , {max}]: ");
                if (int.TryParse(Console.ReadLine(), out number) && number >= min && number <= max)
                {
                    return number;
                }
                Console.Write($"\nInvalid input.\n" +
                    $"Please enter a number between ");
            }
        }
        public static int ReadIntNumberBetween( string Message = "")
        {
            int number;
            while (true)
            {
                Console.Write($"{Message}");
                if (int.TryParse(Console.ReadLine(), out number))
                {
                    return number;
                }
                Console.WriteLine($"Invalid input. Because Invalid Format Please Enter Number..");
            }
        }

        public static double ReadDoubleNumberBetween(double min, double max, string Message = "Enter a number between: ")
        {
            double number;
            while (true)
            {
                Console.Write($"{Message} : [{min} , {max}]: ");
                if (double.TryParse(Console.ReadLine(), out number) && number >= min && number <= max)
                {
                    return number;
                }
                Console.WriteLine($"Invalid input.\n" +
                    $"Please enter a number between [{min} , {max}].");
            }
        }
       
        public static string ReadNonEmptyString(string Message)
        {
            string input;
            while (true)
            {
                Console.Write(Message);
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }
                Console.WriteLine("Invalid input. Please enter a non empty string.");
            }
        }

        public static char ReadCharOption(char[] validOptions, string Message = "Enter an option: ")
        {
            char input;
            while (true)
            {
                Console.Write(Message);
                if (char.TryParse(Console.ReadLine(), out input) && Array.Exists(validOptions, option => option == input))
                {
                    return input;
                }
                Console.WriteLine($"Invalid input. Please enter one of the following options: {string.Join(", ", validOptions)}.");
            }
        }


    }

}
