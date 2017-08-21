namespace ZedGraph
{
    public static class AxisExtensions
    {
        public static Axis OfType(this Axis axis, AxisType axisType)
        {
            axis.Type = axisType;
            if (axisType == AxisType.Exponent)
            {
                axis.Scale.Exponent = 2;
            }

            return axis;
        }

        public static Axis WithScale(this Axis axis, double min, double max)
        {
            axis.Scale.Min = min;
            axis.Scale.Max = max;

            return axis;
        }
    }
}
