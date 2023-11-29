using Smartwyre.DeveloperTest.Enums;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public class ProductDataStore: IProductDataStore
{
    public Product GetProduct(string productIdentifier)
    {
        return productIdentifier switch
        {
            "Product1" => new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount, Price = 10},
            "Product2" => new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 10},
            "Product3" => new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom, Price = 10},
            _ => null 
        };
    }
}
