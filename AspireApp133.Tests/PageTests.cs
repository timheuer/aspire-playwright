using Microsoft.Playwright;

namespace AspireApp133.Tests;

[TestClass]
public class PageTests : PageTest
{
    [TestMethod]
    public async Task BrowseAroundFrontEnd()
    {
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.AspireApp133_AppHost>();
        await using var app = await appHost.BuildAsync();
        await app.StartAsync();

        // find the named resource I need
        var webResource = appHost.Resources.Where(r=>r.Name == "webfrontend").FirstOrDefault();

        // get the 'http' endpoint for the resource
        var endpoint = webResource.Annotations.OfType<EndpointAnnotation>().Where(x => x.Name == "http").FirstOrDefault();

        // navigate to the UriString of the allocated endpoint 
        await Page.GotoAsync(endpoint.AllocatedEndpoint.UriString);

        await Page.ScreenshotAsync(new PageScreenshotOptions { Path = ".\\screenshot.png" });

        await Page.GetByRole(AriaRole.Heading, new() { Name = "Hello, world!" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "Counter" }).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Click me" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "Weather" }).ClickAsync();
        await Page.GetByRole(AriaRole.Heading, new() { Name = "Weather" }).ClickAsync();

    }
}
