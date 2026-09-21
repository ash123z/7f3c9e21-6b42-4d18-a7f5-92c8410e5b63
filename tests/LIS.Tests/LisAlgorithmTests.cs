namespace LIS.Tests;

using LIS.Core;
using Xunit;

public class LisAlgorithmTests
{
    // ============================================================================
    // Test Cases from Assessment
    // ============================================================================

    [Fact]
    public void FindLis_TestCase1_ReturnsCorrectSequence()
    {
        // Input: 6 1 5 9 2
        // Output: 1 5 9
        var input = "6 1 5 9 2";
        var expected = new[] { 1, 5, 9 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void FindLis_TestCase10_ReturnsLongestSequence()
    {
        // Input: 6 2 4 6 1 5 9 2
        // The longest increasing subsequence is [2, 4, 6, 9] with length 4
        var input = "6 2 4 6 1 5 9 2";
        var expected = new[] { 2, 4, 6, 9 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void FindLis_TestCase11_ReturnsLongestSequence()
    {
        // Input: 6 2 4 3 1 5 9
        // The longest increasing subsequence is [2, 4, 5, 9] with length 4
        var input = "6 2 4 3 1 5 9";
        var expected = new[] { 2, 4, 5, 9 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    // ============================================================================
    // Edge Cases
    // ============================================================================

    [Fact]
    public void FindLis_SingleElement_ReturnsThatElement()
    {
        var input = "42";
        var expected = new[] { 42 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void FindLis_AllDecreasing_ReturnsFirstElement()
    {
        var input = "9 5 3 1";
        var expected = new[] { 9 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void FindLis_AllIncreasing_ReturnsEntireSequence()
    {
        var input = "1 2 3 4 5";
        var expected = new[] { 1, 2, 3, 4, 5 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void FindLis_WithDuplicates_IgnoresDuplicates()
    {
        // Duplicates break strictly increasing requirement
        var input = "1 1 2 3";
        var expected = new[] { 1, 2, 3 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void FindLis_WithNegativeNumbers_WorksCorrectly()
    {
        var input = "-5 -2 0 3 5";
        var expected = new[] { -5, -2, 0, 3, 5 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void FindLis_MixedPositiveNegative_ReturnsCorrectLis()
    {
        var input = "5 -1 3 -2 4";
        var expected = new[] { -1, 3, 4 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void FindLis_EarliestLisWhenMultipleSameLength()
    {
        // When multiple LIS have the same length, return the one ending earliest
        var input = "1 3 2 4";
        // LIS of length 3: [1, 3, 4] (ends at index 3), [1, 2, 4] (ends at index 3)
        // Both end at index 3, so we return the first one found which is [1, 3, 4]
        var expected = new[] { 1, 3, 4 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    // ============================================================================
    // Error Cases
    // ============================================================================

    [Fact]
    public void FindLis_NullInput_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => LisAlgorithm.FindLis(null!));
    }

    [Fact]
    public void FindLis_EmptyInput_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => LisAlgorithm.FindLis(""));
    }

    [Fact]
    public void FindLis_WhitespaceOnlyInput_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => LisAlgorithm.FindLis("   "));
    }

    [Fact]
    public void FindLis_InvalidInteger_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => LisAlgorithm.FindLis("1 abc 3"));
    }

    [Fact]
    public void FindLis_ExtraWhitespace_ParsesCorrectly()
    {
        var input = "1   2   3";
        var expected = new[] { 1, 2, 3 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    // ============================================================================
    // Tie-Breaking Tests: Multiple LIS of Same Length
    // ============================================================================

    [Fact]
    public void FindLis_TieBreaking_Case1_SimpleTie()
    {
        // Input: [1, 3, 2, 4]
        // Two LIS of length 3:
        // - [1, 3, 4] ending at index 3
        // - [1, 2, 4] ending at index 3
        // Both end at same index, so we return the first one found: [1, 3, 4]
        var input = "1 3 2 4";
        var expected = new[] { 1, 3, 4 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void FindLis_TieBreaking_Case2_RepeatedPattern()
    {
        // Input: [1, 2, 3, 1, 2, 3]
        // Two LIS of length 3:
        // - [1, 2, 3] ending at index 2
        // - [1, 2, 3] ending at index 5
        // Should return the one ending at index 2 (earliest)
        var input = "1 2 3 1 2 3";
        var expected = new[] { 1, 2, 3 };  // From indices [0, 1, 2], not [3, 4, 5]

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void FindLis_TieBreaking_Case3_InterleavedPattern()
    {
        // Input: [1, 4, 2, 5, 3, 6]
        // Multiple LIS of length 3, but longest is 4:
        // - [1, 4, 5, 6] ending at index 5
        // Should return the longest, which is unique
        var input = "1 4 2 5 3 6";
        var expected = new[] { 1, 4, 5, 6 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void FindLis_TieBreaking_Case4_ComplexTie()
    {
        // Input: [2, 1, 3, 2, 4, 3, 5]
        // Multiple LIS of length 4, all ending at index 6:
        // - [1, 3, 4, 5]
        // - [1, 2, 4, 5]
        // - [2, 3, 4, 5]
        // Should return the first one found: [2, 3, 4, 5]
        var input = "2 1 3 2 4 3 5";
        var expected = new[] { 2, 3, 4, 5 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void FindLis_TieBreaking_Case5_EarliestEnding()
    {
        // Input: [1, 5, 2, 6, 3, 7]
        // Multiple LIS of length 3:
        // - [1, 5, 6] ending at index 3
        // - [1, 5, 7] ending at index 5
        // - [1, 2, 6] ending at index 3
        // - [1, 2, 7] ending at index 5
        // - [1, 3, 7] ending at index 5
        // - [2, 6, 7] ending at index 5
        // - [2, 3, 7] ending at index 5
        // But longest is [1, 5, 6, 7] with length 4 at index 5
        var input = "1 5 2 6 3 7";
        var expected = new[] { 1, 5, 6, 7 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void FindLis_TieBreaking_Case6_MultipleEarliestEndings()
    {
        // Input: [3, 1, 4, 2, 5]
        // Multiple LIS of length 3:
        // - [1, 4, 5] ending at index 4
        // - [1, 2, 5] ending at index 4
        // - [3, 4, 5] ending at index 4
        // All end at index 4, so we return the first one found
        var input = "3 1 4 2 5";
        var expected = new[] { 3, 4, 5 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void FindLis_TieBreaking_Case7_TwoEarliestCandidates()
    {
        // Input: [1, 3, 2, 4, 3, 5]
        // Multiple LIS of length 3:
        // - [1, 3, 4] ending at index 3
        // - [1, 3, 5] ending at index 5
        // - [1, 2, 4] ending at index 3
        // - [1, 2, 5] ending at index 5
        // - [1, 4, 5] ending at index 5
        // - [2, 4, 5] ending at index 5
        // - [3, 4, 5] ending at index 5
        // But longest is [1, 3, 4, 5] with length 4 at index 5
        var input = "1 3 2 4 3 5";
        var expected = new[] { 1, 3, 4, 5 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void FindLis_TieBreaking_Case8_IdenticalValues()
    {
        // Input: [5, 1, 2, 3, 1, 2, 3]
        // Two LIS of length 3:
        // - [1, 2, 3] ending at index 3
        // - [1, 2, 3] ending at index 6
        // Should return the one ending at index 3 (earliest)
        var input = "5 1 2 3 1 2 3";
        var expected = new[] { 1, 2, 3 };  // From indices [1, 2, 3], not [4, 5, 6]

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    // ============================================================================
    // Comprehensive Test Suite - All 17 Requirements
    // ============================================================================

    // Requirement 1: Exact provided Test Case 1
    [Fact]
    public void FindLis_Requirement1_ExactTestCase1()
    {
        // Requirement: The exact provided Test Case 1
        // Input: 6 1 5 9 2
        // Expected Output: 1 5 9
        var input = "6 1 5 9 2";
        var expected = new[] { 1, 5, 9 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    // Requirement 2: Exact provided Test Case 2
    [Fact]
    public void FindLis_Requirement2_ExactTestCase2()
    {
        // Requirement: The exact provided Test Case 2
        // Input: 6 2 4 6 1 5 9 2
        // Expected Output: 2 4 6 9 (longest increasing subsequence)
        var input = "6 2 4 6 1 5 9 2";
        var expected = new[] { 2, 4, 6, 9 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    // Requirement 3: Exact provided Test Case 3
    [Fact]
    public void FindLis_Requirement3_ExactTestCase3()
    {
        // Requirement: The exact provided Test Case 3
        // Input: 6 2 4 3 1 5 9
        // Expected Output: 2 4 5 9 (longest increasing subsequence)
        var input = "6 2 4 3 1 5 9";
        var expected = new[] { 2, 4, 5, 9 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    // Requirement 4: Simple increasing input
    [Fact]
    public void FindLis_Requirement4_SimpleIncreasingInput()
    {
        // Requirement: Simple increasing input
        // Input: 1 2 3 4 5
        // Expected: Entire sequence is LIS
        var input = "1 2 3 4 5";
        var expected = new[] { 1, 2, 3, 4, 5 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    // Requirement 5: Simple decreasing input
    [Fact]
    public void FindLis_Requirement5_SimpleDecreasingInput()
    {
        // Requirement: Simple decreasing input
        // Input: 5 4 3 2 1
        // Expected: First element only (no increasing subsequence possible)
        var input = "5 4 3 2 1";
        var expected = new[] { 5 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    // Requirement 6: Single element
    [Fact]
    public void FindLis_Requirement6_SingleElement()
    {
        // Requirement: Single element
        // Input: 42
        // Expected: That single element
        var input = "42";
        var expected = new[] { 42 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    // Requirement 7: Two elements increasing
    [Fact]
    public void FindLis_Requirement7_TwoElementsIncreasing()
    {
        // Requirement: Two elements increasing
        // Input: 1 2
        // Expected: Both elements
        var input = "1 2";
        var expected = new[] { 1, 2 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    // Requirement 8: Two elements decreasing
    [Fact]
    public void FindLis_Requirement8_TwoElementsDecreasing()
    {
        // Requirement: Two elements decreasing
        // Input: 2 1
        // Expected: First element only
        var input = "2 1";
        var expected = new[] { 2 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    // Requirement 9: Duplicate values
    [Fact]
    public void FindLis_Requirement9_DuplicateValues()
    {
        // Requirement: Duplicate values
        // Input: 1 2 2 3 3 3 4
        // Expected: Strictly increasing, so duplicates are skipped
        var input = "1 2 2 3 3 3 4";
        var expected = new[] { 1, 2, 3, 4 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    // Requirement 10: Negative numbers
    [Fact]
    public void FindLis_Requirement10_NegativeNumbers()
    {
        // Requirement: Negative numbers
        // Input: -5 -3 -1 0 2
        // Expected: All elements form increasing sequence
        var input = "-5 -3 -1 0 2";
        var expected = new[] { -5, -3, -1, 0, 2 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    // Requirement 11: Zero and negative values
    [Fact]
    public void FindLis_Requirement11_ZeroAndNegativeValues()
    {
        // Requirement: Zero and negative values
        // Input: -3 -1 0 1 3
        // Expected: All elements form increasing sequence
        var input = "-3 -1 0 1 3";
        var expected = new[] { -3, -1, 0, 1, 3 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    // Requirement 12: Mixed positive and negative values
    [Fact]
    public void FindLis_Requirement12_MixedPositiveNegative()
    {
        // Requirement: Mixed positive and negative values
        // Input: 5 -1 3 -2 4 0 6
        // Expected: Longest increasing subsequence
        var input = "5 -1 3 -2 4 0 6";
        var expected = new[] { -1, 3, 4, 6 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    // Requirement 13: Multiple longest increasing subsequences - earliest must be returned
    [Fact]
    public void FindLis_Requirement13_MultipleLisEarliestReturned()
    {
        // Requirement: Multiple LIS of same length, earliest one returned
        // Input: 1 2 3 1 2 3
        // Two LIS of length 3: [1,2,3] at indices [0,1,2] and [3,4,5]
        // Expected: The one ending earliest (at index 2)
        var input = "1 2 3 1 2 3";
        var expected = new[] { 1, 2, 3 };  // From indices [0, 1, 2]

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    // Requirement 14: Longest subsequence is not contiguous
    [Fact]
    public void FindLis_Requirement14_NonContiguousSubsequence()
    {
        // Requirement: Longest subsequence is not contiguous
        // Input: 10 1 5 20 2 9 30
        // Expected: [1, 5, 20, 30] - not contiguous in input
        var input = "10 1 5 20 2 9 30";
        var expected = new[] { 1, 5, 20, 30 };

        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected, result);
    }

    // Requirement 15: Large input
    [Fact]
    public void FindLis_Requirement15_LargeInput()
    {
        // Requirement: Large input
        // Input: 64 numbers from assessment test case 4
        var input = "923 11613 30483 19569 24201 13461 1189 30793 8848 16914 16053 21700 22116 3852 20909 5231 31469 3862 16353 22813 28735 4421 3618 32303 9932 31892 7823 22547 28888 11143 11695 3339 2094 11023 9661 27440 7186 24750 15427 24502 31606 23515 3563 29553 12145 22184 11409 28824 6636 10658 21404 5578 27807 14073 13967 31310 3132 4321 7643 1951 13289 24375 17912 11304";
        
        // The algorithm should find a valid LIS
        var result = LisAlgorithm.FindLis(input);

        // Verify it's a valid LIS:
        // 1. Result should not be empty
        Assert.NotEmpty(result);
        
        // 2. Result should be strictly increasing
        for (int i = 1; i < result.Length; i++)
        {
            Assert.True(result[i] > result[i - 1], $"Subsequence not strictly increasing at index {i}");
        }
        
        // 3. Result should be a subsequence of input (values appear in order)
        var inputNumbers = input.Split(' ').Select(int.Parse).ToList();
        int inputIndex = 0;
        foreach (var value in result)
        {
            while (inputIndex < inputNumbers.Count && inputNumbers[inputIndex] != value)
            {
                inputIndex++;
            }
            Assert.True(inputIndex < inputNumbers.Count, $"Value {value} not found in input");
            inputIndex++;
        }
    }

    // Requirement 16: Empty input handling (if supported by API)
    [Fact]
    public void FindLis_Requirement16_EmptyInputThrowsException()
    {
        // Requirement: Empty input handling
        // The API throws ArgumentException for empty input
        var input = "";

        Assert.Throws<ArgumentException>(() => LisAlgorithm.FindLis(input));
    }

    // Requirement 17: Whitespace/format validation
    [Fact]
    public void FindLis_Requirement17_WhitespaceFormatValidation()
    {
        // Requirement: Whitespace/format validation
        // Test 1: Extra whitespace between numbers
        var input1 = "1   2   3";
        var expected1 = new[] { 1, 2, 3 };
        var result1 = LisAlgorithm.FindLis(input1);
        Assert.Equal(expected1, result1);

        // Test 2: Whitespace-only input should throw
        var input2 = "   ";
        Assert.Throws<ArgumentException>(() => LisAlgorithm.FindLis(input2));

        // Test 3: Invalid format (non-integer) should throw
        var input3 = "1 abc 3";
        Assert.Throws<ArgumentException>(() => LisAlgorithm.FindLis(input3));

        // Test 4: Null input should throw
        Assert.Throws<ArgumentException>(() => LisAlgorithm.FindLis(null!));
    }

    // ============================================================================
    // Large Test Case Regression Tests - Assessment Data
    // ============================================================================

    [Fact]
    public void FindLis_LargeTestCase4_AssessmentData_ReturnsCorrectLis()
    {
        // Assessment Test Case 4: Large Input (64 integers)
        // This is a regression test using exact data from the assessment
        var input = LargeTestData.TestCase4.Input;
        var expected = LargeTestData.TestCase4.ExpectedOutput;

        var result = LisAlgorithm.FindLis(input);

        // Verify the result
        Assert.Equal(expected, result);
    }

    [Fact]
    public void FindLis_LargeTestCase4_VerifyInputSize()
    {
        // Verify the test data has the expected number of integers
        var input = LargeTestData.TestCase4.Input;
        var inputNumbers = input.Split(' ').Select(int.Parse).ToArray();

        Assert.Equal(LargeTestData.TestCase4.InputCount, inputNumbers.Length);
    }

    [Fact]
    public void FindLis_LargeTestCase4_VerifyOutputProperties()
    {
        // Verify the output is a valid LIS
        var input = LargeTestData.TestCase4.Input;
        var result = LisAlgorithm.FindLis(input);
        var inputNumbers = input.Split(' ').Select(int.Parse).ToList();

        // Property 1: Result is not empty
        Assert.NotEmpty(result);

        // Property 2: Result is strictly increasing
        for (int i = 1; i < result.Length; i++)
        {
            Assert.True(result[i] > result[i - 1], 
                $"Result not strictly increasing: {result[i - 1]} >= {result[i]} at index {i}");
        }

        // Property 3: Result is a valid subsequence of input
        // (all values appear in the input in the same order)
        int inputIndex = 0;
        foreach (var value in result)
        {
            while (inputIndex < inputNumbers.Count && inputNumbers[inputIndex] != value)
            {
                inputIndex++;
            }
            Assert.True(inputIndex < inputNumbers.Count, 
                $"Value {value} from result not found in input at or after index {inputIndex}");
            inputIndex++;
        }
    }

    [Fact]
    public void FindLis_LargeTestCase4_VerifyOutputLength()
    {
        // Verify the output has the expected length
        var input = LargeTestData.TestCase4.Input;
        var expected = LargeTestData.TestCase4.ExpectedOutput;
        var result = LisAlgorithm.FindLis(input);

        Assert.Equal(expected.Length, result.Length);
    }

    [Fact]
    public void FindLis_LargeTestCase4_VerifyOutputValues()
    {
        // Verify each value in the output matches expected
        var input = LargeTestData.TestCase4.Input;
        var expected = LargeTestData.TestCase4.ExpectedOutput;
        var result = LisAlgorithm.FindLis(input);

        for (int i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], result[i]);
        }
    }
}
