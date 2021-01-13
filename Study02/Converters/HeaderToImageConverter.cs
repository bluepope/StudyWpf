using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Study02.Converters
{
    [ValueConversion(typeof(string),typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = value as string;

            if (path == null)
                return null;

            if (File.Exists(path))
            {
                return new BitmapImage(new Uri($"pack://application:,,,/Images/file.png"));
            }
            else if (Directory.Exists(path))
            {
                return new BitmapImage(new Uri($"pack://application:,,,/Images/folder.png"));
            }
            else
            {
                return new BitmapImage(new Uri($"pack://application:,,,/Images/drive.png"));
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
