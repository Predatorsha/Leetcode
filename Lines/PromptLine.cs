namespace Leetcode;

public class PromptLine : ILine
{
    public int Left => 0;
    public int Top => 2;

    public static HashSet<ConsoleKey> AllowedKeys => new HashSet<ConsoleKey>()
    {
        ConsoleKey.A,
        ConsoleKey.LeftArrow,
        ConsoleKey.D,
        ConsoleKey.RightArrow,
        ConsoleKey.Delete,
        ConsoleKey.Backspace,
        ConsoleKey.NumPad0,
        ConsoleKey.NumPad1,
        ConsoleKey.NumPad2,
        ConsoleKey.NumPad3,
        ConsoleKey.NumPad4,
        ConsoleKey.NumPad5,
        ConsoleKey.NumPad6,
        ConsoleKey.NumPad7,
        ConsoleKey.NumPad8,
        ConsoleKey.NumPad9,
        ConsoleKey.OemMinus,
        ConsoleKey.Enter,
    };

}