

using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;

public class Helpers
{


    #region "Math Utils"
    public static decimal GCD(decimal a, decimal b)
    {
        if (a == 0)
            return b;
        return GCD(b % a, a);
    }

    public static decimal LCM(decimal a, decimal b)
    {
        return a * b / GCD(a, b);
    }

    //TODO: Gauss Area Formula
    public static double GaussArea(List<Location<int>> v)
    {
        double suma = 0;

        for (int i = 0; i < v.Count; i++)
        {
            double x1 = v[i].x;
            double y1 = v[i].y;
            double x2 = v[(i + 1) % v.Count].x;
            double y2 = v[(i + 1) % v.Count].y;

            suma += (x1 * y2) - (x2 * y1);
        }

        double area = Math.Abs(suma) / 2.0;
        return area;
    }
    //TODO: Pick's theorem
    #endregion

    public static char[][] CopyMatrix(char[][] original)
    {
        int filas = original.Length;
        char[][] copia = new char[filas][];

        for (int i = 0; i < filas; i++)
        {
            copia[i] = (char[])original[i].Clone();
        }

        return copia;
    }

    /// <summary>
    /// Total different chars between two strings with same length
    /// </summary>
    /// <returns></returns>
    public static int TotalDiffChars(string a, string b) { 
       return Enumerable.Range(0,a.Length).Select(i => a[i] ^ b[i]).Count(x=> x>0);
    }

    public static void ShowMap(char[][] map, string separator = "") {
        for (int i=0; i< map.Length;i++) { 
            for (int j =0 ;j<map[0].Length;j++) { 
                System.Console.Write(map[i][j]+separator);
            }
            System.Console.WriteLine("");
        }
    }

    public static bool InBounds(char[][] map, int r, int c) {
        int height = map.Length;
        int width = map[0].Length;
        return (r >= 0 && r < height && c >= 0 && c < width);
    }

    public static void RotateMatrixClockwise(char[][] matrix)
    {
         int n = matrix.Length;

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                // change (i, j) with (j, i)
                char temp = matrix[i][j];
                matrix[i][j] = matrix[j][i];
                matrix[j][i] = temp;
            }
        }

        FlipMatrixHorizontal(matrix);
    }

    public static void FlipMatrixHorizontal(char[][] matrix)
    {
        for (int i = 0; i < matrix.Length; i++)
            Array.Reverse(matrix[i]);
    }

    public static string GetSHA256(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // Convertir la cadena de entrada en bytes
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            // Calcular el hash SHA-256
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            // Convertir el hash en una cadena hexadecimal
            StringBuilder hashStringBuilder = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                hashStringBuilder.Append(b.ToString("x2"));
            }

            return hashStringBuilder.ToString();
        }
    }

}
