using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
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
using ClassLibrary1;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Microsoft.Win32;
using QuizzPage;
using StudentPage;
using System.Windows.Controls.Primitives;
using GradePage;

namespace ClassePage
{
    /// <summary>
    /// Logique d'interaction pour PageToShowClasses.xaml
    /// </summary>
    public partial class PageToShowClasses : Window
    {
        #region DATA AND PROPRETIES :

        MyDataForClasses myData = null;
        MyDataForQuizzes dataForQuizzes;
        MyDataForStudents dataForStudents;
        MyDataForGrades dataForGrades;

        private bool forceClose = false;
        private bool? result;

        public PageToShowClasses()
        {
            InitializeComponent();

            RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\TestCSharp")!;

            if (registryKey2 != null)
            {
                this.Left = (int)registryKey2.GetValue("XPosition", 0);
                this.Top = (int)registryKey2.GetValue("YPosition", 0);
            }

            LoadData();
            LoadDataForQuizzes();
            LoadDataForStudents();
            LoadDataForGrades();

            DataContext = myData;
        }

        #endregion

        #region MENU BUTTONS :

        private void ShowMyStudents_Click(object sender, RoutedEventArgs e)
        {
            PageToShowStudents windowForStudents = new PageToShowStudents();

            MessageBox.Show("Affichage de la page des élèves.", "Redirection", MessageBoxButton.OK, MessageBoxImage.Information);
            windowForStudents.Show();
        }

        private void ShowMyQuizzes_Click(object sender, RoutedEventArgs e)
        {
            PageToShowQuizzes windowForQuizzes = new PageToShowQuizzes(dataForQuizzes);

            MessageBox.Show("Affichage de la page des interrogation.", "Redirection", MessageBoxButton.OK, MessageBoxImage.Information);
            windowForQuizzes.Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        #region LOAD AND SAVE DATA METHODS :

        private void LoadData()
        {
            string dataPathForClasses = "C:\\Users\\Utilisateur\\source\\repos\\Projet_final_CSharp_CARON\\Fichiers_JSON_donnees\\myClasses.json";

            if (File.Exists(dataPathForClasses))
            {
                string jsonFileForClasses = File.ReadAllText(dataPathForClasses);
                myData = JsonConvert.DeserializeObject<MyDataForClasses>(jsonFileForClasses);
            }
            else
            {
                myData = new MyDataForClasses();
            }

            DataContext = myData;
        }

        private void LoadDataForQuizzes()
        {
            string dataPathForQuizzes = "C:\\Users\\Utilisateur\\source\\repos\\Projet_final_CSharp_CARON\\Fichiers_JSON_donnees\\myQuizzes.json";

            if (File.Exists(dataPathForQuizzes))
            {
                string jsonFileForQuizzes = File.ReadAllText(dataPathForQuizzes);
                dataForQuizzes = JsonConvert.DeserializeObject<MyDataForQuizzes>(jsonFileForQuizzes);
            }
            else
            {
                dataForQuizzes = new MyDataForQuizzes();
            }

            DataContext = dataForQuizzes;
        }

        private void LoadDataForStudents()
        {
            string dataPathForStudents = "C:\\Users\\Utilisateur\\source\\repos\\Projet_final_CSharp_CARON\\Fichiers_JSON_donnees\\myStudents.json";

            if (File.Exists(dataPathForStudents))
            {
                string jsonFileForStudents = File.ReadAllText(dataPathForStudents);
                dataForStudents = JsonConvert.DeserializeObject<MyDataForStudents>(jsonFileForStudents);
            }
            else
            {
                dataForStudents = new MyDataForStudents();
            }

            DataContext = dataForStudents;
        }

        private void LoadDataForGrades()
        {
            string dataPathForGrades = "C:\\Users\\Utilisateur\\source\\repos\\Projet_final_CSharp_CARON\\Fichiers_JSON_donnees\\myGrades.json";

            if (File.Exists(dataPathForGrades))
            {
                string jsonFileForGrades = File.ReadAllText(dataPathForGrades);
                dataForGrades = JsonConvert.DeserializeObject<MyDataForGrades>(jsonFileForGrades);
            }
            else
            {
                dataForGrades = new MyDataForGrades();
            }

            DataContext = dataForGrades;
        }

        private void SaveData()
        {
            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };

            File.WriteAllText("C:\\Users\\Utilisateur\\source\\repos\\Projet_final_CSharp_CARON\\Fichiers_JSON_donnees\\myClasses.json", System.Text.Json.JsonSerializer.Serialize(myData, options));
        }

        private void SaveStudents(MyDataForStudents data)
        {
            JsonSerializerOptions options = new()
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            File.WriteAllText("C:\\Users\\Utilisateur\\source\\repos\\Projet_final_CSharp_CARON\\Fichiers_JSON_donnees\\myStudents.json", System.Text.Json.JsonSerializer.Serialize(data, options));
        }

        #endregion

        #region ADD A CLASS BUTTON :

        private void AddAClass_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FormToAddClass pageAjoutClass = new FormToAddClass(myData);

                if (pageAjoutClass.ShowDialog() == true)
                {
                    SaveData();
                    LoadData();
                    MessageBox.Show("Votre classe a bien été ajoutée", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region UPDATE A CLASS BUTTON :

        private void UpdateAClass_Click(Object sender, RoutedEventArgs e)
        {
            try
            {   
                if (dataGrid.SelectedItem is Class selectedClass)
                {
                    if (selectedClass.StudentAdded == false)
                    {
                        FormToEditClass editWindow = new FormToEditClass(selectedClass, myData);
                        editWindow.Owner = this;

                        if (editWindow.ShowDialog() == true)
                        {
                            SaveData();
                            LoadData();
                            dataGrid.Items.Refresh();
                            MessageBox.Show("Votre classe a bien été modifiée", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cette classe contient des élèves ! Vous ne pouvez pas la modifier !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez sélectionner une classe à modifier.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region DELETE A CLASS BUTTON :

        private void DeleteAClass_Click(Object sender, RoutedEventArgs e)
        {
            try
            {
                if(dataGrid.SelectedItem is Class selectedClass)
                {
                    if (selectedClass.StudentAdded == false)
                    {
                        var confirmationDelete = MessageBox.Show("Êtes-vous sur de vouloir supprimer cette classe ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (confirmationDelete == MessageBoxResult.Yes)
                        {
                            myData.ListClasses.Remove(selectedClass);

                            SaveData();
                            MessageBox.Show("Votre classe a bien été supprimée", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Opération annulée", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cette classe contient des élèves ! Vous ne pouvez pas la supprimer !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez sélectionner une classe à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region ADD A QUIZZ BUTTON :

        private void AddAQuizz_Click(Object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedItem is Class selectedClass)
                {
                    if (selectedClass.StudentAdded == true)
                    {
                        QuizzPage.FormToAddQuizz window = new QuizzPage.FormToAddQuizz(dataForQuizzes,selectedClass.ClassName,selectedClass.NbStudents);
                        
                        window.Show();
                    }
                    else
                    {
                        MessageBox.Show("Cette classe ne contient pas d'élèves ! Vous ne pouvez pas donc lui ajouter une interrogation !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez sélectionner la classe pour laquelle vous souhaitez ajouter une interrogation.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region ADD A STUDENT BUTTON :

        private void AddAStudent_Click(Object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedItem is Class selectedClass)
                {
                    if (!selectedClass.StudentAdded)
                    {
                        var tempStudents = new MyDataForStudents();

                        for (int i = 0; i < selectedClass.NbStudents; i++)
                        {
                            FormToAddStudent window = new FormToAddStudent(tempStudents, selectedClass.ClassName, selectedClass.NbStudents);
                            result = window.ShowDialog();

                            if (result != true)
                            {
                                MessageBox.Show("Ajout d'élève annulé", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                                return;
                            }
                        }

                        // Si tous les ajouts d'élèves ont été confirmés, mettez à jour dataForStudents
                        foreach (var student in tempStudents.ListStudents)
                        {
                            dataForStudents.AddStudent(student);
                        }

                        SaveStudents(dataForStudents);

                        selectedClass.StudentAdded = true;
                        myData.NotifyPropertyChanged(nameof(myData.CurrentClass.StudentAdded));

                        SaveData();

                        MessageBox.Show("Tous les élèves ont été ajoutés avec succès", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                        StudentPage.PageToShowStudents windowShow = new PageToShowStudents();
                        windowShow.Show();
                    }
                    else
                    {
                        MessageBox.Show("Cette classe contient déjà ses élèves", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez sélectionner la classe pour laquelle vous souhaitez ajouter vos élèves.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
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
                else
                {
                    RegistryKey registryKey1 = Registry.CurrentUser.CreateSubKey("SOFTWARE\\TestCSharp");

                    if (registryKey1 != null)
                    {
                        registryKey1.SetValue("XPosition", (int)this.Left);
                        registryKey1.SetValue("YPosition", (int)this.Top);
                    }
                }
            }
        }

        #endregion
    }
}
