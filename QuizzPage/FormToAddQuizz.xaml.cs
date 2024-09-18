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
using ClassLibrary1;
using System.Globalization;

namespace QuizzPage
{
    /// <summary>
    /// Logique d'interaction pour FormToAddQuizz.xaml
    /// </summary>
    public partial class FormToAddQuizz : Window
    {
        #region FIELDS + CONSTRUCTOR :

        private MyDataForQuizzes myData;
        private String nameOfQuizz = null;
        private int numberStudents = 0;

        private bool forceClose = false;

        public FormToAddQuizz(MyDataForQuizzes data,String classNameQuizz,int nbStudentsOfClass)
        {
            InitializeComponent();

            nameOfQuizz = classNameQuizz;
            numberStudents = nbStudentsOfClass;
            myData = data;
        }

        #endregion

        #region ADD BUTTON :

        private void AddQuizzClick(object sender, RoutedEventArgs e)
        {
            DateTime? selectedDate = dateOfQuizz.SelectedDate;
            DateTime currentDate = DateTime.Now.Date;

            if (!string.IsNullOrEmpty(textBoxQuizzTitle.Text)
                && !string.IsNullOrEmpty(textBoxQuizzTotal.Text)
                && selectedDate.HasValue
                && comboBoxCourseQuizz.SelectedItem is ComboBoxItem selectedItem)
            {
                if (selectedDate.Value > currentDate)
                {
                    MessageBox.Show("La date de l'interrogation ne peut pas être postérieure à la date actuelle.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    bool exists = CheckIfQuizzNameExists(textBoxQuizzTitle.Text);

                    if (!exists)
                    {
                        string dateString = selectedDate.Value.ToString("dd/MM/yyyy");
                        DateTime formattedDate = DateTime.ParseExact(dateString, "dd/M/yyyy",null);
                        string course = selectedItem.Content.ToString();
                        int total = int.Parse((string)textBoxQuizzTotal.Text);

                        Quizz newQuizz = new Quizz(textBoxQuizzTitle.Text, formattedDate, new Class(nameOfQuizz, numberStudents,true), course,total,false);

                        myData.AddQuizz(newQuizz);

                        jSaveToJson(sender, e);

                        MessageBox.Show("Interrogation ajoutée avec succès", "OK", MessageBoxButton.OK, MessageBoxImage.Information);

                        
                        PageToShowQuizzes window = new PageToShowQuizzes(myData);
                        window.Show();

                        forceClose = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Cette interrogation existe déjà. Veuillez entrer une nouvel intitulé.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Un des champs n'a pas été complété", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
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

        #region WRITE DATA IN JSON FILE :

        private void jSaveToJson(object sender, RoutedEventArgs e)
        {
            JsonSerializerOptions options = new()
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            File.WriteAllText("C:\\Users\\Utilisateur\\source\\repos\\Projet_final_CSharp_CARON\\Fichiers_JSON_donnees\\myQuizzes.json", System.Text.Json.JsonSerializer.Serialize(myData, options));
        }

        #endregion

        #region CANCEL BUTTON :

        private void CancelAddQuizz(object sender, RoutedEventArgs e)
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
            if(!forceClose)
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
