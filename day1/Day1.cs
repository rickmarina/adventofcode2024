namespace adventofcode2024;

//--- Day 1: Historian Hysteria ---
public class Day1 : IDay
{
    public void SolvePart1()
    {
        var lines = File.ReadAllLines(@"./day1/input1.txt").ToList(); 

        List<int> list1 = []; 
        List<int> list2 = []; 

        lines.ForEach(x => {
            var parts = x.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            list1.Add(int.Parse(parts[0]));
            list2.Add(int.Parse(parts[1]));
        });

        list1 = list1.Order().ToList();
        list2 = list2.Order().ToList(); 

        long res = list1.Select((x,i)=> Math.Abs(list2[i] - x)).Sum();

        System.Console.WriteLine(res);
    }

    public void SolvePart2()
    {
        var lines = File.ReadAllLines(@"./day1/input1.txt").ToList(); 
        List<int> list1 = []; 
        Dictionary<int, int> count = []; 

        lines.ForEach(x => {
            var parts = x.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            list1.Add(int.Parse(parts[0]));

            if (!count.TryAdd(int.Parse(parts[1]), 1)) {
                count[int.Parse(parts[1])]++;
            }
        });

        long res = list1.Sum(x=> count.ContainsKey(x) ? x*count[x] : 0);

        System.Console.WriteLine(res);

    }
}