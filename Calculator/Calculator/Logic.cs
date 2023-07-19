using Calculator.Resources;
using System.Text.RegularExpressions;

namespace Calculator;

public class Logic
{
    public string GetResultOfCalculate(string input, string parametr = CalculatorConstants.fileConstant)
    {
        if (string.IsNullOrEmpty(input) || (input.Any(x => char.IsLetter(x))) ||
            (parametr == "console" && input.Contains("(")))
        {
            return CalculateResources.WrongInput;
        }

        string pattern = CalculatorConstants.regexPattern;
        string[] tokens = Regex.Split(input, pattern);
        tokens = tokens.Where(val => val != "").ToArray();

        var operations = new Stack<string>();
        var results = new Stack<double>();

        foreach (string token in tokens)
        {
            if (double.TryParse(token, out double number))
            {
                results.Push(number);
            }
            else if (token == "(")
            {
                operations.Push(token);
            }
            else if (token == ")")
            {
                while (operations.Peek() != "(")
                {
                    PushResultsInStack(results, operations);
                }

                operations.Pop();
            }
            else
            {
                EvaluateToken(operations, token, results);
            }
        }

        try
        {
            while (operations.Any())
            {
                PushResultsInStack(results, operations);
            }

            if (double.IsInfinity(results.Peek()))
            {
                return CalculateResources.DivideByZero;
            }
        }
        catch (InvalidOperationException)
        {
            return CalculateResources.WrongInput;
        }

        return $"{results.Pop()}";
    }

    private void EvaluateToken(Stack<string> operations, string token, Stack<double> results)
    {
        try
        {
            if (operations.Any() && operations.Peek() != "(")
            {
                if (GetPriority(token) < GetPriority(operations.Peek()))
                {
                    PushResultsInStack(results, operations);
                    operations.Push(token);
                }
                else if (GetPriority(token) == GetPriority(operations.Peek()))
                {
                    PushResultsInStack(results, operations);
                }
                else
                {
                    operations.Push(token);
                }
            }
            else
            {
                operations.Push(token);
            }
        }
        catch (InvalidOperationException)
        {
            throw new InvalidOperationException("Invalid input");
        }
    }

    private void PushResultsInStack(Stack<double> results, Stack<string> operations)
    {
        if (results.Count < 2) return;
        var num2 = results.Pop();
        var num1 = results.Pop();
        var oper = operations.Pop();
        double result = 0;

        switch (oper)
        {
            case "*":
                result = num1 * num2;
                break;
            case "/":
                result = num1 / num2;
                break;
            case "+":
                result = num1 + num2;
                break;
            case "-":
                result = num1 - num2;
                break;
        }

        results.Push(result);
    }

    public int GetPriority(string oper)
    {
        var priorities = new Dictionary<string, int>
        {
            { "*", CalculatorConstants.priorityMultiDivide },
            { "/", CalculatorConstants.priorityMultiDivide },
            { "+", CalculatorConstants.priorityPlusMinus },
            { "-", CalculatorConstants.priorityPlusMinus }
        };

        if (priorities.ContainsKey(oper))
        {
            return priorities[oper];
        }

        return 0;
    }
}