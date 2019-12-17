using System;
using System.Collections.Generic;
using System.Linq;

namespace Leetcode.leetcode_cn.backtrack
{
    public class TreeNode {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) { val = x; }
    }
    namespace p1
    {
        public class Solution {
            public IList<string> ReadBinaryWatch(int num) {
                var rs = new List<string>();
                for (var i = 0; i <= num; i++)
                {
                    var hour = i;
                    var minute = num - i;
                    if(hour>4||minute>6)continue;
                    var hourList = ReadingHour(hour);
                    var minuteList = ReadingMinute(minute);
                    foreach (var hourStr in hourList)
                    {
                        foreach (var minuteStr in minuteList)
                        {
                            if (int.Parse(hourStr)>11||int.Parse(minuteStr)>59) continue;
                            rs.Add($"{hourStr}:{minuteStr}");
                        }
                    }
                }

                return rs;
            }

            public IList<string> ReadingHour(int num)
            {
                var numList = new List<int>();
                Check(num,4,1,0,numList);
                return numList.Select(elem => $"{elem}").ToList();
            }

            public void Check(int num, int vacuum,int baseNum, int sum, IList<int> rs)
            {
                if (num > vacuum) return;
                if (num == 0)
                {
                    rs.Add(sum);
                }
                else
                {
                    Check(num-1,vacuum-1,baseNum<<1,sum+baseNum,rs);
                    Check(num,vacuum-1,baseNum<<1,sum,rs);
                }
            }

            public IList<string> ReadingMinute(int num)
            {
                var numList = new List<int>();
                Check(num,6,1,0,numList);
                return numList.Select(elem => elem < 10 ? $"0{elem}" : $"{elem}").ToList();
            }
        }
    }

    namespace p2
    {
        public class Solution {
            public int LongestUnivaluePath(TreeNode root)
            {
                if (root == null) return 0;
                var rs= SubSearch(root, out var singlePath);
                return rs - 1;
            }

            public int SubSearch(TreeNode root, out int singlePath)
            {
                if (root == null)
                {
                    singlePath = 0;
                    return 0;
                }

                var leftAll = SubSearch(root.left, out var leftSingle);
                var rightAll = SubSearch(root.right, out var rightSingle);
                var rs = Math.Max(leftAll, rightAll);
                var max = 1;
                singlePath = 1;
                if (root.left != null && root.left.val == root.val)
                {
                    max += leftSingle;
                    singlePath = Math.Max(singlePath, 1 + leftSingle);
                }

                if (root.right != null && root.right.val == root.val)
                {
                    max += rightSingle;
                    singlePath = Math.Max(singlePath, 1 + rightSingle);
                }

                rs = Math.Max(rs, max);
                return rs;
            }
        }
    }
}