using Sortable_Shapes.Interfaces;
using System;

namespace Sortable_Shapes.Shapes
{
    class Triangle : IShape, IComparable
    {
        public double BaseSide { get; set; }
        public double Height { get; set; }

        public Triangle(double baseSide, double height)
        {
            BaseSide = baseSide;
            Height = height;
        }

        public double GetArea()
        {
            return BaseSide * Height / 2;
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
