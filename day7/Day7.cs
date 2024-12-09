//--- Day 7: Bridge Repair ---
public class Day7 : IDay
{
    public void SolvePart1()
    {
        var data = File.ReadAllLines(@"./day7/input.txt").Select(x => x.Split(":")).ToArray();


        int total = 0;
        long ans = 0;
        foreach (var d in data) { 
            long result = long.Parse(d[0]);
            int[] nums = d[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            System.Console.WriteLine($"{result} = {string.Join(",", nums)} ");
            bool eval = SolveSerie(result, nums, 1, nums[0]);
            if (eval) { total++; ans += result; }
            System.Console.WriteLine($"{result} = {string.Join(",", nums)} eval: {eval}");
        }

        System.Console.WriteLine($"TOTAL: {total} sum: {ans}"); //7710205485870
    }

    private bool SolveSerie(long result, int[] nums, int index, long total) { 
        System.Console.WriteLine($"SolveSerie. index: {index} total: {total}");
        if (index > nums.Length-1 || total > result)
            return false; 

        if (index == nums.Length-1)  {
            if (total + nums[index] == result || total * nums[index] == result)
                return true;
        }

        if (total < result) {
            return SolveSerie(result, nums, index+1, total + nums[index]) || SolveSerie(result, nums, index+1, total * nums[index]);
        }

        return false;
    }

    public void SolvePart2()
    {
        var data = File.ReadAllLines(@"./day7/input.txt").Select(x => x.Split(":")).ToArray();


        int total = 0;
        long ans = 0;
        foreach (var d in data) { 
            long result = long.Parse(d[0]);
            int[] nums = d[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            System.Console.WriteLine($"{result} = {string.Join(",", nums)} ");
            bool eval = SolveSerieConcat(result, nums, 1, nums[0]);
            if (eval) { 
                total++; 
                ans += result; 
                System.Console.WriteLine($"{result} = {string.Join(",", nums)} eval: {eval}");
            }
        }

        System.Console.WriteLine($"TOTAL: {total} sum: {ans}"); //7710205485870
    }

    private bool SolveSerieConcat(long result, int[] nums, int index, long total) { 
        // System.Console.WriteLine($"SolveSerie. total: {total} next index: {index}");
        if (index > nums.Length-1 || total > result)
            return false; 

        if (index == nums.Length-1)  {
            if (total + nums[index] == result || total * nums[index] == result || total.ToString()+nums[index].ToString() == result.ToString())
                return true;
        }

        if (total < result) {
            return SolveSerieConcat(result, nums, index+1, long.Parse(total.ToString()+nums[index].ToString())) || SolveSerieConcat(result, nums, index+1, total + nums[index]) || SolveSerieConcat(result, nums, index+1, total * nums[index]);
        }

        return false;
    }
}
