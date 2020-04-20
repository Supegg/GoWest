using System;
using System.Collections.Generic;

namespace ConsoleDijkstra
{
    class Program
    {
        private const int INF = 100000000;//单位10^-4m，单边最大长度10km，断不至于一条路径来回跑20次//int.MaxValue;
        private const int MaxV = 10;    //最大节点个数

        struct MGraph                    //图的定义
        {
            public int[,] edges;       //邻接矩阵
            public int n;             //节点数
        };

        static void Dijkstra(MGraph g, int v)
        {
            int[] dist = new int[MaxV]; //从源点v到其他各节点的最短路径长度
            int[] prev = new int[MaxV]; //path[i]表示从源点v到节点i之间最短路径的前驱节点
            bool[] s = new bool[MaxV];    //选定的节点的集合

            for (int i = 0; i < g.n; i++)
            {
                dist[i] = g.edges[v, i];    //距离初始化
                s[i] = false;                   //s[]初始化false
                if (g.edges[v, i] < INF)    //初始化前驱节点
                {
                    prev[i] = v;
                }
                else
                {
                    prev[i] = -1;
                }
            }
            s[v] = true;//源点编号v放入s中

            for (int i = 0; i < g.n; i++)
            {
                int mindis = INF;
                int u = -1; //-1 表示没有可到达的最近节点
                for (int j = 0; j < g.n; j++)//选取不在s中且具有最小距离的节点u
                {
                    if (!s[j] && dist[j] < mindis)
                    {
                        u = j;
                        mindis = dist[j];
                    }
                }
                if (u == -1)//如没有找到新的最近点，即剩余都是无穷远点/孤点
                {
                    break;//孤点/图情景，剪去不必要的计算
                }
                else
                {
                    s[u] = true;//节点u加入s中
                }

                for (int j = 0; j < g.n; j++)
                {
                    if (!s[j] && g.edges[u, j] != INF && dist[u] + g.edges[u, j] < dist[j])   //如果通过u能到达j，并且比之前的路径更短
                    {
                        dist[j] = dist[u] + g.edges[u, j];//更新路径长度
                        prev[j] = u;//更新j的前驱节点
                    }
                }
            }

            Console.Write("\t前驱节点表：", v);
            foreach (int p in prev)
            {
                Console.Write("{0,4}", p);
            }
            Console.WriteLine();

            Display(dist, prev, s, g.n, v); //输出最短路径
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
            for (int j = 0; j < n; j++)
            {
                if (s[j])  //路径存在
                {
                    Console.Write("\t到{0}的最短路径长度为:{1}\t路径为:", j, dist[j]);

                    List<int> ps = new List<int>();
                    int k = j;
                    ps.Add(j);
                    do
                    {
                        k = path[k];
                        if (k != j)
                        {
                            ps.Insert(0, k);
                        }
                    } while (k != v);

                    foreach (var p in ps)
                    {
                        Console.Write("{0},", p);
                    }
                    Console.Write("\b \b");
                    Console.WriteLine();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("\t从{0}到{1}不存在路径", v, j);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

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
    }
}
