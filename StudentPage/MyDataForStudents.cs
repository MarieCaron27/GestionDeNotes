
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;

namespace StudentPage
{
    public class MyDataForStudents
    {
        #region FIELDS AND PROPERTIES :

        private ObservableCollection<Student> _listStudents;

        public ObservableCollection<Student> ListStudents
        {
            get
            {
                return _listStudents;
            }

            set
            {
                if (_listStudents != value)
                {
                    _listStudents = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Student _currentStudent;

        public Student CurrentStudent
        {
            get
            {
                return _currentStudent;
            }

            set
            {
                if (_currentStudent != value)
                {
                    _currentStudent = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region CONSTRUCTOR :

        public MyDataForStudents()
        {
            ListStudents = new ObservableCollection<Student>();

            if (ListStudents.Count > 0)
            {
                for (int i = 0; i < ListStudents.Count; i++)
                {
                    CurrentStudent = ListStudents.ElementAt(i);
                }
            }
            else
            {
                CurrentStudent = null;
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

        #region ADD STUDENT + ID
        public void AddStudent(Student student)
        {
            ListStudents.Add(student);
            SortStudents();
        }

        public void SortStudents()
        {
            // Group students by class and sort within each class
            var groupedStudents = ListStudents
                .GroupBy(s => s.ClassStudent.ClassName) //Regroupe les étudiants par classes
                .SelectMany(g => g.OrderBy(s => s.LastName).ThenBy(s => s.FirstName)) //Transforme chaque groupe d'étudiants en une seule séquence d'étudiants triés
                .ToList();

            // Assign new IDs within each class group
            string currentClass = null;
            int id = 1;

            foreach (var student in groupedStudents)
            {
                if (currentClass == null || student.ClassStudent.ClassName != currentClass)
                {
                    currentClass = student.ClassStudent.ClassName;
                    id = 1;
                }
                student.Id = id++;
            }

            // Update the list with sorted and re-identified students
            ListStudents = new ObservableCollection<Student>(groupedStudents);
        }

        #endregion
    }
}
