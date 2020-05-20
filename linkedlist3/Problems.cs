using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leetcode.leetcode_cn.weeklyleetcode.linkedlist3
{
    namespace p1
    {

          public class ListNode {
              public int val;
              public ListNode next;
              public ListNode(int val=0, ListNode next=null) {
                  this.val = val;
                  this.next = next;
              }
          }
 
        public class Solution {
            public ListNode DeleteDuplicates(ListNode head)
            {
                if (head == null || head.next == null) return head;
                var header = new ListNode();
                header.next = head;
                var slow = header;
                var fast = header;
                while (true)
                {
                    while (fast.next != null && slow.next.val == fast.next.val) fast = fast.next;
                    if (fast.next == null)
                    {
                        slow.next.next = null;
                        break;
                    }
                    else
                    {
                        slow.next.next = fast.next;
                        slow = slow.next;
                    }
                }

                return header.next;
            }
        }
    }

    namespace p2
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
            public void DeleteNode(ListNode node)
            {
                node.val = node.next.val;
                node.next = node.next.next;
            }
        }
    }

    namespace p3
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
            public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
            {
                var stackA = new Stack<ListNode>();
                var stackB = new Stack<ListNode>();
                while (headA != null)
                {
                    stackA.Push(headA);
                    headA = headA.next;
                }

                while (headB != null)
                {
                    stackB.Push(headB);
                    headB = headB.next;
                }

                ListNode rs = null;
                while (stackA.Any()&&stackB.Any()&&stackA.Peek() == stackB.Peek())
                {
                    rs = stackA.Pop();
                    stackB.Pop();
                }

                return rs;
            }
        }
    }

    namespace p4
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
            public ListNode DeleteNode(ListNode head, int val)
            {
                var header = new ListNode(-1);
                header.next = head;
                var p = header;
                while (p.next != null && p.next.val != val)
                {
                    p = p.next;
                }

                if (p.next == null) return header.next;
                else
                {
                    p.next = p.next.next;
                    return header.next;
                }
            }
        }
    }

    namespace p5
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
            public int[] NextLargerNodes(ListNode head)
            {
                var len = 0;
                var node = head;
                while (node != null)
                {
                    len++;
                    node = node.next;
                }
                var rs = new int[len];
                var stack = new Stack<Tuple<ListNode, int>>();
                var idx = 0;
                var p = head;
                while (p != null)
                {
                    if (!stack.Any()||stack.Peek().Item1.val>=p.val)
                    {
                        stack.Push(new Tuple<ListNode, int>(p,idx));
                        idx++;
                        p = p.next;
                    }
                    else
                    {
                        var val = p.val;
                        while (stack.Any() && stack.Peek().Item1.val < val)
                        {
                            var pop = stack.Pop();
                            var popIdx = pop.Item2;
                            rs[popIdx] = val;
                        }
                        stack.Push(new Tuple<ListNode, int>(p,idx));
                        idx++;
                        p = p.next;
                    }
                }

                return rs;
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
            public ListNode SwapPairs(ListNode head)
            {
                var header = new ListNode(-1);
                header.next = head;
                var p = header;
                while (p.next != null && p.next.next != null)
                {
                    var p1 = p.next;
                    var p2 = p1.next;
                    p.next = p2;
                    p1.next = p2.next;
                    p2.next = p1;
                    p = p1;
                }

                return header.next;
            }
        }
    }

    namespace p7
    {

        public class ListNode
        {
            public int val;
            public ListNode next;

            public ListNode(int x)
            {
                val = x;
                next = null;
            }
        }

        public class Solution
        {
            public ListNode DetectCycle(ListNode head)
            {
                var header = new ListNode(-1);
                header.next = head;
                var fast = header;
                var slow = header;
                while (true)
                {
                    if (fast.next == null || fast.next.next == null)
                    {
                        return null;
                    }

                    fast = fast.next.next;
                    slow = slow.next;
                    if (fast == slow)
                    {
                        var p = header;
                        while (p != slow)
                        {
                            slow = slow.next;
                            p = p.next;
                        }

                        return p;
                    }
                }
            }
        }
    }

}