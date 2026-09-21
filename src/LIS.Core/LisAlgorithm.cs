namespace LIS.Core;

/// <summary>
/// Finds the longest strictly increasing subsequence in a sequence of integers.
/// Uses dynamic programming with O(n²) time complexity.
/// </summary>
public static class LisAlgorithm
{
    /// <summary>
    /// Finds the longest strictly increasing subsequence from a space-separated string of integers.
    /// If multiple subsequences have the same maximum length, returns the earliest one
    /// (the one that ends at the earliest position in the input).
    /// </summary>
    /// <param name="input">Space-separated integers (e.g., "6 1 5 9 2")</param>
    /// <returns>Array of integers representing the longest increasing subsequence</returns>
    /// <exception cref="ArgumentException">Thrown if input is null, empty, or contains invalid integers</exception>
    public static int[] FindLis(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Input cannot be null or empty.", nameof(input));

        var numbers = ParseInput(input);

        if (numbers.Length == 0)
            throw new ArgumentException("Input must contain at least one integer.", nameof(input));

        return ComputeLis(numbers);
    }

    /// <summary>
    /// Parses space-separated integers from input string.
    /// </summary>
    private static int[] ParseInput(string input)
    {
        try
        {
            return input
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }
        catch (FormatException)
        {
            throw new ArgumentException("Input contains invalid integers.", nameof(input));
        }
    }

    /// <summary>
    /// Computes the longest increasing subsequence using dynamic programming.
    /// Returns the earliest LIS (ending at the earliest position) if multiple exist with same length.
    /// </summary>
    private static int[] ComputeLis(int[] numbers)
    {
        int n = numbers.Length;

        // dp[i] = length of LIS ending at index i
        var dp = new int[n];
        Array.Fill(dp, 1);

        // parent[i] = previous index in LIS ending at i (-1 if no previous)
        var parent = new int[n];
        Array.Fill(parent, -1);

        // Build DP table
        for (int i = 1; i < n; i++)
        {
            for (int j = 0; j < i; j++)
            {
                // If we can extend the LIS ending at j
                if (numbers[j] < numbers[i] && dp[j] + 1 > dp[i])
                {
                    dp[i] = dp[j] + 1;
                    parent[i] = j;
                }
            }
        }

        // Find the maximum LIS length
        int maxLength = dp.Max();

        // Find the FIRST (earliest) index with maximum LIS length
        // This ensures we return the earliest subsequence
        int maxIndex = Array.IndexOf(dp, maxLength);

        // Reconstruct the LIS by backtracking
        return ReconstructLis(numbers, parent, maxIndex);
    }

    /// <summary>
    /// Reconstructs the actual LIS by backtracking through the parent array.
    /// </summary>
    private static int[] ReconstructLis(int[] numbers, int[] parent, int endIndex)
    {
        var result = new List<int>();
        int current = endIndex;

        while (current != -1)
        {
            result.Add(numbers[current]);
            current = parent[current];
        }

        result.Reverse();
        return result.ToArray();
    }
}
