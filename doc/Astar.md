# A\*算法

**A\*** （**A-Star**)算法是一种静态路网中求解最短路径最有效的**直接搜索**方法，注意——是最有效的直接搜索算法，之后涌现了很多**预处理**算法（如ALT，CH，HL等等）速度更快

公式表示为： f(n)=g(n)+h(n)， g和h有相同的衡量单位

* f(n) 是从初始状态经由状态n到目标状态的代价估计
* g(n) 是在状态空间中从初始状态到状态n的实际代价
* h(n) 是从状态n到目标状态的最佳路径的估计代价，是一个启发（**heuristics**）方法

## 当h(n)=0时，退化为dijkstra算法，如本 demo

### 算法过程

* 初始化open和close
* 将起点加入open中，并设置代价为0
* 不断从open中取出最小代价节点n，直到open为空
  * 如果节点n已经在close中并且代价更大，continue，否则加入到close中
  * 如果节点n为终点，找到终点，break
  * 遍历n的子节点m
    * 如果在open中并且代价更大，continue
    * 如果在close中并且代价更大，continue
    * 设置节点m的parent为节点n，加入到open
* 如果找到路径，从终点开始逐步追踪parent节点，一直达到起点，**算法结束**
---

[参考PathFinder](https://github.com/Supegg/PathFinder)
