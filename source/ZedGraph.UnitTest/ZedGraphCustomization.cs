namespace ZedGraph
{
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoRhinoMock;

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
