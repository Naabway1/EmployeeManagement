using System;
using System.Globalization;
using System.Windows.Data;

namespace EmployeeManagement.Core
{
    public class NullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Возвращаем true, если value не равно null, иначе false
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Обратное преобразование обычно не требуется, выбрасываем исключение
            throw new NotImplementedException();
        }
    }
}
