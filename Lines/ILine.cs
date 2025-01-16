namespace Leetcode;

public interface ILine
{
    int Left { get; }
    int Top { get; }
    
    public void Render(string message)
    {
        Helper.ClearLine(Left, Top);
        Console.SetCursorPosition(Left, Top);
        Console.Write(message);
    }

    public void Clear()
    {
        Helper.ClearLine(Left, Top);
    }
}