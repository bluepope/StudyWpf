using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

using Study03.Directorys.Data;

namespace Study03.Converters
{
    [ValueConversion(typeof(DirectoryItemType),typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var directoryItemType = (DirectoryItemType)value;

            if (directoryItemType == DirectoryItemType.Drive)
            {
                return new BitmapImage(new Uri($"pack://application:,,,/Images/drive.png"));
            }
            else if(directoryItemType == DirectoryItemType.Folder)
            {
                return new BitmapImage(new Uri($"pack://application:,,,/Images/folder.png"));
            }
            else
            {
                return new BitmapImage(new Uri($"pack://application:,,,/Images/file.png"));
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
