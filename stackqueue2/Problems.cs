using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leetcode.leetcode_cn.weeklyleetcode.stackqueue2
{
    namespace p1
    {
        public class Solution {
            public bool BackspaceCompare(string S, string T)
            {
                return CompareStack(ToStack(S), ToStack(T));
            }

            public Stack<char> ToStack(string str)
            {
                var stack = new Stack<char>();
                foreach (var letter in str)
                {
                    if (letter == '#')
                    {
                        if (stack.Any())
                        {
                            stack.Pop();
                        }
                    }
                    else
                    {
                        stack.Push(letter);
                    }
                }

                return stack;
            }

            public bool CompareStack(Stack<char> stack1,Stack<char> stack2)
            {
                if (stack1.Count != stack2.Count)
                {
                    return false;
                }

                while (stack1.Any())
                {
                    if (stack1.Pop() != stack2.Pop())
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }

    namespace p2
    {
        public class Solution {
            public int CalPoints(string[] ops) {
                var stack = new Stack<int>();
                foreach (var str in ops)
                {
                    switch (str)
                    {
                        case "C":
                            stack.Pop(); break;
                        case "D":
                            var top = stack.Peek();
                            stack.Push(top*2);
                            break;
                        case "+":
                            var last = stack.Pop();
                            var secondLast = stack.Peek();
                            stack.Push(last);
                            stack.Push(last+secondLast);
                            break;
                        default:
                            var parsed =int.TryParse(str, out var val);
                            stack.Push(val);
                            break;
                    }
                }

                var rs = 0;
                while (stack.Any())
                {
                    rs += stack.Pop();
                }

                return rs;
            }
        }
    }

    namespace p3
    {
        public class Solution {
            public string RemoveDuplicates(string S) {
                var stack = new Stack<char>();
                foreach (var letter in S)
                {
                    if (stack.Any() && stack.Peek() == letter)
                    {
                        stack.Pop();
                    }
                    else
                    {
                        stack.Push(letter);
                    }
                }

                var list = stack.ToList();
                var builder = new StringBuilder();
                foreach (var letter in list)
                { 
                    builder.Append(letter);
                }

                var rs = builder.ToString();
                builder.Clear();
                for (int i = 0; i < rs.Length; i++)
                {
                    builder.Append(rs[rs.Length - 1 - i]);
                }

                return builder.ToString();
            }
        }
    }

    namespace p4
    {

        class Employee
        {
            public int id;
            public int importance;
            public IList<int> subordinates;
        }


        class Solution
        {
            public int GetImportance(IList<Employee> employees, int id)
            {
                var dict = new Dictionary<int,Employee>();
                foreach (var employee in employees)
                {
                    dict[employee.id] = employee;
                }

                var rs = 0;
                var queue = new Queue<Employee>();
                queue.Enqueue(dict[id]);
                while (queue.Any())
                {
                    var ep = queue.Dequeue();
                    rs += ep.importance;
                    foreach (var subordinate in ep.subordinates)
                    {
                        queue.Enqueue(dict[subordinate]);
                    }
                }

                return rs;
            }
        }
    }

    namespace p5
    {
        
  // This is the interface that allows for creating nested lists.
  // You should not implement it, or speculate about its implementation
  public interface NestedInteger {
 
      // @return true if this NestedInteger holds a single integer, rather than a nested list.
      bool IsInteger();
 
      // @return the single integer that this NestedInteger holds, if it holds a single integer
      // Return null if this NestedInteger holds a nested list
      int GetInteger();
 
      // @return the nested list that this NestedInteger holds, if it holds a nested list
      // Return null if this NestedInteger holds a single integer
      IList<NestedInteger> GetList();
  }
 
        public class NestedIterator {

            public List<int> IntList = new List<int>();
            public int Index { get; set; }
            public NestedIterator(IList<NestedInteger> nestedList) {
                MakeList(nestedList,IntList);
                Index = 0;
            }

            public void MakeList(IList<NestedInteger> nestedIntegers,IList<int> nestedInt)
            {
                foreach (var nestedInteger in nestedIntegers)
                {
                    if (nestedInteger.IsInteger())
                    {
                        nestedInt.Add(nestedInteger.GetInteger());
                    }
                    else
                    {
                        MakeList(nestedInteger.GetList(),nestedInt);
                    }
                }
            }
            public bool HasNext()
            {
                return Index < IntList.Count;
            }

            public int Next()
            {
                var rs = IntList[Index];
                Index++;
                return rs;
            }
        }

        /**
         * Your NestedIterator will be called like this:
         * NestedIterator i = new NestedIterator(nestedList);
         * while (i.HasNext()) v[f()] = i.Next();
         */
    }

    namespace p6
    {
        public class Solution {
            public int MinAddToMakeValid(string S) {
                var stack = new Stack<char>();
                foreach (var ch in S)
                {
                    if (ch == '(')
                    {
                        stack.Push(ch);
                    }
                    else
                    {
                        if (stack.Any() && stack.Peek() == '(')
                        {
                            stack.Pop();
                        }
                        else
                        {
                            stack.Push(ch);
                        }
                    }
                }

                return stack.Count;
            }
        }
    }

    namespace p7
    {
        public class NestedInteger {
 
            // Constructor initializes an empty nested list.
            public NestedInteger(){}
            private List<NestedInteger> _nestedIntegers = new List<NestedInteger>();

            private int _integer = 0;
            // Constructor initializes a single integer.
            public NestedInteger(int value){}
 
            // @return true if this NestedInteger holds a single integer, rather than a nested list.
            public bool IsInteger()
            {
                return !_nestedIntegers.Any();
            }
 
            // @return the single integer that this NestedInteger holds, if it holds a single integer
            // Return null if this NestedInteger holds a nested list
            public int GetInteger()
            {
                return _integer;
            }
 
            // Set this NestedInteger to hold a single integer.
            public void SetInteger(int value)
            {
                _integer = value;
            }
 
            // Set this NestedInteger to hold a nested list and adds a nested integer to it.
            public void Add(NestedInteger ni)
            {
                _nestedIntegers.Add(ni);
            }
 
            // @return the nested list that this NestedInteger holds, if it holds a nested list
            // Return null if this NestedInteger holds a single integer
            public IList<NestedInteger> GetList()
            {
                return _nestedIntegers;
            }
        }
        
        public class Solution {
            //"[123,[456,[789]]]"
            public IList<string> SplitString(string s)
            {
                var str = s.Substring(1, s.Length - 2);
                var nested = 0;
                var splitList = new List<int>();
                var rs = new List<string>();
                for (var i = 0; i < str.Length; i++)
                {
                    var ch = str[i];
                    if (ch == '[')
                    {
                        nested++;
                    }
                    else if (ch == ']')
                    {
                        nested--;
                    }
                    else if (ch == ',')
                    {
                        if (nested == 0)
                        {
                            splitList.Add(i);
                        }
                    }
                }

                var starter = 0;
                foreach (var idx in splitList)
                {
                    rs.Add(str.Substring(starter,idx-starter));
                    starter = idx + 1;
                }
                rs.Add(str.Substring(starter,str.Length-starter));

                return rs;
            }
            public NestedInteger Deserialize(string s) {
                if (s == "[]")
                {
                    return new NestedInteger();
                }
                var rs = new NestedInteger();
                if (s[0] == '[')
                {
                    var splitList = SplitString(s);
                    foreach (var str in splitList)
                    {
                        rs.Add(Deserialize(str));
                    }
                }
                else
                {
                    rs.SetInteger(int.Parse(s));
                }
                return rs;
            }
        }
    }
    
}