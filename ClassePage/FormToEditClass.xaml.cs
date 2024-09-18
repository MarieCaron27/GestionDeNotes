using ClassLibrary1;
using Newtonsoft.Json;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace ClassePage
{
    public partial class FormToEditClass : Window
    {
        #region FIELDS AND CONSTRUCTOR :

        private Class _classToEdit;
        private MyDataForClasses _myData;
        private string _originalClassName;

        private bool forceClose = false;
        public FormToEditClass(Class classToEdit, MyDataForClasses myData)
        {
            InitializeComponent();
            _classToEdit = classToEdit;
            _originalClassName = _classToEdit.ClassName;
            DataContext = _classToEdit;
            _myData = myData;
        }

        #endregion

        #region SAVE A CLASS BUTTON :

        private void SaveClass_Click(object sender, RoutedEventArgs e)
        {
            string myRegex = @"^[1-6][A-Za-z]$";

            if (!string.IsNullOrEmpty(textBoxEditClass.Text)
                && Regex.IsMatch(textBoxEditClass.Text, myRegex)
                 && !string.IsNullOrEmpty(textBoxEditNbStudents.Text))
            {
                string newClassName = textBoxEditClass.Text.Trim();
                int newNbStudents = int.Parse((string)textBoxEditNbStudents.Text.Trim());

                bool exists = CheckIfClassNameExists(newClassName);

                if (!exists || newClassName == _originalClassName)
                {
                    if (newNbStudents < 0 || newNbStudents > 30)
                    {
                        MessageBox.Show("Le nombre d'étudiants dans votre classe est trop petit.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        _classToEdit.ClassName = newClassName;
                        _classToEdit.NbStudents = newNbStudents;
                        jSaveToJson();

                        MessageBox.Show("Votre classe a bien été modifiée.", "Modification validée", MessageBoxButton.OK, MessageBoxImage.Information);

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
                MessageBox.Show("Le nom de la classe est invalide. Veuillez entrer un nom au format correct (ex: 3A, 2B, etc.).", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
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

        #region SAVE TO JSON METHOD :

        private void jSaveToJson()
        {
            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };

            File.WriteAllText("C:\\Users\\Utilisateur\\source\\repos\\Projet_final_CSharp_CARON\\Fichiers_JSON_donnees\\myClasses.json", System.Text.Json.JsonSerializer.Serialize(_myData, options));
        }

        #endregion

        #region CANCEL BUTTON :

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Voulez-vous vraiment quitter?", "Annulation ajout classe", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _classToEdit.ClassName = _originalClassName;

                MessageBox.Show("Opération annulée", "Annulation ajout classe", MessageBoxButton.OK, MessageBoxImage.Information);

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

