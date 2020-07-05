using System;
using System.Collections.ObjectModel;

namespace ConverterPhantomFramework
{
    public class ViewModel : BaseINPC
    {
        public ObservableCollection<Square> Squares { get; }
            = new ObservableCollection<Square>()
            {
                new Square {X = 8, Y = 7}
            };


        private Range _range = new Range(0, 10);
        public Range Range { get => _range; set => Set(ref _range, value); }

        private RelayCommand _clearCommand;
        public RelayCommand ClearCommand => _clearCommand ?? (_clearCommand = new RelayCommand(ClearMethod));

        private void ClearMethod(object parameter)
        {
            for (int i = 0; i < Squares.Count; i++)
                Squares[i] = null;

            Squares.Clear();
            
        }

        private RelayCommand _resizeCommand;
        public RelayCommand ResizeCommand => _resizeCommand ?? (_resizeCommand = new RelayCommand(ResizeMethod));

        private void ResizeMethod(object parameter)
        {
            Range = new Range(0, 5);
        }
    }
}

