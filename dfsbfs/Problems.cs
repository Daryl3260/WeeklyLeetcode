using System;

namespace Leetcode.leetcode_cn.dfsbfs
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
            public bool IsSymmetric(TreeNode root)
            {
                if (root == null) return true;
                else return IsMirror(root.left, root.right);
            }

            public bool IsMirror(TreeNode left, TreeNode right)
            {
                if (left == null && right == null) return true;
                if (left == null || right == null) return false;
                return left.val == right.val && IsMirror(left.left, right.right) && IsMirror(left.right, right.left);
            }
        }
    }

    namespace p2
    {
        public class Solution {
            public int MaxDepth(TreeNode root)
            {
                if (root == null) return 0;
                else return 1 + Math.Max(MaxDepth(root.left), MaxDepth(root.right));
            }
        }
    }

    namespace p3
    {
        /**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
        public class Solution {
                     public TreeNode BuildTree(int[] preorder, int[] inorder)
                     {
                         if (preorder == null || preorder.Length == 0 || inorder == null || inorder.Length == 0 || preorder.Length!=inorder.Length) return null;
                         return SubBuild(preorder, 0, preorder.Length, inorder, 0, preorder.Length);
                     }
         
                     public TreeNode SubBuild(int[] preorder, int pStart, int pLen, int[] inorder, int iStart, int iLen)
                     {
                         if (pLen < 1) return null;
                         var root = new TreeNode(preorder[pStart]);
                         var idx = -1;
                         for (var i = 0; i < iLen; i++)
                         {
                             if (inorder[iStart+i] == root.val)
                             {
                                 idx = iStart+i;
                                 break;
                             }
                         }
         
                         var leftLen = idx - iStart;
                         var rightLen = iLen - leftLen - 1;
                         root.left = SubBuild(preorder, pStart + 1, leftLen, inorder, iStart, leftLen);
                         root.right = SubBuild(preorder, pStart + leftLen + 1, rightLen, inorder, idx + 1, rightLen);
                         return root;
                     }
                 }
    }

    namespace p4
    {
        /**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
        //choose the value to insert
        public class Solution {
            public TreeNode SortedArrayToBST(int[] nums)
            {
                if (nums == null || nums.Length == 0) return null;
                return SubConstruct(nums, 0, nums.Length);
            }

            public TreeNode SubConstruct(int[] nums, int start, int len)
            {
                if (start<0||start>=nums.Length||len < 1) return null;
                var half = len / 2;
                var mid = start + half;
                var root= new TreeNode(nums[mid]);
                root.left = SubConstruct(nums, start, mid - start);
                root.right = SubConstruct(nums, mid + 1, len - half - 1);
                return root;
            }
        }
    }

    public class ListNode
    {
        public int val;
        public ListNode next;

        public ListNode(int x)
        {
            val = x;
        }
    }

    namespace p5
    {
        public class Solution {
            public int Len(ListNode head)
            {
                var sum = 0;
                while (head != null)
                {
                    sum++;
                    head = head.next;
                }
                return sum;
            }

            public ListNode Move(ListNode head, int steps,out ListNode pre)
            {
                pre = null;
                for (var i = 0; i < steps; i++)
                {
                    pre = head;
                    head = head.next;
                }
                return head;
            }
            public TreeNode SortedListToBST(ListNode head)
            {
                var len = Len(head);
                if (len == 0) return null;
                var half = len / 2;
                var mid = Move(head, half,out var pre);
                var root = new TreeNode(mid.val);
                if (pre != null)
                {
                    pre.next = null;
                    root.left = SortedListToBST(head);
                }

                var rightStart = mid.next;
                mid.next = null;
                root.right = SortedListToBST(rightStart);
                return root;
            }
        }
    }

    namespace p6
    {
        public class Solution {
            public void Flatten(TreeNode root)
            {
                if (root == null) return;
                else SubFlatten(root);
            }
            public TreeNode SubFlatten(TreeNode root)
            {
                if (root == null) return null;
                var left = root.left;
                var right = root.right;
                if (left == null && right == null)
                {
                    return root;
                }
                else if (left != null && right == null)
                {
                    root.left = null;
                    root.right = left;
                    return SubFlatten(left);
                }
                else if (left == null && right != null)
                {
                    return SubFlatten(right);
                }
                else
                {
                    var leftLast = SubFlatten(left);
                    var rightLast = SubFlatten(right);
                    root.left = null;
                    root.right = left;
                    leftLast.right = right;
                    return rightLast;
                }
            }
        }
    }
    public class Node {
        public int val;
        public Node left;
        public Node right;
        public Node next;

        public Node() {}

        public Node(int _val) {
            val = _val;
        }

        public Node(int _val, Node _left, Node _right, Node _next) {
            val = _val;
            left = _left;
            right = _right;
            next = _next;
        }
    }
    namespace p7
    {
        public class Solution {
            public Node Connect(Node root)
            {
                if (root == null) return null;
                var node = root;
                while (node.left!=null)
                {
                    Node pre = null;
                    for (var p = node; p != null; p = p.next)
                    {
                        if (pre != null)
                        {
                            pre.next = p.left;
                        }

                        p.left.next = p.right;
                        pre = p.right;
                    }
                    node = node.left;
                }
                return root;
            }
        }
    }
}