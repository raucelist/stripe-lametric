using Microsoft.AspNetCore.Mvc;
using MoveVirtual.LaMetric.Api.Contracts;
using MoveVirtual.LaMetric.Api.DTO;

namespace MoveVirtual.LaMetric.Api.Controllers;

[ApiController]
[Route("/")]
public class IndexController : ControllerBase
{
    private readonly ILaMetric _service;

    public IndexController(ILaMetric service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<Response> GetStripeMetricsAsync()
    {
        Response r = new();

        try
        {
            long balance = await _service.GetStripeBalanceAsync();
            long mrr = await _service.GetStripeMonthlyRecurringRevenueAsync();
            r.AddFrame(balance.ToString("N0"));
            r.AddFrame($"{mrr:N0} MRR");
        }
        catch (Exception e)
        {
            r.AddErrorFrame(e.Message);
        }

        return r;
    }

    [HttpGet("balance")]
    public async Task<Response> GetStripeBalanceAsync()
    {
        Response r = new();

        try
        {
            long balance = await _service.GetStripeBalanceAsync();
            r.AddFrame(balance.ToString("N2"));
        }
        catch (Exception e)
        {
            r.AddErrorFrame(e.Message);
        }

        return r;
    }

    [HttpGet("mrr")]
    public async Task<Response> GetStripeMonthlyRecurringRevenueAsync()
    {
        Response r = new();

        try
        {
            long mrr = await _service.GetStripeMonthlyRecurringRevenueAsync();
            r.AddFrame($"{mrr} MRR");
        }
        catch (Exception e)
        {
            r.AddErrorFrame(e.Message);
        }

        return r;
    }

}
