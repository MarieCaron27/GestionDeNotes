using ClassLibrary1;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.RegularExpressions;
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
using Newtonsoft.Json;
using System.ComponentModel;

namespace ClassePage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class FormToAddClass : Window
    {
        #region FIELD + CONSTRUCTOR :

        private MyDataForClasses myData;
        private bool forceClose = false;
 
        public FormToAddClass(MyDataForClasses data)
        {
            InitializeComponent();
            myData = data;
        }

        #endregion

        #region ADD BUTTON :

        private void AddClassClick(object sender, RoutedEventArgs e)
        {
            string myRegex = @"^[1-6][A-Za-z]$";

            if (!string.IsNullOrEmpty(textBoxAddClass.Text) 
                && Regex.IsMatch(textBoxAddClass.Text, myRegex)
                && !string.IsNullOrEmpty(textBoxNbStudents.Text))
            {

                bool exists = CheckIfClassNameExists(textBoxAddClass.Text);

                if (!exists)
                {
                    int nbStudents = int.Parse((string)textBoxNbStudents.Text);

                    if(nbStudents < 0 || nbStudents > 30)
                    {
                        MessageBox.Show("Le nombre d'étudiants dans votre classe est trop petit.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        Class newClass = new Class(textBoxAddClass.Text, nbStudents, false);

                        myData.ListClasses.Add(newClass);

                        jSaveToJson(sender, e);

                        MessageBox.Show("Classe ajoutée avec succès", "OK", MessageBoxButton.OK, MessageBoxImage.Information);

                        forceClose = true;
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Cette classe existe déjà. Veuillez entrer une nouvelle classe.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show("Le nom de la classe est invalide. Veuillez entrer un nom au format correct.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region VERIFY THAT CLASS NAME DOESN'T ALREADY EXISTS :

        public static bool CheckIfClassNameExists(string theClass)
        {
            string filePath = "C:\\Users\\Utilisateur\\source\\repos\\Projet_final_CSharp_CARON\\Fichiers_JSON_donnees\\myClasses.json";

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                MyDataForClasses myData = JsonConvert.DeserializeObject<MyDataForClasses>(json);

                foreach (var classItem in myData.ListClasses)
                {
                    if (classItem.ClassName.Equals(theClass, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion

        #region WRITE DATA IN JSON FILE :

        private void jSaveToJson(object sender, RoutedEventArgs e)
        {
            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };

            File.WriteAllText("C:\\Users\\Utilisateur\\source\\repos\\Projet_final_CSharp_CARON\\Fichiers_JSON_donnees\\myClasses.json", System.Text.Json.JsonSerializer.Serialize(myData, options));
        }

        #endregion

        #region CANCEL BUTTON :

        private void CancelAddClass(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Voulez-vous vraiment quitter?", "Annulation ajout classe", MessageBoxButton.YesNo, MessageBoxImage.Question);
 
            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Opération annulée", "Annulation ajout classe", MessageBoxButton.OK, MessageBoxImage.Error);

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