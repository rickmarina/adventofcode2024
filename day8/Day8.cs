//--- Day 8: Resonant Collinearity ---
public class Day8 : IDay
{
    public void SolvePart1()
    {
        Dictionary<char, List<Location<int>>> locations = [];
        var map = File.ReadAllLines(@"./day8/input.txt").Select(x => x.ToCharArray()).ToArray();

        Helpers.ShowMap(map, " ");

        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[0].Length; j++)
            {
                if (map[i][j] != '.')
                {
                    var loc = new Location<int>(j, i);
                    if (!locations.TryAdd(map[i][j], [loc]))
                    {
                        locations[map[i][j]].Add(loc);
                    }
                }
            }
        }

        HashSet<Location<int>> antinodes = [];
        foreach (var loc in locations)
        {
            for (int i = 0; i < loc.Value.Count - 1; i++)
            {
                for (int j = i + 1; j < loc.Value.Count; j++)
                {
                    var offsetX = loc.Value[i].x - loc.Value[j].x;
                    var offsetY = loc.Value[i].y - loc.Value[j].y;

                    Location<int> antinodeI = new(loc.Value[i].x + offsetX, loc.Value[i].y + offsetY);
                    Location<int> antinodeJ = new(loc.Value[j].x + (-1 * offsetX), loc.Value[j].y + (-1 * offsetY));


                    if (Helpers.InBounds(map, antinodeI.y, antinodeI.x))
                    {
                        map[antinodeI.y][antinodeI.x] = '+';
                        antinodes.Add(antinodeI);
                    }
                    if (Helpers.InBounds(map, antinodeJ.y, antinodeJ.x))
                    {
                        map[antinodeJ.y][antinodeJ.x] = '+';
                        antinodes.Add(antinodeJ);
                    }

                    // System.Console.WriteLine($"Para {loc.Value[i]} tenemos antinode1: {antinodeI} y antinode2: {antinodeJ}");
                    // Console.ReadKey();
                    // Helpers.ShowMap(map, " ");

                }
            }
        }

        System.Console.WriteLine("------------------------------");

        Helpers.ShowMap(map, " ");


        System.Console.WriteLine($"total antinodes: {antinodes.Count}");

    }

    public void SolvePart2()
    {
        Dictionary<char, List<Location<int>>> locations = [];
        var map = File.ReadAllLines(@"./day8/input.txt").Select(x => x.ToCharArray()).ToArray();

        Helpers.ShowMap(map, " ");

        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[0].Length; j++)
            {
                if (map[i][j] != '.')
                {
                    var loc = new Location<int>(j, i);
                    if (!locations.TryAdd(map[i][j], [loc]))
                    {
                        locations[map[i][j]].Add(loc);
                    }
                }
            }
        }

        HashSet<string> antinodes = [];
        foreach (var loc in locations)
        {
            for (int i = 0; i < loc.Value.Count - 1; i++)
            {
                for (int j = i + 1; j < loc.Value.Count; j++)
                {
                    var offsetX = loc.Value[i].x - loc.Value[j].x;
                    var offsetY = loc.Value[i].y - loc.Value[j].y;

                    bool anyIn = true;
                    Location<int> antinodeI = new Location<int>(loc.Value[i].x, loc.Value[i].y);
                    Location<int> antinodeJ = new Location<int>(loc.Value[j].x, loc.Value[j].y);

                    antinodes.Add(antinodeI.ToString());
                    antinodes.Add(antinodeJ.ToString());

                    while (anyIn)
                    {
                        anyIn = false; 
                        (antinodeI.x, antinodeI.y) = (antinodeI.x + offsetX, antinodeI.y + offsetY);
                        (antinodeJ.x, antinodeJ.y) = (antinodeJ.x + (-1 * offsetX), antinodeJ.y + (-1 * offsetY));

                        if (Helpers.InBounds(map, antinodeI.y, antinodeI.x))
                        {
                            map[antinodeI.y][antinodeI.x] = '+';
                            antinodes.Add(antinodeI.ToString());
                            anyIn = true;
                        }
                        if (Helpers.InBounds(map, antinodeJ.y, antinodeJ.x))
                        {
                            map[antinodeJ.y][antinodeJ.x] = '+';
                            antinodes.Add(antinodeJ.ToString());
                            anyIn = true;
                        }
                    }
                    // System.Console.WriteLine($"Para {loc.Value[i]} tenemos antinode1: {antinodeI} y antinode2: {antinodeJ}");
                    // Console.ReadKey();
                    // Helpers.ShowMap(map, " ");

                }
            }
        }

        System.Console.WriteLine("------------------------------");

        Helpers.ShowMap(map, " ");


        System.Console.WriteLine($"total antinodes: {antinodes.Count}");
    }
}
