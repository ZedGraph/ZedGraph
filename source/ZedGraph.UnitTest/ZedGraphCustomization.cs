namespace ZedGraph
{
    using AutoFixture;
    using AutoFixture.AutoRhinoMock;

    public class ZedGraphCustomization : CompositeCustomization
    {
        public ZedGraphCustomization()
            : base(
                new AutoRhinoMockCustomization(),
                new AxisCustomization(),
                new AxisLabelCustomization(),
                new DrawingCustomization())
        {
        }
    }
}
