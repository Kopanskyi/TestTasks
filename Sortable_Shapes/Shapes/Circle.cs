using System;

namespace Sortable_Shapes.Shapes
{
    class Circle : Shape 
    {
        public double Radius { get; set; }

        public Circle(double radius)
        {
            Radius = radius;
        }

        public override double GetArea()
        {
            return Math.Pow(Math.PI * Radius, 2);
        }

    }
}
