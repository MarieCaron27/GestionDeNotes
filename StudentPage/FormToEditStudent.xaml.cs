using ClassLibrary1;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StudentPage
{
    /// <summary>
    /// Logique d'interaction pour FormToEditStudent.xaml
    /// </summary>
    public partial class FormToEditStudent : Window
    {

        #region FIELDS AND CONSTRUCTOR :

        private Student _studentToEdit;
        private MyDataForStudents _myData;

        private string _originalFirstName;
        private string _originalLastName;
        private string _originalPicture;
        private Class _originalStudentClass;

        private string selectedImagePath = null;
        private const string defaultImagePath = "C:\\Users\\Utilisateur\\source\\repos\\Projet_final_CSharp_CARON\\Images\\defaultPersonImage.jpg";

        private bool forceClose = false;
        public FormToEditStudent(Student student, MyDataForStudents data)
        {
            InitializeComponent();

            _studentToEdit = student;

            _originalFirstName = _studentToEdit.FirstName;
            _originalLastName = _studentToEdit.LastName;
            _originalPicture = _studentToEdit.Picture;
            _originalStudentClass = _studentToEdit.ClassStudent;

            DataContext = _studentToEdit;
            _myData = data;
        }

        #endregion

        #region UPDATE STUDENT BUTTON :

        private void SaveStudent_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxEditLastNameStudent.Text)
                && !string.IsNullOrEmpty(textBoxEditFirstNameStudent.Text))
            {
                if ((CheckIfImageExists(selectedImagePath) == false && selectedImagePath != defaultImagePath) || selectedImagePath == _originalPicture)
                {
                    string newLastName = textBoxEditLastNameStudent.Text.Trim();
                    string newFirstName = textBoxEditFirstNameStudent.Text.Trim();

                    if (_myData.ListStudents.Any(s => s.LastName == newLastName && s.FirstName == newFirstName)
                        && _originalFirstName != newFirstName
                        && _originalLastName != newLastName)
                    {
                        MessageBox.Show("Cet élève existe déjà", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        _studentToEdit.Picture = selectedImagePath ?? defaultImagePath;
                        _studentToEdit.ClassStudent = _originalStudentClass;
                        _studentToEdit.FirstName = newFirstName;
                        _studentToEdit.LastName = newLastName;

                        jSaveToJson();

                        MessageBox.Show("Votre élève a bien été modifiée.", "Modification validée", MessageBoxButton.OK, MessageBoxImage.Information);

                        forceClose = true;
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("L'image existe déjà pour un autre élève...", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Un de vos champs est invalide ou incomplet !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region PICTURE :

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                selectedImagePath = openFileDialog.FileName;
                ProfileEditImage.Source = new BitmapImage(new Uri(selectedImagePath));
            }
        }

        public bool CheckIfImageExists(string theImagePath)
        {
            if (!string.IsNullOrEmpty(selectedImagePath) && File.Exists(selectedImagePath))
            {
                foreach (var student in _myData.ListStudents)
                {
                    if (student.Picture.Equals(theImagePath, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion

        #region SAVE TO JSON :

        private void jSaveToJson()
        {
            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };

            File.WriteAllText("C:\\Users\\Utilisateur\\source\\repos\\Projet_final_CSharp_CARON\\Fichiers_JSON_donnees\\myStudents.json", System.Text.Json.JsonSerializer.Serialize(_myData, options));
        }

        #endregion

        #region CANCEL BUTTON :

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Voulez-vous vraiment quitter?", "Annulation ajout classe", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                 _studentToEdit.FirstName = _originalFirstName;
                _studentToEdit.LastName = _originalLastName;
                _studentToEdit.Picture = _originalPicture;
                _studentToEdit.ClassStudent = _originalStudentClass;

                MessageBox.Show("Opération annulée", "Annulation modification", MessageBoxButton.OK, MessageBoxImage.Information);

                forceClose = true;
                this.Close();
            }
        }

        #endregion

        #region WINDOW CLOSING BUTTON :

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!forceClose)
            {
                var result = MessageBox.Show("Voulez-vous quitter?", "Quitter?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        #endregion
    }
}
