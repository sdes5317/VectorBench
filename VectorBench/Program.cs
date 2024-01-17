using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace VectorBench
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new DebugInProcessConfig());
            var summary = BenchmarkRunner.Run<MyClass>();
        }
    }

    public class MyClass
    {
        private List<Point> data;
        private List<Vector2> data2;

        [GlobalSetup]
        public void Setup()
        {
            data = new List<Point>(new Point[500000]);
            data2 = new List<Vector2>(new Vector2[500000]);
        }

        //[Benchmark]
        public void VectorWithCast()
        {

            Vector2 minBounds = new Vector2(0, 0);
            Vector2 maxBounds = new Vector2(0, 100);

            foreach (Point d in data)
            {
                Vector2 point = new Vector2(d.X, d.Y);
                bool isInBounds = IsWithinBounds(point, minBounds, maxBounds);
            }
        }


        [Benchmark]
        public void VectorOriginal()
        {

            Vector2 minBounds = new Vector2(0, 0);
            Vector2 maxBounds = new Vector2(0, 100);
            foreach (Vector2 d in data2)
            {
                bool isInBounds = IsWithinBounds(d, minBounds, maxBounds);
            }
        }

        [Benchmark]
        public void PorintArray()
        {

            Point minBounds = new Point(0, 0);
            Point maxBounds = new Point(0, 100);

            foreach (Point d in data)
            {
                bool isInBounds = IsWithinBounds(d, minBounds, maxBounds);
            }
        }

        bool IsWithinBounds(Vector2 point, Vector2 min, Vector2 max)
        {
            return Vector2.Min(point, max) == point && Vector2.Max(point, min) == point;
        }
        bool IsWithinBounds2(Vector2 point, Vector2 min, Vector2 max)
        {
            return point.X > min.X && point.Y > min.Y && point.X < max.X && point.Y < max.Y;
        }


        bool IsWithinBounds(Point point, Point min, Point max)
        {
            return point.X > min.X && point.Y > min.Y && point.X < max.X && point.Y < max.Y;
        }
    }
}
