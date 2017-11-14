using System;
using System.Linq;

namespace AlgoLib.Functions
{
    public static class EditDistance
    {
        public static int LevensteinDistance(string a, string b)
        {
            if (a.Length == 0)
                return b.Length;

            if (b.Length == 0)
                return a.Length;

            if (a.Length == b.Length)
                return HammingDistance(a, b);

            var matrix = new int[a.Length + 1][];

            for (var row = 1; row < matrix.Length; row++)
            {
                matrix[row] = new int[b.Length + 1];
                matrix[row][0] = row;
            }

            for (var col = 1; col < matrix[0].Length; col++)
            {
                matrix[0][col] = col;
            }

            for (var row = 1; row < matrix.Length; row++)
            {
                for (var col = 1; col < matrix[row].Length; col++)
                {
                    var subtitutions = a[row - 1] == b[col - 1] ? 0 : 1;

                    matrix[row][col] = Min
                    (
                        matrix[row - 1][col] + 1, //deletions
                        matrix[row][col - 1] + 1, //insetions
                        matrix[row - 1][col - 1] + subtitutions //substitution
                    );
                }
            }

            return matrix[a.Length][b.Length];
        }

        public static int HammingDistance(string a, string b)
        {
            if (a.Length != b.Length)
                throw new InvalidOperationException($"{nameof(a)} and {nameof(b)} must be of equal length.");

            return a.Zip(b, (cA, cB) => cA == cB ? 0 : 1).Sum();
        }

        private static int Min(params int[] numbers) => numbers.Min();
    }
}