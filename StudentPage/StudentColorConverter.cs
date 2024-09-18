using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace StudentPage
{
    public class StudentColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string ImagePath)
            {
                if (ImagePath.Equals("C:\\Users\\Utilisateur\\source\\repos\\ClassesTest\\Images\\defaultPersonImage.jpg"))
                {
                    return Brushes.LightSkyBlue;
                }
                else
                {
                    return Brushes.LightGreen;
                }
            }

            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

