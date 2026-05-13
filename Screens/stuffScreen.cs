using System;
using System.Collections.Generic;
using static StudentProject.Course;
namespace StudentProject
{
    internal class stuffScreen
    {
        enum enOptions { AddStudentScreen = 1, AddGradeComponent = 2 , Main=3}

        //-------------------------AddComponentName-----------------------------
        static GradeComponent _AddComponentName()
        {
            GradeComponent grade  =new GradeComponent();    
            grade.ComponentName = ValidationInput.ReadNonEmptyString("\nGrade Name: ");
            return grade;
        }
        static void _Policy(Course C, GradeComponent grade)
        {
            
            if (C.Policy== Course.enPolicy.WeightedPolicy)
              {
                Console.WriteLine($"\nThis Course Grading By Weighted Policy,,, SO Max Score This Course {C.CourseName} Will Be (100%) \n");
                grade.MaxScore = 100;
                grade.Weigth = ValidationInput.ReadDoubleNumberBetween(0,100, "Please Enter Wiegth by Percentage Format : ");
              }
                else
            {
                Console.Write($"\nThis Course Grading By Percentage Policy,,, SO ");
                grade.MaxScore = ValidationInput.ReadDoubleNumberBetween(0, 100, "Enter MAX Score: ");
            }
        }
        static void _AddComponentForStudents()
        {
            

           List <Student> Info = Student.LoadALLDataFromFile();
            string CourseCode = ValidationInput.ReadNonEmptyString("Enter Course Code :");

            while(!Student.CoursIsRegisteredByStudent(CourseCode))
            {
                Console.WriteLine($"{CourseCode} Is NOT Registered By any Student ... Enter Anthour course ");
                CourseCode = ValidationInput.ReadNonEmptyString("Enter Course Code :");
            }
            bool PolicyDone= false;
            char Answer;
            do
            {
                PolicyDone = false;
                GradeComponent grade = _AddComponentName();
                foreach (Student student in Info)
                {
                    for (int I = 0; I < student.GetCourses().Count; I++)
                    {
                        if (student.GetCourses()[I].CourseCode.ToLower() == CourseCode.ToLower())
                        {
                            if (!PolicyDone)
                            { _Policy(student.GetCourses()[I], grade);
                                PolicyDone=true;
                            }
                            student.GetCourses()[I].AddComponent(grade);
                            student.SaveAfterModifications();
                        }
                    }
                }
               
                Console.Write("\nDo You Want Add another Component (Y,N): ");
                Answer=char.Parse(Console.ReadLine());  
            } while (Answer=='Y'|| Answer=='y');


        }
        static void ShowAddGradeComponent()
        {
            Console.WriteLine("___________________________ Add Component Screen ___________________________\n");
            _AddComponentForStudents();
        }
        //---------------------------------------------------------------------


        //-------------------------ADD Student---------------------------------
        static Student _StudentInfo()
        {
            Student Student = new Student();
            Student.StudentID = ValidationInput.ReadIntNumberBetween("Enter Student ID         : ");

            while (Student.Find(Student.StudentID) != null)
            {
                Student.StudentID = ValidationInput.ReadIntNumberBetween($"\n ID : {Student.StudentID} Already Exists  Enter Anthour ID: ");
            }
            Student.Name = ValidationInput.ReadNonEmptyString("\nEnter Student Name       : ");
            Student.Department = ValidationInput.ReadNonEmptyString("\nEnter Student Department : ");

            return Student;
        }
        static void _AddStudent()
        {
            char Answer;
            Student student;
            do
            {
                student = _StudentInfo();
                if (student.AddStudentInfoToFile())
                {
                    Console.WriteLine($"\nStudent [ {student.Name} ] Added Successfully");
                }
                else
                {
                    Console.WriteLine($"\nStudent [ {student.Name} ] Not Added Successfully");
                }

                Console.Write("\nDo You Want Add another Student (Y,N): ");
                Answer = char.Parse(Console.ReadLine());
            } while (Answer == 'Y' || Answer == 'y');
        }
        static void ShowAddStudentScreen()
        {
            Console.WriteLine("___________________________ Add Student Screen ___________________________\n");
            _AddStudent();
        }
        //---------------------------------------------------------------------



        //----------------------Screen Methods----------------------
        static void _GoBack()
        {
            Console.Write("\nTo Go Back Step , Press Any Key: ");
            Console.ReadKey();
            StaffOptionScreen();
        }
        static void _PeformOption(enOptions option)
        {
            if (option == enOptions.AddStudentScreen)
            {
                Console.Clear();
                ShowAddStudentScreen();
                _GoBack();
            }

            else if (option == enOptions.AddGradeComponent)
            {
                Console.Clear();
                ShowAddGradeComponent();
                _GoBack();
            }
            else if(option == enOptions.Main)
            {
                Console.Clear();
                MainMenuScreen.ShowMainMenuScreen();

            }

        }
        static public void StaffOptionScreen()
        {
            Console.Clear();
            Console.WriteLine("___________________________ Staff Option Screen ___________________________\n");

            Console.WriteLine("\t [1]. Add Student Screen.");
            Console.WriteLine("\t [2]. Add GradeComponent  Screen.");
            Console.WriteLine("\t [3]. Main Manue.");
            Console.WriteLine("______________________________________________________________________________\n");
            int OP = ValidationInput.ReadIntNumberBetween(1, 3, "Enter Option ");
            _PeformOption((enOptions)OP);
        }

        //--------------------------------------------------
    }

}
