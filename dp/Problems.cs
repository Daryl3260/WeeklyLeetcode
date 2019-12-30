using System;
using System.Collections.Generic;
using System.Linq;

namespace Leetcode.leetcode_cn.dp
{
    namespace p1
    {
        public class Solution {
            public int ClimbStairs(int n)
            {
                if (n < 1) return 0;
                if (n <3) return n;
                var first = 1;
                var second = 2;
                for (var i = n; i > 2; i--)
                {
                    var temp = second;
                    second = first + second;
                    first = temp;
                }
                return second;
            }
        }
    }

    namespace p2
    {
        public class Solution {
            public int Rob(int[] nums)
            {
                if (nums == null || nums.Length == 0) return 0;
                if (nums.Length == 1) return nums[0];
                if (nums.Length == 2) return Math.Max(nums[0], nums[1]);
                var first = nums[0];
                var second = Math.Max(nums[0], nums[1]);
                for (var i = 2; i < nums.Length; i++)
                {
                    var temp = second;
                    second = Math.Max(first + nums[i], second);
                    first = temp;
                }
                return second;
            }
        }
    }

    namespace p3
    {
        public class NumArray
        {
            private int[] sums;
            public NumArray(int[] nums)
            {
                if (nums == null || nums.Length == 0)
                {
                    sums = null;
                    return;
                }
                sums = new int[nums.Length];
                sums[0] = nums[0];
                for (var i = 1; i < nums.Length; i++)
                {
                    sums[i] = sums[i - 1] + nums[i];
                }
            }

            public int GetInt(int idx)
            {
                if (idx == -1) return 0;
                else return sums[idx];
            }
            public int SumRange(int i, int j)
            {
                if (sums == null || sums.Length < 0) return 0;
                if (i < 0 || i >= sums.Length || j < 0 || j >= sums.Length || i > j) return 0;
                return GetInt(j) - GetInt(i - 1);
            }
        }
    }

    namespace p4
    {
        public class Solution {
            public int MinPathSum(int[][] grid)
            {
                var rows = grid.Length;
                var cols = grid[0].Length;
                var sums = new int[rows][];
                for (var i = 0; i < rows; i++)
                {
                    sums[i] = new int[cols];
                }

                sums[rows - 1][cols - 1] = grid[rows - 1][cols - 1];
                for (var i = rows - 2; i > -1; i--)
                {
                    sums[i][cols - 1] = grid[i][cols - 1] + sums[i + 1][cols - 1];
                }

                for (var i = cols - 2; i > -1; i--)
                {
                    sums[rows - 1][i] = sums[rows - 1][i + 1] + grid[rows - 1][i];
                }

                for (var i = rows - 2; i > -1; i--)
                {
                    for (var j = cols - 2; j > -1; j--)
                    {
                        sums[i][j] = grid[i][j] + Math.Min(sums[i + 1][j], sums[i][j + 1]);
                    }
                }
                return sums[0][0];
            }
        }
    }

    namespace p5
    {
        public class Solution {
            public string LongestPalindrome(string s)
            {
                if (string.IsNullOrEmpty(s)) return string.Empty;
                var maxLeft = 0;
                var maxRight = 0;
                var doubleList = new List<int>();
                for (var i = 0; i < s.Length - 1; i++)
                {
                    if (s[i] == s[i + 1])
                    {
                        SearchDouble(s,i,out var left,out var right);
                        if (right - left > maxRight - maxLeft)
                        {
                            maxLeft = left;
                            maxRight = right;
                        }
                    }
                    SearchSingle(s,i,out var leftSingle,out var rightSingle);
                    if (rightSingle - leftSingle > maxRight - maxLeft)
                    {
                        maxLeft = leftSingle;
                        maxRight = rightSingle;
                    }
                }

                return s.Substring(maxLeft, maxRight - maxLeft + 1);
            }

            private void SearchSingle(string s, int mid, out int left, out int right)
            {
                left = mid;
                right = mid;
                while (true)
                {
                    if (left < 0 || right == s.Length || s[left] != s[right])
                    {
                        left++;
                        right--;
                        return;
                    }
                    else
                    {
                        left--;
                        right++;
                    }
                }
            }

            private void SearchDouble(string s, int midL, out int left, out int right)
            {
                left = midL;
                right = midL + 1;
                while (true)
                {
                    if (left < 0 || right == s.Length || s[left] != s[right])
                    {
                        left++;
                        right--;
                        return;
                    }
                    else
                    {
                        left--;
                        right++;
                    }
                }
            }
            
        }
    }

    namespace p6
    {
        public class Solution {
            private Dictionary<int,int>[] backup;
            public int LengthOfLIS(int[] nums)
            {
                if (nums == null || nums.Length == 0) return 0;
                else if (nums.Length == 1) return 1;
                backup = new Dictionary<int, int>[nums.Length];
                for (var i = 0; i < nums.Length; i++)
                {
                    backup[i]=new Dictionary<int, int>();
                }
                return Longest(nums,0,-1);
            }

            public int Longest(int[] nums, int i, int lastIdx)
            {
                if (i >= nums.Length) return 0;
                var dict = backup[i];
                if (dict.ContainsKey(lastIdx)) return dict[lastIdx];
                var rs = -1;
                if (lastIdx == -1 || nums[lastIdx] < nums[i])
                {
                    rs = Math.Max(Longest(nums, i + 1, lastIdx), Longest(nums, i + 1, i) + 1);
                }
                else
                {
                    rs = Longest(nums, i + 1, lastIdx);
                }
                dict[lastIdx] = rs;
                return rs;
            }
        }
    }

    namespace p7
    {
        public class Solution {
            public Dictionary<int,int> backup = new Dictionary<int, int>();
            public int CoinChange(int[] coins, int amount) {
                var list = new HashSet<int>(coins).ToList();
                list.Sort();
                backup.Clear();
                return SubSearch(list, amount);
            }

            public int SubSearch(IList<int> list, int amount)
            {
                if (amount == 0) return 0;
                if (backup.ContainsKey(amount)) return backup[amount];
                var len = list.Count;
                var rs = -1;
                for (var i = len - 1; i > -1; i--)
                {
                    var item = list[i];
                    if (item > amount) continue;
                    var subRS = SubSearch(list, amount - item);
                    if (subRS != -1&&((rs == -1 || rs > subRS + 1)))
                    {
                        rs = subRS + 1;
                    }
                }
                backup[amount] = rs;
                return rs;
            }
        }
    }
}