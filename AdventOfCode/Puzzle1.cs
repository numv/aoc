namespace AdventOfCode;

public class Puzzle1
{
    private readonly bool _withDebug;

    public static void Run(bool withDebug = false)
    {
        Puzzle1 p = new(withDebug);
        p.Execute();
    }

    private Puzzle1(bool withDebug)
    {
        _withDebug = withDebug;
    }

    private readonly Dictionary<int, string[]> enDigits = new()
    {
        {1,new[]{"1","one"}},
        {2,new[]{"2","two"}},
        {3,new[]{"3","three"}},
        {4,new[]{"4","four"}},
        {5,new[]{"5","five"}},
        {6,new[]{"6","six"}},
        {7,new[]{"7","seven"}},
        {8,new[]{"8","eight"}},
        {9,new[]{"9","nine"}},
    };
    
    private void Execute()
    {
        var inputFile = new FileInfo("../../../inputFile1.txt");
        if (!inputFile.Exists)
        {
            Console.WriteLine($"Input File not found! ({inputFile.FullName})");
            return;
        }

        using var read = inputFile.OpenText();
        int sum = 0;
        while (!read.EndOfStream)
        {
            var line = read.ReadLine();
            if(string.IsNullOrEmpty(line))continue;
            
            
            var span = line.AsSpan();
            var firstDigit = GetFirstDigit(span);
            var lastDigit = GetLastDigit(span);
            var num = $"{firstDigit?.Key}{lastDigit?.Key}";
            Write($"{line}: {num}");
            sum += int.Parse(num);
        }

        Write($"Calibration Result: {sum}", false);
    }

    private KeyValuePair<int, string[]>? GetLastDigit(ReadOnlySpan<char> span)
    {
        KeyValuePair<int, string[]>? lastDigit = null;
        for (var index = span.Length - 1; index >= 0; index--)
        {
            var c = span[index];
            foreach (var pair in enDigits.Where(pair => pair.Value.Any(n => n.EndsWith(c))))
            {
                if (c.ToString() == pair.Value[0])
                {
                    lastDigit = pair;
                    break;
                }

                var fullDigit = pair.Value[1];
                var start = index + 1 - fullDigit.Length;
                if (start < 0) continue;

                var digit = span[start..(index + 1)].ToString();
                if (digit == fullDigit)
                {
                    lastDigit = pair;
                    break;
                }
            }

            if (lastDigit != null)
            {
                break;
            }
        }

        return lastDigit;
    }
    private KeyValuePair<int, string[]>? GetFirstDigit(ReadOnlySpan<char> span)
    {
        KeyValuePair<int, string[]>? firstDigit = null;
        for (var index = 0; index < span.Length; index++)
        {
            var c = span[index];
            foreach (var pair in enDigits.Where(pair => pair.Value.Any(n => n.StartsWith(c))))
            {
                if (c.ToString() == pair.Value[0])
                {
                    firstDigit = pair;
                    break;
                }

                var fullDigit = pair.Value[1];
                var end = index + fullDigit.Length;
                if (end >= span.Length) continue;
                var digit = span[index..end].ToString();
                if (digit == fullDigit)
                {
                    firstDigit = pair;
                    break;
                }
            }

            if (firstDigit != null)
            {
                break;
            }
        }

        return firstDigit;
    }

    private void Write(string message, bool debug = true)
    {
        if(!debug || _withDebug) Console.WriteLine(message);
    }
}