namespace ZedGraph
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using NUnit.Framework;

    using Ploeh.AutoFixture;

    [TestFixture]
    public class ZedGraphControlTests
    {
        private class TestZedGraphControl : ZedGraphControl
        {
            public new void ZoomScale(Axis axis, double zoomFraction, double centerVal, bool isZoomOnCenter)
            {
                base.ZoomScale(axis, zoomFraction, centerVal, isZoomOnCenter);
            }

            public new void ZoomPane(
                GraphPane graphPane,
                double zoomFraction,
                PointF centerPoint,
                bool isZoomOnCenter,
                bool isRefresh)
            {
                base.ZoomPane(graphPane, zoomFraction, centerPoint, isZoomOnCenter, isRefresh);
            }

            public new void ZedGraphControl_MouseWheel(object sender, MouseEventArgs e)
            {
                base.ZedGraphControl_MouseWheel(sender, e);
            }
        }

        private const bool ZoomOnScaleCenter = false;

        private const bool ZoomOnCenterValue = true;

        private const double MinZoomFraction = 0.0001;

        private const double MaxZoomFraction = 1000;

        private readonly Random random = new Random();

        [Test]
        public void ZoomPane()
        {
            IFixture fixture = new Fixture().Customize(new ZedGraphCustomization());
            var sut = new ZedGraphControl();
        }

        [Test]
        public void MouseWheel()
        {
            IFixture fixture = new Fixture().Customize(new ZedGraphCustomization());
            var sut = new TestZedGraphControl();
        }

        [Test]
        public void ZoomScale_AxisIsNull_DoesNotThrowException()
        {
            IFixture fixture = new Fixture().Customize(new ZedGraphCustomization());
            var sut = new TestZedGraphControl();

            Assert.That(
                () => sut.ZoomScale(null, fixture.Create<double>(), fixture.Create<double>(), fixture.Create<bool>()),
                Throws.Nothing);
        }

        [Test]
        public void ZoomScale_ZoomFractionLessThanMinimum_ScaleDoesNotChange()
        {
            IFixture fixture = new Fixture().Customize(new ZedGraphCustomization());
            var min = fixture.Create<double>();
            var max = fixture.Create<double>();
            Axis axis = fixture.Create<Axis>()
                .WithScale(min, max);
            Scale scale = axis.Scale;
            scale.MinAuto = scale.MaxAuto = true;
            var sut = new TestZedGraphControl();

            sut.ZoomScale(axis, MinZoomFraction, fixture.Create<double>(), fixture.Create<bool>());

            Assert.That(scale.Min, Is.EqualTo(min), "The scale's minimum value has changed unexpectantly");
            Assert.That(scale.Max, Is.EqualTo(max), "The scale's maximum value has changed unexpectantly");
            Assert.That(scale.MinAuto, Is.True, "The scale's automatic minimum flag appears to have changed");
            Assert.That(scale.MaxAuto, Is.True, "The scale's automatic maximum flag appears to have changed");
        }

        [Test]
        public void ZoomScale_ZoomFractionGreaterThanMaximum_ScaleDoesNotChange()
        {
            IFixture fixture = new Fixture().Customize(new ZedGraphCustomization());
            var min = fixture.Create<double>();
            var max = fixture.Create<double>();
            Axis axis = fixture.Create<Axis>()
                .WithScale(min, max);
            Scale scale = axis.Scale;
            scale.MinAuto = scale.MaxAuto = true;
            var sut = new TestZedGraphControl();

            sut.ZoomScale(axis, MaxZoomFraction, fixture.Create<double>(), fixture.Create<bool>());

            Assert.That(scale.Min, Is.EqualTo(min), "The scale's minimum value has changed unexpectantly");
            Assert.That(scale.Max, Is.EqualTo(max), "The scale's maximum value has changed unexpectantly");
            Assert.That(scale.MinAuto, Is.True, "The scale's automatic minimum flag appears to have changed");
            Assert.That(scale.MaxAuto, Is.True, "The scale's automatic maximum flag appears to have changed");
        }

        [Test]
        [Theory]
        public void ZoomScale_ZoomOnScaleCenter(AxisType axisType)
        {
            IFixture fixture = new Fixture().Customize(new ZedGraphCustomization());
            double zoomFraction = this.random.NextDouble() * 2;
            var min = fixture.Create<double>();
            double max = min + fixture.Create<double>();
            Axis axis = fixture.Create<Axis>()
                .OfType(axisType)
                .WithScale(min, max);
            Scale scale = axis.Scale;
            double linearMax = scale.Linearize(max);
            double linearMin = scale.Linearize(min);
            double scaleCenter = (linearMin + linearMax) / 2;
            double halfRange = (linearMax - linearMin) * zoomFraction / 2.0;
            double expectedMin = scale.DeLinearize(scaleCenter - halfRange);
            double expectedMax = scale.DeLinearize(scaleCenter + halfRange);
            var sut = new TestZedGraphControl();

            sut.ZoomScale(axis, zoomFraction, fixture.Create<double>(), ZoomOnScaleCenter);

            Assert.That(
                scale.Min,
                Is.EqualTo(expectedMin)
                    .Within(0.001),
                "The scale's minimum was not the expected value");
            Assert.That(
                scale.Max,
                Is.EqualTo(expectedMax)
                    .Within(0.001),
                "The scale's maximum was not the expected value");
            Assert.That(scale.MinAuto, Is.False, "The scale's automatic minimum flag was not set to false");
            Assert.That(scale.MinAuto, Is.False, "The scale's automatic maximum flag was not set to false");
        }

        [Test]
        [Theory]
        public void ZoomScale_ZoomOnCenterValue(AxisType axisType)
        {
            IFixture fixture = new Fixture().Customize(new ZedGraphCustomization());
            double zoomFraction = this.random.NextDouble() * 2;
            var min = fixture.Create<double>();
            double centerValue = min + fixture.Create<double>();
            double max = centerValue + fixture.Create<double>();
            Axis axis = fixture.Create<Axis>()
                .OfType(axisType)
                .WithScale(min, max);
            Scale scale = axis.Scale;
            double linearMax = scale.Linearize(max);
            double linearMin = scale.Linearize(min);
            double expectedMin = scale.DeLinearize(centerValue - ((centerValue - linearMin) * zoomFraction));
            double expectedMax = scale.DeLinearize(centerValue + ((linearMax - centerValue) * zoomFraction));
            var sut = new TestZedGraphControl();

            sut.ZoomScale(axis, zoomFraction, centerValue, ZoomOnCenterValue);

            Assert.That(
                scale.Min,
                Is.EqualTo(expectedMin)
                    .Within(0.001),
                "The scale's minimum was not the expected value");
            Assert.That(
                scale.Max,
                Is.EqualTo(expectedMax)
                    .Within(0.001),
                "The scale's maximum was not the expected value");
            Assert.That(scale.MinAuto, Is.False, "The scale's automatic minimum flag was not set to false");
            Assert.That(scale.MinAuto, Is.False, "The scale's automatic maximum flag was not set to false");
        }
    }
}
