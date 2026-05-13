using System;
namespace StudentProject
{
    internal class MainMenuScreen
    {

        enum enOptions { Doctor = 1, Staff = 2, Student = 3, exit=4 }

        static void _PeformOption(enOptions option)
        {
            if (option == enOptions.Doctor)
            {
                Console.Clear();
                DoctorScreen.DoctorOptionScreen();
            }

            else if (option == enOptions.Staff)
            {
                Console.Clear();
                stuffScreen.StaffOptionScreen();
            }
            else if (option == enOptions.Student)
            {
                Console.Clear();
                StudentOption.SudentOptionScreen();

            }
            else if (option == enOptions.exit)
            {
                Console.Clear();
                Console.WriteLine("\n\nProgramm Is Closed \n\n");
            }

        }

        static public void ShowMainMenuScreen()
        {
            Console.Clear();
            Console.WriteLine("___________________________ Main Menu Screen ___________________________\n");
            Console.WriteLine("Who Are You: ");
            Console.WriteLine("\t [1]. Doctor.");
            Console.WriteLine("\t [2]. Staff.");
            Console.WriteLine("\t [3]. Student.");
            Console.WriteLine("\t [4]. Exit");
            Console.WriteLine("______________________________________________________________________________\n");

            int OP = ValidationInput.ReadIntNumberBetween(1,4, "Enter Option ");
            _PeformOption((enOptions)OP);
        }
    }
}
