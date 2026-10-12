using System.ComponentModel.DataAnnotations;
using AcxiomCRM.Models;
using Xunit;

namespace AcxiomCRM.Tests;
public class ModelValidationTests
{
    private static IList<ValidationResult> Validate(object model)
    {
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(model, new ValidationContext(model), results, validateAllProperties: true);
        return results;
    }

    [Fact]
    public void Customer_RejectsInvalidEmail()
    {
        var customer = new Customer { CustomerCode = "CUS-001", CustomerName = "Example", Email = "not-an-email", Phone = "9876543210" };
        Assert.Contains(Validate(customer), r => r.MemberNames.Contains(nameof(Customer.Email)));
    }

    [Fact]
    public void Customer_RequiresName()
    {
        var customer = new Customer { CustomerCode = "CUS-001", Email = "test@example.com", Phone = "9876543210" };
        Assert.Contains(Validate(customer), r => r.MemberNames.Contains(nameof(Customer.CustomerName)));
    }

    [Fact]
    public void Opportunity_RejectsZeroAmount()
    {
        var opportunity = new Opportunity { OpportunityName = "Demo deal", Amount = 0, Probability = 50, ExpectedCloseDate = DateTime.Today.AddDays(10) };
        Assert.Contains(Validate(opportunity), r => r.MemberNames.Contains(nameof(Opportunity.Amount)));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void Opportunity_RejectsProbabilityOutsideRange(int probability)
    {
        var opportunity = new Opportunity { OpportunityName = "Demo deal", Amount = 100, Probability = probability, ExpectedCloseDate = DateTime.Today.AddDays(10) };
        Assert.Contains(Validate(opportunity), r => r.MemberNames.Contains(nameof(Opportunity.Probability)));
    }
}
