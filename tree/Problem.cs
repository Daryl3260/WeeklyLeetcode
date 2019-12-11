using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Leetcode.leetcode_cn.tree
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;

        public TreeNode(int x)
        {
            val = x;
        }
    }

    namespace p1
    {



        public class Solution
        {
            public IList<IList<int>> LevelOrderBottom(TreeNode root)
            {
                var rs = new List<IList<int>>();
                if (root == null) return rs;
                var queue = new Queue<Tuple<TreeNode, int>>();
                queue.Enqueue(new Tuple<TreeNode, int>(root, 0));
                var currentLevel = 0;
                var subList = new List<int>();
                while (queue.Any())
                {
                    var tuple = queue.Dequeue();
                    var node = tuple.Item1;
                    var level = tuple.Item2;
                    if (level > currentLevel)
                    {
                        rs.Add(subList);
                        subList = new List<int>();
                        currentLevel = level;
                    }

                    subList.Add(node.val);
                    if (node.left != null)
                    {
                        queue.Enqueue(new Tuple<TreeNode, int>(node.left, level + 1));
                    }

                    if (node.right != null)
                    {
                        queue.Enqueue(new Tuple<TreeNode, int>(node.right, level + 1));
                    }

                }

                rs.Add(subList);
                var reversed = new List<IList<int>>();
                for (var i = rs.Count - 1; i > -1; i--)
                {
                    reversed.Add(rs[i]);
                }

                return reversed;
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            public bool HasPathSum(TreeNode root, int sum)
            {
                return CheckSum(root, sum);
            }

            public bool CheckSum(TreeNode root, int target)
            {
                if (root == null) return false;
                if (root.left == null && root.right == null)
                {
                    return root.val == target;
                }

                return CheckSum(root.left, target - root.val) || CheckSum(root.right, target - root.val);
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
            public TreeNode InvertTree(TreeNode root)
            {
                if (root == null) return null;
                var left = root.left;
                var right = root.right;
                root.left = InvertTree(right);
                root.right = InvertTree(left);
                return root;
            }
        }
    }

    namespace p4
    {
        public class Solution
        {
            public IList<int> InorderTraversal(TreeNode root)
            {
                var stack = new Stack<TreeNode>();
                var rs = new List<int>();
                if (root == null) return rs;
                var node = root;

                while (node != null)
                {
                    stack.Push(node);
                    node = node.left;
                }

                while (stack.Any())
                {
                    var top = stack.Pop();
                    rs.Add(top.val);
                    if (top.right != null)
                    {
                        node = top.right;
                        while (node != null)
                        {
                            stack.Push(node);
                            node = node.left;
                        }
                    }
                }

                return rs;
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
            public IList<TreeNode> GenerateTrees(int n)
            {
                var list = new List<int>();
                for (int i = 1; i <= n; i++)
                {
                    list.Add(i);
                }

                return SubGenerate(list);
            }

            public IList<TreeNode> SubGenerate(IList<int> list)
            {
                if (!list.Any()) return new List<TreeNode>();
                var rs = new List<TreeNode>();
                for (var i = 0; i < list.Count; i++)
                {
                    var val = list[i];
                    var leftList = new List<int>();
                    var rightList = new List<int>();
                    for (var j = 0; j < i; j++)
                    {
                        leftList.Add(list[j]);
                    }

                    for (var j = i + 1; j < list.Count; j++)
                    {
                        rightList.Add(list[j]);
                    }

                    var leftTrees = SubGenerate(leftList);
                    var rightTrees = SubGenerate(rightList);
                    if (!leftList.Any()) leftTrees.Add(null);
                    if (!rightList.Any()) rightTrees.Add(null);
                    foreach (var leftTree in leftTrees)
                    {
                        foreach (var rightTree in rightTrees)
                        {
                            var root = new TreeNode(val);
                            root.left = leftTree;
                            root.right = rightTree;
                            rs.Add(root);
                        }
                    }
                }

                return rs;
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
            public IList<int> PreorderTraversal(TreeNode root)
            {
                var rs = new List<int>();
                if (root == null) return rs;
                var stack = new Stack<TreeNode>();
                var node = root;
                while (node != null)
                {
                    rs.Add(node.val);
                    stack.Push(node.right);
                    node = node.left;
                }

                while (stack.Any())
                {
                    node = stack.Pop();
                    while (node != null)
                    {
                        rs.Add(node.val);
                        stack.Push(node.right);
                        node = node.left;
                    }
                }

                return rs;
            }
        }
    }

    namespace p7
    {
        public class Solution
        {
            public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
            {
                var pList = new List<TreeNode>();
                var qList = new List<TreeNode>();
                BuildAncestors(root, p, pList);
                BuildAncestors(root, q, qList);
                var len = Math.Min(pList.Count, qList.Count);
                var lca = root;
                for (var i = 0; i < len; i++)
                {
                    if (pList[i] == qList[i])
                    {
                        lca = pList[i];
                    }
                    else break;
                }

                return lca;
            }

            public bool BuildAncestors(TreeNode root, TreeNode target, IList<TreeNode> ancestors)
            {
                if (root == null) return false;
                if (root == target)
                {
                    ancestors.Add(root);
                    return true;
                }

                ancestors.Add(root);
                if (BuildAncestors(root.left, target, ancestors) || BuildAncestors(root.right, target, ancestors))
                    return true;
                else
                {
                    ancestors.RemoveAt(ancestors.Count - 1);
                    return false;
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
            public int MaxPathSum(TreeNode root)
            {
                if (root == null) return 0;
                SubMaxSum(root, out var maxSum, out var maxSinglePath);
                return maxSum;
            }

            public void SubMaxSum(TreeNode root, out int maxSum, out int maxSinglePath)
            {
                if (root.left == null && root.right == null)
                {
                    maxSum = root.val;
                    maxSinglePath = root.val;
                }
                else if (root.left == null || root.right == null)
                {
                    var child = root.left ?? root.right;
                    SubMaxSum(child, out var subMaxSum, out var subMaxSinglePath);
                    if (subMaxSinglePath >= 0)
                    {
                        maxSinglePath = subMaxSinglePath + root.val;
                    }
                    else maxSinglePath = root.val;

                    maxSum = Math.Max(maxSinglePath, subMaxSum);
                }
                else
                {
                    SubMaxSum(root.left, out var leftMax, out var leftSingle);
                    SubMaxSum(root.right, out var rightMax, out var rightSingle);
                    maxSinglePath = Math.Max(Math.Max(root.val, root.val + leftSingle), root.val + rightSingle);
                    maxSum = Math.Max(Math.Max(leftMax, rightMax),
                        Math.Max(maxSinglePath, root.val + leftSingle + rightSingle));
                }
            }
        }
    }

    namespace p9
    {
        public class Codec
        {
            public int FindDepth(TreeNode root)
            {
                if (root == null) return 0;
                else return 1 + Math.Max(FindDepth(root.left), FindDepth(root.right));
            }

            // Encodes a tree to a single string.
            public string serialize(TreeNode root)
            {
                if (root == null) return "null";
                var currentDepth = 0;
                var subList = new List<TreeNode>();
                var queue = new Queue<Tuple<TreeNode,int>>();
                queue.Enqueue(new Tuple<TreeNode, int>(root,0));
                var builder = new StringBuilder();
                while (queue.Any())
                {
                    var top = queue.Dequeue();
                    var node = top.Item1;
                    var depth = top.Item2;
                    if (depth > currentDepth)
                    {
                        currentDepth = depth;
                        foreach (var treeNode in subList)
                        {
                            if (treeNode == null)
                            {
                                builder.Append("null").Append(",");
                            }
                            else
                            {
                                builder.Append(treeNode.val).Append(",");
                            }
                        }
                        builder.Append("|");
                        subList.Clear();
                    }
                    subList.Add(node);
                    if (node != null)
                    {
                        queue.Enqueue(new Tuple<TreeNode, int>(node.left,depth+1));
                        queue.Enqueue(new Tuple<TreeNode, int>(node.right,depth+1));
                    }
                }

                return builder.ToString();
            }

            // Decodes your encoded data to tree.
            public TreeNode deserialize(string data)
            {
                if (data == "null") return null;
                var levels = data.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
                var topLevel = levels[0];
                var values = topLevel.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
                var root = new TreeNode(int.Parse(values[0]));
                var previousLevel = new List<TreeNode>{root};
                var nodeList = new List<TreeNode>();
                for (var i = 1; i < levels.Length; i++)
                {
                    var level = levels[i];
                    values = level.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var value in values)
                    {
                        if (value == "null")
                        {
                            nodeList.Add(null);
                        }
                        else
                        {
                            nodeList.Add(new TreeNode(int.Parse(value)));
                        }
                    }

                    for (var j = 0; j < previousLevel.Count; j++)
                    {
                        var parent = previousLevel[j];
                        parent.left = nodeList[j * 2];
                        parent.right = nodeList[j * 2 + 1];
                    }
                    previousLevel.Clear();
                    foreach (var treeNode in nodeList)
                    {
                        if(treeNode!=null)previousLevel.Add(treeNode);
                    }
                    nodeList.Clear();
                }
                
                return root;
            }
        }

        
    }

    namespace p10
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
            public IList<int> PostorderTraversal(TreeNode root)
            {
                var stack = new Stack<object>();
                var rs = new List<int>();
                if (root == null) return rs;
                stack.Push(root);
                while (stack.Any())
                {
                    var top = stack.Pop();
                    if (top is TreeNode)
                    {
                        var node = top as TreeNode;
                        stack.Push(node.val);
                        if (node.right != null)
                        {
                            stack.Push(node.right);
                        }
                        if (node.left != null)
                        {
                            stack.Push(node.left);
                        }
                    }
                    else
                    {
                        var val = (int) (top);
                        rs.Add(val);
                    }
                }

                return rs;
            }
        }
    }

    namespace p10.V2
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
            public IList<int> PostorderTraversal(TreeNode root)
            {
                var rs = new LinkedList<int>();
                if (root == null) return new List<int>(rs);
                var stack = new Stack<TreeNode>();
                stack.Push(root);
                while (stack.Any())
                {
                    var top = stack.Pop();
                    rs.AddFirst(top.val);
                    if (top.left != null)
                    {
                        stack.Push(top.left);
                    }

                    if (top.right != null)
                    {
                        stack.Push(top.right);
                    }
                }
                return new List<int>(rs);
            }
        }
    }

    namespace p10.V3
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
            public class Wrapper
            {
                public int Val { get; set; }
            }
            public IList<int> PostorderTraversal(TreeNode root) {
                if(root==null)return new List<int>();
                var stack = new Stack<object>();
                stack.Push(root);
                var rs = new List<int>();
                while (stack.Any())
                {
                    var top = stack.Pop();
                    if (top is TreeNode)
                    {
                        var node = top as TreeNode;
                        stack.Push(new Wrapper{Val = node.val});
                        if(node.right!=null) stack.Push(node.right);
                        if(node.left!=null) stack.Push(node.left);
                    }
                    else
                    {
                        var wrapper = top as Wrapper;
                        rs.Add(wrapper.Val);
                    }
                }
                return rs;
            }
        }
}
}