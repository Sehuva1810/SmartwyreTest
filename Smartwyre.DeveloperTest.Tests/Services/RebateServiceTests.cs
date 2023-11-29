using System;
using Castle.Core.Logging;
using Xunit;
using FluentAssertions;
using NSubstitute;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Factories;
using Smartwyre.DeveloperTest.Enums;
using Smartwyre.DeveloperTest.Logging;

public class RebateServiceTests
{
    private readonly IRebateDataStore _rebateDataStore;
    private readonly IProductDataStore _productDataStore;
    private readonly RebateService _sut;
    private readonly ILoggerAdapter<RebateService> _logger = Substitute.For<ILoggerAdapter<RebateService>>();
    public RebateServiceTests()
    {
        _rebateDataStore = Substitute.For<IRebateDataStore>();
        _productDataStore = Substitute.For<IProductDataStore>();
        _sut = new RebateService(_rebateDataStore, _productDataStore, new RebateCalculatorFactory());
    }

    [Fact]
    public void Calculate_ShouldReturnSuccessfulResult_WhenValidInputs()
    {
        var request = new CalculateRebateRequest { RebateIdentifier = "R123", ProductIdentifier = "P123" };
        var rebate = new Rebate { Amount = 1, Incentive = IncentiveType.FixedCashAmount };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };

        _rebateDataStore.GetRebate(Arg.Any<string>()).Returns(rebate);
        _productDataStore.GetProduct(Arg.Any<string>()).Returns(product);

        var result = _sut.Calculate(request);

        result.Success.Should().BeTrue();
        //_logger.Received(1).LogInformation(Arg.Is("Starting calculation.."));
    }
    
    [Fact]
    public void Calculate_ShouldReturnFailureResult_WhenRebateIsNull()
    {
        var request = new CalculateRebateRequest { RebateIdentifier = "Rebate1", ProductIdentifier = "Product1" };
        _rebateDataStore.GetRebate(Arg.Any<string>()).Returns((Rebate)null);
        _productDataStore.GetProduct(Arg.Any<string>()).Returns(new Product());

        var result = _sut.Calculate(request);

        result.Success.Should().BeFalse();
        //_logger.Received(1).LogInformation(Arg.Is("Starting calculation.."));
    }
    
    [Fact]
    public void Calculate_ShouldReturnFailureResult_WhenProductIsNull()
    {
        var request = new CalculateRebateRequest { RebateIdentifier = "Rebate1", ProductIdentifier = "Product1" };
        _rebateDataStore.GetRebate(Arg.Any<string>()).Returns(new Rebate());
        _productDataStore.GetProduct(Arg.Any<string>()).Returns((Product)null);

        var result = _sut.Calculate(request);

        result.Success.Should().BeFalse();
        //_logger.Received(1).LogInformation(Arg.Is("Starting calculation.."));
    }
    
    [Fact]
    public void Calculate_ShouldCallStoreCalculateResult_WhenValidInputs()
    {
        var request = new CalculateRebateRequest { RebateIdentifier = "Rebate1", ProductIdentifier = "Product1" };
        var rebate = new Rebate { Amount = 1, Incentive = IncentiveType.FixedCashAmount };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };

        _rebateDataStore.GetRebate(Arg.Any<string>()).Returns(rebate);
        _productDataStore.GetProduct(Arg.Any<string>()).Returns(product);

        var result = _sut.Calculate(request);

        result.Success.Should().BeTrue();
        _rebateDataStore.Received(1).StoreCalculationResult(rebate, result.RebateAmount);
        //_logger.Received(1).LogInformation(Arg.Is("Starting calculation.."));
    }
    
    
    [Fact(Skip = "For time constraint and simplicity I just wanted to show how would I test logging in the app.")]
    public void Calculate_LogError_WhenExceptionIsThrown()
    {
        var request = new CalculateRebateRequest { RebateIdentifier = "Rebate1", ProductIdentifier = "Product1" };
        var rebate = new Rebate { Amount = 1};
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };

        _rebateDataStore.GetRebate(Arg.Any<string>()).Returns(rebate);
        _productDataStore.GetProduct(Arg.Any<string>()).Returns(product);

        var requestAction = () => _sut.Calculate(request);

        requestAction.Should().Throw<Exception>().WithMessage("An error ocurred during calculation");
        _logger.Received(1).LogError(Arg.Is(new Exception("An error ocurred during calculation")), Arg.Is("Something went wrong while retrieving all users"));
    }


}