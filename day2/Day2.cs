//Day 2: Red-Nosed Reports

using System.Runtime.InteropServices;

public class Day2 : IDay
{
    public void SolvePart1()
    {
        var reports = File.ReadAllLines(@"./day2/input1.txt").Select(x => x.Split(" ").Select(int.Parse).ToList());

        int totalSafe = 0;

        foreach (var r in reports)
        {
            bool safe = SafeReport(r);
            if (safe)
            {
                totalSafe++;
                System.Console.WriteLine("safe");
            }
        }

        System.Console.WriteLine($"total safe: {totalSafe}");


    }

    public void SolvePart2()
    {
        var reports = File.ReadAllLines(@"./day2/input1.txt").Select(x => x.Split(" ").Select(int.Parse).ToList());

        int totalSafe = 0;

        foreach (var r in reports)
        {
            System.Console.WriteLine(string.Join(" ", r));
            bool safe = SafeReport(r);

            if (safe)
            {
                totalSafe++;
                System.Console.WriteLine("safe");
            } else {
                for (int i=0; i< r.Count; i++) { 
                    List<int> newlist = new(r);
                    newlist.RemoveAt(i);
                    if (SafeReport(newlist)) {
                        totalSafe++;
                        break;
                    }
                }
            }
        }

        System.Console.WriteLine($"total safe: {totalSafe}");
    }

    public static bool SafeReport(List<int> r)
    {
        bool safe = true;
        int sign = Math.Sign(r[0] - r[1]);
        for (int i = 0; i < r.Count - 1; i++)
        {
            int diff = r[i] - r[i + 1];
            if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3 || Math.Sign(diff) != sign)
            {
                safe = false;
                break;
            }
        }
        return safe;
    }
}