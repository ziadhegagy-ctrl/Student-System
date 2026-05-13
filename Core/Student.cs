using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static StudentProject.Course;

namespace StudentProject
{
    internal class Student
    {
        int _StudentID;
        string _Name;
        string _Department;
        List<Course> _lCourses;

        public List<Course> GetCourses()
        {
            return _lCourses;
        }

        static List<string> _SplitLine(string Line, string Seperator = "#//#")
        {

            return Line.Split(new string[] { Seperator }, StringSplitOptions.None).ToList();
        }



        //-----------------Base Methods For Save To File---------
        void _AddStudentToFile(List<Student> lStudents, bool Append)
        {
           
                using (StreamWriter MyFile = new StreamWriter("StudentsInfo.txt", Append))
                {
                    string Line;
                    foreach (Student std in lStudents)
                    {
                        Line = $"Student#//#{std.StudentID}#//#{std.Name}#//#{std.Department}";
                        MyFile.WriteLine(Line);
                        _AddCoursesToFile(MyFile, std);
                        MyFile.WriteLine("EndStudent");
                    }
                }
            
           
            
        }
        void _AddCoursesToFile(StreamWriter MyFile, Student std)
        {

            string Line;
            foreach (var C in std._lCourses)
            {
                Line = $"Course#//#{C.CourseCode}#//#{C.CourseName}#//#{C.CreditHours}#//#{(int)C.Policy}";
                MyFile.WriteLine(Line);

                foreach (var Grade in C.GetGradeComponent())
                {
                    if (C.Policy == enPolicy.PercentagPeolicy)
                        Line = $"Grade#//#{Grade.ComponentName}#//#{Grade.Score}#//#{Grade.MaxScore}";
                    else
                        Line = $"Grade#//#{Grade.ComponentName}#//#{Grade.Score}#//#{Grade.MaxScore}#//#{Grade.Weigth}";

                    MyFile.WriteLine(Line);
                }
            }

        }
        //------------------------------------------------------




        //---------------------Save To File--------------------------
        void _AddInformationToFile(List<Student> lStudents, bool Append)
        {
            try
            {
                _AddStudentToFile(lStudents, Append);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("\n\n\n\nSave Failed Because:");
                Console.WriteLine("\n\n\t\tFile NOT found , Contact support and try again.\n\nPress any key to return to the main menu.");
                Console.ReadKey();
                MainMenuScreen.ShowMainMenuScreen();
            }
            catch (IOException)
            {
                Console.WriteLine("\n\n\n\nSave Failed Because:");
                Console.WriteLine("\t\t\n\nAn error occurred while accessing the file. Please try again.\n\nPress any key to return to the main menu.");
                Console.ReadKey();
                MainMenuScreen.ShowMainMenuScreen();
            }

        }
        public bool AddStudentInfoToFile()
        {
            List<Student> lStudents = new List<Student>();
            lStudents.Add(this);
            _AddInformationToFile(lStudents, true);
            return true;
        }
        void _SaveAllDataAfterModifications(List<Student> lStudents)
        {
            _AddInformationToFile(lStudents, false);
        }
        public bool SaveAfterModifications()
        {
            List<Student> std = LoadALLDataFromFile();
            for (int i = 0; i < std.Count; i++)
            {
                if (this.StudentID == std[i].StudentID)
                {
                    std[i] = this;
                    _SaveAllDataAfterModifications(std);
                    return true;
                }
            }
            return false;
        }
        //----------------------------------------------------



        //-----------------Load From File-------------------------
        static List<Student> _LoadDataFromFile()
        {
            List<string> Parts;
            Student student = null;
            Course course = null;
            List<Student> Students = new List<Student>();
            using (StreamReader MyFile = new StreamReader("StudentsInfo.txt"))
            {

                string Line;
                while ((Line = MyFile.ReadLine()) != null)
                {

                    Parts = _SplitLine(Line);

                    if (Parts[0] == "Student")
                    {
                        student = new Student(int.Parse(Parts[1]), Parts[2], Parts[3], new List<Course>());
                    }
                    else if (Parts[0] == "Course")
                    {

                        course = new Course(Parts[1], Parts[2], int.Parse(Parts[3]), (enPolicy)int.Parse(Parts[4]));
                        student._lCourses.Add(course);
                    }

                    else if (Parts[0] == "Grade")
                    {
                        
                            if (course.Policy == enPolicy.PercentagPeolicy)
                                course.AddComponent(new GradeComponent(Parts[1], double.Parse(Parts[2]), double.Parse(Parts[3])));
                            else
                                course.AddComponent(new GradeComponent(Parts[1], double.Parse(Parts[2]),
                                    double.Parse(Parts[3]), double.Parse(Parts[4])));
                    }
                    else if (Parts[0] == "EndStudent")
                    {
                        Students.Add(student);
                        student = null;
                        course = null;
                    }

                }
            }
            return Students;
        }
        static public List<Student> LoadALLDataFromFile()
        {
            try
            {
                return _LoadDataFromFile();

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("\t\t\n\nFile not found , Contact support and try again.\n\nPress any key to return to the main menu.");
                Console.ReadKey();
                MainMenuScreen.ShowMainMenuScreen();
            }
            catch (IOException)
            {
                Console.WriteLine("\t\t\n\nAn error occurred while accessing the file. Please try again.\n\nPress any key to return to the main menu.");
                Console.ReadKey();
                MainMenuScreen.ShowMainMenuScreen();
            }
            return null;
        }

        //------------------------------------------------------
      

        

        // ----------------Constractor------------------------
        public Student(int ID, string name, string department)
        {
            StudentID = ID;
            Name = name;
            Department = department;

        }
        public Student(int ID, string name, string department, List<Course> courses)
        {
            StudentID = ID;
            Name = name;
            Department = department;
            _lCourses = courses;
        }

        public Student()
        {
            StudentID = 0;
            Name = "Unknown";
            Department = "Unknown";
            _lCourses = new List<Course>();
        }
        // ---------------------------------------------------




        //----------Property-----------------------------
        public int StudentID
        {
            get { return _StudentID; }

            set
            {
                if (value >= 0)
                    _StudentID = value;
            }
        }
        public string Name
        {
            get { return _Name; }

            set
            {

                _Name = value;
            }
        }
        public string Department
        {
            get { return _Department; }

            set
            {

                _Department = value;
            }
        }

        //----------------------------------------------






        //------------------Base Methods-----------------------
        public static Student Find(int ID)
        {
            List<Student> std=new List<Student>();
            std = LoadALLDataFromFile();

            foreach (Student student in std)
            {
                if (student.StudentID == ID)
                    return student;
            }
            return null;

        }

        public static bool CoursIsRegisteredByStudent(string Code)
        {
            List<Student> std = new List<Student>();
            std = LoadALLDataFromFile();
            foreach (Student student in std)
            {
                foreach (Course C in student._lCourses)
                {
                    if (C.CourseCode == Code)
                        return true;
                }
            }
            return false;
        }
        public void EnorllInCources(Course AddCourse)
        {
            this._lCourses.Add(AddCourse);
        }
        public float CalculateGPA()
        {
            double TotalPoints = 0;
            int TotalCreditHours = 0;
            foreach (Course course in _lCourses)
            {
                TotalPoints += course.PointsOFCourse() * course.CreditHours;
                TotalCreditHours += course.CreditHours;
            }
            if (TotalCreditHours == 0)
                return 0;
            return (float)(TotalPoints / TotalCreditHours);

        }

        public void DisplayInfo()
        {
            Console.WriteLine($"\nStudent ID : {StudentID}");
            Console.WriteLine($"Name       : {Name}");
            Console.WriteLine($"Department : {Department}");
            Console.WriteLine($"Your GPA   : {CalculateGPA()}");
            Console.WriteLine("\n_____________________________Courses_________________________________\n");
            foreach (Course course in _lCourses)
            {
                Console.WriteLine($"\tCourse Code     : {course.CourseCode}");
                Console.WriteLine($"\tCourse Name     : {course.CourseName}");
                Console.WriteLine($"\tCredit Hours    : {course.CreditHours}");
                Console.WriteLine("\tGrade Components:\n");
                foreach (GradeComponent component in course.GetGradeComponent())
                {
                    Console.WriteLine($"\t\tComponent Name : {component.ComponentName}");
                    Console.WriteLine($"\t\tScore          : {component.Score}");
                    Console.WriteLine($"\t\tMax Score      : {component.MaxScore}\n");

                }
                Console.WriteLine("\t\tPoints OF Course   : " + course.PointsOFCourse());
                Console.WriteLine($"\t\tGrade Letter       : {course.GetGradeLetter()}");
                Console.WriteLine("\n_________________________________________________________________");
            }
        }
        //----------------------------------------------------
    }
}
