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
        var sb = new StringBuilder();
        
        PromptLine.Clear();
        PromptLine.Render(prompt);

        var numberOfCharactersEntered = 0;

        var regex = positivesNumbers ? PositiveNumbersRegex() : SignedNumbersRegex();

        while (true)
        {
            var inputCursorPositionLeft = promptLenght + numberOfCharactersEntered;
            var inputCursorPositionTop = PromptLine.Top;
            
            Console.SetCursorPosition(inputCursorPositionLeft, inputCursorPositionTop);
            
            var mCharKeyInfo = Console.ReadKey();
            var @char = Convert.ToChar(mCharKeyInfo.KeyChar);
            
            var newInput = sb.ToString() + @char;
            
            if (!regex.IsMatch(newInput) && mCharKeyInfo.Key != ConsoleKey.Enter)
            {
                AttentionLine.Render(positivesNumbers
                    ? $"{{{parameterName}}} может быть только цифрой"
                    : $"{{{parameterName}}} может быть только цифрой или минусом");

                Console.SetCursorPosition(inputCursorPositionLeft, inputCursorPositionTop);
                Console.Write(" ");
                
                continue;
            }
            
            if (mCharKeyInfo.Key == ConsoleKey.Enter)
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

            numberOfCharactersEntered++;
            sb.Append(@char);
        }
        
        AttentionLine.Clear();
        PromptLine.Clear();
        
        var result = sb.ToString();
        sb.Clear();
        
        var number = int.Parse(result);
        return number;
    }
    
    private static void Merge(double[] nums1, int m, double[] nums2, int n)
    {
        var nums2Counter = 0;

        for (var nums1Counter = 0; nums1Counter < nums1.Length; nums1Counter++)
        {
            if (nums2.Length == 0)
            {
                break;
            }
            
            if (nums1[nums1Counter] > nums2[nums2Counter] && nums2Counter < n)
            {
                for (var i = m - 1 + nums2Counter ; i >= nums1Counter ; i--)
                {
                    nums1[i + 1] = nums1[i];
                }

                nums1[nums1Counter] = nums2[nums2Counter];
                nums2Counter++;
            }
        }

        if (nums2.Length != 0 && nums1[^1] == 0 && nums2[^1] != 0)
        {
            nums1[^1] = nums2[^1];
        }

        ResultLine.Render("Итоговый массив: " + Helper.ConvertArrayToString(nums1));
    }

    private static void Merge1(double[] nums1, int m, double[] nums2, int n)
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