using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace StudentProject
{
    internal class DoctorScreen
    {
        enum enOptions { UpdateScoreForAllStudent = 1, UpdateScoreForOneStudent = 2, Main = 3 }

        //----------------------Shows Info Methods----------------------
        static void _ShowStudentInfo(Student std)
        {
            Console.WriteLine("\n________________Student Info________________");
            Console.WriteLine($"Student ID: {std.StudentID}, Name: {std.Name}, Department: {std.Department}");
            Console.WriteLine("_____________________________________________\n");
        }
        static  List<Student> _ShowStudentsRegisteredInCourse(string courseCode)
        {
            List<Student> StudentsRegistered= new List<Student>();
            List <Student> Info = Student.LoadALLDataFromFile();

            Console.WriteLine("_______________________________________ Students Registered In Course ___________________________________");
            foreach (Student std in Info)
            { 
                foreach (Course C in  std.GetCourses())
                {
                    if (C.CourseCode == courseCode)
                    {
                        StudentsRegistered.Add(std);
                        Console.WriteLine($"\nStudent ID: {std.StudentID}  ,   Name: {std.Name}    ,Department: {std.Department}");
                    }
                }
            }
            Console.WriteLine("__________________________________________________________________________________________________________");
            return StudentsRegistered;
        }

        static void  CourseInfo(Course C)
        {
            Console.WriteLine($"\nCourse Name :  {C.CourseName} , Credit Hours : {C.CreditHours} , Grading By : {C.Policy}");
        }
        static void _UbdateateGradeComponent(Course C)
        {
            for (int I = 0; I < C.GetGradeComponent().Count; I++)
            {
                Console.WriteLine($"\nComponent Name: {C.GetGradeComponent()[I].ComponentName}    ,Max Score : {C.GetGradeComponent()[I].MaxScore}     ,Score: {C.GetGradeComponent()[I].Score}");

                double newGrade = ValidationInput.ReadDoubleNumberBetween(0, C.GetGradeComponent()[I].MaxScore, "Enter new Score: ");

                C.GetGradeComponent()[I].Score = newGrade;
            }


        }
        //------------------------------------------------------------


        //----------------------For All Student----------------------
     
        static void _AddGradeForAllStudentsforCourse()  
           {
            string courseCode = ValidationInput.ReadNonEmptyString("Enter course code: ");
            List<Student> StudentsRegistered = _ShowStudentsRegisteredInCourse(courseCode);
            while(StudentsRegistered.Count==0)
            {
                Console.WriteLine($"{courseCode} Is NOT Registered By any Student ... Enter Anthour course ");
                courseCode = ValidationInput.ReadNonEmptyString("Enter course code: ");
                StudentsRegistered= _ShowStudentsRegisteredInCourse(courseCode);
            }

            bool CouseInfoIsShow = false;
            foreach (Student std in StudentsRegistered)
            {
                for (int I = 0; I < std.GetCourses().Count; I++)
                {
                    if (std.GetCourses()[I].CourseCode.ToLower() == courseCode.ToLower())
                    {
                        if (!CouseInfoIsShow)
                        {
                            CourseInfo(std.GetCourses()[I]);
                                CouseInfoIsShow=true;
                        }
                        _ShowStudentInfo(std);
                        _UbdateateGradeComponent(std.GetCourses()[I]);
                        std.SaveAfterModifications();
                        Console.WriteLine("\nUpdated successfully.\n");
                     
                    }
                }

            }

        }
        static  void UpdateScoreForAllStudent()
        {
            Console.WriteLine("___________________________  Update Score For All Student Screen ___________________________\n");

            _AddGradeForAllStudentsforCourse();
        }

        //--------------------------------------------------------


        //----------------------Only For One Student----------------------
        static void _UbdateGradeForOneStudent()
        {
            string courseCode = ValidationInput.ReadNonEmptyString("Enter course code: ");
            List<Student> StudentsRegistered = _ShowStudentsRegisteredInCourse(courseCode);
            while (StudentsRegistered.Count == 0)
            {
                Console.WriteLine($"{courseCode} Is NOT Registered By any Student ... Enter Anthour course ");
                courseCode = ValidationInput.ReadNonEmptyString("Enter course code: ");
                StudentsRegistered=_ShowStudentsRegisteredInCourse(courseCode);
            }

            int studentID = ValidationInput.ReadIntNumberBetween(0, StudentsRegistered.Count, "\nEnter a valid Student ID: ");
            Student std = null;
            foreach (Student student in StudentsRegistered)
            {
                if (student.StudentID == studentID)
                {
                    std = student;
                    break;
                }
            }

            if (std != null)
            {
                _ShowStudentInfo(std);
                for (int I = 0; I < std.GetCourses().Count; I++)
                {
                    if (std.GetCourses()[I].CourseCode.ToLower() == courseCode.ToLower())
                    {
                        CourseInfo(std.GetCourses()[I]);
                        _UbdateateGradeComponent(std.GetCourses()[I]);
                        std.SaveAfterModifications();

                        Console.WriteLine("Updated successfully.\n");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Student not found in the course.");
            }
        }
        static void UpdateScoreForOneStudent()
        {
            Console.WriteLine("___________________________  Update Score For One Student Screen ___________________________\n");

            _UbdateGradeForOneStudent();
        }
        //----------------------------------------------------------------




        //----------------------Screen Methods----------------------
        static void _GoBack()
        {
            Console.Write("\nTo Go Back Step , Press Any Key: ");
            Console.ReadKey();
            DoctorOptionScreen();
        }
        static void _PeformOption(enOptions option)
        {
            if (option == enOptions.UpdateScoreForAllStudent)
            {
                Console.Clear();
                UpdateScoreForAllStudent();
                _GoBack();
            }

            else if (option == enOptions.UpdateScoreForOneStudent)
            {
                Console.Clear();
                UpdateScoreForOneStudent();
                _GoBack();
            }
            else if (option == enOptions.Main)
            {
                Console.Clear();
                MainMenuScreen.ShowMainMenuScreen();

            }
        }
        static public void DoctorOptionScreen()
        {
            Console.Clear();
            Console.WriteLine("___________________________ Doctor Option Screen ___________________________\n");

            Console.WriteLine("\t [1]. Update Score For All Student.");
            Console.WriteLine("\t [2]. Update Score For One Student.");
            Console.WriteLine("\t [3]. Main Manue.");
            Console.WriteLine("______________________________________________________________________________\n");
            int OP = ValidationInput.ReadIntNumberBetween(1,3, "Enter Option  ");
            _PeformOption((enOptions)OP);
        }
        //-----------------------------------------------------------
    }
}
