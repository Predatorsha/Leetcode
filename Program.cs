// See https://aka.ms/new-console-template for more information

using System.Text;
using System.Text.RegularExpressions;

namespace Leetcode;

public partial class Program
{
    [GeneratedRegex(@"^\d+\s*$")]
    private static partial Regex PositiveNumbersRegex();
    [GeneratedRegex(@"^-?\d+\s*$")]
    private static partial Regex NegativeNumbersRegex();
    [GeneratedRegex(@"^-?\d*$")]
    private static partial Regex SignedNumbersRegex();
    
    private static readonly ILine PromptLine = new PromptLine();
    private static readonly ILine AttentionLine = new AttentionLine();
    private static readonly ILine MParameterLine = new MParameterLine();
    private static readonly ILine NParameterLine = new NParameterLine();
    private static readonly ILine Nums1Line = new Nums1Line();
    private static readonly ILine Nums2Line = new Nums2Line();
    private static readonly ILine ResultLine = new ResultLine();

    private static void Main()
    {
        var(m, n) = FillNumberOfArrayElements();
        var nums1 = FillElementOfArray("nums1", m, n);
        var nums2 = FillElementOfArray("nums2", n);

        Merge(nums1, m, nums2, n);
        
        AttentionLine.Render("Нажмите Enter или Escape что бы выйти");
        while (true)
        {
            var key = Console.ReadKey(true);
            if (key.Key is ConsoleKey.Enter or ConsoleKey.Escape)
            {
                break;
            }
        }
    }

    private static (int m, int n) FillNumberOfArrayElements()
    {
        while (true)
        {
            const string mString = "m";
            var m = GetValidNumberOfArrayElementsInput(mString);
            
            const string nString = "n";
            var n = GetValidNumberOfArrayElementsInput(nString);
            
            var sum = m + n;
            if (sum is < 1 or > 200)
            {
                AttentionLine.Render("Сумма m и n должна быть в диапозоне от 1 до 200");
                continue;
            }
            AttentionLine.Clear();
            
            MParameterLine.Render($"{mString} = {m}");
            NParameterLine.Render($"{nString} = {n}");
            
            return (m, n);
        }
    }

    private static int GetValidNumberOfArrayElementsInput(string parameterName)
    {
        var outOfRangeWarning = $"{{{parameterName}}} должен быть в диапозоне от 0 до 200";
        
        while (true)
        {
            var numberOfArrayElements = (int)InputElements(parameterName, true);
            if (numberOfArrayElements is < 0 or > 200)
            {
                AttentionLine.Render(outOfRangeWarning);
                continue;
            }
            
            AttentionLine.Clear();
            return numberOfArrayElements;
        }
    }

    private static double[] FillElementOfArray(string parameterName, int validCount, int zeroCount = 0)
    {
        var outOfRangeWarning = $"Элемент массива {parameterName} должен быть в диапозоне от -10\u2079 до 10\u2079";
        
        var array =  new double[validCount + zeroCount];

        for (var i = 0; i < validCount; i++)
        {
            Double elementOfArray;
            while (true)
            {
                elementOfArray = InputElements(parameterName, false);
                if (elementOfArray is < -1e9 or > 1e9)
                {
                    AttentionLine.Render(outOfRangeWarning);
                    continue;
                }
                if (i > 0 && array[i - 1] > elementOfArray)
                {
                    AttentionLine.Render($"Следующий элемент массива должен быть больше {array[i - 1]}");
                    continue;
                }
                break;
            }

            AttentionLine.Clear();
            array[i] = elementOfArray;
            
            AttentionLine.Render(Helper.ConvertArrayToString(array));
        }
        
        var finishedArray = Helper.ConvertArrayToString(array);
        switch (parameterName)
        {
            case "nums1":
                Nums1Line.Render("nums1 = " + finishedArray);
                break;
            case "nums2":
                Nums2Line.Render("nums2 = " + finishedArray);
                break;
        }
        
        return array;
    }

    private static double InputElements(string parameterName, bool positivesNumbers)
    {
        var prompt = $"Введите {parameterName} = ";
        var promptLenght = prompt.Length;
        
        var charsEnteredCount  = 0;
        var inputNumbersMaxLength = 12;
        
        Cursor.SetPosition(promptLenght, PromptLine.Top);
        
        var sb = new StringBuilder();
        
        PromptLine.Clear();
        PromptLine.Render(prompt);

        var regex = positivesNumbers ? PositiveNumbersRegex() : SignedNumbersRegex();

        while (true)
        {
            Cursor.SetPosition(Cursor.Left, Cursor.Top);

            var mCharKeyInfo = Console.ReadKey(true);

            if (PromptLine.)
            {
                
            }

            if (mCharKeyInfo.Key is ConsoleKey.A or ConsoleKey.LeftArrow or ConsoleKey.D or ConsoleKey.RightArrow or ConsoleKey.Delete or ConsoleKey.Backspace)
            {
                if (mCharKeyInfo.Key is ConsoleKey.A or ConsoleKey.LeftArrow && promptLenght >= Cursor.Left || 
                    mCharKeyInfo.Key is ConsoleKey.D or ConsoleKey.RightArrow && Cursor.Left >= promptLenght + inputNumbersMaxLength)
                {
                    continue;
                }
                
                switch (mCharKeyInfo.Key)
                {
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        Cursor.MoveLeft(Cursor.Left);
                        continue;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        Cursor.MoveRight(Cursor.Left);
                        continue;
                    case ConsoleKey.Delete:
                        Cursor.Delete(Cursor.Left);
                        continue;
                    case ConsoleKey.Backspace:
                        Cursor.Backspace(Cursor.Left);
                        continue;
                }
            }
            
            var @char = Convert.ToChar(mCharKeyInfo.KeyChar);
            var newInput = sb.ToString() + @char;
            
            Console.Write(@char);

            if (!regex.IsMatch(newInput) && mCharKeyInfo.Key != ConsoleKey.Enter)
            {
                AttentionLine.Render(positivesNumbers
                    ? $"{{{parameterName}}} может быть только цифрой"
                    : $"{{{parameterName}}} может быть только цифрой или минусом");

                Cursor.Backspace(Cursor.Left, Cursor.Top);
                
                continue;
            }
            
            if (mCharKeyInfo.Key == ConsoleKey.Enter || newInput.Length == inputNumbersMaxLength)
            {
                if (NegativeNumbersRegex().IsMatch(newInput) && !positivesNumbers)
                {
                    break;
                }
                if (PositiveNumbersRegex().IsMatch(newInput) && positivesNumbers)
                {
                    break;
                }
                continue;
            }

            Cursor.SetPosition(Cursor.Left + 1, Cursor.Top);
            charsEnteredCount++;

            sb.Append(@char);
        }
        
        AttentionLine.Clear();
        PromptLine.Clear();
        
        var result = sb.ToString();
        sb.Clear();
        
        var number = double.Parse(result);
        return number;
    }

    private static void Merge(double[] nums1, int m, double[] nums2, int n)
    {
        var lastIndexNums1 = m - 1;
        var lastIndexNums2 = n - 1;
        var mergePosition = m + n - 1;

        while (lastIndexNums2 >= 0)
        {
            if (lastIndexNums1 >= 0 && nums1[lastIndexNums1] > nums2[lastIndexNums2])
            {
                nums1[mergePosition] = nums1[lastIndexNums1];
                lastIndexNums1--;
            }
            else
            {
                nums1[mergePosition] = nums2[lastIndexNums2];
                lastIndexNums2--;
            }
            mergePosition--;
        }

        ResultLine.Render("Итоговый массив: " + Helper.ConvertArrayToString(nums1));
    }
}