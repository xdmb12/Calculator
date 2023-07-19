namespace Calculator;

public class MainProgram
{
    public static void Main(string[] args)
    {
        var calc = new UserInteractive();
        try
        {
            calc.CalculatorInteractive();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}