using System;
using System.Collections.Generic;

namespace Leetcode.leetcode_cn.array
{
    namespace p1
    {
        public class Solution {
            public int[] TwoSum(int[] nums, int target)
            {
                var copy = new int[nums.Length];
                Array.Copy(nums,copy,nums.Length);
                Array.Sort(nums);
                var i = 0;
                var j = nums.Length - 1;
                var rs = new int[2];
                while (true)
                {
                    if (i >= j) return null;
                    var sum = nums[i] + nums[j];
                    if (sum < target)
                    {
                        i++;
                    }
                    else if (sum > target)
                    {
                        j--;
                    }
                    else
                    {
                        rs[0] = nums[i];
                        rs[1] = nums[j];
                        break;
                    }
                }

                var rt = new int[2]{-1,-1};

                for (var idx = 0; idx < copy.Length; idx++)
                {
                    if (rt[0] == -1 && rs[0] == copy[idx])
                    {
                        rt[0] = idx;
                    }
                    else if (rt[1] == -1 && rs[1] == copy[idx])
                    {
                        rt[1] = idx;
                    }
                }

                return rt;
            }
        }
    }

    namespace p2
    {
        public class Solution {
            public int MaxSubArray(int[] nums)
            {
                if (nums == null || nums.Length == 0) return 0;
                var max = nums[0];
                var here = nums[0];
                for (var i = 1; i < nums.Length; i++)
                {
                    var value = nums[i];
                    if (here <= 0)
                    {
                        here = value;
                    }
                    else
                    {
                        here = value + here;
                    }

                    max = Math.Max(max, here);
                }

                return max;
            }
        }
    }

    namespace p3
    {
        public class Solution {
            public int MaxProfit(int[] prices)
            {
                if (prices == null || prices.Length < 2) return 0;
                var maxRight = new int[prices.Length];
                maxRight[maxRight.Length - 1] = -1;
                for (var i = maxRight.Length - 2; i > -1; i--)
                {
                    if (maxRight[i + 1] == -1 || prices[i + 1] > prices[maxRight[i + 1]])
                    {
                        maxRight[i] = i + 1;
                    }
                    else
                    {
                        maxRight[i] = maxRight[i + 1];
                    }
                }

                var maxProfit = 0;
                for (var i = 0; i < prices.Length - 1; i++)
                {
                    var profit = prices[maxRight[i]] - prices[i];
                    maxProfit = Math.Max(maxProfit, profit);
                }

                return maxProfit;

            }
        }
    }

    namespace p4
    {
        public class Solution {
            public int MaxArea(int[] height)
            {
                var max = 0;
                for (var i = 0; i < height.Length; i++)
                {
                    for (var j = 0; j < i; j++)
                    {
                        if (height[j] >= height[i])
                        {
                            var area = height[i] * (i - j);
                            max = Math.Max(max, area);
                            break;
                        }
                    }

                    for (var j = height.Length - 1; j > i; j--)
                    {
                        if (height[j] >= height[i])
                        {
                            var area = height[i] * (j - i);
                            max = Math.Max(max, area);
                            break;
                        }
                    }
                }

                return max;
            }
        }
    }

    namespace p4.V2
    {
        public class Solution {
            public int MaxArea(int[] height)
            {
                var i = 0;
                var j = height.Length - 1;
                var max = (j - i) * Math.Min(height[i], height[j]);
                while (i < j)
                {
                    var area = (j - i) * Math.Min(height[i], height[j]);
                    max = Math.Max(max, area);
                    if (height[i] < height[j])
                    {
                        i++;
                    }
                    else
                    {
                        j--;
                    }
                }
                return max;
            }
        }
    }

    namespace p5
    {
        public class Solution {
            public int Search(int[] nums, int target)
            {
                if (nums == null || nums.Length == 0) return -1;
                var lo = 0;
                var hi = nums.Length - 1;
                while (hi - lo > 5L)
                {
                    var mid = lo + (hi - lo) / 2;
                    var midValue = nums[mid];
                    if (midValue == target) return mid;
                    if (nums[lo] < nums[hi])
                    {
                        if (midValue < target) lo = mid + 1;
                        else
                        {
                            hi = mid - 1;
                        }
                    }
                    else
                    {
                        if (midValue > nums[lo])
                        {
                            if (target >= nums[lo] && target < midValue)
                            {
                                hi = mid - 1;
                            }
                            else
                            {
                                lo = mid + 1;
                            }
                        }
                        else
                        {
                            if (target > midValue && target <= nums[hi])
                            {
                                lo = mid + 1;
                            }
                            else
                            {
                                hi = mid - 1;
                            }
                        }
                    }
                    
                }

                for (var i = lo; i <= hi; i++)
                {
                    if (nums[i] == target)
                    {
                        return i;
                    }
                }

                return -1;
            }
        }
    }

    namespace p6
    {
        public class Solution {
            public IList<int> SpiralOrder(int[][] matrix)
            {
                if(matrix==null||matrix.Length==0||matrix[0].Length==0)return new List<int>();
                var m = matrix.Length;
                var n = matrix[0].Length;
                var circle = Math.Min((m + 1) / 2, (n + 1) / 2);
                var rs = new List<int>();
                for (var i = 0; i < circle; i++)
                {
                    int x;
                    int y;
                    
                    x = i;
                    for (y = i; y < n - i; y++)
                    {
                        rs.Add(matrix[x][y]);
                    }

                    y = n - i - 1;
                    for (x = i + 1; x < m - i; x++)
                    {
                        rs.Add(matrix[x][y]);
                    }

                    if (m - i - 1 > i)
                    {
                        x = m - i - 1;
                        for (y = n - i - 2; y >= i; y--)
                        {
                            rs.Add(matrix[x][y]);
                        }
                    }

                    if (i < n - i - 1)
                    {
                        y = i;
                        for (x = m - i - 2; x > i; x--)
                        {
                            rs.Add(matrix[x][i]);
                        }
                    }
                    

                }

                return rs;
            }
        }
    }

    namespace p7
    {
        public class Solution {
            public class MyComparer : Comparer<int[]>
            {
                public override int Compare(int[] x, int[] y)
                {
                    if (x[0] != y[0])
                    {
                        return x[0] - y[0];
                    }
                    else
                    {
                        return x[1] - y[1];
                    }
                }
            }

            public class Node
            {
                public Node Prev { get; set; }
                public Node Next { get; set; }
                public int[] Value { get; set; }
            }
            
            public int[][] Merge(int[][] intervals)
            {
                if (intervals == null || intervals.Length < 2) return intervals;
                Array.Sort(intervals,new MyComparer());
                Node header = new Node();
                Node trailer = new Node();
                header.Next = trailer;
                trailer.Prev = header;

                foreach (var interval in intervals)
                {
                    var node = new Node();
                    node.Next = trailer;
                    node.Prev = trailer.Prev;
                    node.Prev.Next = node;
                    node.Next.Prev = node;
                    node.Value = interval;
                }

                while (true)
                {
                    var merged = false;
                    var node = header.Next;
                    while (node.Next!=trailer)
                    {
                        var next = node.Next;
                        var first = node.Value;
                        var second = next.Value;
                        if(first[1]<second[0])
                        {
                            node = node.Next;
                        }
                        else
                        {
                            var left = Math.Min(first[0], second[0]);
                            var right = Math.Max(first[1], second[1]);
                            merged = true;
                            node.Value[0] = left;
                            node.Value[1] = right;
                            node.Next = next.Next;
                            node.Next.Prev = node;
                        }
                    }
                    if (!merged) break;
                }
                var list = new List<int[]>();
                var n = header.Next;
                while (n != trailer)
                {
                    list.Add(n.Value);
                    n = n.Next;
                }

                return list.ToArray();
            }
            
        }
    }
}