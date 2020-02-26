#pragma once
#include <vector>
#include <algorithm>
#include <cmath>
using namespace std;
namespace array_0
{
class Solution
{
public:
	void merge(vector<int> &nums1, int m, vector<int> &nums2, int n)
	{
		int i = m - 1;
		int j = n - 1;
		int k = m + n - 1;
		while (k > -1)
		{
			if (j > -1 && (i < 0 || nums1[i] < nums2[j]))
			{
				nums1[k--] = nums2[j--];
			}
			else
			{
				nums1[k--] = nums1[i--];
			}
		}
	}
};
} // namespace array_0

namespace array_1
{
class Solution
{
public:
	int removeElement(vector<int> &nums, int val)
	{
		// sort(nums.begin(), nums.end());
		if (nums.size() == 0)
			return 0;
		int left = 0;
		int right = nums.size();
		while (left < right)
		{
			if (nums[left] != val)
			{
				left++;
			}
			else
			{
				int temp = nums[left];
				nums[left] = nums[right - 1];
				nums[right - 1] = temp;
				right--;
			}
		}
		return left;
	}
};
} // namespace array_1

namespace array_2
{
class Solution
{
public:
	int removeDuplicates(vector<int> &nums)
	{
		if (nums.size() < 2)
			return nums.size();
		int i = 0;
		int j = 0;
		while (true)
		{
			while (j < nums.size() && nums[i] == nums[j])
			{
				j++;
			}
			if (j < nums.size())
			{
				nums[i + 1] = nums[j];
				i++;
			}
			else
			{
				break;
			}
		}
		return i + 1;
	}
};
} // namespace array_2

namespace array_3
{
class Solution
{
public:
	int removeDuplicates(vector<int> &nums)
	{
		if (nums.size() < 2)
			return nums.size();
		int i = 0;
		int j = 0;
		int previous = 0;
		while (true)
		{
			while (j < nums.size() && nums[i] == nums[j])
			{
				j++;
			}
			int len = j - previous;
			if (len < 3)
			{
				for (int k = 0; k < len; k++)
				{
					nums[i + k] = nums[i];
				}
				i += len;
				previous = j;
			}
			else
			{
				len = 2;
				for (int k = 0; k < len; k++)
				{
					nums[i + k] = nums[i];
				}
				i += len;
				previous = j;
			}
			if (j < nums.size())
			{
				nums[i] = nums[j];
			}
			else
			{
				break;
			}
		}
		return i;
	}
};
} // namespace array_3

namespace array_4
{
class Solution
{
public:
	int threeSumClosest(vector<int> &nums, int target)
	{
		sort(nums.begin(), nums.end());
		int found = 0;
		int rs = 0;
		for (int i = 0; i < nums.size(); i++)
		{
			int val = nums[i];
			int left = 0;
			int right = nums.size() - 1;
			if (left == i)
				left++;
			if (right == i)
				right--;
			while (left < right)
			{

				int sum = nums[left] + nums[right] + val;
				if (found == 0 || abs(sum - target) < abs(rs - target))
				{
					found = 1;
					rs = sum;
				}
				if (sum < target)
				{
					left++;
				}
				else if (sum > target)
				{
					right--;
				}
				else
					return target;
				if (left == i)
					left++;
				if (right == i)
					right--;
			}
		}
		return rs;
	}
};
} // namespace array_4

namespace array_5
{
class Solution
{
public:
	vector<vector<int>> generateMatrix(int n)
	{
		if (n == 1)
		{
			vector<vector<int>> rs;
			vector<int> inner;
			inner.push_back(1);
			rs.push_back(inner);
			return rs;
		}
		vector<vector<int>> rs;
		for (int i = 0; i < n; i++)
		{
			vector<int> row;
			for (int j = 0; j < n; j++)
			{
				row.push_back(0);
			}
			rs.push_back(row);
		}
		int counter = 1;
		for (int i = 0; i < (n + 1) / 2; i++)
		{
			for (int col = i; col < n - i; col++)
			{
				rs[i][col] = counter++;
			}
			for (int row = i + 1; row < n - i; row++)
			{
				rs[row][n - 1 - i] = counter++;
			}
			if (n - 1 - i > i)
				for (int col = n - 2 - i; col >= i; col--)
				{
					rs[n - 1 - i][col] = counter++;
				}
			if (n - 1 - i > i)
				for (int row = n - 2 - i; row > i; row--)
				{
					rs[row][i] = counter++;
				}
		}
		return rs;
	}
};
} // namespace array_5

namespace array_6
{
class Solution
{
public:
	void rotate(vector<vector<int>> &matrix)
	{
		int n = matrix.size();
		for (int i = 0; i < (n + 1) / 2; i++)
		{
			for (int k = i; k < n - i - 1; k++)
			{
				int temp = matrix[i][k];
				matrix[i][k] = matrix[n - 1 - k][i];
				matrix[n - 1 - k][i] = matrix[n - 1 - i][n - 1 - k];
				matrix[n - 1 - i][n - 1 - k] = matrix[k][n - 1 - i];
				matrix[k][n - 1 - i] = temp;
			}
		}
	}
};
} // namespace array_6
