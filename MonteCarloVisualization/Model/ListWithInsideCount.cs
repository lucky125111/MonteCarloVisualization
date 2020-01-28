using System.Collections.Generic;
using System.Windows;

namespace MonteCarloVisualization.Model
{
    public class ListWithInsideCount
    {
        public IEnumerable<Point> List;
        public int Count;

        public ListWithInsideCount(int count, IEnumerable<Point> list)
        {
            Count = count;
            List = list;
        }
    }
}