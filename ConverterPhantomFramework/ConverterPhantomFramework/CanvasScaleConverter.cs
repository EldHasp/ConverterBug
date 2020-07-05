using System;
using System.Globalization;
using System.Windows.Data;

namespace ConverterPhantomFramework
{
    public sealed class CanvasScaleConverter : IValueConverter
    {
        private const double Scale = 100;

        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (value is double valueToScale)
            {
                return valueToScale * Scale;
            }

            return value;
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
