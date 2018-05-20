namespace ZedGraph
{
    using Ploeh.AutoFixture;

    public class AxisCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new AxisRelay());
        }
    }
}
