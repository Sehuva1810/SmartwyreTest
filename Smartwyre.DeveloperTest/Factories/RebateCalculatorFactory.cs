using System;
using Smartwyre.DeveloperTest.Calculators;
using Smartwyre.DeveloperTest.Enums;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Factories;

public class RebateCalculatorFactory: IRebateCalculatorFactory
{
    public IRebateCalculator GetCalculator(IncentiveType incentive)
    {
        switch (incentive)
        {
            case IncentiveType.FixedCashAmount:
                return new FixedCashedAmountCalculator();
            case IncentiveType.FixedRateRebate:
                return new FixedRateRebateCalculator();
            case IncentiveType.AmountPerUom:
                return new AmountPerUomCalculator();
            default:
                throw new InvalidOperationException("Unknown incentive type");
        }
    }
}