namespace ZedGraph
{
    using AutoFixture;
    using AutoFixture.Kernel;

    public class AxisLabelCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<AxisLabel>(c => c.FromFactory(new MethodInvoker(new GreedyConstructorQuery())));
        }
    }
}
