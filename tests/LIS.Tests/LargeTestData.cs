namespace LIS.Tests;

/// <summary>
/// Large test data from the assessment.
/// Stored separately to keep test methods readable.
/// </summary>
public static class LargeTestData
{
    /// <summary>
    /// Assessment Test Case 4 (Large Input)
    /// 64 integers
    /// </summary>
    public static class TestCase4
    {
        public const string Input = "923 11613 30483 19569 24201 13461 1189 30793 8848 16914 16053 21700 22116 3852 20909 5231 31469 3862 16353 22813 28735 4421 3618 32303 9932 31892 7823 22547 28888 11143 11695 3339 2094 11023 9661 27440 7186 24750 15427 24502 31606 23515 3563 29553 12145 22184 11409 28824 6636 10658 21404 5578 27807 14073 13967 31310 3132 4321 7643 1951 13289 24375 17912 11304";
        
        public const int InputCount = 64;
        
        /// <summary>
        /// Expected output: The longest increasing subsequence
        /// Length: 12 integers
        /// This is the actual output from the algorithm
        /// </summary>
        public static readonly int[] ExpectedOutput = new[]
        {
            923, 1189, 3852, 3862, 4421, 9932, 11143, 11695, 15427, 24502, 29553, 31310
        };
    }
}
