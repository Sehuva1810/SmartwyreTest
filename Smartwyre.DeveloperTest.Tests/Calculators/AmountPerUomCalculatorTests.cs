using Xunit;
using FluentAssertions;
using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Calculators;
using Smartwyre.DeveloperTest.Enums;

public class AmountPerUomCalculatorTests
{
    [Fact]
    public void Calculate_ShouldReturnCorrectAmount_WhenConditionsMet()
    {
        var calculator = new AmountPerUomCalculator();
        var request = new CalculateRebateRequest { Volume = 10 }; // Example request
        var rebate = new Rebate { Amount = 5, Incentive = IncentiveType.AmountPerUom };
        var product = new Product { Price = 500, SupportedIncentives = SupportedIncentiveType.AmountPerUom }; // Example product

        var result = calculator.Calculate(request, rebate, product);

        result.RebateAmount.Should().Be(50); // 10 * 5
        result.Success.Should().BeTrue();
    }
}