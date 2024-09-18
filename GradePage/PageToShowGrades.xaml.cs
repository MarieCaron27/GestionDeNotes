using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassLibrary1;
using Newtonsoft.Json;
using System.Text.Json;
using System.IO;
using StudentPage;

namespace GradePage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PageToShowGrades : Window
    {
        #region FIELDS AND CONSTRUCTOR :

        private MyDataForGrades dataForGrades;
        private MyDataForStudents dataForStudents;
        private Quizz selectedQuizz;

        private bool forceClose = false;

        public PageToShowGrades(MyDataForGrades grades, Quizz theQuizz, MyDataForStudents students)
        {
            InitializeComponent();

            dataForGrades = grades;
            dataForStudents = students;
            selectedQuizz = theQuizz;

            LoadDataForGrades();
            LoadDataForStudents();

            var studentsInClass = dataForStudents.ListStudents.Where(student => student.ClassStudent.ClassName == selectedQuizz.QuizzClass.ClassName).ToList();

            List<Grade> gradesForDisplay = new List<Grade>();
            foreach (var student in studentsInClass)
            {
                var grade = new Grade(student, selectedQuizz.QuizzClass, selectedQuizz, 0);
                gradesForDisplay.Add(grade);
            }

            dataGrid.ItemsSource = gradesForDisplay;
        }

        #endregion

        #region LOAD AND SAVE DATA :

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
        }

        private void SaveGrade_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in dataGrid.Items)
            {
                if (item is Grade grade)
                {
                    if (grade.Result < 0 || grade.Result > selectedQuizz.QuizzTotal)
                    {
                        MessageBox.Show("Une des notes n'est pas valide !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    dataForGrades.AddGrade(grade);
                }
            }

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            File.WriteAllText("C:\\Users\\Utilisateur\\source\\repos\\Projet_final_CSharp_CARON\\Fichiers_JSON_donnees\\myGrades.json", System.Text.Json.JsonSerializer.Serialize(dataForGrades, options));

            MessageBox.Show("Notes enregistrées avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        #endregion

        #region WINDOW CLOSING BUTTON :

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!forceClose)
            {
                if (MessageBox.Show("Voulez-vous quitter?", "Quitter?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        #endregion
    }
}