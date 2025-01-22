namespace Leetcode;

public static class Cursor
{
    public static int Left { get; private set; }
    public static int Top { get; private set; }

    public static void SetPosition(int left, int top)
    {
        Left = left;
        Top = top;
        Console.SetCursorPosition(left, top);
    }

    public static void MoveRight(int? left = null)
    {
        var leftPosition  = (left ?? Left) + 1;
        Left = leftPosition;
        
        Console.SetCursorPosition(leftPosition, Console.CursorTop);
    }

    public static void MoveLeft(int? left = null)
    {
        var leftPosition  = (left ?? Left) - 1;
        Left = leftPosition;
        
        Console.SetCursorPosition(leftPosition, Console.CursorTop);
    }

    public static void Backspace(int? left = null, int? top = null)
    {
        var leftPosition  = (left ?? Left) - 1;
        Left = leftPosition;
        
        var topPosition  = top ?? Top;
        Top  = topPosition;
        
        Console.SetCursorPosition(leftPosition , topPosition);
        Console.Write(" ");
    }
    
    public static void Delete(int? left = null, int? top = null)
    {
        var leftPosition  = (left ?? Left) + 1;
        Left = leftPosition;
        
        var topPosition  = top ?? Top;
        Top  = topPosition;
        
        Console.Write(" ");
    }
}