using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GradePage
{
    public class MyDataForGrades
    {
        #region FIELDS AND PROPERTIES :

        private ObservableCollection<Grade> _listGrades;

        public ObservableCollection<Grade> ListGrades
        {
            get
            {
                return _listGrades;
            }

            set
            {
                if (_listGrades != value)
                {
                    _listGrades = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Grade _currentGrade;

        public Grade CurrentGrade
        {
            get
            {
                return _currentGrade;
            }

            set
            {
                if (_currentGrade != value)
                {
                    _currentGrade = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region CONSTRUCTOR :

        public MyDataForGrades()
        {
            ListGrades = new ObservableCollection<Grade>();

            if (ListGrades.Count > 0)
            {
                for (int i = 0; i < ListGrades.Count; i++)
                {
                    CurrentGrade = ListGrades.ElementAt(i);
                }
            }
            else
            {
                CurrentGrade = null;
            }

        }

        #endregion

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region ADD GRADE + ID
        public void AddGrade(Grade theGrade)
        {
            ListGrades.Add(theGrade);
        }

        #endregion
    }
}
