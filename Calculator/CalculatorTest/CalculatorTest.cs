using Calculator;

namespace CalculatorTest;

public class UnitTest1
{
    private Logic lg = new Logic();

    [Theory]
    [InlineData("2+2*3", "console", "8")]
    [InlineData("2/0", "console", "Exception. Divide by zero.")]
    [InlineData("1+2*(3+2)", "file", "11")]
    [InlineData("1+x+4", "console", "Exception. Wrong input.")]
    [InlineData("2+15/3+4*2", "file", "15")]
    [InlineData("(2+2*3)", "file", "8")]
    [InlineData("(2+2)*3", "console", "Exception. Wrong input.")]
    [InlineData("", "file", "Exception. Wrong input.")]
    [InlineData("1+2*(1+4/2+3)", "file", "13")]
    [InlineData(null, "console", "Exception. Wrong input.")]
    public void InputExpressionGetResult(string exp, string parametr, string expectedResult)
    {
        var result = lg.GetResultOfCalculate(exp, parametr);
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData("+", 1)]
    [InlineData("-", 1)]
    [InlineData("*", 2)]
    [InlineData("/", 2)]
    [InlineData("", 0)]
    [InlineData("^", 0)]
    public void InputOperationGetPriority(string oper, int expectedResult)
    {
        var result = lg.GetPriority(oper);
        Assert.Equal(expectedResult, result);
    }
}