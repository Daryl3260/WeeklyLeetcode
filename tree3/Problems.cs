using System;
using System.Collections.Generic;

namespace Leetcode.leetcode_cn.weeklyleetcode.tree3
{
    namespace p1
    {
        public class Solution {
            public bool IsBalanced(TreeNode root)
            {
                Height(root, out var balanced);
                return balanced;
            }

            public int Height(TreeNode root,out bool balanced)
            {
                if (root == null)
                {
                    balanced = true;
                    return 0;
                }
                else
                {
                    var leftHeight = Height(root.left,out var lb);
                    var rightHeight = Height(root.right,out var rb);
                    if (lb && rb && Math.Abs(leftHeight - rightHeight) < 2) balanced = true;
                    else balanced = false;
                    return 1 + Math.Max(leftHeight, rightHeight);
                }
            }
        }
    }
  public class TreeNode {
      public int val;
      public TreeNode left;
      public TreeNode right;
      public TreeNode(int x) { val = x; }
  }
 
    

    namespace p2
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
            public int GetMinimumDifference(TreeNode root) {
                var list = new List<int>();
                Traverse(root,list);
                list.Sort();
                var minDiff = int.MaxValue;
                for (int i = 0; i < list.Count - 1; i++)
                {
                    var diff = list[i + 1] - list[i];
                    minDiff = Math.Min(minDiff, diff);
                }
                return minDiff;
            }

            public void Traverse(TreeNode root, IList<int> list)
            {
                if (root == null) return;
                list.Add(root.val);
                Traverse(root.left,list);
                Traverse(root.right,list);
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
            
            public TreeNode ConvertBST(TreeNode root)
            {
                TraverseOne(root,null);
                return root;
            }

            public void TraverseOne(TreeNode root,TreeNode rightParent)
            {
                if (root == null) return;
                if (root.right != null)
                {
                    TraverseOne(root.right,rightParent);
                }

                if (root.right != null)
                {
                    var leftMost = root.right;
                    while (leftMost.left != null) leftMost = leftMost.left;
                    root.val += leftMost.val;
                }

                if (root.right==null&&rightParent != null)
                {
                    root.val += rightParent.val;
                }

                if (root.left != null)
                {
                    TraverseOne(root.left,root);
                }   
                
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
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
        public class Solution {
            public int SumRootToLeaf(TreeNode root)
            {
                var rs = 0;
                var list = new List<int>();
                Traverse(root,list,ref rs);
                return rs;
            }

            public int ConvertToInt(IList<int> list)
            {
                var sum = 0;
                var basic = 1;
                for (int i = 0; i < list.Count; i++)
                {
                    sum += list[list.Count - 1 - i] * basic;
                    basic <<= 1;
                }

                return sum;
            }

            public void Traverse(TreeNode root, IList<int> list,ref int sum)
            {
                if (root == null) return;
                list.Add(root.val);
                if (root.left==null&&root.right==null)
                {
                    sum += ConvertToInt(list);
                }
                else
                {
                    Traverse(root.left,list,ref sum);
                    Traverse(root.right,list,ref sum);
                }
                list.RemoveAt(list.Count-1);
            }
        }
    }

    namespace p5
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
            public bool IsValidBST(TreeNode root)
            {
                if (root == null) return true;
                if (root.left != null)
                {
                    var rightMost = root.left;
                    while (rightMost.right != null)
                    {
                        rightMost = rightMost.right;
                    }

                    if (rightMost.val >= root.val) return false;
                }

                if (root.right != null)
                {
                    var leftMost = root.right;
                    while (leftMost.left != null)
                    {
                        leftMost = leftMost.left;
                    }

                    if (leftMost.val <= root.val) return false;
                }

                return IsValidBST(root.left) && IsValidBST(root.right);
            }
        }
    }

    namespace p6
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
            public TreeNode InorderSuccessor(TreeNode root, TreeNode p) {
                if (p.right != null)
                {
                    var ptr = p.right;
                    while (ptr.left != null) ptr = ptr.left;
                    return ptr;
                }
                else
                {
                    TreeNode rightParent = null;
                    Search(root,p,ref rightParent);
                    return rightParent;
                }
            }

            public void Search(TreeNode root, TreeNode p, ref TreeNode rightParent)
            {
                if (root == p)
                {
                    return;
                }
                else if (root.val < p.val)
                {
                    Search(root.right,p,ref rightParent);
                }
                else
                {
                    rightParent = root;
                    Search(root.left,p,ref rightParent);
                }
            }
        }
    }

    namespace p7
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
            public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q) {
                var pList = new List<TreeNode>();
                var qList = new List<TreeNode>();
                Traverse(root, p, pList);
                Traverse(root, q, qList);
                var idx = 0;
                while (true)
                {
                    if (idx == pList.Count || idx == qList.Count || pList[idx] != qList[idx]) break;
                    else idx++;
                }

                return pList[idx - 1];
            }

            public bool Traverse(TreeNode root, TreeNode target, IList<TreeNode> ancestors)
            {
                if (root == null) return false;
                ancestors.Add(root);
                if (root == target)
                {
                    return true;
                }
                else
                {
                    if (Traverse(root.left, target, ancestors) || Traverse(root.right, target, ancestors))
                    {
                        return true;
                    }
                    else
                    {
                        ancestors.RemoveAt(ancestors.Count-1);
                        return false;
                    }
                }
            }
        }
    }
    
}
