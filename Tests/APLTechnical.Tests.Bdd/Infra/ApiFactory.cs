using APLTechnical.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace APLTechnical.Tests.Bdd.Infra;
public class ApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // 🔧 Optional: override real services with fakes/mocks here for tests
            // e.g. replace external APIs, queues, etc.
            //
            // var descriptor = services.Single(d => d.ServiceType == typeof(IMyService));
            // services.Remove(descriptor);
            // services.AddSingleton<IMyService, FakeMyService>();
        });
    }
}
