//--- Day 11: Plutonian Pebbles ---
using Cache = System.Collections.Concurrent.ConcurrentDictionary<(string, int), long>;

public class Day11 : IDay
{
    public void SolvePart1()
    {
        var stones = "2 72 8949 0 981038 86311 246 7636740".Split(" ").Select(long.Parse);


        Stack<long> stack = new(stones);

        int iteration = 0;
        int totalIterations = 25;

        while (iteration < totalIterations)
        {
            Stack<long> tempStack = [];

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (current == 0)
                    tempStack.Push(1);
                else if (current.ToString().Length % 2 == 0)
                {
                    tempStack.Push(Convert.ToInt32(current.ToString().Substring(0, current.ToString().Length / 2)));
                    tempStack.Push(Convert.ToInt32(current.ToString().Substring(current.ToString().Length / 2)));
                }
                else
                {
                    tempStack.Push(2024 * current);
                }
            }

            stack = tempStack;

            System.Console.WriteLine($"stones: iteration[{iteration}] #stones: {stack.Count}"); //202019
            iteration++;
        }



    }

    public void SolvePart2()
    {
        string stones = "2 72 8949 0 981038 86311 246 7636740";

        Cache cache = [];

        long result = stones.Split(" ").Sum(n=> Eval(long.Parse(n), 75, cache));

        System.Console.WriteLine($"result: {result}");
        
    }

     long Eval(long n, int blinks, Cache cache) =>
        cache.GetOrAdd((n.ToString(), blinks), key => 
            key switch {
                (_, 0)   => 1,

                ("0", _) => 
                    Eval(1, blinks - 1, cache),
                
                (var st, _) when st.Length % 2 == 0 =>
                    Eval(long.Parse(st[0..(st.Length / 2)]), blinks - 1, cache) +
                    Eval(long.Parse(st[(st.Length / 2)..]),  blinks - 1, cache),

                _ =>  
                    Eval(2024 * n, blinks - 1, cache)   
            }
        );

}
