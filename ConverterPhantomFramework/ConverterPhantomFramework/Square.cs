namespace ConverterPhantomFramework
{
    public sealed class Square : BaseINPC
    {
        private double _x;
        public double X { get => _x; set => Set(ref _x, value); }

        private double _y;
        public double Y { get => _y; set => Set(ref _y, value); }
    }

}
