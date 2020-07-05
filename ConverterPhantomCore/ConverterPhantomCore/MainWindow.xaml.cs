using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;

namespace ConverterPhantomCore
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Min = 0;
            Max = 10;
            Size = Max - Min;
            Squares = new ObservableCollection<Square>
            {
                new Square
                {
                    X = 8,
                    Y = 7,
                },
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Resize(object sender, RoutedEventArgs e)
        {
            Min = 0;
            Max = 5;
            Size = Max - Min;
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            Squares.Clear();
        }

        private ObservableCollection<Square> _squares;

        public ObservableCollection<Square> Squares
        {
            get => _squares;
            set
            {
                _squares = value;
                OnPropertyChanged();
            }
        }

        private double _size;

        public double Size
        {
            get => _size;
            set
            {
                _size = value;
                OnPropertyChanged();
            }
        }

        private double _min;

        public double Min
        {
            get => _min;
            set
            {
                _min = value;
                OnPropertyChanged();
            }
        }

        private double _max;

        public double Max
        {
            get => _max;
            set
            {
                _max = value;
                OnPropertyChanged();
            }
        }
    }

    public sealed class Square : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private double _x;

        public double X
        {
            get => _x;
            set
            {
                _x = value;
                OnPropertyChanged();
            }
        }

        private double _y;

        public double Y
        {
            get => _y;
            set
            {
                _y = value;
                OnPropertyChanged();
            }
        }
    }

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

    public sealed class CanvasPositionScaleConverter : IMultiValueConverter
    {
        public object Convert(
            object[] values,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (values.Length != 4 ||
                !(values[0] is double value) ||
                !(values[1] is double min) ||
                !(values[2] is double max) ||
                !(values[3] is double canvasWidthHeight))
            {
                return null;
            }

            return canvasWidthHeight * RangeToNormalizedValue(min, max, value);
        }

        // This is in a utility class normally
        private const string ValueLessThanMin = "Value cannot be less than min";
        private const string ValueGreaterThanMax = "Value cannot be greater than max";

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
                throw new ArgumentException(ValueLessThanMin);
            }

            if (value > max)
            {
                throw new ArgumentException(ValueGreaterThanMax);
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
