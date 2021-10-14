using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace highmem_test;

class Program
{
    public static async Task<int> Main(string[] args)
    {
        IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddHostedService<Worker>();
            })
            .UseSystemd()
            .Build();

        await host.RunAsync();
        return 0;
    }
}
