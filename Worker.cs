namespace highmem_test;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly HttpClient _httpClient = new HttpClient();

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var tasks = new Task[5];
        for (var i = 0; i < tasks.Length; i++)
        {
            tasks[i] = MakeHttpRequest(stoppingToken);
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        await Task.WhenAll(tasks);
    }

    private async Task MakeHttpRequest(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            using var response = await _httpClient.GetAsync("https://api.github.com/users/dhhoang");
            //_logger.LogInformation("Response code {ResponseCode}", response.StatusCode);
            await Task.Delay(1000, cancellationToken);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
