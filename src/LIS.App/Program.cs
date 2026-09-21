using LIS.Core;

if (args.Length == 0)
{
    Console.Error.WriteLine("Usage: LIS.App <space-separated integers>");
    Console.Error.WriteLine("Example: LIS.App \"6 1 5 9 2\"");
    Environment.Exit(1);
}

try
{
    var input = args[0];
    var result = LisAlgorithm.FindLis(input);
    Console.WriteLine(string.Join(" ", result));
}
catch (ArgumentException ex)
{
    Console.Error.WriteLine($"Error: {ex.Message}");
    Environment.Exit(1);
}
