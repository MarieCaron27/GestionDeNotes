using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;
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
using System.IO;
using ClassLibrary1;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace StudentPage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PageToShowStudents : Window
    {
        #region FIELD AND CONSTRUCTOR :

        MyDataForStudents myData = null;
        
        public PageToShowStudents()
        {
            InitializeComponent();
            LoadData();
            DataContext = myData;
        }

        #endregion

        #region LOAD AND SAVE DATA METHODS :

        private void LoadData()
        {
            string dataPathForStudents = "C:\\Users\\Utilisateur\\source\\repos\\Projet_final_CSharp_CARON\\Fichiers_JSON_donnees\\myStudents.json";

            if (File.Exists(dataPathForStudents))
            {
                string jsonFileForStudents = File.ReadAllText(dataPathForStudents);
                myData = JsonConvert.DeserializeObject<MyDataForStudents>(jsonFileForStudents);
            }
            else
            {
                myData = new MyDataForStudents();
            }

            DataContext = myData;
        }

        private void SaveData()
        {
            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };

            File.WriteAllText("C:\\Users\\Utilisateur\\source\\repos\\Projet_final_CSharp_CARON\\Fichiers_JSON_donnees\\myStudents.json", System.Text.Json.JsonSerializer.Serialize(myData, options));
        }

        #endregion

        #region UPDATE A STUDENT :

        private void UpdateAStudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedItem is Student selectedStudent)
                {
                    FormToEditStudent editWindow = new FormToEditStudent(selectedStudent,myData);
                    editWindow.Owner = this;

                    if (editWindow.ShowDialog() == true)
                    {
                        SaveData();
                        LoadData();
                        dataGrid.ItemsSource = myData.ListStudents;
                        dataGrid.Items.Refresh();
                        MessageBox.Show("Votre élève a bien été modifiée", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez sélectionner un élève à modifier.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
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
            var result = MessageBox.Show("Voullez-vous quitter?", "Quitter?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        #endregion
    }
}