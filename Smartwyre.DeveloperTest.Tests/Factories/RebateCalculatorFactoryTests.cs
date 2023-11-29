using System;
using Xunit;
using FluentAssertions;
using Smartwyre.DeveloperTest.Factories;
using Smartwyre.DeveloperTest.Calculators;
using Smartwyre.DeveloperTest.Enums;

public class RebateCalculatorFactoryTests
{
    private readonly RebateCalculatorFactory _sut = new();

    [Fact]
    public void GetCalculator_ShouldReturnFixedCashAmountCalculator_ForFixedCashAmountIncentive()
    {
        var calculator = _sut.GetCalculator(IncentiveType.FixedCashAmount);
        calculator.Should().BeOfType<FixedCashedAmountCalculator>();
    }

    [Fact]
    public void GetCalculator_ShouldReturnFixedRateRebateCalculator_ForFixedRateRebateIncentive()
    {
        var calculator = _sut.GetCalculator(IncentiveType.FixedRateRebate);
        calculator.Should().BeOfType<FixedRateRebateCalculator>();
    }

    [Fact]
    public void GetCalculator_ShouldReturnAmountPerUomCalculator_ForAmountPerUomIncentive()
    {
        var calculator = _sut.GetCalculator(IncentiveType.AmountPerUom);
        calculator.Should().BeOfType<AmountPerUomCalculator>();
    }
    
    [Fact]
    public void GetCalculator_ShouldThrowInvalidOperationException_ForInvalidIncetive()
    {
        var requestAction = () => _sut.GetCalculator((IncentiveType)(-1));
        requestAction.Should().Throw<InvalidOperationException>().WithMessage("Unknown incentive type");
    }
}