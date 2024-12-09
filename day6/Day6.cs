using System.Data;
using System.Numerics;

//--- Day 6: Guard Gallivant ---

public class Day6 : IDay
{
    char[][] map;
    Location<int> guardPos = new(0, 0);

    public void Common()
    {

        map = File.ReadAllLines(@"./day6/input1.txt").Select(x => x.ToCharArray()).ToArray();
        Helpers.ShowMap(map);
        for (int r = 0; r < map.Length; r++)
        {
            for (int c = 0; c < map.Length; c++)
            {
                if (map[r][c] == '^')
                    (guardPos.x, guardPos.y) = (c, r);
            }
        }
    }

    public HashSet<Location<int>> GetGuardPath(char[][] map, int startX, int startY)
    {
        Vector2 direction = new Vector2(0, -1);
        Location<int> currentPos = new Location<int>(startX, startY);
        HashSet<Location<int>> seen = [currentPos];
        bool exit = false;
        while (!exit)
        {
            int nx = currentPos.x + (int)direction.X;
            int ny = currentPos.y + (int)direction.Y;

            if (!Helpers.InBounds(map, ny, nx))
            {
                exit = true;
            }
            else
            {
                if (map[ny][nx] == '#')
                {
                    direction = direction.Rotate90CW();
                }

                currentPos.x += (int)direction.X;
                currentPos.y += (int)direction.Y;

                seen.Add(new Location<int>(currentPos.x, currentPos.y));
            }
        }
        return seen;
    }

    public void SolvePart1()
    {
        Common();

        var path = GetGuardPath(map, guardPos.x, guardPos.y);

        System.Console.WriteLine($"{path.Count}");

    }

    public void SolvePart2()
    {
        Common();
        var path = GetGuardPath(map, guardPos.x, guardPos.y);

        System.Console.WriteLine($"Path lenght: {path.Count}");

        HashSet<string> seen = [];

        int total = 0;
        foreach (var p in path)
        {
                if (IsInfiniteLoop(map, p.y, p.x, new Location<int>(guardPos.x, guardPos.y)))
                {
                    total++;
                }
        }

        System.Console.WriteLine($"{total}");

    }

    public bool IsInfiniteLoop(char[][] map, int tempY, int tempX, Location<int> initialGuardPos)
    {
        char aux = map[tempY][tempX];
        map[tempY][tempX] = '#';
        Vector2 direction = new Vector2(0, -1);
        HashSet<string> seen = [initialGuardPos.ToString() + "|" + direction.ToString()];

        bool loop = false;
        while (true)
        {
            int nx = initialGuardPos.x + (int)direction.X;
            int ny = initialGuardPos.y + (int)direction.Y;

            if (!Helpers.InBounds(map, ny, nx))
                break;

            if (map[ny][nx] == '#')
            {
                direction = direction.Rotate90CW();
            }
            else
            {
                initialGuardPos.x += (int)direction.X;
                initialGuardPos.y += (int)direction.Y;
            }

            if (!seen.Add(initialGuardPos.ToString() + "|" + direction.ToString()))
            {
                loop = true;
                break;
            }

        }

        // if (loop) 
        //      Helpers.ShowMap(map, " ");

        map[tempY][tempX] = aux;

        return loop;
        // map[tempY][tempX] = aux;
        // return countSeen == 2;
    }
}



public static class VectorExtension
{
    public static Vector2 Rotate90CW(this Vector2 v) => new Vector2(-v.Y, v.X);
}
