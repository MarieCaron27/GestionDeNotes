using ClassLibrary1;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClassePage
{
    public class MyDataForClasses : INotifyPropertyChanged
    {
        private ObservableCollection<Class> _listClasses = new ObservableCollection<Class>();
        private Class _currentClass = null;

        public ObservableCollection<Class> ListClasses
        {
            get { return _listClasses; }
            set
            {
                if (_listClasses != value)
                {
                    _listClasses = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Class CurrentClass
        {
            get { return _currentClass; }
            set
            {
                if (_currentClass != value)
                {
                    _currentClass = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public MyDataForClasses()
        {
            ListClasses = new ObservableCollection<Class>();

            if (ListClasses.Count > 0)
            {
                for(int i =0;i<ListClasses.Count;i++)
                {
                    CurrentClass = ListClasses.ElementAt(i);
                }
            }
            else
            {
                CurrentClass = null;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
