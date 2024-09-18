using ClassLibrary1;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GradePage;
using StudentPage;

namespace QuizzPage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PageToShowQuizzes : Window
    {
        #region DATA AND PROPRETIES :

        MyDataForQuizzes myData = null;
        MyDataForGrades dataForGrades;
        MyDataForStudents dataForStudents; 

        private bool forceClose = false;

        public PageToShowQuizzes(MyDataForQuizzes data)
        {
            InitializeComponent();
            LoadData();
            LoadDataForGrades();
            LoadDataForStudents();
            myData = data;
            DataContext = myData;
        }

        #endregion

        #region LOAD DATA METHODS :

        private void LoadData()
        {
            string dataPathForQuizzes = "C:\\Users\\Utilisateur\\source\\repos\\Projet_final_CSharp_CARON\\Fichiers_JSON_donnees\\myQuizzes.json";

            if (File.Exists(dataPathForQuizzes))
            {
                string jsonFileForQuizzes = File.ReadAllText(dataPathForQuizzes);
                myData = JsonConvert.DeserializeObject<MyDataForQuizzes>(jsonFileForQuizzes);
            }
            else
            {
                myData = new MyDataForQuizzes();
            }

            DataContext = myData;
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

        private void SaveData()
        {
            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };

            File.WriteAllText("C:\\Users\\Utilisateur\\source\\repos\\Projet_final_CSharp_CARON\\Fichiers_JSON_donnees\\myQuizzes.json", System.Text.Json.JsonSerializer.Serialize(myData, options));
        }

        #endregion

        #region UPDATE A QUIZZ BUTTON :

        private void UpdateAQuizz_Click(Object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedItem is Quizz selectedQuizz)
                {
                    if (selectedQuizz.ResultsAdded == false)
                    {
                        FormToEditQuizz editWindow = new FormToEditQuizz(selectedQuizz, myData);
                        editWindow.Owner = this;

                        if (editWindow.ShowDialog() == true)
                        {
                            SaveData();
                            LoadData();
                            dataGrid.Items.Refresh();
                            MessageBox.Show("Votre interrogation a bien été modifiée", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cette interrogation contient des résultats ! Vous ne pouvez pas la modifier !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez sélectionner une interrogation à modifier.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region DELETE A QUIZZ BUTTON :

        private void DeleteAQuizz_Click(Object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedItem is Quizz selectedQuizz)
                {
                    if (selectedQuizz.ResultsAdded == false)
                    {
                        var confirmationDelete = MessageBox.Show("Êtes-vous sur de vouloir supprimer cette interrogation ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (confirmationDelete == MessageBoxResult.Yes)
                        {
                            myData.ListQuizzes.Remove(selectedQuizz);

                            SaveData();
                            MessageBox.Show("Votre interrogation a bien été supprimée", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Opération annulée", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cette interrogation contient des résultats ! Vous ne pouvez pas la supprimer !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez sélectionner une interrogation à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region ADD A GRADE BUTTON :

        private void AddAGrade_Click(Object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedItem is Quizz quizzSelected)
                {
                    if (quizzSelected.ResultsAdded == false)
                    {
                        PageToShowGrades windowGrade = new PageToShowGrades(dataForGrades, quizzSelected, dataForStudents);
                        windowGrade.Show();

                        quizzSelected.ResultsAdded = true;
                        myData.NotifyPropertyChanged(nameof(myData.CurrentQuizz.ResultsAdded));
                        SaveData();

                        forceClose = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Cette interrogation contient déjà ses résultats", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez sélectionner l'interrogation pour laquelle vous souhaitez ajouter vos résultats.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
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
            }
        }

        #endregion
    }
}