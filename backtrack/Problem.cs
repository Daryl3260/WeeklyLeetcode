using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    namespace p3
    {
        public class Solution {
            public bool CanPartitionKSubsets(int[] nums, int k)
            {
                if (nums == null || nums.Length ==0||nums.Length<k) return false;
                var subSets = new List<IList<int>>();
                subSets.Add(new List<int>{nums[0]});
                return SubSearch(nums, 1, subSets, k);
            }

            public bool SubSearch(int[] nums, int j, IList<IList<int>> subSets, int k)
            {
                if (j == nums.Length)
                {
                    var sum = subSets[0].Sum(elem => elem);
                    return subSets.All(subSet => subSet.Sum(elem => elem) == sum);
                }
                var item = nums[j];
                if (nums.Length - j + subSets.Count-1 >= k)
                {
                    for (var i = 0; i < subSets.Count; i++)
                    {
                        var subSet = subSets[i];
                        subSet.Add(item);
                        if (SubSearch(nums, j + 1, subSets, k)) return true;
                        subSet.RemoveAt(subSet.Count-1);
                    }
                }

                if (subSets.Count < k)
                {
                    var newSet = new List<int>{item};
                    subSets.Add(newSet);
                    if (SubSearch(nums, j + 1, subSets, k)) return true;
                    subSets.RemoveAt(subSets.Count-1);
                }

                return false;
            }
        }

        
    }

    namespace p3.v2
    {
        public class Solution 
        {
            public bool CanPartitionKSubsets(int[] nums, int k) 
            {
                // calculate sum needed per group
                int sum = nums.Sum();              
                if(sum % k != 0) return false;  
                int groupSum = sum / k;
        
                return CanPartitionSubset(nums, groupSum, k, index: 0, currentSum : 0);
            }
    
            private bool CanPartitionSubset(
                int[] nums,
                int sum,
                int k,
                int index,
                int currentSum)
            {
                // Single bucket remaining implies we should be able to place last one as well and don't need
                // to explicitly check for it. Implied by we have already placed rest.
                if(k == 1)
                {
                    return true;
                }
                else if(currentSum == sum)
                {            
                    return CanPartitionSubset(
                        nums,
                        sum,
                        k-1,
                        index: 0,
                        currentSum: 0);
                }
                else
                {
                    for(int i = index; i < nums.Length; i++)
                    {
                        if(nums[i] == -1) continue;
                
                        if(currentSum + nums[i] <= sum)
                        {
                            int temp = nums[i];  
                            nums[i] = -1;
                            bool bCanPartition = CanPartitionSubset(
                                nums,
                                sum, 
                                k, 
                                i+1, 
                                currentSum + temp);
                    
                            if(bCanPartition) return true;
                            if(!bCanPartition) nums[i] = temp;
                        }
                    }
            
                    return false;
                }
            }
        }
    }

    namespace p3.v3
    {
        public class Solution {
            public bool CanPartitionKSubsets(int[] nums, int k)
            {
                if (nums == null || nums.Length < k) return false;
                if (k < 1) return false;
                if (k == 1) return true;
                var sum = nums.Sum(elem => elem);
                if (sum % k != 0) return false;
                var targetSum = sum / k;
                var visited = new bool[nums.Length];
                Array.Sort(nums);
                var temp = new int[nums.Length];
                for (var i = 0; i < nums.Length; i++)
                {
                    temp[i] = nums[nums.Length - 1 - i];
                }
                nums = temp;
                return SubSearch(nums, k, visited,0, 0, targetSum);
            }
            public static List<int> tempList = new List<int>();
            public bool SubSearch(SortedSet<int> nums, int k, int currentSum, int targetSum)
            {
                if (k == 0)
                {
                    return !nums.Any();
                }

                if (currentSum > targetSum) return false;
                if (currentSum == targetSum)
                {
                    return SubSearch(nums, k - 1, 0, targetSum);
                }
                tempList.Clear();
                tempList.AddRange(nums);
                foreach (var elem in tempList)
                {
                    nums.Remove(elem);
                    if (SubSearch(nums, k, currentSum + elem, targetSum)) return true;
                    nums.Add(elem);
                }

                return false;
            }
            public bool SubSearch(int[] nums, int k, bool[] visited,int startIdx, int currentSum, int targetSum)
            {
                if (k == 1) return true;
                for (var i = startIdx; i < nums.Length; i++)
                {
                    if (visited[i]) continue;
                    if (nums[i] + currentSum == targetSum)
                    {
                        visited[i] = true;
                        if (SubSearch(nums, k - 1, visited, 0, 0, targetSum)) return true;
                        visited[i] = false;
                    }
                    else if (nums[i] + currentSum < targetSum)
                    {
                        visited[i] = true;
                        if (SubSearch(nums, k, visited, i + 1, currentSum + nums[i], targetSum)) return true;
                        visited[i] = false;
                    }
                }
                return false;
            }
        }
    }

    namespace p4
    {
        public class Solution {
            public static Dictionary<char,IList<char>> dict = new Dictionary<char, IList<char>>
            {
                {'2',new List<char>{'a','b','c'}},
                {'3',new List<char>{'d','e','f'}},
                {'4',new List<char>{'g','h','i'}},
                {'5',new List<char>{'j','k','l'}},
                {'6',new List<char>{'m','n','o'}},
                {'7',new List<char>{'p','q','r','s'}},
                {'8',new List<char>{'t','u','v'}},
                {'9',new List<char>{'w','x','y','z'}}
            };

            public IList<string> LetterCombinations(string digits) {
                var rs = new List<string>();
                if (string.IsNullOrEmpty(digits)) return rs;
                SubSearch(digits,0,"",rs);
                return rs;
            }

            public void SubSearch(string digits, int idx, string pre, IList<string> rs)
            {
                if (idx == digits.Length)
                {
                    rs.Add(pre);
                }
                else
                {
                    var digit = digits[idx];
                    var paths = dict[digit];
                    foreach (var path in paths)
                    {
                        SubSearch(digits,idx+1,$"{pre}{path}",rs);
                    }
                }
            }
        }
    }

    namespace p5
    {
        //add left when there is left available
        //add right when there is right available and num(left) > num(right)
        public class Solution {
            public IList<string> GenerateParenthesis(int n) {
                var rs = new List<string>();
                if (n <= 0) return rs;
                SubSearch("",0,n,rs);
                return rs;
            }

            public void SubSearch(string pre, int leftSubRight, int leftLeft,IList<string> rs)
            {
                if (leftSubRight == 0 && leftLeft == 0)
                {
                    rs.Add(pre);
                    return;
                }
                if (leftLeft > 0)
                {
                    SubSearch(pre+"(",leftSubRight+1,leftLeft-1,rs);
                }

                if (leftSubRight > 0)
                {
                    SubSearch(pre+")",leftSubRight-1,leftLeft,rs);
                }
                
                    
            }
        }
    }

    namespace p6
    {
        public class Solution {
            public IList<IList<int>> Permute(int[] nums) {
                var pre = new List<int>();
                var rs = new List<IList<int>>();
                if (nums == null || nums.Length == 0) return rs;
                var len = nums.Length;
                var visited = new bool[len];
                SubSearch(nums, visited, pre, rs);
                return rs;
            }

            public void SubSearch(int[] nums, bool[] visited, IList<int> pre, IList<IList<int>> rs)
            {
                if (pre.Count == nums.Length)
                {
                    var list = new List<int>(pre);
                    rs.Add(list);
                    return;
                }

                for (var i = 0; i < visited.Length; i++)
                {
                    if (!visited[i])
                    {
                        var num = nums[i];
                        visited[i] = true;
                        pre.Add(num);
                        SubSearch(nums,visited,pre,rs);
                        pre.RemoveAt(pre.Count-1);
                        visited[i] = false;
                    }
                }
                
            }
        }
    }

    namespace p6.v2
    {
        public class Solution {
            public IList<IList<int>> Permute(int[] nums) {
                if(nums==null||nums.Length==0)return new List<IList<int>>();
                var rs = new List<IList<int>>();
                SubSearch(new HashSet<int>(nums),new List<int>(),rs  );
                return rs;
            }

            public void SubSearch(ISet<int> remains,IList<int> pre,IList<IList<int>> rs)
            {
                if (remains.Any())
                {
                    var temp = new List<int>(remains);
                    foreach (var elem in temp)
                    {
                        remains.Remove(elem);
                        pre.Add(elem);
                        SubSearch(remains,pre,rs);
                        pre.RemoveAt(pre.Count-1);
                        remains.Add(elem);
                    }
                }
                else
                {
                    var list = new List<int>(pre);
                    rs.Add(list);
                }
            }
        }
    }

    namespace p7
    {
        public class Solution {
            public IList<IList<string>> SolveNQueens(int n) {
                if(n==1)return new List<IList<string>>{new List<string>{"Q"}};
                if(n<4)return new List<IList<string>>();
                var record = new int[n];
                var rs = new List<IList<string>>();
                SubSearch(record,0,rs);
                return rs;
            }

            public IList<string> RecordToResult(int[] record)
            {
                var rs = new List<string>();
                var n = record.Length;
                foreach (var elem in record)
                {
                    var builder = new StringBuilder();
                    for (var i = 0; i < elem; i++)
                    {
                        builder.Append('.');
                    }

                    builder.Append('Q');
                    for (var i = elem + 1; i < n; i++)
                    {
                        builder.Append('.');
                    }
                    rs.Add(builder.ToString());
                }

                return rs;
            }

            public IList<int> Available(int[] record,int idx)
            {
                var taken = new HashSet<int>();
                var n = record.Length;
                for (var i = 0; i < idx; i++)
                {
                    var position = record[i];
                    taken.Add(position);
                    var left = position - (idx - i);
                    if (left > -1) taken.Add(left);
                    var right = position + (idx - i);
                    if (right < n) taken.Add(right);
                }
                var rs = new List<int>();
                for (var i = 0; i < n; i++)
                {
                    if (!taken.Contains(i))
                    {
                        rs.Add(i);
                    }
                }

                return rs;
            }
            public void SubSearch(int[] record, int idx, IList<IList<string>> rs)
            {
                if (idx == record.Length)
                {
                    rs.Add(RecordToResult(record));
                }

                var available = Available(record, idx);
                foreach (var elem in available)
                {
                    record[idx] = elem;
                    SubSearch(record,idx+1,rs);
                }
            }
        }
    }

    namespace p8
    {
        public class Solution {
            public IList<IList<int>> Subsets(int[] nums) {
                var rs = new List<IList<int>>();
                SubSearch(new HashSet<int>(),nums,0,rs );
                return rs;
            }

            public void SubSearch(ISet<int> set, int[] nums, int idx, IList<IList<int>> rs)
            {
                if (idx == nums.Length)
                {
                    rs.Add(set.ToList());
                }
                else
                {
                    SubSearch(set,nums,idx+1,rs);
                    set.Add(nums[idx]);
                    SubSearch(set,nums,idx+1,rs);
                    set.Remove(nums[idx]);
                }
            }
        }
    }
}