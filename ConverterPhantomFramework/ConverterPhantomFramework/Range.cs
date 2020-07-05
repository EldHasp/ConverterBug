namespace ConverterPhantomFramework
{
    public class Range
    {
        public double Min { get; }
        public double Max { get; }
        public double Size { get; }

        public Range(double min, double max)
        {
            Min = min;
            Max = max;
            Size = Max - Min;
        }
    }
}

