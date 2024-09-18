using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Grade
    {
        #region FIELDS AND PROPRETIES :

        private Student _studentForGrade;

        public Student StudentForGrade
        {
            set
            {

                if (value != _studentForGrade)
                {
                    _studentForGrade = value;
                    NotifyPropertyChanged();
                }
            }

            get
            {
                return _studentForGrade;
            }
        }

        private Class _classForGrade;

        public Class ClassForGrade
        {
            set
            {

                if (value != _classForGrade)
                {
                    _classForGrade = value;
                    NotifyPropertyChanged();
                }
            }

            get
            {
                return _classForGrade;
            }
        }

        private Quizz _quizzForGrade;

        public Quizz QuizzForGrade
        {
            set
            {

                if (value != _quizzForGrade)
                {
                    _quizzForGrade = value;
                    NotifyPropertyChanged();
                }
            }

            get
            {
                return _quizzForGrade;
            }
        }

        private double _result;

        public double Result
        {
            set
            {

                if (value != _result)
                {
                    _result = value;
                    NotifyPropertyChanged();
                }
            }

            get
            {
                return _result;
            }
        }

        #endregion

        #region CONSTRUCTORS :

        public Grade(Student theStudent,Class theClass,Quizz theQuizz,double theResult)
        {
            StudentForGrade = theStudent;
            ClassForGrade = theClass;
            QuizzForGrade = theQuizz;
            Result = theResult;
        }

        /*public Grade() : this(ne"Marie","Caron",new Class("2E", 2, true); "Eva","Roger",new Class("2E", 2, true)},new Class("2E",2,true),new Quizz(""))
        {

        }*/

        #endregion


        #region INotifyPropertyChanged implementation :

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

    }
}
