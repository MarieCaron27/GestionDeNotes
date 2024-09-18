using ClassLibrary1;
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

namespace QuizzPage
{
    /// <summary>
    /// Logique d'interaction pour FormToEditQuizz.xaml
    /// </summary>
    public partial class FormToEditQuizz : Window
    {
        #region FIELDS AND CONSTRUCTOR :

        private Quizz _quizzToEdit;
        private MyDataForQuizzes _myData;

        private string _originalQuizzTitle;
        private DateTime _originalQuizzDate;
        private Class _originalQuizzClass;
        private string _originalQuizzCourse;
        private int _originalQuizzTotal;

        private bool forceClose = false;
        public FormToEditQuizz(Quizz quizzToEdit, MyDataForQuizzes myData)
        {
            InitializeComponent();

            _quizzToEdit = quizzToEdit;

            _originalQuizzTitle = _quizzToEdit.QuizzTitle;
            _originalQuizzDate = _quizzToEdit.QuizzDate;
            _originalQuizzClass = _quizzToEdit.QuizzClass;
            _originalQuizzCourse = _quizzToEdit.QuizzCourse;
            _originalQuizzTotal = _quizzToEdit.QuizzTotal;

            DataContext = _quizzToEdit;
            _myData = myData;
        }

        #endregion

        #region ADD A QUIZZ BUTTON :

        private void SaveQuizz_Click(object sender, RoutedEventArgs e)
        {
            DateTime? selectedDate = dateEditQuizz.SelectedDate;
            DateTime currentDate = DateTime.Now.Date;

            if (!string.IsNullOrEmpty(textBoxEdtQuizzTitle.Text)
                && !string.IsNullOrEmpty(textBoxEditQuizzTotal.Text)
                && selectedDate.HasValue
                && comboBoxEditCourseQuizz.SelectedItem is ComboBoxItem selectedItem)
            {
                string newQuizzTitle = textBoxEdtQuizzTitle.Text.Trim();
                DateTime newQuizzDate = selectedDate.Value;
                string newQuizzCourse = comboBoxEditCourseQuizz.Text.Trim();
                int total = int.Parse((string)textBoxEditQuizzTotal.Text);

                bool exists = CheckIfQuizzNameExists(newQuizzTitle);

                if (!exists || newQuizzTitle == _originalQuizzTitle)
                {
                    _quizzToEdit.QuizzTitle = newQuizzTitle;
                    _quizzToEdit.QuizzCourse = newQuizzCourse;
                    _quizzToEdit.QuizzDate = newQuizzDate;
                    _quizzToEdit.QuizzClass = _originalQuizzClass;
                    _quizzToEdit.QuizzTotal = total;
                    _quizzToEdit.ResultsAdded = false;

                    jSaveToJson();

                    MessageBox.Show("Votre interrogation a bien été modifiée.", "Modification validée", MessageBoxButton.OK, MessageBoxImage.Information);

                    forceClose = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Cette interrogation existe déjà. Veuillez entrer une nouvelle interrogation.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Un de vos champs est incorrect.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region VERIFY THAT QUIZZ NAME DOESN'T ALREADY EXISTS :

        public static bool CheckIfQuizzNameExists(string theQuizz)
        {
            string filePath = "C:\\Users\\Utilisateur\\source\\repos\\Projet_final_CSharp_CARON\\Fichiers_JSON_donnees\\myQuizzes.json";

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                MyDataForQuizzes myData = JsonConvert.DeserializeObject<MyDataForQuizzes>(json);

                foreach (var quizzItem in myData.ListQuizzes)
                {
                    if (quizzItem.QuizzTitle.Equals(theQuizz, StringComparison.OrdinalIgnoreCase))
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
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
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
                _quizzToEdit.QuizzTitle = _originalQuizzTitle;
                _quizzToEdit.QuizzDate = _originalQuizzDate;
                _quizzToEdit.QuizzClass = _originalQuizzClass;
                _quizzToEdit.QuizzCourse = _originalQuizzCourse;

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
