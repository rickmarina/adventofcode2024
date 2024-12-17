//--- Day 12: Garden Groups ---
public class Day12 : IDay
{
    public void SolvePart1()
    {
        var map = File.ReadAllLines(@"./day12/input.txt").Select(x => x.ToCharArray()).ToArray();

        Helpers.ShowMap(map, " ");

        HashSet<Location<int>> seen = [];
        List<List<Location<int>>> regions = [];

        for (int r = 0; r < map.Length; r++)
        {
            for (int c = 0; c < map[0].Length; c++)
            {
                if (!seen.Contains(new Location<int>(c, r)))
                    regions.Add(GetRegion(map, r, c, seen));

            }
        }

        long result = 0;

        regions.ForEach(r =>
        {
            // System.Console.WriteLine($"{string.Join("|", r)}");
            int perimeter = GetPerimeter(map, r);
            System.Console.WriteLine($"perimeter: {perimeter} area: {r.Count}");
            result += perimeter*r.Count;
        });

        //1488414
        System.Console.WriteLine($"result: {result}");

    }

    public int GetPerimeter(char[][] map, List<Location<int>> region) => region.Sum(x=> 4-Surrounded(map, region, x));

    public int Surrounded(char[][] map, List<Location<int>> region, Location<int> location) {
        return new[] {(1,0),(-1,0),(0,1),(0,-1)}.Select(x=> new Location<int>(x.Item1+location.x, x.Item2+location.y)).Count(x=> region.Any(p=> p.Equals(x)));
    }

    public List<Location<int>> GetRegion(char[][] map, int r, int c, HashSet<Location<int>> globalSeen)
    {

        Queue<Location<int>> queue = new();
        Location<int> start = new Location<int>(c, r);

        List<Location<int>> regionSeen = [start];
        globalSeen.Add(start);
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            foreach (var next in GetNeighbours(map, current.y, current.x))
            {

                var nextLocation = new Location<int>(next.Item1, next.Item2);
                if (Helpers.InBounds(map, nextLocation.y, nextLocation.x) && map[r][c] == map[nextLocation.y][nextLocation.x])
                {
                    if (globalSeen.Add(nextLocation))
                    {
                        regionSeen.Add(nextLocation);
                        queue.Enqueue(nextLocation);
                    }
                }
            }
        }
        return regionSeen;
    }

    public IEnumerable<(int, int)> GetNeighbours(char[][] map, int row, int col)
    {
        return new[] { (1, 0), (-1, 0), (0, 1), (0, -1) }.Select(d => (col + d.Item1, row + d.Item2));
    }


    public void SolvePart2()
    {
        //Eliminar los que no formen parte del perímetro 
        //Localizar el punto mas norte - oeste y empezar a recorrer el perímetro en el map sólo por los puntos restantes del procedimiento anterior
        //cuando se produzca un cambio de dirección incrementar los lados del perímetro en 1
        throw new NotImplementedException();
    }
}


