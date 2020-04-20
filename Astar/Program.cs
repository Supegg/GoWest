using System;
using System.Collections.Generic;

namespace Astar
{
    struct MapPoint
    {
        public int Index;
        /// <summary>
        /// index, G
        /// </summary>
        public Dictionary<int, int> Children;
    }

    struct PathNode
    {
        public int Index;
        public int Parent;
        public int G, H;
        public int F;
    }

    private static void astar(List<MapPoint> g, int from, int to)
    {
        throw new NotImplementedException();
    }

    class Program
    {
        static void Main(string[] args)
        {
            int INF = int.MaxValue; // 表示不可达
            var matrix = new int[,]//初始化邻接矩阵
            {
                {  0,   3,   4,   7,   8, INF, INF, INF, INF, INF},
                {  3,   0, INF,   5,   4, INF, INF, INF, INF, INF},
                {  4, INF,   0, INF,   5, INF, INF, INF, INF, INF},
                {  7,   5, INF,   0, INF,   4, INF, INF, INF, INF},
                {  8,   4,   5, INF,   0,   3, INF, INF, INF, INF},
                {INF, INF, INF,   4,   3,   0, INF, INF, INF, INF},
                {INF, INF, INF, INF, INF, INF,   0,   4,   3, INF},
                {INF, INF, INF, INF, INF, INF,   4,   0,   8, INF},
                {INF, INF, INF, INF, INF, INF,   3,   8,   0, INF},
                {INF, INF, INF, INF, INF, INF, INF, INF, INF,   0},
            };

            List<MapPoint> map = new List<MapPoint>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var children = new Dictionary<int, int>();
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] > 0 && matrix[i, j] < INF)
                    {
                        children.Add(j, matrix[i, j]);
                    }
                }
                map.Add(new MapPoint() { Index = i, Children = children });
            }

            for (int k = 0; k < g.n; k++)
            {
                Console.WriteLine("从{0}出发...", k);
                Astar();
                Console.WriteLine();
            }

            Console.ReadKey();

        }
    }
}
