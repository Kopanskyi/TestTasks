using Sortable_Shapes.Interfaces;
using System;

namespace Sortable_Shapes.Shapes
{
    class Circle : IShape, IComparable
    {
        public double Radius { get; set; }

        public Circle(double radius)
        {
            Radius = radius;
        }

        public double GetArea()
        {
            return Math.Pow(Math.PI * Radius, 2);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            var otherShape = obj as IShape;

            if (otherShape == null)
            {
                throw new ArgumentException("Object is not a Shape");
            }

            var thisArea = GetArea();
            var otherArea = otherShape.GetArea();

            if (thisArea > otherArea)
            {
                return 1;
            }

            if (thisArea < otherArea)
            {
                return -1;
            }

            return 0;
        }
    }
}
