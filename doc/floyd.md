# Floyd算法是一个经典的动态规划算法

## 从任意节点i到任意节点j的最短路径不外乎2种可能
* 直接从i到j
* 从i经过若干个节点k到j

## 算法过程

* 假设dist(i,j)为节点u到节点v的最短路径的距离，对于每一个节点k，我们检查dist(i,k) + dist(k,j) < dist(i,j)是否成立
* 如果成立，证明从i到k再到j的路径比i直接到j的路径短，我们便设置dist(i,j) = dist(i,k) + dist(k,j)
* 当我们遍历完所有节点k，dist(i,j)中记录的便是i到j的最短路径的距离

**十字交叉法推断：i,j,k任意两个值相等时对路径无影响，可以加速计算**

---

[十字交叉法](https://blog.csdn.net/winbobob/article/details/38272679)

[最短路径—Dijkstra算法和Floyd算法](https://www.cnblogs.com/biyeymyhjob/archive/2012/07/31/2615833.html)
