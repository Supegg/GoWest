using System;
using System.Collections.Generic;

namespace ConsoleFloyd
{
    class Program
    {
        private const int INF = int.MaxValue;
        private const int MaxV = 10;    //最大顶点个数

        struct MGraph                    //图的定义
        {
            public int[,] edges;       //邻接矩阵
            public int n;             //顶点数
        };

        static void Floyd(MGraph g)
        {
            int[,] dist = new int[MaxV, MaxV];//顶点之间的最短长度矩阵
            int[,] path = new int[MaxV, MaxV];//顶点之间的最短路径矩阵
            int i, j, k;

            //初始化节点间距及前驱节点
            for (i = 0; i < g.n; i++)
            {
                for (j = 0; j < g.n; j++)
                {
                    dist[i, j] = g.edges[i, j];
                    if (dist[i, j] == INF)
                    {
                        path[i, j] = -1;
                    }
                    else
                    {
                        path[i, j] = i;
                    }
                }
            }

            for (k = 0; k < g.n; k++)//中间节点在最外层
            {
                for (i = 0; i < g.n; i++)//起点
                {
                    if (i == k) //十字交叉法优化
                    {
                        continue;
                    }

                    for (j = 0; j < g.n; j++)//终点
                    {
                        if (j == i || j == k) //十字交叉法优化
                        {
                            continue;
                        }

                        if (dist[i, k] != INF && dist[k, j] != INF && dist[i, k] + dist[k, j] < dist[i, j])//从i经过k到j的路径比从i到j的路径短
                        {
                            dist[i, j] = dist[i, k] + dist[k, j];//更新路径长度
                            path[i, j] = path[k, j];//更新前驱节点
                        }
                    }
                }
            }

            //for (i = 0; i < g.n; i++)//起点
            //{
            //    for (j = 0; j < g.n; j++)//终点
            //    {
            //        Console.Write("{0,6}", path[i, j]);
            //    }
            //    Console.WriteLine("\n");
            //}

            Display(dist, path, g.n);   //输出最短路径
        }

        static void Display(int[,] dist, int[,] path, int n)
        {
            int i, j;
            for (i = 0; i < n; i++)
            {
                Console.WriteLine("从{0}出发...", i);
                for (j = 0; j < n; j++)
                {
                    if (dist[i, j] == INF)
                    {
                        if (i != j)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("\t从{0}到{1}不存在路径", i, j);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    else
                    {
                        Console.Write("\t到{0}的最短路径长度为:{1}\t路径为:", j, dist[i, j]);

                        List<int> ps = new List<int>();
                        int k = j;
                        ps.Add(j);
                        do
                        {
                            k = path[i, k];
                            ps.Add(k);
                        } while (k != i);

                        ps.Reverse();
                        ps.RemoveAt(0);

                        Console.Write("{0}", i);    //输出路径上的起点
                        foreach (var p in ps)
                        {
                            Console.Write(",{0}", p);
                        }
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            MGraph g;
            g.n = MaxV;
            g.edges = new int[MaxV, MaxV];

            g.edges = new int[MaxV, MaxV]//初始化邻接矩阵
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

            Floyd(g);

            Console.ReadKey();
        }
    }
}
