namespace Sortable_Shapes.Shapes
{
    class Triangle : Shape 
    {
        public double BaseSide { get; set; }
        public double Height { get; set; }

        public Triangle(double baseSide, double height)
        {
            BaseSide = baseSide;
            Height = height;
        }

        public override double GetArea()
        {
            return BaseSide * Height / 2;
        }

    }
}
