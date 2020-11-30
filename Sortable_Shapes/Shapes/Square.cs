using System;

namespace Sortable_Shapes.Shapes
{
    class Square : Shape
    {
        public double Side { get; set; }

        public Square(double side)
        {
            Side = side;
        }

        public override double GetArea()
        {
            return Math.Pow(Side, 2);
        }

    }
}
