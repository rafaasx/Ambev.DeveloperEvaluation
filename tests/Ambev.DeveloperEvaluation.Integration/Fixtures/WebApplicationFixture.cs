using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Fixtures
{
    public class WebApplicationFixture : IAsyncLifetime
    {
        public HttpClient Client { get; set; } = default!;
        private IHost _host = default!;

        public async Task InitializeAsync()
        {
            StartDockerCompose();

            var factory = new WebApplicationFactory<WebApi.Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("Testing");
                });

            _host = factory.Services.GetRequiredService<IHost>();
            Client = factory.CreateClient();
        }

        public async Task DisposeAsync()
        {
            Client?.Dispose();
            StopDockerCompose();
        }

        private void StartDockerCompose()
        {
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "docker-compose",
                Arguments = "up -d",
                WorkingDirectory = Directory.GetCurrentDirectory(),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            });

            process!.WaitForExit();
        }

        private void StopDockerCompose()
        {
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "docker-compose",
                Arguments = "down",
                WorkingDirectory = Directory.GetCurrentDirectory(),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            });

            process!.WaitForExit();
        }
    }
}
