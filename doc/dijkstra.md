# dijkstra算法原理：最优子路径存在

## 假设从S→E存在一条最短路径SE，且该路径经过点A，那么可以确定SA子路径一定是S→A的最短路径

## 证明：反证法

> 如果子路径SA不是最短的，那么就必然存在一条更短的SA'，从而SE路径也就不是最短，与原假设矛盾。

---

[最短路径—Dijkstra算法和Floyd算法](https://www.cnblogs.com/biyeymyhjob/archive/2012/07/31/2615833.html)