using System.Text;

namespace Leetcode;

public static class Helper
{
    public static void ClearLine(int left, int top)
    {
        Console.SetCursorPosition(left, top);
        Console.Write(new string(' ', Console.WindowWidth));
    }
    
    public static string ConvertArrayToString(double[] array)
    {
        var sb = new StringBuilder();
        sb.Append('[');

        for (var i = 0; i < array.Length; i++)
        {
            sb.Append(array[i]);
            
            if (i != array.Length - 1)
                sb.Append(',');
        }
        sb.Append(']');
        
        return sb.ToString();
    }
}