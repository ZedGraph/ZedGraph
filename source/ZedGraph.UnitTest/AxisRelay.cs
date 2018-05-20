namespace ZedGraph
{
    using System;

    using Ploeh.AutoFixture.Kernel;

    public class AxisRelay : ISpecimenBuilder
    {
        private readonly Type[] concreteTypes = { typeof(XAxis), typeof(YAxis), typeof(X2Axis), typeof(Y2Axis) };

        private readonly Random random = new Random();

        public object Create(object request, ISpecimenContext context)
        {
            if (!typeof(Axis).Equals(request))
            {
                return new NoSpecimen();
            }

            int index = this.random.Next(0, this.concreteTypes.Length - 1);
            Type concreteType = this.concreteTypes[index];

            return context.Resolve(concreteType);
        }
    }
}
