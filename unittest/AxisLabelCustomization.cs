namespace ZedGraph
{
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    public class AxisLabelCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<AxisLabel>(c => c.FromFactory(new MethodInvoker(new GreedyConstructorQuery())));
        }
    }
}
