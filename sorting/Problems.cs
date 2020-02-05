using System;
using System.Collections.Generic;
using System.Linq;

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
}