using Smartwyre.DeveloperTest.Calculators;
using Smartwyre.DeveloperTest.Enums;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public class RebateDataStore: IRebateDataStore
{
    public Rebate GetRebate(string rebateIdentifier)
    {
        return rebateIdentifier switch
        {
            "Rebate1" => new Rebate { Incentive = IncentiveType.FixedCashAmount, Amount = 10},
            "Rebate2" => new Rebate { Incentive = IncentiveType.FixedRateRebate, Amount = 10, Percentage = 10},
            "Rebate3" => new Rebate { Incentive = IncentiveType.AmountPerUom, Amount = 10},
            _ => null // or some default rebate
        };
    }

    public void StoreCalculationResult(Rebate account, decimal rebateAmount)
    {
        // Update account in database, code removed for brevity
    }
}
