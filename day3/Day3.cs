using System.Text.RegularExpressions;


//--- Day 3: Mull It Over ---
public class Day3 : IDay
{
    public void SolvePart1()
    {
        var lines = File.ReadAllLines(@"./day3/input1.txt");

        Regex r = new Regex(@"mul\((\d*,\d*)\)");

        long result = 0;
        foreach (var l in lines) {
            var matches = r.Matches(l);
            foreach (Match m in matches) {
                Console.WriteLine(m.Groups[1].Value);
                (int x, int y) = (int.Parse(m.Groups[1].Value.Split(",")[0]),int.Parse(m.Groups[1].Value.Split(",")[1]));
                result += x*y;
            }
        }

        System.Console.WriteLine(result);
    }

    public void SolvePart2()
    {
        var lines = File.ReadAllLines(@"./day3/input1.txt");

        Regex r = new Regex(@"don't\(\)|do\(\)|mul\((\d*,\d*)\)");

        bool enabled = true; 
        long result = 0;
        foreach (var l in lines) {
            var matches = r.Matches(l);
            foreach (Match m in matches) {
                string match = m.Groups[0].Value;
                if (match.Equals(@"don't()")) {
                    enabled = false; 
                } 
                else if (match.Equals(@"do()")) {
                    enabled = true; 
                } else if (enabled) { 
                    Console.WriteLine(m.Groups[1].Value);
                    (int x, int y) = (int.Parse(m.Groups[1].Value.Split(",")[0]),int.Parse(m.Groups[1].Value.Split(",")[1]));
                    result += x*y;
                }
            }
        }

        System.Console.WriteLine(result);
    }
}