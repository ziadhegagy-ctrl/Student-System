using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StudentProject.Course;

namespace StudentProject
{
    internal class StudentOption
    {

         enum enOptions{ AddCourseScreen=1, StudentInformation=2, Main = 3 }

        //-------------ADD Course ------------------
         static void AddCourse(Student CurrentStudent)
        {
            char Answer;
            do
            {
                Course C = new Course();
                GradeComponent grade = new GradeComponent();
                C.CourseCode = ValidationInput.ReadNonEmptyString("Enter Course Code: ");
                C.CourseName = ValidationInput.ReadNonEmptyString("Enter Course Name: ");
                C.CreditHours = ValidationInput.ReadIntNumberBetween(0, 3, "Enter CreditHours: ");

                //!!!!!!!!!!!
                C.Policy = (enPolicy)ValidationInput.ReadIntNumberBetween(1, 2, "Enter Policy , PercentagPolicy:1  , WeightedPolicy:2 : Enter ");

                CurrentStudent.EnorllInCources(C);
                CurrentStudent.SaveAfterModifications();
                Console.WriteLine("\n\nCourse Added Sucssfully -) ");
                Console.Write("\n\nDo You Want Add another Course (Y,N): ");
                Answer = char.Parse(Console.ReadLine());
            } while (Answer == 'Y' || Answer == 'y');
        }
         static void ShowAddCourseScreen()
        {
            Console.WriteLine("___________________________ Add Course Screen ___________________________\n");
            Student CurrentStudent = _GetStudent();
           AddCourse(CurrentStudent);
        }
        //-------------------------------------


        //--------------------Show Student Info--------------
        static Student _GetStudent()
        {
            int ID;

            ID = ValidationInput.ReadIntNumberBetween("Enter Your ID : ");

            Student CurrentStudent;
            while ((CurrentStudent = Student.Find(ID)) == null)
            {
                ID = ValidationInput.ReadIntNumberBetween($" ID : {ID} NOT Founded  Enter Anthour ID: ");
            }

            return CurrentStudent;
        }
        static void ShowStudentInfo()
        {
            Console.WriteLine("___________________________ Student Information Screen ___________________________\n");

            _GetStudent().DisplayInfo();
        }
       //--------------------------------------------------

        //--------------------Show Screen------------------
        static  void _GoBack()
        {
            Console.Write("\nTo Go Back Step , Press Any Key: ");
            Console.ReadKey();
           SudentOptionScreen();
        }
        static  void _PeformOption(enOptions option)
        {
            if (option== enOptions.AddCourseScreen)
            {
                Console.Clear();
                ShowAddCourseScreen();
                _GoBack();
            }
                
            else if(option == enOptions.StudentInformation)
            {
                Console.Clear();
                ShowStudentInfo();
                _GoBack();
            }
            else if (option == enOptions.Main)
            {
                Console.Clear();
                MainMenuScreen.ShowMainMenuScreen();

            }
        }
       static public   void SudentOptionScreen()
        {
            Console.Clear();
            Console.WriteLine("___________________________ Student Option Screen ___________________________\n");

            Console.WriteLine("\t [1]. Add Course Screen.");
            Console.WriteLine("\t [2]. Student Information Screen.");
            Console.WriteLine("\t [3]. Main Manue.");
            Console.WriteLine("______________________________________________________________________________\n");

            int OP = ValidationInput.ReadIntNumberBetween(1,3, "Enter Option ");
            _PeformOption((enOptions)OP);
        }
        //__________________________________________________________
    }

}

   
