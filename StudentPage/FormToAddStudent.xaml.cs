using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
using System.IO;
using Microsoft.Win32;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace StudentPage
{
    public partial class FormToAddStudent : Window
    {
        private MyDataForStudents myData;
        private String classnameOfStudent = null;
        private int numberStudents = 0;
        private string selectedImagePath = null;
        private const string defaultImagePath = "C:\\Users\\Utilisateur\\source\\repos\\Projet_final_CSharp_CARON\\Images\\defaultPersonImage.jpg";
        private bool forceClose = false;

        public FormToAddStudent(MyDataForStudents data, String classNameStudent, int nbStudentsOfClass)
        {
            InitializeComponent();
            classnameOfStudent = classNameStudent;
            numberStudents = nbStudentsOfClass;
            myData = data;
        }

        private void AddStudentClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxLastNameStudent.Text) &&
                !string.IsNullOrEmpty(textBoxFirstNameStudent.Text))
            {
                if (CheckIfImageExists(selectedImagePath) == false && selectedImagePath != defaultImagePath)
                {
                    Student newStudent = new Student(textBoxLastNameStudent.Text, textBoxFirstNameStudent.Text, selectedImagePath ?? defaultImagePath, new Class(classnameOfStudent, numberStudents, true));

                    // Charger les données existantes
                    var existingData = LoadExistingData();

                    // Vérifier si l'élève existe déjà dans les données existantes
                    if (existingData.ListStudents.Any(s => s.LastName == newStudent.LastName && s.FirstName == newStudent.FirstName))
                    {
                        MessageBox.Show("Cet élève est déjà ajouté dans cette classe.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        // Ajouter le nouvel élève à la liste temporaire
                        myData.AddStudent(newStudent);

                        MessageBox.Show("Élève ajouté avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                        this.DialogResult = true;
                        forceClose = true;
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Vous avez déjà séléctionné cette image pour un élève !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Veuillez vérifier que vos champs sont complets et valides !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                selectedImagePath = openFileDialog.FileName;
                ProfileImage.Source = new BitmapImage(new Uri(selectedImagePath));
            }
        }

        public bool CheckIfImageExists(string theImagePath)
        {
            if (!string.IsNullOrEmpty(selectedImagePath) && File.Exists(selectedImagePath))
            {
                foreach (var student in myData.ListStudents)
                {
                    if (student.Picture.Equals(theImagePath, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private MyDataForStudents LoadExistingData()
        {
            string dataPathForStudents = "C:\\Users\\Utilisateur\\source\\repos\\Projet_final_CSharp_CARON\\Fichiers_JSON_donnees\\myStudents.json";

            if (File.Exists(dataPathForStudents))
            {
                string jsonFileForStudents = File.ReadAllText(dataPathForStudents);
                return JsonConvert.DeserializeObject<MyDataForStudents>(jsonFileForStudents);
            }
            else
            {
                return new MyDataForStudents();
            }
        }

        private void CancelAddStudent(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Voulez-vous vraiment quitter?", "Annulation ajout étudiants", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Opération annulée", "Annulation ajout étudiants", MessageBoxButton.OK, MessageBoxImage.Error);
                forceClose = true;
                this.Close();
            }
        }

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
    }
}

