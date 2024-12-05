//--- Day 5: Print Queue ---
public class Day5 : IDay
{
    public void SolvePart1()
    {
        var file = File.ReadAllText(@"./day5/input1.txt");
        var ordering = file.Split("\r\n\r\n")[0].Split("\r\n");
        var sequences = file.Split("\r\n\r\n")[1].Split("\r\n");

        Dictionary<int, HashSet<int>> rules = [];

        foreach (var o in ordering)
        {
            var dat = o.Split("|").Select(int.Parse).ToArray();
            if (!rules.TryAdd(dat[0], new HashSet<int>() { dat[1] }))
            {
                rules[dat[0]].Add(dat[1]);
            }
        }

        // Console.WriteLine(string.Join(",",rules.Where(x=> x.Key == 47).SelectMany(x=> x.Value)));
        int totalvalid = 0;
        foreach (var s in sequences)
        {
            var seq = s.Split(",").Select(int.Parse).ToArray();
            bool valid = IsValidSequence(rules, seq);
            if (valid)
            {
                System.Console.WriteLine($"seq valid: {s} midpoint: {seq[(seq.Length / 2)]}");
                totalvalid += seq[(seq.Length / 2)];
            }
        }

        System.Console.WriteLine($"total valid: {totalvalid}");

    }

    public bool IsValidSequence(Dictionary<int, HashSet<int>> rules, int[] sequence)
    {
        HashSet<int> seen = [sequence[0]];

        for (int i = 1; i < sequence.Length; i++)
        {
            if (rules.ContainsKey(sequence[i]) && rules[sequence[i]].Intersect(seen).Count() > 0)
                return false;
            seen.Add(sequence[i]);
        }

        return true;
    }

    public void SolvePart2()
    {
        var file = File.ReadAllText(@"./day5/input1.txt");
        var ordering = file.Split("\r\n\r\n")[0].Split("\r\n");
        var sequences = file.Split("\r\n\r\n")[1].Split("\r\n");

        Dictionary<int, HashSet<int>> rules = [];

        foreach (var o in ordering)
        {
            var dat = o.Split("|").Select(int.Parse).ToArray();
            if (!rules.TryAdd(dat[0], new HashSet<int>() { dat[1] }))
            {
                rules[dat[0]].Add(dat[1]);
            }
        }

        int totalValid = 0;
        foreach (var s in sequences)
        {
            var seq = s.Split(",").Select(int.Parse).ToArray();
            if (!IsValidSequence(rules, seq))
            {
                PrintSequence(seq);
                var validsequence = FixBadSquence(rules, seq);
                Console.Write($"valid sequence: ");
                PrintSequence(validsequence);
                totalValid += validsequence[validsequence.Length / 2];
            }
        }

        System.Console.WriteLine($"total valid: {totalValid}");

    }

    public int[] FixBadSquence(Dictionary<int, HashSet<int>> rules, int[] sequence)
    {
        HashSet<int> seen = [sequence[0]];
        for (int i = 1; i < sequence.Length; i++)
        {
            if (rules.ContainsKey(sequence[i]) && rules[sequence[i]].Intersect(seen).Count() > 0)
            {
                // System.Console.WriteLine($"num incorrect: {sequence[i]} i {i}");
                seen.Remove(sequence[i-1]);
                seen.Add(sequence[i]);
                (sequence[i - 1], sequence[i]) = (sequence[i], sequence[i - 1]);
                i-=2;
            }
            else
            {
                seen.Add(sequence[i]);
            }

        }

        return sequence;
    }

    public void PrintSequence<T>(IEnumerable<T> s) => Console.WriteLine(string.Join(",", s));
}