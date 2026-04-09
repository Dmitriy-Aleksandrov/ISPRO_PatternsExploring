using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explore_Patterns
{
    class prototype
    {
        public abstract class Shape
        {
            public int X { get; set; }
            public int Y { get; set; }
            public string Color { get; set; }

            public abstract Shape Clone();
            public abstract void Draw();
        }

        // Конкретный прототип: Круг
        public class Circle : Shape
        {
            public int Radius { get; set; }

            public Circle(int radius, int x, int y, string color)
            {
                Radius = radius;
                X = x;
                Y = y;
                Color = color;
            }

            public override Shape Clone()
            {
                // Поверхностное копирование (для простых типов)
                return (Circle)this.MemberwiseClone();
            }

            public override void Draw()
            {
                Console.WriteLine($"Круг: Радиус={Radius}, Позиция=({X},{Y}), Цвет={Color}");
            }
        }

        // Конкретный прототип: Прямоугольник
        public class Rectangle : Shape
        {
            public int Width { get; set; }
            public int Height { get; set; }

            public Rectangle(int width, int height, int x, int y, string color)
            {
                Width = width;
                Height = height;
                X = x;
                Y = y;
                Color = color;
            }

            public override Shape Clone()
            {
                return (Rectangle)this.MemberwiseClone();
            }

            public override void Draw()
            {
                Console.WriteLine($"Прямоугольник: {Width}x{Height}, Позиция=({X},{Y}), Цвет={Color}");
            }
        }
        public class ComplexObject : ICloneable
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<string> Tags { get; set; }
            public NestedObject Nested { get; set; }

            public object Clone()
            {
                return new ComplexObject
                {
                    Id = this.Id,
                    Name = this.Name,
                    Tags = new List<string>(this.Tags),
                    Nested = (NestedObject)this.Nested?.Clone()
                };
            }
        }

        public class NestedObject : ICloneable
        {
            public string Value { get; set; }

            public object Clone()
            {
                return new NestedObject { Value = this.Value };
            }
        }
    }
}
