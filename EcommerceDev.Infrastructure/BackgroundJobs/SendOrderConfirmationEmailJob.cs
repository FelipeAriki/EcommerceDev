using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EcommerceDev.Infrastructure.BackgroundJobs;

public class SendOrderConfirmationEmailJob
{
    private readonly ILogger<SendOrderConfirmationEmailJob> _logger;
    private readonly IConfiguration _configuration;

    public SendOrderConfirmationEmailJob(ILogger<SendOrderConfirmationEmailJob> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task ExecuteAsync(Guid orderId, string customerEmail)
    {
        _logger.LogInformation("Sending email for order {OrderId} to {CustomerEmail}.", orderId, customerEmail);

        await Task.Delay(3000);

        _logger.LogInformation("Order confirmation email sent successfully to {CustomerEmail}.", customerEmail);
    }
}
