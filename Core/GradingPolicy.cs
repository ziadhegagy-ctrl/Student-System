using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentProject
{
    internal abstract class GradingPolicy
    {
      public abstract double CalculateFinalGrade(List<GradeComponent> gradeComponents);
    }
}
