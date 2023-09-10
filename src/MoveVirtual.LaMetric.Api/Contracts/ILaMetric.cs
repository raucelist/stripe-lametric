namespace MoveVirtual.LaMetric.Api.Contracts;

public interface ILaMetric 
{
    Task<long> GetStripeBalanceAsync();
    Task<long> GetStripeMonthlyRecurringRevenueAsync();
}