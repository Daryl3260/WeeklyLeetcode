using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Leetcode.leetcode_cn.greed
{
    namespace p1
    {
        public class Solution {
            public int MaxProfit(int[] prices)
            {
                var i = 0;
                var profit = 0;
                while (i < prices.Length-1)
                {
                    var j = i + 1;
                    if (prices[i] >= prices[j]) i++;
                    else
                    {
                        while (j < prices.Length && prices[j] > prices[j - 1]) j++;
                        profit += prices[j - 1] - prices[i];
                        i = j;
                    }
                }
                return profit;
            }
        }
    }

    namespace p2
    {
        public class Solution {
            public int FindContentChildren(int[] g, int[] s)
            {
                if (g == null || g.Length == 0) return 0;
                var dict = CountCookies(s);
                Array.Sort(g);
                var content = 0;
                foreach (var child in g)
                {
                    var min = int.MaxValue;
                    foreach (var key in dict.Keys)
                    {
                        if (key >= child && key < min)
                        {
                            min = key;
                        }
                    }

                    if (min < int.MaxValue)
                    {
                        content++;
                        dict[min]--;
                        if (dict[min] == 0) dict.Remove(min);
                    }
                    else break;
                }
                return content;
            }

            public IDictionary<int,int> CountCookies(int[] s)
            {
                var dict = new Dictionary<int,int>();
                foreach (var item in s)
                {
                    if (!dict.ContainsKey(item))
                    {
                        dict[item] = 0;
                    }
                    dict[item]++;
                }

                return dict;
            }
        }
    }

    namespace p3
    {
        public class Solution {
            public int[][] ReconstructQueue(int[][] people)
            {
                if (people == null || people.Length == 0) return people;
                var rs = new int[people.Length][];
                var dict = new Dictionary<int[],int[]>();
                var copySet = new HashSet<int[]>();
                foreach (var person in people)
                {
                    var copy = new int[2];
                    copy[0] = person[0];
                    copy[1] = person[1];
                    dict[copy] = person;
                    copySet.Add(copy);
                }
                var list = new List<int[]>();
                while (list.Count < people.Length)
                {
                    int[] first = null;
                    foreach (var copy in copySet)
                    {
                        if (copy[1] == 0 && (first == null || copy[0] < first[0])) first = copy;
                    }
                    list.Add(first);
                    copySet.Remove(first);
                    foreach (var copy in copySet)
                    {
                        if (copy[0] <= first[0]) copy[1]--;
                    }
                }

                for (var i = 0; i < list.Count; i++)
                {
                    rs[i] = dict[list[i]];
                }
                return rs;
            }
        }
    }

    namespace p4
    {
        public class Solution {
            public int Profit(int[] prices, Tuple<int, int> tuple, int fee)
            {
                var profit = prices[tuple.Item2] - prices[tuple.Item1] - fee;
                return Math.Max(profit, 0);
            }
            public int MaxProfit(int[] prices, int fee)
            {
                if (prices == null || prices.Length < 2) return 0;
                var upStreams = UpStream(prices);
                if (upStreams.Count == 0) return 0;
                var rs = 0;
                if (upStreams.Count == 1)
                {
                    var item = upStreams[0];
                    var profit = prices[item.Item2] - prices[item.Item1] - fee;
                    return Math.Max(profit, 0);
                }

                var last = upStreams[0];
                for (var i = 1; i < upStreams.Count; i++)
                {
                    var tuple = upStreams[i];
                    if (prices[last.Item2] - prices[tuple.Item1] > fee || prices[last.Item1] > prices[tuple.Item1])
                    {
                        rs += Profit(prices, last, fee);
                        last = tuple;
                    }
                    else
                    {
                        last = new Tuple<int, int>(last.Item1,tuple.Item2);
                    }
                }
                rs += Profit(prices, last, fee);
                return rs;
            }

            public IList<Tuple<int, int>> UpStream(int[] prices)
            {
                var i = 0;
                var rs = new List<Tuple<int,int>>();
                while (i < prices.Length - 1)
                {
                    if (prices[i] >= prices[i + 1]) i++;
                    else
                    {
                        var j = i + 1;
                        while (j < prices.Length && prices[j] > prices[j - 1]) j++;
                        rs.Add(new Tuple<int, int>(i,j-1));
                        i = j;
                    }
                }
                return rs;
            }
        }
    }

    namespace p4.V2
    {
        public class Solution {
            public int MaxProfit(int[] prices, int fee)
            {
                if (prices == null || prices.Length < 2) return 0;
                var cash = 0;
                var hold = -prices[0];
                for (var i = 1; i < prices.Length; i++)
                {
                    cash = Math.Max(cash, hold + prices[i] - fee);
                    hold = Math.Max(hold, cash - prices[i]);
                }
                return cash;
            }
        }
    }

    namespace p5
    {
        public class Solution {
            public int CanCompleteCircuit(int[] gas, int[] cost)
            {
                if (gas.Sum(elem => elem) < cost.Sum(elem => elem)) return -1;
                else if (gas.Length == 1) return 0;
                var list = new int[gas.Length];
                for (var i = 0; i < list.Length; i++)
                {
                    list[i] = gas[i] - cost[i];
                }

                var header = ConstructList(list);
                var starter = FindStarter(header);
                if (starter == null) return -1;
                while (true)
                {
                    if (starter.Next == starter) return starter.StartIdx;
                    var mergeResult = MergeNode(starter);
                    if (!mergeResult) return -1;
                }
            }

            public Node ConstructList(int[] list)
            {
                if (list.Length == 2)
                {
                    var first = new Node{StartIdx = 0,Value = list[0]};
                    var second = new Node{StartIdx = 1,Value = list[1]};
                    first.Next = second;
                    first.Prev = second;
                    second.Next = first;
                    second.Prev = first;
                    return first;
                }
                Node[] array = new Node[list.Length];
                for (var i = 0; i < list.Length; i++)
                {
                    array[i]=new Node{StartIdx = i,Value = list[i]};
                }

                for (var i = 1; i < list.Length - 1; i++)
                {
                    array[i].Prev = array[i - 1];
                    array[i].Next = array[i + 1];
                    array[i].Prev.Next = array[i];
                    array[i].Next.Prev = array[i];
                }

                array[0].Prev = array[array.Length - 1];
                array[0].Prev.Next = array[0];
                return array[0];
            }
            public Node FindStarter(Node header)
            {
                if (header.Value >= 0) return header;
                var temp = header;
                while (true)
                {
                    if (header.Value >= 0) return header;
                    else header = header.Next;
                    if (header == temp) return null;
                }
            }
            public bool MergeNode(Node header)
            {
                if (header.Next == header)
                {
                    return false;
                }
                var node = header;
                var merged = false;
                while (true)
                {
                    if (node.Value < 0) node = node.Next;
                    else
                    {
                        var next = node.Next;
                        if (node.Value + next.Value >= 0)
                        {
                            if (node.Next != header)
                            {
                                node.Value += next.Value;
                                node.Next = next.Next;
                                node.Next.Prev = node;
                                merged = true;
                                break;
                            }
                            else
                            {
                                header.Value += node.Value;
                                header.Prev = node.Prev;
                                header.StartIdx = node.StartIdx;
                                header.Prev.Next = header;
                                merged = true;
                                break;
                            }
                        }
                        else
                        {
                            node = next;
                        }
                    }
                    if (node == header) break;
                }
                return merged;
            }
            
            
            
            public class Node
            {
                public int Value { get; set; }
                public int StartIdx { get; set; }
                public Node Prev { get; set; }
                public Node Next { get; set; }
            }
        }
    }

    namespace p6
    {
        public class Solution {
            public int WiggleMaxLength(int[] nums)
            {
                if (nums == null) return 0;
                else if (nums.Length < 2) return nums.Length;
                var upTaken = new int[nums.Length];
                var downTaken = new int[nums.Length];
                var bigger = NextBigger(nums);
                var smaller = NextSmaller(nums);
                var max = 1;
                for (var i = 0; i < nums.Length; i++)
                {
                    var maxUp = MaxUp(nums, i, bigger, smaller, upTaken, downTaken);
                    var maxDown = MaxDown(nums, i, bigger, smaller, upTaken, downTaken);
                    max = Math.Max(max, Math.Max(maxUp, maxDown));
                }

                return max;
            }

            public int MaxUp(int[] nums, int idx, int[] bigger, int[] smaller,int[] upTaken,int[] downTaken)
            {
                if (upTaken[idx] != 0) return upTaken[idx];
                var nextBiggerList = new List<int>();
                for (var i = idx + 1; i < nums.Length; i++)
                {
                    if(nums[i]>nums[idx])nextBiggerList.Add(i);
                }

                var rs = -1;
                if (!nextBiggerList.Any()) rs = 1;
                else
                {
                    var nextBigger = nextBiggerList[0];
                    for (var i = 1; i < nextBiggerList.Count; i++)
                    {
                        if (smaller[nextBiggerList[i]] < smaller[nextBigger])
                        {
                            nextBigger = nextBiggerList[i];
                        }
                    }

                    if (smaller[nextBigger] == int.MaxValue)
                    {
                        rs = 1 + 1;
                    }
                    else rs = 1 + MaxDown(nums, nextBigger, bigger, smaller, upTaken, downTaken);
                }

                upTaken[idx] = rs;
                return rs;
            }

            public int MaxDown(int[] nums, int idx, int[] bigger, int[] smaller,int[] upTaken,int[] downTaken)
            {
                if (downTaken[idx] != 0) return downTaken[idx];
                var nextSmallerList = new List<int>();
                for (var i = idx + 1; i < nums.Length; i++)
                {
                    if(nums[i]<nums[idx])nextSmallerList.Add(i);
                }

                var rs = -1;
                if (!nextSmallerList.Any()) rs = 1;
                else
                {
                    var nextSmaller = nextSmallerList[0];
                    for (var i = 1; i < nextSmallerList.Count; i++)
                    {
                        if (bigger[nextSmallerList[i]] < bigger[nextSmaller])
                        {
                            nextSmaller = nextSmallerList[i];
                        }
                    }

                    if (bigger[nextSmaller] == int.MaxValue)
                    {
                        rs = 1 + 1;
                    }
                    else rs = 1 + MaxUp(nums, nextSmaller, bigger, smaller, upTaken, downTaken);
                }

                downTaken[idx] = rs;
                return rs;
            }
            public int[] NextBigger(int[] nums)
            {
                var rs = new int[nums.Length];
                for (var i = 0; i < nums.Length; i++)
                {
                    rs[i] = int.MaxValue;
                    for (var j = i + 1; j < nums.Length; j++)
                    {
                        if (nums[j] > nums[i])
                        {
                            rs[i] = j;
                            break;
                        }
                    }
                }

                return rs;
            }

            public int[] NextSmaller(int[] nums)
            {
                var rs = new int[nums.Length];
                for (var i = 0; i < nums.Length; i++)
                {
                    rs[i] = int.MaxValue;
                    for (var j = i + 1; j < nums.Length; j++)
                    {
                        if (nums[j] < nums[i])
                        {
                            rs[i] = j;
                            break;
                        }
                    }
                }

                return rs;
            }
        }
    }

    namespace p6.V2
    {
        public class Solution {
            public int WiggleMaxLength(int[] nums)
            {
                if (nums == null) return 0;
                else if (nums.Length < 2) return nums.Length;
                var up = new int[nums.Length];
                var down = new int[nums.Length];
                up[0] = 1;
                down[0] = 1;
                for (var i = 1; i < nums.Length; i++)
                {
                    if (nums[i] > nums[i - 1])
                    {
                        up[i] = 1 + down[i - 1];
                        down[i] = down[i - 1];
                    }
                    else if (nums[i] < nums[i - 1])
                    {
                        down[i] = 1 + up[i-1];
                        up[i] = up[i - 1];
                    }
                    else
                    {
                        up[i] = up[i - 1];
                        down[i] = down[i - 1];
                    }
                }
                return Math.Max(up[nums.Length - 1], down[nums.Length - 1]);
            }
        }
    }

    namespace p7
    {
        public class Solution {
            public int LeastInterval(char[] tasks, int n)
            {
                if (tasks == null) return 0;
                else if (tasks.Length == 1) return 1;
                var rs = 0;
                var taskCounter = CountTask(tasks);
                var intervalCounter = new Dictionary<char,int>();
                foreach (var key in taskCounter.Keys)
                {
                    intervalCounter[key] = 0;
                }

                while (taskCounter.Any())
                {
                    var readyKeys = intervalCounter.Where(entry => entry.Value <=0).Select(entry => entry.Key);
                    var taskList = taskCounter.Where(entry => readyKeys.Contains(entry.Key)).ToList();
                    if (taskList.Any())
                    {
                        var maxEntry = taskList[0];
                        foreach (var entry in taskList)
                        {
                            if (maxEntry.Value < entry.Value) maxEntry = entry;
                        }
                        var keys = intervalCounter.Keys.ToList();
                        foreach (var key in keys)
                        {
                            intervalCounter[key]--;
                        }
                        intervalCounter[maxEntry.Key] = n;
                        taskCounter[maxEntry.Key]--;
                        if (taskCounter[maxEntry.Key] == 0)
                        {
                            taskCounter.Remove(maxEntry.Key);
                            intervalCounter.Remove(maxEntry.Key);
                        }
                    }
                    else
                    {
                        var keys = intervalCounter.Keys.ToList();
                        foreach (var key in keys)
                        {
                            intervalCounter[key]--;
                        }
                    }

                    rs++;

                }
                return rs;
            }

            public IDictionary<char, int> CountTask(char[] tasks)
            {
                var rs = new Dictionary<char,int>();
                foreach (var task in tasks)
                {
                    if (!rs.ContainsKey(task))
                    {
                        rs[task] = 0;
                    }

                    rs[task]++;
                }
                return rs;
            }
        }

        namespace V2
        {
            public class Solution {
                public class Node:IComparable<Node>
                {
                    public char Task { get; set; }
                    public int Count { get; set; }
                    public int Interval { get; set; }
                    public int CompareTo(Node other)
                    {
                        if (this.Interval != other.Interval)
                        {
                            return -(this.Interval - other.Interval);
                        }
                        else if (Count != other.Count)
                        {
                            return (Count - other.Count);
                        }
                        else return Task - other.Task;
                    }
                }
                public int LeastInterval(char[] tasks, int n)
                {
                    var rs = 0;
                    var set = ConstructSet(tasks);
                    var list = new List<Node>();
                    list.AddRange(set);
                    var idleCounter = 0;
                    while (list.Any())
                    {
                        rs++;
                        var last = list[list.Count - 1];
                        if (last.Interval > idleCounter)
                        {
                            idleCounter++;
                        }
                        else
                        {
                            foreach (var node in list)
                            {
                                node.Interval -= idleCounter + 1;
                                if (node.Interval < 0) node.Interval = 0;
                            }
                            idleCounter = 0;
                            last.Interval = n;
                            last.Count--;
                            if (last.Count == 0)
                            {
                                list.RemoveAt(list.Count-1);
                            }
                        }
                        list.Sort();
                    }

                    return rs;
                }

                public SortedSet<Node> ConstructSet(char[] tasks)
                {
                    var set = new SortedSet<Node>();
                    var dict = new Dictionary<char,int>();
                    foreach (var task in tasks)
                    {
                        if (!dict.ContainsKey(task))
                        {
                            dict[task] = 0;
                        }

                        dict[task]++;
                    }

                    foreach (var entry in dict)
                    {
                        set.Add(new Node {Task = entry.Key, Count = entry.Value, Interval = 0});
                    }

                    return set;
                }
                    
            }
            
        }

        namespace v3
        {
            public class Solution {
                public int LeastInterval(char[] tasks, int n)
                {
                    var counts = new int[26];
                    foreach (var task in tasks)
                    {
                        counts[task - 'A']++;
                    }

                    var time = 0;
                    Array.Sort(counts);
                    while (counts[25] > 0)
                    {
                        for (var i = 0; i < n+1; i++)
                        {
                            if (counts[25] == 0) break;
                            if (25-i>-1&&counts[25 - i] > 0) counts[25 - i]--;
                            time++;
                        }
                        Array.Sort(counts);
                    }
                    return time;
                }
            }
        }
    }
}