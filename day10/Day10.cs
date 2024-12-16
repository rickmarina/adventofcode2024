//--- Day 10: Hoof It ---

public class Day10 : IDay
{
    public void SolvePart1()
    {
        var map = File.ReadAllLines(@"./day10/input.txt").Select(x => x.ToCharArray()).ToArray();

        Helpers.ShowMap(map, " ");

        List<Location<int>> startPositions = [];
        for (int r =0; r< map.Length; r++) { 
            for (int c =0; c<map[0].Length; c++) {
                if (map[r][c] == '0')
                    startPositions.Add(new Location<int>(c, r));
            }
        }

        int result = 0;
        
        foreach (var p in startPositions) {

            int pathScore = GetScore(p.y, p.x, map);

            System.Console.WriteLine($"Location: {p.ToString()} score: {pathScore}");
            result += pathScore; 
        }

        System.Console.WriteLine($"result: {result}");
    }

    public int GetScore(int r, int c, char[][] map) {
        int score =0;

        var start = new Location<int>(c, r);
        
        HashSet<Location<int>> seen = [start];
        Queue<Location<int>> queue = new(); 
        queue.Enqueue(start); 

        while (queue.Count > 0) { 
            var current  = queue.Dequeue();
            
            if (map[current.y][current.x] == '9') 
                score++;

            foreach (var neigh in GetNeighbors(current, map)) {
                if (seen.Add(neigh))
                    queue.Enqueue(neigh);
            }
        }

        return score; 
    }

    public IEnumerable<Location<int>> GetNeighbors(Location<int> location, char[][] map) {

        List<(int, int)> directions = [(1,0), (0,1), (-1,0), (0,-1)];
        foreach (var d in directions) {
            var next = new Location<int>(location.x+d.Item1, location.y+d.Item2);

            if (Helpers.InBounds(map, next.y,next.x) && map[next.y][next.x] != '.' && Convert.ToInt32(map[location.y][location.x].ToString()) + 1 == Convert.ToInt32(map[next.y][next.x].ToString())) 
                yield return next;
        }
    }

    public void SolvePart2()
    {
        var map = File.ReadAllLines(@"./day10/input.txt").Select(x => x.ToCharArray()).ToArray();

        Helpers.ShowMap(map, " ");

        List<Location<int>> startPositions = [];
        for (int r =0; r< map.Length; r++) { 
            for (int c =0; c<map[0].Length; c++) {
                if (map[r][c] == '0')
                    startPositions.Add(new Location<int>(c, r));
            }
        }

        int total = 0; 

        foreach (var p in startPositions.Where(x=> GetScore(x.y, x.x, map) > 0)) {
            total += GetDistinctTrails(p.y,p.x, map);
        }


        System.Console.WriteLine($"total paths: { total}");
    }


    public int GetDistinctTrails(int r, int c, char[][] map) {
        int score =0;

        var start = new Location<int>(c, r);
        
        HashSet<Location<int>> seen = [start];
        Queue<Location<int>> queue = new(); 
        queue.Enqueue(start); 

        while (queue.Count > 0) { 
            var current  = queue.Dequeue();
            
            if (map[current.y][current.x] == '9') {
                score++;
                }

            foreach (var neigh in GetNeighbors(current, map)) {
                // if (seen.Add(neigh))
                    queue.Enqueue(neigh);
            }
        }

        return score; 
    }
}
