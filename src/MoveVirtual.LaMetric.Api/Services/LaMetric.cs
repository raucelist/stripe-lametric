using MoveVirtual.LaMetric.Api.Contracts;
using Stripe;

namespace MoveVirtual.LaMetric.Api.Services;

public class LaMetric : ILaMetric
{
    public LaMetric(IConfiguration configuration)
    {
        IConfigurationSection stripe = configuration.GetSection("Stripe");
        StripeConfiguration.ApiKey = stripe["ApiKey"];
    }
    public async Task<long> GetStripeBalanceAsync()
    {
        BalanceService service = new();
        Balance balance = await service.GetAsync();
        long amount = 0;
        balance.Available.ForEach(x =>
        {
            amount += x.Amount / 100;
        });
        return amount;
    }

    //https://support.stripe.com/questions/understanding-monthly-recurring-revenue-(mrr)
    public async Task<long> GetStripeMonthlyRecurringRevenueAsync()
    {
        long amount = 0;
        SubscriptionService service = new();
        StripeList<Subscription> subscriptions = await service.ListAsync();
        
        subscriptions.Data.ForEach(s =>
        {
            if (s.Status == "active" || s.Status == "past_due")
            {
                foreach (SubscriptionItem item in s.Items)
                {
                    long unit = (item.Price.UnitAmount ?? 0) / 100;

                    switch (item.Price.Recurring.Interval)
                    {
                        case "year":
                            amount += unit / 12;
                            break;
                        case "month":
                            amount += unit;
                            break;
                        case "week":
                            amount += 4 * unit;
                            break;
                    }
                }
            }
        });

        return amount;
    }
}