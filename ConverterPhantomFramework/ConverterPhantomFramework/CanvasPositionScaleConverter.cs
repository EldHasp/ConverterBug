using System;
using System.Globalization;
using System.Windows.Data;

namespace ConverterPhantomFramework
{
    public sealed class CanvasPositionScaleConverter : IMultiValueConverter
    {
        public object Convert(
            object[] values,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (values.Length == 4 &&
                values[0] is double value &&
                values[1] is double min &&
                values[2] is double max &&
                values[3] is double canvasWidthHeight)
            {
                return canvasWidthHeight * RangeToNormalizedValue(min, max, value);
            }

            return values;
        }

        private static double RangeToNormalizedValue(
            double min,
            double max,
            double value)
        {
            if (min > max)
            {
                throw new ArgumentException("Min cannot be less than max");
            }

            if (value < min)
            {
                throw new ArgumentException("Value cannot be less than min");
            }

            if (value > max)
            {
                throw new ArgumentException("Value cannot be greater than max");
            }

            return (value - min) / (max - min);
        }

        public object[] ConvertBack(
            object value,
            Type[] targetTypes,
            object parameter,
            CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
