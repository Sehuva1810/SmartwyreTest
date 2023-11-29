using Smartwyre.DeveloperTest.Calculators;
using Smartwyre.DeveloperTest.Enums;

namespace Smartwyre.DeveloperTest.Factories;

public interface IRebateCalculatorFactory
{
   IRebateCalculator GetCalculator(IncentiveType incentive);
}