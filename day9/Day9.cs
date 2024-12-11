using System.Diagnostics;
using System.Text;
//--- Day 9: Disk Fragmenter ---
public class Day9 : IDay
{
    public void SolvePart1()
    {
        string input = File.ReadAllText(@"./day9/input.txt");

        var disk = input.Select((x, i) => i % 2 == 0 ? Enumerable.Repeat((i / 2), int.Parse(x.ToString())) : Enumerable.Repeat(-1, int.Parse(x.ToString()))).SelectMany(x => x).ToArray();

        System.Console.WriteLine(string.Join(",", disk));

        int i = 0;
        int j = disk.Length - 1;
        long total = 0;

        while (i <= j)
        {
            if (disk[i] == -1)
            {
                if (disk[j] != -1)
                {
                    (disk[i], disk[j]) = (disk[j], disk[i]);
                    total += i * disk[i];
                    i++;
                    j--;
                }
                else
                    j--;
            }
            else
            {
                total += i * disk[i];
                i++;
            }
        }

        System.Console.WriteLine(string.Join(",", disk));
        System.Console.WriteLine($"total: {total}");
    }

    class Filedat {
        public int id;
        public int blocks {get;set;}
    }

    public void SolvePart2()
    {
        string input = File.ReadAllText(@"./day9/input.txt");

        var disk = input.Select((x, i) => i % 2 == 0 ? new Filedat() { id = (i/2), blocks = int.Parse(x.ToString()) }: new Filedat() {id = -1, blocks = int.Parse(x.ToString())}).ToList();

        System.Console.WriteLine("disk: "+string.Join(",", disk.Where(x=> x.blocks > 0).Select(x=> x.id)));


        //defrag process
        for (int j=disk.Count-1; j>=0;j--) {
            if (disk[j].id != -1) {
                int idx = disk.FindIndex(x=> x.id == -1 && x.blocks >= disk[j].blocks);
                if (idx < j && idx > -1) {
                    disk[idx].blocks -= disk[j].blocks;
                    disk.Insert(idx, new Filedat() { id = disk[j].id, blocks = disk[j].blocks});
                    disk[j+1].id = -1;
                }
            }
        }

        System.Console.WriteLine("disk: "+string.Join(",", disk.Where(x=> x.blocks > 0).Select(x=> x.id)));

        //checksum
        long pos = 0;
        long ans = 0;
        for (int i=0; i< disk.Count; i++) {
            for (int p=0; p< disk[i].blocks; p++) {
                if (disk[i].id > -1) {
                    ans += pos*disk[i].id;
                }
                pos++;
            }
            
        }

        System.Console.WriteLine($"total: {ans}");
    }
}
