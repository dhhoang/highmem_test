using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace highmem_test
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private static readonly string[] _queries = new string[]
        {
            "DNA", "RNA", "protein", "lipid"
        };
        private readonly Random _random = new Random();
        private readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://api.plos.org")
        };

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try
                {
                    await DoRequestAsync(stoppingToken);

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    await Task.Delay(2000, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }
        }

        private async Task DoRequestAsync(CancellationToken stopToken)
        {
            var data = await QueryAsync(stopToken);
            var docs = data.Response.Docs.OrderBy(d => d.Score).ToList();

            if (docs.Count == 0)
            {
                _logger.LogInformation("Queried 0 item");
            }

            _logger.LogInformation("Queried {Count} items, max score {MaxScore}, min score {MinScore}",
                docs.Count, docs.Last().Score, docs.First().Score);
        }

        private async Task<ArticleResponse> QueryAsync(CancellationToken stopToken)
        {
            var query = _queries[_random.Next(_queries.Length)];
            using var request = new HttpRequestMessage(HttpMethod.Get, $"/search?q=title:{query}");

            using var response = await _httpClient.SendAsync(request, stopToken);
            var data = await response.Content.ReadFromJsonAsync<ArticleResponse>();
            return data;
        }
    }
}
