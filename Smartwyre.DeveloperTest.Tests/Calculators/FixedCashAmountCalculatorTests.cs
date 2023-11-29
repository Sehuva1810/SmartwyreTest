using Xunit;
using FluentAssertions;
using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Calculators;
using Smartwyre.DeveloperTest.Enums;

public class FixedCashAmountCalculatorTests
{
    [Fact]
    public void Calculate_ShouldReturnCorrectAmount_WhenConditionsMet()
    {
        var calculator = new FixedCashedAmountCalculator();
        var request = new CalculateRebateRequest { Volume = 1 };
        var rebate = new Rebate { Amount = 100, Incentive = IncentiveType.FixedCashAmount };
        var product = new Product { Price = 500, SupportedIncentives = SupportedIncentiveType.FixedCashAmount}; // Example product

        var result = calculator.Calculate(request, rebate, product);

        result.RebateAmount.Should().Be(100);
        result.Success.Should().BeTrue();
    }
}