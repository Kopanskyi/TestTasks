using Sortable_Shapes.Interfaces;
using Sortable_Shapes.Shapes;
using System;
using System.Collections.Generic;

namespace Sortable_Shapes
{
    class Program
    {
        static void Main(string[] args)
        {
            var side1 = 1.1234D;
            var side2 = 2.1234D;
            var radius = 1.1234D;
            var baseSide = 5D;
            var height = 2D;

            var shapes = new List<IShape> 
            {
                new Rectangle(side1, side2),
                new Circle(radius),
                new Triangle(baseSide, height),
                new Square(side1)               
            };

            ShowList(shapes);
            shapes.Sort();
            ShowList(shapes);

            Console.ReadKey();
        }

        public static void ShowList(IEnumerable<IShape> shapes)
        {
            foreach (var shape in shapes)
            {
                Console.WriteLine($"{shape.GetType().Name} area: {shape.GetArea()}\n");
            }

            Console.WriteLine();
        }
    }
}
