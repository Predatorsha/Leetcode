namespace Leetcode;

public static class Helper
{
    public static void ClearLine(int left, int top)
    {
        Console.SetCursorPosition(left, top);
        Console.Write(new string(' ', Console.WindowWidth));
    }
}