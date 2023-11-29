using Xunit;
using FluentAssertions;
using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Calculators;
using Smartwyre.DeveloperTest.Enums;

public class FixedRateRebateCalculatorTests
{
    [Fact]
    public void Calculate_ShouldReturnCorrectAmount_WhenConditionsMet()
    {
        var calculator = new FixedRateRebateCalculator();
        var request = new CalculateRebateRequest { Volume = 10 };
        var rebate = new Rebate { Percentage = 0.1m, Incentive = IncentiveType.FixedRateRebate };
        var product = new Product { Price = 500, SupportedIncentives = SupportedIncentiveType.FixedRateRebate};

        var result = calculator.Calculate(request, rebate, product);

        result.RebateAmount.Should().Be(500);
        result.Success.Should().BeTrue();
    }
    
}