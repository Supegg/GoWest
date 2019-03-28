﻿using System;
using System.Collections.Generic;

namespace ConsoleDijkstra
{
    class Program
    {
        private const int INF = int.MaxValue;
        private const int MaxV = 10;    //最大节点个数

        struct MGraph                    //图的定义
        {
            public int[,] edges;       //邻接矩阵
            public int n;             //节点数
        };

        static void Main(string[] args)
        {
            MGraph g;
            g.n = MaxV;

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

            for (int v = 0; v < g.n; v++)
            {
                Console.WriteLine("从{0}出发...", v);
                Dijkstra(g, v);
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        static void Dijkstra(MGraph g, int v)
        {
            int[] dist = new int[MaxV]; //从源点v到其他各节点的最短路径长度
            int[] path = new int[MaxV]; //path[i]表示从源点v到节点i之间最短路径的前驱节点
            bool[] s = new bool[MaxV];    //选定的节点的集合
            int mindis = INF;//辅助变量，暂存最新的最近距离
            int u = -1;//辅助变量,暂存最新的最近点索引
            int lastU = -1;//辅助变量，存储最后一次的最近点索引u。解决孤点/图问题，并减少计算

            for (int i = 0; i < g.n; i++)
            {
                dist[i] = g.edges[v, i];    //距离初始化
                s[i] = false;                   //s[]置空  0表示i不在s集合中
                if (g.edges[v, i] < INF)    //路径初始化
                {
                    path[i] = v;
                }
                else
                {
                    path[i] = -1;
                }
            }
            s[v] = true;//源点编号v放入s中

            for (int i = 0; i < g.n; i++)
            {
                mindis = INF;           //mindis置最小长度初值
                for (int j = 0; j < g.n; j++)//选取不在s中且具有最小距离的节点u
                {
                    if (!s[j] && dist[j] < mindis)
                    {
                        u = j;
                        mindis = dist[j];
                    }
                }
                if (u == lastU)//如没有找到新的最近点，即剩余都是无穷远点/孤点
                {
                    break;//剪去不必要计算
                }
                else
                {
                    s[u] = true;//节点u加入s中
                }

                for (int j = 0; j < g.n; j++)
                {
                    if (!s[j] && g.edges[u,j] != INF && dist[u] + g.edges[u, j] < dist[j])   //如果通过u能到达j，并且比之前的路径更短
                    {
                        dist[j] = dist[u] + g.edges[u, j];//更新路径长度
                        path[j] = u;//更新j的前驱节点
                    }
                }

                lastU = u;//记录最后选择的点
            }

            Console.Write("\t前驱节点表：", v);
            foreach(int p in path)
            {
                Console.Write("{0,4}", p);
            }
            Console.WriteLine();

            Display(dist, path, s, g.n, v); //输出最短路径
        }

        /// <summary>
        /// from v to everywhere
        /// </summary>
        /// <param name="dist">距离列表</param>
        /// <param name="path">前驱节点表</param>
        /// <param name="s">路径是否存在数组</param>
        /// <param name="n">节点数</param>
        /// <param name="v">起点</param>
        static void Display(int[] dist, int[] path, bool[] s, int n, int v)
        {
            int i;
            for (i = 0; i < n; i++)
            {
                if (s[i])  //路径存在
                {
                    Console.Write("\t到{0}的最短路径长度为:{1}\t路径为:", i, dist[i]);
                    
                    List<int> ps = new List<int>();
                    int k = i;
                    ps.Add(i);
                    do
                    {
                        k = path[k];
                        ps.Add(k);
                    } while (k != v);

                    Console.Write("{0}", v);
                    ps.Reverse();
                    ps.RemoveAt(0);
                    foreach (var p in ps)
                    {
                        Console.Write(",{0}", p);
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\t从{0}到{1}不存在路径", v, i);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }
        }
    }
}
