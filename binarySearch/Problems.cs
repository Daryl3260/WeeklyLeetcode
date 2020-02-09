using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Leetcode.leetcode_cn.weeklyleetcode.binarySearch
{
    namespace p1
    {
        public class Solution {
            public int Search(int[] nums, int target)
            {
                if (nums == null || nums.Length == 0) return -1;
                var lo = 0;
                var hi = nums.Length - 1;
                while (hi - lo > 5)
                {
                    var mid = lo + (hi - lo) / 2;
                    var midValue = nums[mid];
                    if (midValue == target)
                    {
                        return mid;
                    }
                    else if (midValue > target)
                    {
                        hi = mid - 1;
                    }
                    else
                    {
                        lo = mid + 1;
                    }
                }

                for (var i = lo; i <= hi; i++)
                {
                    if (nums[i] == target)
                    {
                        return i;
                    }
                }

                return -1;
            }
        }
}

    namespace p2
    {
        public class Solution {
            public int SearchInsert(int[] nums, int target)
            {
                if (nums == null || nums.Length == 0) return 0;
                var lo = 0;
                var hi = nums.Length - 1;
                while (hi - lo < 5)
                {
                    if (nums[hi] == target) return hi;
                    if (nums[lo] >= target) return lo;
                    if (nums[hi] < target) return hi + 1;
                    var mid = lo + (hi - lo) / 2;
                    var midValue = nums[mid];
                    if (midValue == target)
                    {
                        return mid;
                    }
                    else if (midValue > target)
                    {
                        hi = mid - 1;
                    }
                    else
                    {
                        lo = mid + 1;
                    }
                }
                if (nums[hi] == target) return hi;
                if (nums[lo] >= target) return lo;
                if (nums[hi] < target) return hi + 1;
                for (var i = lo; i < hi; i++)
                {
                    if (nums[i] >= target) return i;
                }

                return -1;
            }
        }
}

    namespace p3
    {
        public class Solution {
            public int[] TwoSum(int[] numbers, int target)
            {
                var lo = 0;
                var hi = numbers.Length - 1;
                while (true)
                {
                    var sum = numbers[lo] + numbers[hi];
                    if (sum == target) break;
                    else if (sum < target) lo++;
                    else hi--;
                }

                return new[] {lo + 1, hi + 1};
            }
        }
    }

    namespace p4
    {
        public class Solution {
            public bool IsPerfectSquare(int num)
            {
                return IsPerfectSquareLong(num);
            }

            public bool IsPerfectSquareLong(long num)
            {
                if (num < 0) return false;
                else if (num < 2) return true;
                var lo = 1L;
                var hi = num / 2;
                while (hi-lo>5L)
                {
                    var mid = lo + (hi - lo) / 2;
                    var midsquare = mid * mid;
                    if (midsquare == num)
                    {
                        return true;
                    }
                    else if (midsquare < num)
                    {
                        lo = mid + 1;
                    }
                    else
                    {
                        hi = mid - 1;
                    }
                }

                for (var i = lo; i <= hi; i++)
                {
                    if (i * i == num) return true;
                }

                return false;
            }
        }
    }

    namespace p5
    {
        public class Solution {
            public int MySqrt(int x)
            {
                return (int) MySqrtLong(x);
            }

            public long MySqrtLong(long x)
            {
                if (x < 2) return x;
                var lo = 1L;
                var hi = x / 2;
                while (hi-lo>5L)
                {
                    var mid = lo + (hi - lo) / 2;
                    var midSquare = mid * mid;
                    if (midSquare == x) return mid;
                    else if (midSquare > x)
                    {
                        hi = mid - 1;
                    }
                    else if ((mid + 1) * (mid + 1) > x)
                    {
                        return mid;
                    }
                    else
                    {
                        lo = mid + 1;
                    }
                }

                for (var i = lo; i <= hi; i++)
                {
                    if (i * i == x) return i;
                    else if (i * i > x) return i - 1;
                }

                return hi;
            }
        }
    }

    namespace p6
    {
        public class Solution {
            public bool SearchMatrix(int[][] matrix, int target)
            {
                if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return false;
                var row = 0;
                var col = matrix[0].Length - 1;
                while (row > -1 && row < matrix.Length && col > -1 && col < matrix[0].Length)
                {
                    var val = matrix[row][col];
                    if (val == target) return true;
                    else if (val > target)
                    {
                        col--;
                    }
                    else
                    {
                        row++;
                    }
                }

                return false;
            }
        }
    }

    namespace p7
    {
        public class Solution {
            public int Search(int[] nums, int target)
            {
                if (nums == null || nums.Length == 0) return -1;
                var lo = 0;
                var hi = nums.Length - 1;
                while (hi - lo > 5L)
                {
                    var mid = lo + (hi - lo) / 2;
                    var midValue = nums[mid];
                    if (midValue == target) return mid;
                    if (nums[lo] < nums[hi])
                    {
                        if (midValue < target) lo = mid + 1;
                        else
                        {
                            hi = mid - 1;
                        }
                    }
                    else
                    {
                        if (midValue > nums[lo])
                        {
                            if (target >= nums[lo] && target < midValue)
                            {
                                hi = mid - 1;
                            }
                            else
                            {
                                lo = mid + 1;
                            }
                        }
                        else
                        {
                            if (target > midValue && target <= nums[hi])
                            {
                                lo = mid + 1;
                            }
                            else
                            {
                                hi = mid - 1;
                            }
                        }
                    }
                    
                }

                for (var i = lo; i <= hi; i++)
                {
                    if (nums[i] == target)
                    {
                        return i;
                    }
                }

                return -1;
            }
        }
    }

    namespace p8
    {
        public class Solution {
            private Dictionary<int,double> dict = new Dictionary<int, double>();
            public double MyPow(double x, int n)
            {
                if (n == 1) return x;
                else if (n == 0) return 1.0;
                else if (n < 0) return 1.0 / MyPow(x,-(n+1))/x;
                else if (dict.ContainsKey(n))
                {
                    return dict[n];
                }
                else
                {
                    var half = MyPow(x, n / 2);
                    if (n % 2 == 0)
                    {
                        var rs = half * half;
                        dict[n / 2] = half;
                        dict[n] = rs;
                        return rs;
                    }
                    else
                    {
                        var rs = half * half * x;
                        dict[n / 2] = half;
                        dict[n] = rs;
                        return rs;
                    }
                }
            }
        }
    }

    namespace p9
    {
        public class Solution {
            public int[] SearchRange(int[] nums, int target)
            {
                if (nums == null || nums.Length == 0) return new[] {-1, -1};
                var first = SearchFirst(nums, target, 0, nums.Length);
                if (first == -1) return new[] {-1, -1};
                else
                {
                    var last = SearchLast(nums, target, 0, nums.Length);
                    return new[] {first, last};
                }
            }

            public int SearchFirst(int[] nums,int target, int lo, int hi)
            {
                while (true)
                {
                    if (hi - lo < 5)
                    {
                        for (var i = lo; i < hi; i++)
                        {
                            if (nums[i] == target) return i;
                        }

                        return -1;
                    }

                    var mid = lo + (hi - lo) / 2;
                    var midValue = nums[mid];
                    if (midValue == target)
                    {
                        if (mid == lo || nums[mid - 1] < target)
                        {
                            return mid;
                        }
                        else
                        {
                            hi = mid;
                        }
                    }
                    else if (midValue > target)
                    {
                        hi = mid;
                    }
                    else
                    {
                        lo = mid + 1;
                    }
                }
                
            }

            public int SearchLast(int[] nums, int target, int lo, int hi)
            {
                while (true)
                {
                    if (hi - lo < 5)
                    {
                        for (var i = hi-1; i >=lo; i--)
                        {
                            if (nums[i] == target) return i;
                        }

                        return -1;
                    }

                    var mid = lo + (hi - lo) / 2;
                    var midValue = nums[mid];
                    if (midValue == target)
                    {
                        if (mid == hi-1 || nums[mid + 1] > target)
                        {
                            return mid;
                        }
                        else
                        {
                            lo = mid + 1;
                        }
                    }
                    else if (midValue > target)
                    {
                        hi = mid;
                    }
                    else
                    {
                        lo = mid + 1;
                    }
                }
            }
        }
    }

    namespace p10
    {
        public class Solution {
            public double FindMedianSortedArrays(int[] nums1, int[] nums2)
            {
                if (nums1 == null)
                {
                    return FindMedianSingle(nums2);
                }

                if (nums2 == null)
                {
                    return FindMedianSingle(nums1);
                }
                var len = nums1.Length + nums2.Length;
                if (len % 2 == 1)
                {
                    return FindKth(nums1, 0, nums1.Length, nums2, 0, nums2.Length, len / 2 + 1);
                }
                else
                {
                    var left = FindKth(nums1, 0, nums1.Length, nums2, 0, nums2.Length, len / 2);
                    var right = FindKth(nums1, 0, nums1.Length, nums2, 0, nums2.Length, len / 2+1);
                    return (left + right) * 1.0 / 2;
                }
            }

            public double FindMedianSingle(int[] nums)
            {
                if (nums.Length % 2 == 1)
                {
                    return nums[nums.Length / 2];
                }
                else
                {
                    var left = nums[nums.Length / 2 - 1];
                    var right = nums[nums.Length / 2];
                    return (left + right) * 1.0 / 2;
                }
            }
            public int FindLessThanCount(int[] nums, int lo, int hi, int target)
            {
                if (target <= nums[lo]) return 0;
                else if (target > nums[hi - 1]) return hi - lo;
                var olo = lo;
                var ohi = hi;
                var idx = -1;
                while (true)
                {
                    if (hi - lo < 5)
                    {
                        for (var i = lo; i < hi; i++)
                        {
                            if (nums[i] >= target)
                            {
                                idx = i;
                                break;
                            }
                        }
                        break;
                    }

                    var mid = lo + (hi - lo) / 2;
                    var midValue = nums[mid];
                    if (midValue >= target && nums[mid - 1] < target)
                    {
                        idx = mid;
                        break;
                    }
                    else if (midValue >= target)
                    {
                        hi = mid;
                    }
                    else lo = mid + 1;
                }
                return idx - olo;
            }

            public int FindKthSingle(int[] nums, int lo, int hi, int k)
            {
                return nums[lo + k-1];
            }
            public int FindKth(int[] nums1, int l1, int h1, int[] nums2, int l2, int h2, int k)
            {
                if (l1 >= h1)
                {
                    return FindKthSingle(nums2, l2, h2, k);
                }

                if (l2 >= h2)
                {
                    return FindKthSingle(nums1, l1, h2, k);
                }
                if (h1 - l1 + h2 - l2 < 10)
                {
                    var list = new List<int>();
                    for (var i = l1; i < h1; i++)
                    {
                        list.Add(nums1[i]);
                    }

                    for (var i = l2; i < h2; i++)
                    {
                        list.Add(nums2[i]);
                    }
                    list.Sort();
                    return list[k - 1];
                }

                if (h1 - l1 == 1)
                {
                    var idx = FindLessThanCount(nums2, l2, h2, nums1[l1]);
                    if (idx > k - 1)
                    {
                        return FindKthSingle(nums2, l2, h2, k);
                    }
                    else if(k-1==idx)
                    {
                        return nums1[l1];
                    }
                    else
                    {
                        return FindKthSingle(nums2, l2, h2, k-1);
                    }
                }

                if (h2 - l2 == 1)
                {
                    var idx = FindLessThanCount(nums1, l1, h1, nums2[l2]);
                    if (idx > k - 1)
                    {
                        return FindKthSingle(nums1, l1, h1, k);
                    }
                    else if(k-1==idx)
                    {
                        return nums2[l2];
                    }
                    else
                    {
                        return FindKthSingle(nums1, l1, h1, k-1);
                    }
                }

                if (h1 - l1 < h2 - l2)
                {
                    var temp = nums1;
                    nums1 = nums2;
                    nums2 = temp;
                    var t = l1;
                    l1 = l2;
                    l2 = t;
                    t = h1;
                    h1 = h2;
                    h2 = t;
                }
                var mid1 = l1 + (h1 - l1) / 2;
                var midValue1 = nums1[mid1]; 
                var lessThan = FindLessThanCount(nums2, l2, h2, midValue1);
                
                var left = mid1 - l1 + lessThan;
                    if (k <= left)
                    {
                        return FindKth(nums1, l1, mid1, nums2, l2, lessThan + l2, k);
                    }
                    else
                    {
                        return FindKth(nums1, mid1, h1, nums2, l2 + lessThan, h2, k - left);
                    }
                
                
                
            }
        }
    }

    namespace p10.V2
    {
        public class Solution {
            public double FindMedianSortedArrays(int[] nums1, int[] nums2)
            {
                return 1;
            }
            public int FindInsertion(int[] nums, int lo, int hi, int target)
            {
                if (target <= nums[lo]) return lo;
                else if (target > nums[hi - 1]) return hi;
                while (true)
                {
                    if (hi - lo < 5)
                    {
                        for (var i = lo; i < hi; i++)
                        {
                            if (nums[i] >= target)
                            {
                                return i;
                            }
                        }

                        return -1;
                    }

                    var mid = lo + (hi - lo) / 2;
                    var midValue = nums[mid];
                    if (midValue >= target && nums[mid - 1] < target)
                    {
                        return mid - lo;
                    }
                    else if (midValue >= target)
                    {
                        hi = mid;
                    }
                    else
                    {
                        lo = mid + 1;
                    }
                }
            }
        }
    }
}