using APLTechnical.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Reqnroll;

namespace APLTechnical.Tests.Bdd.Hooks;

[Binding]
public class HostHooks(ScenarioContext scenarioContext)
{
    private readonly ScenarioContext _scenarioContext = scenarioContext;

    [BeforeScenario]
    public void BeforeScenario()
    {
        WebApplicationFactory<Program> factory =
            _scenarioContext.ScenarioInfo.Tags.Contains("blob")
                ? new BlobApiFactory()
                : new FileSystemApiFactory();

        var client = factory.CreateClient();
        _scenarioContext.Set(factory, "Factory");
        _scenarioContext.Set(client, nameof(HttpClient));
    }

    [AfterScenario]
    public void AfterScenario()
    {
        if (_scenarioContext.TryGetValue("Factory", out WebApplicationFactory<Program> factory))
        {
            factory.Dispose();
        }

        if (_scenarioContext.TryGetValue(nameof(HttpClient), out HttpClient client))
        {
            client.Dispose();
        }
    }
}
