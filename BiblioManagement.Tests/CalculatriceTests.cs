using Xunit;

public class CalculatriceTests
{
    private readonly ICalculatrice _calc = new Calculatrice();

    [Fact]
    public void Add_TwoNumbers_ReturnsSum() =>
        Assert.Equal(7, _calc.Add(3, 4));

    [Fact]
    public void Multiply_TwoNumbers_ReturnsProduct() =>
        Assert.Equal(12, _calc.Multiply(3, 4));

    [Fact]
    public void Divide_TwoNumbers_ReturnsQuotient() =>
        Assert.Equal(2, _calc.Divide(8, 4));

    [Fact]
    public void Divide_ByZero_Throws() =>
        Assert.Throws<DivideByZeroException>(() => _calc.Divide(5, 0));
}
