using System;
using System.Collections.Generic;
namespace StudentProject
{
    internal class PercentageBasedPolicy : GradingPolicy
    {
        public override double CalculateFinalGrade(List<GradeComponent> gradeComponents)
        {
            double TotalGrade = 0;
            double totalMaxScore = 0;
            foreach (GradeComponent component in gradeComponents)
            {
                TotalGrade += component.Score;
                totalMaxScore += component.MaxScore;
            }
            if (totalMaxScore == 0)
                return 0; 

            return (TotalGrade / totalMaxScore) * 100;
        }
    }
}
