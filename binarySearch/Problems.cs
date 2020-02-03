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
}