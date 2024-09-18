using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;

namespace QuizzPage
{
    public class MyDataForQuizzes
    {
        #region FIELDS AND PROPREPITIES : 

        private ObservableCollection<Quizz> _listQuizzes = new ObservableCollection<Quizz>();
        private Quizz _currentQuizz = null;

        public ObservableCollection<Quizz> ListQuizzes
        {
            get { return _listQuizzes; }
            set
            {
                if (_listQuizzes != value)
                {
                    _listQuizzes = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Quizz CurrentQuizz
        {
            get { return _currentQuizz; }
            set
            {
                if (_currentQuizz != value)
                {
                    _currentQuizz = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region CONSTRUCTOR :

        public MyDataForQuizzes()
        {
            ListQuizzes = new ObservableCollection<Quizz>();

            if (ListQuizzes.Count > 0)
            {
                for (int i = 0; i < ListQuizzes.Count; i++)
                {
                    CurrentQuizz = ListQuizzes.ElementAt(i);
                }
            }
            else
            {
                CurrentQuizz = null;
            }
        }

        #endregion

        #region ID INITIALISATION :

        public void AddQuizz(Quizz theQuizz)
        {
            theQuizz.QuizzId = InitializationIdQuizz();
            ListQuizzes.Add(theQuizz);
        }

        public int InitializationIdQuizz()
        {
            if (ListQuizzes.Count == 0)
            {
                return 1;
            }

            return ListQuizzes.Max(q => q.QuizzId) + 1;
        }

        #endregion

        #region INotifyPropertyChanged implementation : 

        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
