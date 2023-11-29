using System;
using Microsoft.Extensions.Logging;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Factories;
using Smartwyre.DeveloperTest.Enums;
using Smartwyre.DeveloperTest.Logging;

namespace Smartwyre.DeveloperTest.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var rebateDataStore = new RebateDataStore(); 
            var productDataStore = new ProductDataStore(); 
            var rebateCalculatorFactory = new RebateCalculatorFactory();
            
            var rebateService = new RebateService(rebateDataStore, productDataStore, rebateCalculatorFactory);
            
            Console.WriteLine("Enter product identifier:");
            var productId = Console.ReadLine();
        
            Console.WriteLine("Enter rebate identifier:");
            var rebateId = Console.ReadLine();
            
            var request = new CalculateRebateRequest
            {
                ProductIdentifier = productId,
                RebateIdentifier = rebateId,
                Volume = 5 // Fixed for simplicity
            };
            
            var result = rebateService.Calculate(request);
            
            if (result.Success)
            {
                Console.WriteLine($"Rebate calculation successful. Amount: {result.RebateAmount}");
            }
            else
            {
                Console.WriteLine("Rebate calculation failed.");
            }
        }
    }
}