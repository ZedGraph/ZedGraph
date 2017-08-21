namespace ZedGraph
{
    using System.Drawing;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    public class DrawingCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new PointBuilder());
            fixture.Customizations.Add(new TypeRelay(typeof(Brush), typeof(SolidBrush)));
        }
    }
}
