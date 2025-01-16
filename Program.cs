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
    private static readonly ILine WarningLine = new WarningLine();
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
        


        var result = Merge(nums1, m, nums2, n);
        ResultLine.Render(result);
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
                WarningLine.Render("Сумма m и n должна быть в диапозоне от 1 до 200");
                continue;
            }
            WarningLine.Clear();
            
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
                WarningLine.Render(outOfRangeWarning);
                continue;
            }
            
            WarningLine.Clear();
            return numberOfArrayElements;
        }
    }

    private static double[] FillElementOfArray(string parameterName, int validCount, int zeroCount = 0)
    {
        var outOfRangeWarning = $"Элемент массива {parameterName} должен быть в диапозоне от -10\u2079 до 10\u2079";
        var nonDecreasingOrderWarning = $"Каждый следующий элемент массива {parameterName} должен быть больше предидущего";
        
        var array =  new double[validCount + zeroCount];
        
        for (var i = 0; i < validCount; i++)
        {
            Double elementOfArray;
            while (true)
            {
                elementOfArray = InputElements(parameterName, false);
                if (elementOfArray is < -1e9 or > 1e9)
                {
                    WarningLine.Render(outOfRangeWarning);
                    continue;
                }
                if (i > 0 && array[i - 1] > elementOfArray)
                {
                    WarningLine.Render(nonDecreasingOrderWarning);
                    continue;
                }
                break;
            }

            WarningLine.Clear();
            array[i] = elementOfArray;
            RenderArray(parameterName, array);
        }
        
        var finishedArray = RenderArray(parameterName, array);
        switch (parameterName)
        {
            case "nums1":
                Nums1Line.Render(finishedArray);
                break;
            case "nums2":
                Nums2Line.Render(finishedArray);
                break;
        }
        
        return array;
    }

    private static string RenderArray(string parameterName, double[] array)
    {
        var sb = new StringBuilder();
        sb.Append('[');

        foreach (var t in array)
        {
            sb.Append(t);
            sb.Append(',');
        }
        sb.Append(']');

        var arrayString = sb.ToString();

        WarningLine.Render(arrayString);

        return arrayString;
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
                WarningLine.Render(positivesNumbers
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
        
        WarningLine.Clear();
        PromptLine.Clear();
        
        var result = sb.ToString();
        sb.Clear();
        
        var number = int.Parse(result);
        return number;
    }

    private static string Merge(double[] nums1, int m, double[] nums2, int n)
    {
        var sb = new StringBuilder();
        
        var resultArray = new Double[m + n];
        for (var i = 0; i <= nums1.Length; i++)
        {
            for (var j = 0; j <= nums2.Length; j++)
            {
                if (nums1[i] < nums2[j])
                {
                    resultArray[i] = nums2[j];
                    continue;
                }
                
                resultArray[i] = nums1[i];
            }

            nums1 = resultArray;
        }
       
        sb.Append('[');

        for (var i = 0; i < nums1.Length; i++)
        {
            sb.Append($"{nums1[i]}");
            if (i == nums1.Length)
            {
                continue;
            }
            sb.Append(',');
        }

        sb.Append(']');

        var result = sb.ToString();
        sb.Clear();
        
        return result;
    }
}