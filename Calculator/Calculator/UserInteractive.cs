using Calculator.Resources;

namespace Calculator;

public class UserInteractive
{
    public void CalculatorInteractive()
    {
        var calc = new Logic();
        var file = new FileInteractive();

        Console.Write(CalculateResources.FileOrConsole);
        var input = Console.ReadLine();

        switch (input)
        {
            case "file":
                Console.Write(CalculateResources.EnterPathOfFile);
                var userPathFile = Console.ReadLine();
                Console.Write(CalculateResources.EnterNewPathOfFile);
                var userNewPathFile = Console.ReadLine();
                Console.WriteLine(file.GetFileWithResults(userNewPathFile, file.FileRider(userPathFile)));
                break;
            case "console":
                Console.Write(CalculateResources.EnterExpression);
                var userExpression = Console.ReadLine();
                Console.WriteLine($"{calc.GetResultOfCalculate(userExpression, CalculatorConstants.consoleConstant)}");
                break;
            default:
                Console.WriteLine(CalculateResources.WrongInput);
                break;
        }
    }
}