using System;
using System.Collections.Generic;
namespace StudentProject
{
    internal class Course
    {
        public enum enPolicy { PercentagPeolicy=1, WeightedPolicy=2}

        string _CourseCode;
        string _CourseName;
        int _CreditHours;
        List<GradeComponent> gradeComponent=new List<GradeComponent>();
        enPolicy _Policy;

        //------------------------------Properties----------------------------------
        public enPolicy Policy
         {
            set { _Policy = value; }
            get { return _Policy; }
         }
        public string CourseCode
        {
            get {  return _CourseCode; }
            set
            {
                _CourseCode = value;
            }
        }
        public string CourseName
        {
            get { return _CourseName; }
            set
            {
                _CourseName = value;
            }
        }
        public int CreditHours
        {
            get { return _CreditHours; }    
             set
            
           
            {
                    if (value >= 0) ;
                _CreditHours = value;
                }
            
        }

        //-----------------------------------------------------------------------------

        //------------------------------Constructors----------------------------------
        public Course()
            {

            }
        public Course(string Code , String Name, int CH, enPolicy policy)
        {
            CourseCode= Code;   
            CourseName= Name;
            CreditHours = CH;
            Policy = policy;
        }
        //-----------------------------------------------------------------------------

        //----------------------------------Base Methods----------------------------------
        public void AddComponent(GradeComponent Com)
        {
            gradeComponent.Add( Com );  
        }
        public List<GradeComponent> GetGradeComponent()
        {
            return gradeComponent;
        }
        public double CalculateTotalGradeForOneCourse()
        {
            GradingPolicy Totalgrade;
            if (this.Policy == enPolicy.PercentagPeolicy)
                Totalgrade = new PercentageBasedPolicy();

            else if (this.Policy == enPolicy.WeightedPolicy)
                Totalgrade = new WeightedPolicy();

            else
                return 0;
            return Totalgrade.CalculateFinalGrade(gradeComponent);
        }
        public float PointsOFCourse()
        {
            double totalGrade = CalculateTotalGradeForOneCourse();
            if (totalGrade >= 96)
                return 4;
            else if (totalGrade >= 92 && totalGrade < 96)
                return 3.7f;
            else if (totalGrade >= 88 && totalGrade < 92)
                return 3.4f;
            else if (totalGrade >= 84 && totalGrade < 88)
                return 3.2f;
            else if (totalGrade >= 80 && totalGrade < 84)
                return 3f;
            else if (totalGrade >= 76 && totalGrade < 80)
                return 2.8f;
            else if (totalGrade >= 72 && totalGrade < 76)
                return 2.6f;
            else if (totalGrade >= 68 && totalGrade < 72)
                return 2.4f;
            else if (totalGrade >= 64 && totalGrade < 68)
                return 2.2f;
            else if (totalGrade >= 60 && totalGrade < 64)
                return 2f;
            else if (totalGrade >= 55 && totalGrade < 60)
                return 1.5f;
            else if (totalGrade >= 50 && totalGrade < 55)
                return 1f;
            else
                return 0;
        }

        public string GetGradeLetter()
        {
            double totalGrade = PointsOFCourse();

            switch(totalGrade)
            {
                case 4:
                    return "A+";
                case 3.7f:
                    return "A";
                case 3.4f:
                    return "A-";
                case 3.2f:
                    return "B+";
                case 3f:
                    return "B";
                case 2.8f:  
                    return "B-";
                case 2.6f:
                    return "C+";
                case 2.4f:
                    return "C";
                case 2.2f:
                    return "C-";
                case 2f:
                    return "D+";
                case 1.5f:
                    return "D";
                case 1f:
                    return "D-";
                default:
                    return "F";

            }
        }

        //----------------------------------------------------------------------------------
        }
}
