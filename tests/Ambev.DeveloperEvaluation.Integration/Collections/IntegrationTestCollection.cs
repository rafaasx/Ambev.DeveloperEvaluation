using Ambev.DeveloperEvaluation.Integration.Fixtures;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Collections
{
    [CollectionDefinition("IntegrationTests")]
    public class IntegrationTestCollection : ICollectionFixture<WebApplicationFixture>
    {
    }
}
