using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Leetcode.leetcode_cn.weeklyleetcode.tree3
{
    namespace p1
    {
        public class Solution
        {
            public bool IsBalanced(TreeNode root)
            {
                Height(root, out var balanced);
                return balanced;
            }

            public int Height(TreeNode root, out bool balanced)
            {
                if (root == null)
                {
                    balanced = true;
                    return 0;
                }
                else
                {
                    var leftHeight = Height(root.left, out var lb);
                    var rightHeight = Height(root.right, out var rb);
                    if (lb && rb && Math.Abs(leftHeight - rightHeight) < 2) balanced = true;
                    else balanced = false;
                    return 1 + Math.Max(leftHeight, rightHeight);
                }
            }
        }
    }
    public class TreeNode
    {
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
        public class Solution
        {
            public int GetMinimumDifference(TreeNode root)
            {
                var list = new List<int>();
                Traverse(root, list);
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
                Traverse(root.left, list);
                Traverse(root.right, list);
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
        public class Solution
        {

            public TreeNode ConvertBST(TreeNode root)
            {
                TraverseOne(root, null);
                return root;
            }

            public void TraverseOne(TreeNode root, TreeNode rightParent)
            {
                if (root == null) return;
                if (root.right != null)
                {
                    TraverseOne(root.right, rightParent);
                }

                if (root.right != null)
                {
                    var leftMost = root.right;
                    while (leftMost.left != null) leftMost = leftMost.left;
                    root.val += leftMost.val;
                }

                if (root.right == null && rightParent != null)
                {
                    root.val += rightParent.val;
                }

                if (root.left != null)
                {
                    TraverseOne(root.left, root);
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
        public class Solution
        {
            public int SumRootToLeaf(TreeNode root)
            {
                var rs = 0;
                var list = new List<int>();
                Traverse(root, list, ref rs);
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

            public void Traverse(TreeNode root, IList<int> list, ref int sum)
            {
                if (root == null) return;
                list.Add(root.val);
                if (root.left == null && root.right == null)
                {
                    sum += ConvertToInt(list);
                }
                else
                {
                    Traverse(root.left, list, ref sum);
                    Traverse(root.right, list, ref sum);
                }
                list.RemoveAt(list.Count - 1);
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
        public class Solution
        {
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
        public class Solution
        {
            public TreeNode InorderSuccessor(TreeNode root, TreeNode p)
            {
                if (p.right != null)
                {
                    var ptr = p.right;
                    while (ptr.left != null) ptr = ptr.left;
                    return ptr;
                }
                else
                {
                    TreeNode rightParent = null;
                    Search(root, p, ref rightParent);
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
                    Search(root.right, p, ref rightParent);
                }
                else
                {
                    rightParent = root;
                    Search(root.left, p, ref rightParent);
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
        public class Solution
        {
            public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
            {
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
                        ancestors.RemoveAt(ancestors.Count - 1);
                        return false;
                    }
                }
            }
        }
    }

    namespace p8
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
        public class Solution
        {
            public int MaxAncestorDiff(TreeNode root)
            {
                var rs = 0;
                Traverse(root, new List<int>(), ref rs);
                return rs;
            }

            public void Traverse(TreeNode root, IList<int> list, ref int maxDiff)
            {
                // if (root == null) return;
                list.Add(root.val);
                if (root.left == null && root.right == null)
                {
                    var min = int.MaxValue;
                    var max = int.MinValue;
                    foreach (var elem in list)
                    {
                        if (elem < min)
                        {
                            min = elem;
                        }

                        if (elem > max)
                        {
                            max = elem;
                        }
                    }

                    var diff = max - min;
                    maxDiff = Math.Max(diff, maxDiff);
                }

                if (root.left != null)
                {
                    Traverse(root.left, list, ref maxDiff);
                }

                if (root.right != null)
                {
                    Traverse(root.right, list, ref maxDiff);
                }
                list.RemoveAt(list.Count - 1);
            }
        }
    }

    namespace p9
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
        public class Solution
        {
            public TreeNode RecoverFromPreorder(string S)
            {
                var list = Serialize(S);
                return RecoverTree(list, 0, 0, list.Count);
            }

            private string DashMaker(int num)
            {
                var builder = new StringBuilder();
                for (int i = 0; i < num; i++)
                {
                    builder.Append('-');
                }

                return builder.ToString();
            }
            //true for val,false for depth
            public IList<Tuple<int, bool>> Serialize(string s)
            {
                var rs = new List<Tuple<int, bool>>();
                var startIdx = 0;
                while (true)
                {
                    var idx = s.IndexOf('-', startIdx);
                    if (idx == -1)
                    {
                        var val = int.Parse(s.Substring(startIdx));
                        rs.Add(new Tuple<int, bool>(val, true));
                        break;
                    }
                    else
                    {
                        var val = int.Parse(s.Substring(startIdx, idx - startIdx));
                        rs.Add(new Tuple<int, bool>(val, true));
                        var secondIdx = idx;
                        while (s[secondIdx] == '-') secondIdx++;
                        var len = secondIdx - idx;
                        rs.Add(new Tuple<int, bool>(len, false));
                        startIdx = secondIdx;
                    }
                }

                return rs;
            }

            public TreeNode RecoverTree(IList<Tuple<int, bool>> strList, int rootDepth, int startIdx, int endIdx)
            {

                var rootVal = strList[startIdx];
                var root = new TreeNode(rootVal.Item1);
                var firstIdx = -1;
                for (int i = startIdx; i < endIdx; i++)
                {
                    if (strList[i].Item2 == false && strList[i].Item1 <= rootDepth) break;
                    if (strList[i].Item2 == false && strList[i].Item1 == rootDepth + 1)
                    {
                        firstIdx = i;
                        break;
                    }
                }

                if (firstIdx == -1) return root;

                var secondIdx = -1;
                for (int i = firstIdx + 1; i < endIdx; i++)
                {
                    if (strList[i].Item2 == false && strList[i].Item1 <= rootDepth) break;
                    if (strList[i].Item2 == false && strList[i].Item1 == rootDepth + 1)
                    {
                        secondIdx = i;
                        break;
                    }
                }

                if (secondIdx == -1)
                {
                    root.left = RecoverTree(strList, rootDepth + 1, firstIdx + 1, endIdx);
                }
                else
                {
                    root.left = RecoverTree(strList, rootDepth + 1, firstIdx + 1, secondIdx);
                    root.right = RecoverTree(strList, rootDepth + 1, secondIdx + 1, endIdx);
                }



                return root;
            }
        }
    }
}
