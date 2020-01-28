using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MonteCarloVisualization.Model;

namespace MonteCarloVisualization
{
    public class MonteCarloSimulator
    {
        private readonly Random _r = new Random(2098344169);
        private readonly int _diameter;

        public MonteCarloSimulator(int diameter)
        {
            _diameter = diameter;
        }

        public IEnumerable<PointsAndPiValue> RunMonteCarlo(FixedNumberContext context)
        {
            var res = MonteCarloSimulation(context.PointCount);
            return new List<PointsAndPiValue>() { res };
        }

        public IEnumerable<PointsAndPiValue> RunMonteCarlo(IterativeContext context)
        {
            var increment = (context.EndCount - context.StartCount) / context.IterationsCount;
            if (increment < 1)
            {
                context.IterationsCount = 1;
                increment = context.IterationsCount;
            }

            var resList = new List<PointsAndPiValue>();
            for (int i = context.StartCount; i <= context.EndCount; i += increment)
            {
                var res = MonteCarloSimulation(i);
                resList.Add(res);
            }

            return resList;
        }

        private PointsAndPiValue MonteCarloSimulation(int totalCount)
        {
            var diameter = _diameter;
            var listWithInsideCount= GeneratePointsAndCountInside(totalCount, diameter);
            var pi = CalculatePi(listWithInsideCount.Count, totalCount);
            return new PointsAndPiValue(listWithInsideCount.List.Distinct(new PointToIntEqualityComparer()), pi, totalCount);
        }

        private static double CalculatePi(int insideCount, int totalCount)
        {
            return insideCount * 4.0 / totalCount;
        }

        private ListWithInsideCount GeneratePointsAndCountInside(int n, int diameter)
        {
            var l = new List<Point>();
            var insideCount = 0;
            for (var i = 0; i < n; i++)
            {
                var point = new Point(_r.NextDouble() * diameter, _r.NextDouble() * diameter);
                l.Add(point);
                if (IsInsideCircle(point, diameter / 2))
                    insideCount++;
            }
            return new ListWithInsideCount(insideCount, l);
        }

        private static bool IsInsideCircle(Point point, int r)
        {
            return Math.Pow(point.X - r * 1.0, 2) + Math.Pow(point.Y - r * 1.0, 2) <= Math.Pow(200 * 1.0, 2);
        }
    }

    public class PointsAndPiValue
    {
        public IEnumerable<Point> List;
        public double Pi;
        public int PointCount;

        public PointsAndPiValue(IEnumerable<Point> list, double pi, int pointCount)
        {
            List = list;
            Pi = pi;
            PointCount = pointCount;
        }
    }

    public class PointToIntEqualityComparer : IEqualityComparer<Point>
    {
        public bool Equals(Point a, Point b)
        {
            return (int)a.X == (int)b.X && (int)a.Y == (int)b.Y;
        }

        public int GetHashCode(Point other)
        {
            return other.X.GetHashCode() * 19 + other.Y.GetHashCode();
        }
    }
}