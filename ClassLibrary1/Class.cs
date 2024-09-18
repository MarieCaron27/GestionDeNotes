using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace ClassLibrary1
{
    public class Class : INotifyPropertyChanged
    {
        #region Fields and properties :

        private string _className;

        public string ClassName 
        {
            set
            {
      
                if(value != _className)
                {
                    _className = value;
                    NotifyPropertyChanged();
                }
            }

            get
            {
                return _className;
            }
        }

        private int _nbStudents;

        public int NbStudents 
        {
            set
            {

                if (value != _nbStudents)
                {
                    _nbStudents = value;
                    NotifyPropertyChanged();
                }
            }

            get
            {
                return _nbStudents;
            }
        }

        private bool _studentAdded;

        public bool StudentAdded
        {
            set
            {

                if (value != _studentAdded)
                {
                    _studentAdded = value;
                    NotifyPropertyChanged();
                }
            }

            get
            {
                return _studentAdded;
            }
        }

        #endregion

        #region Constructors :

        public Class(string cN,int students,bool addedStudent) 
        { 
            ClassName = cN;
            NbStudents = students;
            StudentAdded = addedStudent;
        }

        public Class() : this("3A",25,false)
        { 
        
        }

        #endregion

        #region ToStringMethod :

        public override string ToString()
        {
            return ClassName + NbStudents + StudentAdded;
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
