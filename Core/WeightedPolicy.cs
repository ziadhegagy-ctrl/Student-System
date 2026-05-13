using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentProject
{
    internal class WeightedPolicy : GradingPolicy
    {
        public override double CalculateFinalGrade(List<GradeComponent> gradeComponents)
        {
            double TotalGrade = 0;
            foreach (GradeComponent component in gradeComponents)
            {
                TotalGrade += (component.Score / component.MaxScore) * component.Weigth;
            }
            return TotalGrade;
        }
    }
}
