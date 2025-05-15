namespace ZedGraph
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;

    using NUnit.Framework;

    using Albedo;
    using AutoFixture;
    using AutoFixture.AutoRhinoMock;
    using AutoFixture.Idioms;

    using Rhino.Mocks;

	[TestFixture]
    internal class ValuesToolTipTests
    {
        [Test]
        public void GuardClauses()
        {
            IFixture fixture = new Fixture().Customize(new AutoRhinoMockCustomization());
            fixture.Customize<Control>(c => c.OmitAutoProperties());
            fixture.Customizations.Add(new PointBuilder());
            var methods = new Methods<ValuesToolTip>();
            var members = new List<MemberInfo>(typeof(ValuesToolTip).GetMembers());
            members.Remove(methods.Select(tt => tt.Set(default(string))));
            members.Remove(methods.Select(tt => tt.Set(default(string), default(Point))));

            fixture.Create<GuardClauseAssertion>()
                .Verify(members);
        }

        [Test]
        public void Disable_ActiveCallback_False()
        {
            var fixture = new Fixture();
            fixture.Register(() => MockRepository.GenerateMock<Action<bool>>());
            fixture.Customize<Control>(c => c.OmitAutoProperties());
            var sut = fixture.Create<ValuesToolTip>();

            sut.Disable();

            sut.ActiveCallback.AssertWasCalled(c => c(false));
        }

        [Test]
        public void Enable_ActiveCallback_True()
        {
            var fixture = new Fixture();
            fixture.Register(() => MockRepository.GenerateMock<Action<bool>>());
            fixture.Customize<Control>(c => c.OmitAutoProperties());
            var sut = fixture.Create<ValuesToolTip>();

            sut.Enable();

            sut.ActiveCallback.AssertWasCalled(c => c(true));
        }

        [Test]
        public void Set_SetCallbackCalled_WithControlAndCaption()
        {
            var fixture = new Fixture();
            fixture.Register(() => MockRepository.GenerateMock<Action<Control, string>>());
            fixture.Customize<Control>(c => c.OmitAutoProperties());
            string caption = fixture.Create<string>();
            var sut = fixture.Create<ValuesToolTip>();

            sut.Set(caption);

            sut.SetCallback.AssertWasCalled(c => c(sut.Control, caption));
        }

        [Test]
        public void Set_MultipleDuplicateCaptions_SetOnlyOnce()
        {
            var fixture = new Fixture();
            fixture.Register(() => MockRepository.GenerateMock<Action<Control, string>>());
            fixture.Customize<Control>(c => c.OmitAutoProperties());
            string caption = fixture.Create<string>();
            var sut = fixture.Create<ValuesToolTip>();

            sut.Set(caption);
            sut.Set(caption);

            sut.SetCallback.AssertWasCalled(c => c(sut.Control, caption), o => o.Repeat.Once());
        }

        [Test]
        public void Set_MultipleDuplicateCaptionsAtSamePoint_SetOnlyOnce()
        {
            var fixture = new Fixture();
            fixture.Register(() => MockRepository.GenerateMock<Action<Control, string>>());
            fixture.Customize<Control>(c => c.OmitAutoProperties());
            fixture.Customizations.Add(new PointBuilder());
            var point = fixture.Create<Point>();
            string caption = fixture.Create<string>();
            var sut = fixture.Create<ValuesToolTip>();

            sut.Set(caption, point);
            sut.Set(caption, point);

            sut.SetCallback.AssertWasCalled(c => c(sut.Control, caption), o => o.Repeat.Once());
        }

        [Test]
        public void Set_MultipleDifferentCaptionsAtDifferentPoints_SetMultipleTimes()
        {
            var fixture = new Fixture();
            fixture.Register(() => MockRepository.GenerateMock<Action<Control, string>>());
            fixture.Customize<Control>(c => c.OmitAutoProperties());
            fixture.Customizations.Add(new PointBuilder());
            var point1 = fixture.Create<Point>();
            var point2 = fixture.Create<Point>();
            string caption = fixture.Create<string>();
            var sut = fixture.Create<ValuesToolTip>();
            Assert.That(point1, Is.Not.EqualTo(point2));

            sut.Set(caption, point1);
            sut.Set(caption, point2);

            sut.SetCallback.AssertWasCalled(c => c(sut.Control, caption), o => o.Repeat.Twice());
        }

        [Test]
        public void Set_DifferentCaptionsAtSamePoint_SetMultipleTimes()
        {
            var fixture = new Fixture();
            fixture.Register(() => MockRepository.GenerateMock<Action<Control, string>>());
            fixture.Customize<Control>(c => c.OmitAutoProperties());
            fixture.Customizations.Add(new PointBuilder());
            var point = fixture.Create<Point>();
            var sut = fixture.Create<ValuesToolTip>();

            sut.Set(fixture.Create<string>(), point);
            sut.Set(fixture.Create<string>(), point);

            sut.SetCallback.AssertWasCalled(c => c(Arg.Is(sut.Control), Arg<string>.Is.Anything), o => o.Repeat.Twice());
        }

        [Test]
        public void Set_CaptionAtDefaultPoint_CaptionSet()
        {
            var fixture = new Fixture();
            fixture.Register(() => MockRepository.GenerateMock<Action<Control, string>>());
            fixture.Customize<Control>(c => c.OmitAutoProperties());
            fixture.Customizations.Add(new PointBuilder());
            string caption = fixture.Create<string>();
            var sut = fixture.Create<ValuesToolTip>();

            sut.Set(caption, default(Point));

            sut.SetCallback.AssertWasCalled(c => c(sut.Control, caption));
        }

        [Test]
        public void Create_SetCallback_IsToolTipSetToolTip()
        {
            var toolTip = new ToolTip();
            ValuesToolTip sut = ValuesToolTip.Create(new Control(), toolTip);

            Assert.That(sut.SetCallback, Is.EqualTo(new Action<Control, string>(toolTip.SetToolTip)));
        }

        [Test]
        public void Create_Control_IsSuppliedControl()
        {
            var control = new Control();
            ValuesToolTip sut = ValuesToolTip.Create(control, new ToolTip());

            Assert.That(sut.Control, Is.EqualTo(control));
        }

        [Test]
        public void Create_EnableCreatedToolTip_SetsToolTipActive()
        {
            var toolTip = new ToolTip { Active = false };
            ValuesToolTip sut = ValuesToolTip.Create(new Control(), toolTip);

            sut.Enable();

            Assert.That(toolTip.Active, Is.True, "The non-active tool tip was not set to active.");
        }

        [Test]
        public void Create_DisableCreatedToolTip_SetsToolTipNotActive()
        {
            var toolTip = new ToolTip { Active = true };
            ValuesToolTip sut = ValuesToolTip.Create(new Control(), toolTip);

            sut.Disable();

            Assert.That(toolTip.Active, Is.False, "The active tool tip was still active.");
        }
    }
}
