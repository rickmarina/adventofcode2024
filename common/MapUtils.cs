public static class MapUtils { 

    public static IEnumerable<(int, int)> GetNeighbours4(char[][] map, int r, int c) { 
        return new[] {(1,0), (-1,0), (0,1), (0,-1)}.Select(d=> (c+d.Item1, r+d.Item2));
    }

}