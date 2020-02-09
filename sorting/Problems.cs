using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leetcode.leetcode_cn.weeklyleetcode.sorting
{
    namespace p1
    {
        public class Solution
        {
            public bool IsAnagram(string s, string t)
            {
                var countS = CountLetter(s);
                var countT = CountLetter(t);
                for (int i = 0; i < countS.Length; i++)
                {
                    if (countS[i] != countT[i]) return false;
                }

                return true;
            }

            public int[] CountLetter(string s)
            {
                var rs = new int[26];
                foreach (var ch in s)
                {
                    rs[ch - 'a']++;
                }

                return rs;
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            public int[] SortArrayByParityII(int[] A)
            {
                var evenPosition = new Stack<int>();
                var oddPosition = new Stack<int>();
                for (var i = 0; i < A.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        if (A[i] % 2 != 0)
                        {
                            evenPosition.Push(i);
                        }
                    }
                    else
                    {
                        if (A[i] % 2 == 0)
                        {
                            oddPosition.Push(i);
                        }
                    }
                }

                while (evenPosition.Any())
                {
                    var even = evenPosition.Pop();
                    var odd = oddPosition.Pop();
                    var temp = A[even];
                    A[even] = A[odd];
                    A[odd] = temp;
                }

                return A;
            }
        }
    }

    namespace p3
    {
        public class Solution
        {
            public int[] Intersection(int[] nums1, int[] nums2)
            {
                Array.Sort(nums1);
                Array.Sort(nums2);
                var idx1 = 0;
                var idx2 = 0;
                var intersection = new List<int>();
                while (idx1 < nums1.Length && idx2 < nums2.Length)
                {
                    if (nums1[idx1] == nums2[idx2])
                    {
                        var num = nums1[idx1];
                        idx1++;
                        idx2++;
                        if (!intersection.Any() || intersection[intersection.Count - 1] != num)
                        {
                            intersection.Add(num);
                        }
                    }
                    else if (nums1[idx1] > nums2[idx2])
                    {
                        idx2++;
                    }
                    else
                    {
                        idx1++;
                    }
                }

                return intersection.ToArray();
            }
        }
    }

    namespace p4
    {
        public class Solution
        {
            public int[] Intersect(int[] nums1, int[] nums2)
            {
                Array.Sort(nums1);
                Array.Sort(nums2);
                var intersection = new List<int>();
                var idx1 = 0;
                var idx2 = 0;
                while (idx1 < nums1.Length && idx2 < nums2.Length)
                {
                    if (nums1[idx1] == nums2[idx2])
                    {
                        var num = nums1[idx1];
                        intersection.Add(num);
                        idx1++;
                        idx2++;
                    }
                    else if (nums1[idx1] < nums2[idx2])
                    {
                        idx1++;
                    }
                    else
                    {
                        idx2++;
                    }
                }

                return intersection.ToArray();
            }
        }
    }

    namespace p5
    {
        public class Solution
        {
            public int LargestPerimeter(int[] A)
            {
                if (A == null || A.Length < 3) return 0;
                Array.Sort(A);
                for (var i = A.Length - 1; i > 1; i--)
                {
                    if (A[i] < A[i - 1] + A[i - 2]) return A[i] + A[i - 1] + A[i - 2];
                }

                return 0;
            }
        }
    }

    namespace p6
    {

        public class ListNode
        {
            public int val;
            public ListNode next;

            public ListNode(int x)
            {
                val = x;
            }
        }

        public class Solution
        {
            public ListNode InsertionSortList(ListNode head)
            {
                if (head == null || head.next == null) return head;
                var header = new ListNode(-1);
                header.next = head;
                var tail = header.next;
                while (tail.next != null)
                {
                    var node = tail.next;
                    if (node.val >= tail.val)
                    {
                        tail = node;
                    }
                    else
                    {
                        tail.next = node.next;
                        for (var prev = header; prev != tail; prev = prev.next)
                        {
                            var inNode = prev.next;
                            if (inNode.val >= node.val)
                            {
                                node.next = inNode;
                                prev.next = node;
                                break;
                            }
                        }
                    }
                    
                }

                return header.next;
            }
        }
    }

    namespace p7
    {
        public class Solution {
            public void WiggleSort(int[] nums) {
                Array.Sort(nums);
                var list = new List<int>(nums);
                int i;
                if (nums.Length % 2 == 0)
                {
                    i = nums.Length / 2 - 1;
                }
                else i = nums.Length / 2;

                var j = nums.Length - 1;
                var k = 0;
                while (k<nums.Length)
                {
                    if(k<nums.Length) nums[k++] = list[i--];
                    if(k<nums.Length) nums[k++] = list[j--];
                }
            }
        }
    }

    namespace p8
    {
        public class Solution {
            public string LargestNumber(int[] nums) {
                Array.Sort(nums,new MyComparer());
                var builder = new StringBuilder();
                foreach (var num in nums)
                {
                    builder.Append(num);
                }

                var str= builder.ToString().TrimStart('0');
                if (string.IsNullOrEmpty(str)) return "0";
                else return str;
            }

            public class MyComparer : Comparer<int>
            {
                public override int Compare(int x, int y)
                {
                    var strX = $"{x}{y}";
                    var strY = $"{y}{x}";
                    return -(strX.CompareTo(strY));
                }
            }
        }
    }

    namespace p9
    {
        public class Solution {
            public int[][] KClosest(int[][] points, int K) {
                Array.Sort(points,new MyComparer());
                var list = new List<int[]>();
                for (var i = 0; i < K; i++)
                {
                    list.Add(points[i]);
                }

                return list.ToArray();
            }

            public class MyComparer : Comparer<int[]>
            {
                public override int Compare(int[] x, int[] y)
                {
                    var sqrX = (x[0] * x[0]) + (x[1] * x[1]);
                    var sqrY = (y[0] * y[0]) + (y[1] * y[1]);
                    return sqrX - sqrY;
                }
            }
        }
    }

    namespace p10
    {
        public class Solution {
            List<int> Left = new List<int>();
            List<int> Right = new List<int>();
            public int ReversePairs(int[] nums)
            {
                if (nums == null || nums.Length == 0) return 0;
                return MergeSort(nums, 0, nums.Length);
            }

            public int MergeSort(int[] nums, int lo, int hi)
            {
                if (hi-lo<2) return 0;
                var rs = 0;
                var mid = lo + (hi - lo) / 2;
                rs += MergeSort(nums, lo, mid) + MergeSort(nums, mid, hi);
                for (var i = lo; i < mid; i++)
                {
                    for (var j = mid; j < hi; j++)
                    {
                        var li = (long) (nums[i]);
                        var lj = (long) (nums[j]);
                        if (li > 2L * lj)
                        {
                            rs++;
                        }
                        else break;
                    }
                }
                Merge(nums,lo,mid,hi);
                return rs;
            }

            public void Merge(int[] nums, int lo, int mid, int hi)
            {
                Left.Clear();
                for (var idx = lo; idx < mid; idx++)
                {
                    Left.Add(nums[idx]);
                }
                var i = 0;
                var k = lo;
                var j = mid;
                while (k < hi)
                {
                    if (i < Left.Count && (j == hi || Left[i] < nums[j]))
                    {
                        nums[k++] = Left[i++];
                    }
                    else
                    {
                        nums[k++] = nums[j++];
                    }
                }
                // var i = 0;
                // var j = 0;
                // var k = 0;
                // var counter = 0;
                // while (k < hi - lo)
                // {
                //     if (i >= mid - lo)
                //     {
                //         nums[lo+(k++)] = nums[mid + (j++)];
                //     }
                //     else if (j >= hi - mid)
                //     {
                //         nums[lo + (k++)] = temp[i++];
                //     }
                //     else
                //     {
                //         var left = temp[i];
                //         var right = nums[mid + j];
                //         
                //     }
                // }
            }
        }
    }
}