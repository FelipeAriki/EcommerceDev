using EcommerceDev.Core.Enums;
using EcommerceDev.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceDev.Infrastructure.BackgroundJobs;

public class CancelExpiredOrdersJob
{
    private readonly EcommerceDbContext _db;
    private readonly ILogger<CancelExpiredOrdersJob> _logger;

    public CancelExpiredOrdersJob(EcommerceDbContext db, ILogger<CancelExpiredOrdersJob> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task ExecuteAsync()
    {
        _logger.LogInformation("Starting batch cancellation of expired orders.");

        var expirationTime = DateTime.UtcNow.AddDays(-3);

        var expiredOrders = await _db
            .Orders
            .Where(o => o.Status == OrderStatus.PaymentPending &&
                        o.UpdatedAt < expirationTime)
            .ToListAsync();

        if (!expiredOrders.Any())
        {
            _logger.LogInformation("No expired orders found.");

            return;
        }

        _logger.LogInformation("Found {Count} Expired orders.", expiredOrders.Count);

        foreach (var order in expiredOrders)
        {
            order.MarkAsPaymentExpired();
        }

        await _db.SaveChangesAsync();

        _logger.LogInformation("Expired orders have been updated.");
    }
}
