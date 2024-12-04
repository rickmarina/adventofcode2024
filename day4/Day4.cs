//--- Day 4: Ceres Search ---
public class Day4 : IDay
{
    char[][] map;
    int n =0;
    public void SolvePart1()
    {
        map = File.ReadAllLines(@"./day4/input1.txt").Select(x=> x.ToArray()).ToArray();
        n = map.GetLength(0);


        Helpers.ShowMap(map, " ");

        int total = 0; 
        for (int i=0; i< n; i++) {
            for (int j=0; j< n; j++) {
                if (map[i][j] == 'X') {
                    total += CheckPoint(new Location<int>(j,i));
                }
            }
        }

        System.Console.WriteLine($"total: {total}");
    }

    public bool InBounds(Location<int> p) => p.x >=0 && p.x < n && p.y >= 0 && p.y < n;

    public int CheckPoint(Location<int> p) { 
        List<(int,int)> directions = [(1,0), (-1,0), (0,1),(0,-1),(1,1), (1,-1), (-1,1), (-1,-1)];
        char[] XMAS = ['X', 'M', 'A', 'S'];

        HashSet<(int,int)> badDirections = []; 

        //Un offset por cada dirección
        for (int offset=1; offset<= 3; offset++) { 
            foreach (var dir in directions) {
                var np = new Location<int>(p.x+dir.Item1*offset, p.y+dir.Item2*offset);
                
                if (!InBounds(np) || XMAS[offset] != map[np.y][np.x]) {
                    badDirections.Add(dir);
                }
            }
        }

        return directions.Count - badDirections.Count; 
    }



    public void SolvePart2()
    {
         map = File.ReadAllLines(@"./day4/input1.txt").Select(x=> x.ToArray()).ToArray();
        n = map.GetLength(0);

        Helpers.ShowMap(map, " ");

        int total = 0; 
        for (int i=0; i< n; i++) {
            for (int j=0; j< n; j++) {
                if (map[i][j] == 'A') {
                    total += CheckPointA(new Location<int>(j,i));
                }
            }
        }

        System.Console.WriteLine($"total: {total}");
    }

    //1835
    public int CheckPointA(Location<int> p) { 
        
        //get 4 corners
        Location<int> q1 = new(p.x-1, p.y-1);
        Location<int> q2 = new(p.x-1, p.y+1);
        Location<int> q3 = new(p.x+1, p.y+1);
        Location<int> q4 = new(p.x+1, p.y-1);

        List<Location<int>> corners = [q1, q2, q3, q4];

        if (corners.All(x=> InBounds(x)) && corners.Count(p=> map[p.y][p.x] == 'M') == 2 && corners.Count(p=> map[p.y][p.x] == 'S') == 2 && map[q1.y][q1.x] != map[q3.y][q3.x]) {
            return 1;
        }

        return 0;
    }
}