using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Leetcode.leetcode_cn.weeklyleetcode.stackqueue
{
    namespace p1
    {
        public class Solution
        {
            public bool IsValid(string s)
            {
                var stack = new Stack<char>();
                foreach (var ch in s)
                {
                    if (ch == '(' || ch == '[' || ch == '{')
                    {
                        stack.Push(ch);
                    }
                    else
                    {
                        if (stack.Any())
                        {
                            var top = stack.Peek();
                            if (Match(top, ch))
                            {
                                stack.Pop();
                            }
                            else return false;
                        }
                        else return false;
                    }
                }

                return !stack.Any();
            }

            public bool Match(char left, char right)
            {
                return (left == '(' && right == ')') || (left == '[' && right == ']') || (left == '{' && right == '}');
            }
        }
    }

    namespace p2
    {
        public class MinStack
        {

            Stack<int> rawStack = new Stack<int>();
            Stack<int> companionStack = new Stack<int>();

            /** initialize your data structure here. */
            public MinStack()
            {

            }

            public void Push(int x)
            {
                if (companionStack.Any())
                {
                    companionStack.Push(Math.Min(companionStack.Peek(), x));
                }
                else
                {
                    companionStack.Push(x);
                }

                rawStack.Push(x);

            }

            public void Pop()
            {
                rawStack.Pop();
                companionStack.Pop();
            }

            public int Top()
            {
                return rawStack.Peek();
            }

            public int GetMin()
            {
                return companionStack.Peek();
            }
        }

        /**
         * Your MinStack object will be instantiated and called as such:
         * MinStack obj = new MinStack();
         * obj.Push(x);
         * obj.Pop();
         * int param_3 = obj.Top();
         * int param_4 = obj.GetMin();
         */
    }

    namespace p3
    {
        public class MyQueue
        {
            private Stack<int> firstStack;
            private Stack<int> secondStack;

            /** Initialize your data structure here. */
            public MyQueue()
            {
                firstStack = new Stack<int>();
                secondStack = new Stack<int>();
            }

            /** Push element x to the back of queue. */
            public void Push(int x)
            {
                firstStack.Push(x);
            }

            /** Removes the element from in front of queue and returns that element. */
            public int Pop()
            {
                if (!secondStack.Any())
                {
                    while (firstStack.Any())
                    {
                        secondStack.Push(firstStack.Pop());
                    }
                }

                return secondStack.Pop();
            }

            /** Get the front element. */
            public int Peek()
            {
                if (!secondStack.Any())
                {
                    while (firstStack.Any())
                    {
                        secondStack.Push(firstStack.Pop());
                    }
                }

                return secondStack.Peek();
            }

            /** Returns whether the queue is empty. */
            public bool Empty()
            {
                if (!secondStack.Any())
                {
                    while (firstStack.Any())
                    {
                        secondStack.Push(firstStack.Pop());
                    }
                }

                return !secondStack.Any();
            }
        }

        /**
         * Your MyQueue object will be instantiated and called as such:
         * MyQueue obj = new MyQueue();
         * obj.Push(x);
         * int param_2 = obj.Pop();
         * int param_3 = obj.Peek();
         * bool param_4 = obj.Empty();
         */
    }

    namespace p4
    {
        public class MyStack
        {
            private Queue<int> queue0;
            private Queue<int> queue1;

            /** Initialize your data structure here. */
            public MyStack()
            {
                queue0 = new Queue<int>();
                queue1 = new Queue<int>();
            }

            /** Push element x onto stack. */
            public void Push(int x)
            {
                Queue<int> mainQueue = null;
                Queue<int> secondQueue = null;
                if (queue0.Count > 0)
                {
                    mainQueue = queue0;
                    secondQueue = queue1;
                }
                else
                {
                    mainQueue = queue1;
                    secondQueue = queue0;
                }

                mainQueue.Enqueue(x);
            }

            /** Removes the element on top of the stack and returns that element. */
            public int Pop()
            {
                Queue<int> mainQueue = null;
                Queue<int> secondQueue = null;
                if (queue0.Count > 0)
                {
                    mainQueue = queue0;
                    secondQueue = queue1;
                }
                else
                {
                    mainQueue = queue1;
                    secondQueue = queue0;
                }

                while (mainQueue.Count > 1)
                {
                    secondQueue.Enqueue(mainQueue.Dequeue());
                }

                return mainQueue.Dequeue();
            }

            /** Get the top element. */
            public int Top()
            {
                Queue<int> mainQueue = null;
                Queue<int> secondQueue = null;
                if (queue0.Count > 0)
                {
                    mainQueue = queue0;
                    secondQueue = queue1;
                }
                else
                {
                    mainQueue = queue1;
                    secondQueue = queue0;
                }

                while (mainQueue.Count > 1)
                {
                    secondQueue.Enqueue(mainQueue.Dequeue());
                }

                var top = mainQueue.Dequeue();
                secondQueue.Enqueue(top);
                return top;
            }

            /** Returns whether the stack is empty. */
            public bool Empty()
            {
                return queue0.Count == 0 && queue1.Count == 0;
            }
        }

        /**
         * Your MyStack object will be instantiated and called as such:
         * MyStack obj = new MyStack();
         * obj.Push(x);
         * int param_2 = obj.Pop();
         * int param_3 = obj.Top();
         * bool param_4 = obj.Empty();
         */
    }

    namespace p5
    {
        public class Solution
        {
            public class Node
            {
                public int Value { get; set; }
                public Node Prev { get; set; }
                public Node Next { get; set; }
            }

            Node header = new Node();
            Node trailer = new Node();

            public void Init()
            {
                header.Next = trailer;
                trailer.Prev = header;
            }

            public void InsertAfter(Node node, int value)
            {
                var nodeNew = new Node {Value = value};
                nodeNew.Prev = node;
                nodeNew.Next = node.Next;
                nodeNew.Prev.Next = nodeNew;
                nodeNew.Next.Prev = nodeNew;
            }

            public void InsertBefore(Node node, int value)
            {
                var nodeNew = new Node {Value = value};
                nodeNew.Next = node;
                nodeNew.Prev = node.Prev;
                nodeNew.Prev.Next = nodeNew;
                nodeNew.Next.Prev = nodeNew;
            }

            public void Remove(Node node)
            {
                node.Prev.Next = node.Next;
                node.Next.Prev = node.Prev;
            }

            public int[] NextGreaterElement(int[] nums1, int[] nums2)
            {
                Init();
                var rs = new int[nums1.Length];
                for (var i = 0; i < nums1.Length; i++)
                {
                    rs[i] = -1;
                }

                for (var i = 0; i < nums1.Length; i++)
                {
                    InsertBefore(trailer, i);
                }

                var tailNode = header.Next;
                for (var i = 1; i < nums2.Length; i++)
                {
                    var idx = i - 1;
                    for (var node = tailNode; node != header;)
                    {
                        var value = nums1[node.Value];
                        if (nums2[i] > value)
                        {
                            rs[node.Value] = nums2[i];
                            node = node.Prev;
                            Remove(node.Next);
                        }
                        else
                        {
                            node = node.Prev;
                        }
                    }

                    tailNode = tailNode.Next;
                }

                return rs;
            }
        }
    }

    namespace p5.V2
    {
        public class Solution
        {
            public int[] NextGreaterElement(int[] nums1, int[] nums2)
            {
                if (nums1 == null || nums1.Length == 0) return nums1;
                var dict = MakeDict(nums2);
                var nextGreater = NextGreater(nums2);
                var rs = new int[nums1.Length];
                for (var i = 0; i < nums1.Length; i++)
                {
                    var value = nums1[i];
                    var idx = dict[value];
                    rs[i] = nextGreater[idx];
                }

                return rs;
            }

            public Dictionary<int, int> MakeDict(int[] nums)
            {
                var rs = new Dictionary<int, int>();
                for (var i = 0; i < nums.Length; i++)
                {
                    rs[nums[i]] = i;
                }

                return rs;
            }

            public int[] NextGreater(int[] nums)
            {
                var rs = new int[nums.Length];
                rs[rs.Length - 1] = -1;
                for (var i = rs.Length - 2; i > -1; i--)
                {
                    var found = false;
                    for (var j = i + 1; j < rs.Length; j++)
                    {
                        if (nums[j] > nums[i])
                        {
                            rs[i] = nums[j];
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        rs[i] = -1;
                    }

                }

                return rs;
            }
        }
    }

    namespace p5.V3
    {
        public class Solution
        {
            public int[] NextGreaterElement(int[] nums1, int[] nums2)
            {
                if (nums1 == null || nums1.Length == 0) return nums1;
                var rs = new List<int>();
                var dict = NextGreater(nums2);
                foreach (var num in nums1)
                {
                    rs.Add(dict[num]);
                }

                return rs.ToArray();
            }

            public Dictionary<int, int> NextGreater(int[] nums)
            {
                var rs = new Dictionary<int, int>();
                var stack = new Stack<int>();
                foreach (var num in nums)
                {
                    if (!stack.Any())
                    {
                        stack.Push(num);
                    }
                    else
                    {
                        while (stack.Any() && stack.Peek() < num)
                        {
                            var top = stack.Pop();
                            rs[top] = num;
                        }

                        stack.Push(num);
                    }
                }

                while (stack.Any())
                {
                    rs[stack.Pop()] = -1;
                }

                return rs;
            }
        }
    }

    namespace p5.V4
    {
        public class Solution {
            public int[] NextGreaterElements(int[] nums)
            {
                if (nums == null || nums.Length == 0) return nums;
                var dict = new Dictionary<int,int>();
                var max = nums.Max();
                var stack = new Stack<int>();
                var firstLoop = true;
                var idx = 0;
                while (stack.Any()||firstLoop)
                {
                    var value = nums[idx];
                    if (value == max)
                    {
                        dict[idx] = -1;
                        while (stack.Any())
                        {
                            dict[stack.Pop()] = value;
                        }
                    }
                    else
                    {
                        while (stack.Any() && nums[stack.Peek()] < value)
                        {
                            dict[stack.Pop()] = value;
                        }
                        stack.Push(idx);
                    }

                    idx = (idx + 1) % nums.Length;
                    if (idx == 0) firstLoop = false;
                }

                var rs = new int[nums.Length];
                foreach (var entry in dict)
                {
                    rs[entry.Key] = entry.Value;
                }

                return rs;
            }
        }
    }

    namespace p6
    {
        public class Solution
        {
            public int[] NextGreaterElements(int[] nums)
            {
                if (nums == null || nums.Length == 0) return nums;
                else if (nums.Length == 1) return new[] {-1};
                var indexDict = new Dictionary<int, int>();
                var indexStack = new List<int>();
                var idx = 1;
                indexStack.Add(0);
                var maxValue = nums.Max();
                var firstLoop = true;
                while (indexStack.Any()||firstLoop)
                {
                    var value = nums[idx];
                    
                    var collision = false;
                    while (true)
                    {
                        if (indexStack.Any() && nums[indexStack[indexStack.Count-1]] < value)
                        {
                            var top = indexStack[indexStack.Count - 1];
                            indexStack.RemoveAt(indexStack.Count-1);
                            indexDict[top] = value;
                        }
                        else if (indexStack.Any()&&indexStack[indexStack.Count-1]== idx)
                        {
                            indexDict[idx] = -1;
                            indexStack.RemoveAt(indexStack.Count-1);
                            collision = true;
                            break;
                        }
                        else break;

                    }

                    if (value == maxValue)
                    {
                        indexDict[idx] = -1;
                    }
                    else if (!collision) indexStack.Add(idx);
                    idx = (idx + 1) % nums.Length;
                    if (idx == 0) firstLoop = false;
                }

                for (var i = 0; i < nums.Length; i++)
                {
                    if (nums[i] == maxValue)
                    {
                        indexDict[i] = -1;
                    }
                }
                var rs = new int[indexDict.Count];
                foreach (var entry in indexDict)
                {
                    rs[entry.Key] = entry.Value;
                }

                return rs;
            }


        }
    }

    namespace p7
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

        public class BSTIterator
        {
            private List<int> _list = new List<int>();
            private int idx;
            public BSTIterator(TreeNode root)
            {
                var stack = new Stack<TreeNode>();
                while (root != null)
                {
                    stack.Push(root);
                    root = root.left;
                }

                while (stack.Any())
                {
                    var top = stack.Pop();
                    _list.Add(top.val);
                    var node = top.right;
                    while (node != null)
                    {
                        stack.Push(node);
                        node = node.left;
                    }
                }

                idx = 0;
            }

            /** @return the next smallest number */
            public int Next()
            {
                var rs = _list[idx];
                idx++;
                return rs;
            }

            /** @return whether we have a next smallest number */
            public bool HasNext()
            {
                return idx < _list.Count;
            }
        }
    }

    namespace p8
    {

  public class TreeNode {
      public int val;
      public TreeNode left;
      public TreeNode right;
      public TreeNode(int x) { val = x; }
  }
 
        public class Solution {
            public IList<IList<int>> ZigzagLevelOrder(TreeNode root) {
                if(root==null)return new List<IList<int>>();
                var rs = new List<IList<int>>();
                var queue = new Queue<Tuple<TreeNode,int>>();
                var currentLevel = 0;
                var oneLevel = new List<int>();
                queue.Enqueue(new Tuple<TreeNode, int>(root,0));
                while (queue.Any())
                {
                    var top = queue.Dequeue();
                    var node = top.Item1;
                    var level = top.Item2;
                    if (level > currentLevel)
                    {
                        if (currentLevel % 2 == 0)
                        {
                            rs.Add(oneLevel);
                            oneLevel=new List<int>();
                        }
                        else
                        {
                            var temp = new List<int>();
                            for (var i = oneLevel.Count-1; i > -1; i--)
                            {
                                temp.Add(oneLevel[i]);
                            }
                            rs.Add(temp);
                            oneLevel.Clear();
                        }
                        currentLevel = level;
                    }
                    oneLevel.Add(node.val);
                    if (node.left != null)
                    {
                        queue.Enqueue(new Tuple<TreeNode, int>(node.left,level+1));
                    }

                    if (node.right != null)
                    {
                        queue.Enqueue(new Tuple<TreeNode, int>(node.right,level+1));
                    }
                }

                if (currentLevel % 2 == 0)
                {
                    rs.Add(oneLevel);
                }
                else
                {
                    oneLevel.Reverse();
                    rs.Add(oneLevel);
                }

                return rs;
            }
        }
    }

    namespace p9
    {
        public class Solution {
            public int EvalRPN(string[] tokens) {
                var set = new HashSet<string>{"+","-","*","/"};
                var stack = new Stack<int>();
                foreach (var token in tokens)
                {
                    if (set.Contains(token))
                    {
                        var right = stack.Pop();
                        var left = stack.Pop();
                        var rs = 0;
                        switch (token)
                        {
                            case "+":
                                rs = left + right; break;
                            case "-":
                                rs = left - right; break;
                            case "*":
                                rs = left * right; break;
                            case "/":
                                rs = left / right; break;
                        }
                        stack.Push(rs);
                    }
                    else
                    {
                        stack.Push(int.Parse(token));
                    }
                }

                return stack.Pop();
            }
        }
    }

    namespace p10
    {
        public class Solution {
            public int LargestRectangleArea(int[] heights)
            {
                var rs = 0;
                if (heights == null || heights.Length == 0) return 0;
                for (var i = 0; i < heights.Length; i++)
                {
                    var height = heights[i];
                    var width = 0;
                    for (var j = i; j > -1; j--)
                    {
                        if (heights[j] >= height) width++;
                        else break;
                    }

                    for (var j = i + 1; j < heights.Length; j++)
                    {
                        if (heights[j] >= height)
                        {
                            width++;
                        }
                        else break;
                    }

                    var area = width * height;
                    rs = Math.Max(rs, area);
                }

                return rs;
            }
        }
    }

    namespace p10.V2
    {
        public class Solution {
            public int LargestRectangleArea(int[] heights) {
                var stack = new Stack<int>();
                var maxArea = 0;
                for (var i = 0; i < heights.Length; i++)
                {
                    if (!stack.Any() || heights[stack.Peek()] < heights[i])
                    {
                        stack.Push(i);
                    }
                    else
                    {
                        while (stack.Any() && heights[stack.Peek()] >= heights[i])
                        {
                            var idx = stack.Pop();
                            var h = heights[idx];
                            int area;
                            if (stack.Any())
                            {
                                area = h * (i - stack.Peek() - 1);
                            }
                            else area = h * i;
                            maxArea = Math.Max(maxArea, area);
                        }
                        stack.Push(i);
                    }
                }

                while (stack.Count>1)
                {
                    var idx = stack.Pop();
                    var height = heights[idx];
                    var peek = stack.Peek();
                    var area = (heights.Length - 1 - peek)*height;
                    maxArea = Math.Max(maxArea, area);
                }

                if (stack.Any())
                {
                    var area = (heights[stack.Pop()] * heights.Length);
                    maxArea = Math.Max(area, maxArea);
                }

                return maxArea;
            }
        }
    }
}
