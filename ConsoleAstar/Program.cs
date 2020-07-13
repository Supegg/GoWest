using System;
using System.Collections.Generic;

namespace ConsoleAstar
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
        public int F { get { return G + H; } }
    }

    class ComparePathNode : IComparer<PathNode>
    {
        public int Compare(PathNode a, PathNode b)
        {
            if (a.F == b.F)
            {
                return 0;
            }
            else
            {
                return a.F > b.F ? 1 : -1;
            }
        }
    }

    class Program
    {
        static void Astar(Dictionary<int, MapPoint> map, int f, int t)
        {
            PriorityQueue<PathNode> open = new PriorityQueue<PathNode>(new ComparePathNode());
            List<PathNode> close = new List<PathNode>();
            bool found = false;

            PathNode parentNode = new PathNode
            {
                Index = f,
                Parent = 0,
                G = 0,
                H = 0
            };
            open.Push(parentNode);

            while (open.Count > 0)
            {
                parentNode = open.Pop();

                bool isFarther = false;
                foreach (var node in close)
                {
                    if (node.Index == parentNode.Index && parentNode.F >= node.F)
                    {
                        isFarther = true;
                        break;
                    }
                }
                if (isFarther)
                {
                    continue; // 如果比close中的节点更远则跳过
                }
                close.Add(parentNode);

                if (parentNode.Index == t)
                {
                    //close.Add(parentNode);
                    found = true;
                    break;
                }

                foreach (var point in map[parentNode.Index].Children)
                {
                    PathNode newNode = new PathNode()
                    {
                        Index = point.Key,
                        //Parent = parentNode.Index,
                        //G = parentNode.G + point.Value,
                        //H = 0
                    };
                    int newG = parentNode.G + point.Value; // it usually has a g-function 

                    int openIndex = -1;
                    for (int i = 0; i < open.Count; i++)
                    {
                        if (open[i].Index == newNode.Index)
                        {
                            openIndex = i;
                            break;
                        }
                    }
                    if (openIndex != -1 && open[openIndex].G <= newG)
                    {
                        continue; // 如果比open中的节点更远则跳过
                    }

                    int closeIndex = -1;
                    for (int i = 0; i < close.Count; i++)
                    {
                        if (close[i].Index == newNode.Index)
                        {
                            closeIndex = i;
                            break;
                        }
                    }
                    if (closeIndex != -1 && close[closeIndex].G <= newG)
                    {
                        continue; // 如果比close中的节点更远则跳过
                    }

                    newNode.Parent = parentNode.Index;
                    newNode.G = newG;

                    //give me a heuristic function
                    newNode.H = 0;

                    open.Push(newNode);
                }

                //close.Add(parentNode);
            }

            if (found)
            {
                PathNode dst = close[close.Count - 1];
                Console.Write("\t到{0}的最短路径长度为:{1}\t路径为:", t, dst.F);
                List<PathNode> paths = new List<PathNode>
                {
                    dst // paths[0]
                };

                for (int i = close.Count - 2; i >= 0; i--) // 最后一点是终点，已加入 paths
                {
                    if (close[i].Index == dst.Parent)
                    {
                        paths.Insert(0, close[i]);
                        dst = close[i];
                    }

                    if (close[i].Index == f)
                    {
                        break;
                    }
                }

                foreach (PathNode n in paths)
                {
                    Console.Write("{0},", n.Index);
                }
                Console.Write("\b \b"); // take a step back
                Console.WriteLine();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\t从{0}到{1}不存在路径", f, t);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

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

            Dictionary<int, MapPoint> map = new Dictionary<int, MapPoint>();
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
                map.Add(i, new MapPoint() { Index = i, Children = children });
            }

            for (int f = 0; f < map.Count; f++)
            {
                Console.WriteLine("从{0}出发...", f);
                for (int t = 0; t < map.Count; t++)
                {
                    Astar(map, f, t);
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
