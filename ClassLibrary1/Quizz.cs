using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Quizz
    {
        #region FIELDS AND PROPERTIES :

        private int _quizzId;

        public int QuizzId
        {
            set 
            {

                if (value != _quizzId)
                {
                    _quizzId = value;
                    NotifyPropertyChanged();
                }
            }

            get 
            { 
                return _quizzId; 
            }
        }

        private string _quizzTitle;

        public string QuizzTitle
        {
            set
            {
                if (value != _quizzTitle) 
                {
                    _quizzTitle = value;
                    NotifyPropertyChanged();
                }
            }

            get
            {
                return _quizzTitle;
            }
        }

        private DateTime _quizzDate;

        public DateTime QuizzDate
        {
            set
            {
                if (value != _quizzDate)
                { 
                    _quizzDate = value;
                    NotifyPropertyChanged();
                }
            }

            get
            {
                return _quizzDate;
            }
        }

        private Class _quizzClass;

        public Class QuizzClass
        {
            set
            {
                if (value != _quizzClass)
                { 
                    _quizzClass = value;
                    NotifyPropertyChanged();
                }
            }

            get
            {
                return _quizzClass;
            }
        }

        private string _quizzCourse;

        public string QuizzCourse
        {
            set
            {
                if (value != _quizzCourse)
                {
                    _quizzCourse = value;
                    NotifyPropertyChanged();
                }
            }

            get
            {
                return _quizzCourse;
            }
        }

        private int _quizzTotal;

        public int QuizzTotal
        {
            set
            {
                if (value != _quizzTotal)
                {
                    _quizzTotal = value;
                    NotifyPropertyChanged();
                }
            }

            get
            {
                return _quizzTotal;
            }
        }

        private bool _resultsAdded;

        public bool ResultsAdded
        {
            set
            {

                if (value != _resultsAdded)
                {
                    _resultsAdded = value;
                    NotifyPropertyChanged();
                }
            }

            get
            {
                return _resultsAdded;
            }
        }

        #endregion

        #region CONSTRUCTORS :

        //Constructeur d'initialisation :

        public Quizz(string name,DateTime date,Class classOfQuizz,string course,int total,bool myResultsAdded) 
        { 
            QuizzTitle = name;
            QuizzDate = date;
            QuizzClass = classOfQuizz;
            QuizzCourse = course;
            QuizzTotal = total;
            ResultsAdded = myResultsAdded;
        }

        //Constructeur par défaut :

        public Quizz() : this("Interro 1",new DateTime(2024, 06, 14),new Class("5F",25,true),"Français",20,false)
        { 
        
        }

        #endregion

        #region ToStringMethod :

        public override string ToString()
        {
            return QuizzId + QuizzTitle + QuizzDate + QuizzClass + QuizzCourse + QuizzTotal + ResultsAdded;
        }

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
