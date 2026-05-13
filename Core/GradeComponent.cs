using System;
namespace StudentProject
{
    internal class GradeComponent
    {
        string _ComponentName;
         double _Score;
        double _MaxScore;
        double _Weigth;

        //------------------------------Properties----------------------------------
        public string ComponentName
        {
            get { return _ComponentName; }

            set
                { _ComponentName = value; }
        }
        public double Score
        {
            get { return _Score; }

            set
            {
                if (value > 0)
                {
                    _Score=value;
                }
            }
        }
        public double Weigth
        {
            get { return _Weigth; }

            set
            {
                if (value > 0)
                {
                    _Weigth = value;
                }
            }
        }
        public double MaxScore
        {
            get { return _MaxScore; }

            set
            {
                if (value > 0)
                {
                    _MaxScore = value;
                }
            }
        }
        //-----------------------------------------------------------------------------


        //------------------------------Constructors----------------------------------
        public GradeComponent(string Name,double score,double maxscore, double weigth)
        {
            ComponentName=Name;
            Score=score;
            MaxScore=maxscore;
            Weigth=weigth;
        }
        public GradeComponent(string Name, double score, double maxscore)
        {
            ComponentName = Name;
            Score = score;
            MaxScore = maxscore;
        }
        public GradeComponent()
            {

            }

        //-----------------------------------------------------------------------------

    }
}
