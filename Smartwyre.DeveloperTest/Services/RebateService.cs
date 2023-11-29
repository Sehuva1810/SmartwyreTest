using System;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Factories;
using Smartwyre.DeveloperTest.Logging;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IRebateDataStore _rebateDataStore;
    private readonly IProductDataStore _productDataStore;
    private readonly IRebateCalculatorFactory _calculatorFactory;
    // It should be set up like this for easy testing in a real app. For time and simplicity I omitted this.
    // private readonly ILoggerAdapter<RebateService> _logger;

    public RebateService(IRebateDataStore rebateDataStore, IProductDataStore productDataStore, IRebateCalculatorFactory calculatorFactory)
    {
        _rebateDataStore = rebateDataStore ?? throw new ArgumentNullException(nameof(rebateDataStore));
        _productDataStore = productDataStore ?? throw new ArgumentNullException(nameof(productDataStore));
        _calculatorFactory = calculatorFactory ?? throw new ArgumentNullException(nameof(calculatorFactory));
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        // _logger.LogInformation("Starting calculation..")
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "Request cannot be null.");
        }

        var rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
        if (rebate == null)
        {
            return new CalculateRebateResult { Success = false, Message = "Rebate not found." };
        }

        var product = _productDataStore.GetProduct(request.ProductIdentifier);
        if (product == null)
        {
            return new CalculateRebateResult { Success = false, Message = "Product not found." };
        }

        try
        {
            var calculator = _calculatorFactory.GetCalculator(rebate.Incentive);
            
            var result = calculator.Calculate(request, rebate, product);
            
            if (result.Success)
            {
                _rebateDataStore.StoreCalculationResult(rebate, result.RebateAmount);
            }

            return result;
        }
        catch (Exception ex)
        {
            // _logger.LogError(e, "An error ocurred during calculation");
            return new CalculateRebateResult { Success = false, Message = "An Error ocurred during calculation" };
        }
    }
}