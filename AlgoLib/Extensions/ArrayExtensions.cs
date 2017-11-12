namespace AlgoLib.Extensions
{
    public static class ArrayExtensions
    {
        public static void Swap<T>(this T[] array, int x, int y)
        {
            var temp = array[x];
            array[x] = array[y];
            array[y] = temp;
        }
    }
}