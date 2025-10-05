namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class UpdateCategoryCommand
{
    public string ExternalId { get; private set; } = string.Empty;

    public string Name { get; private set; } = string.Empty;

    public UpdateCategoryCommand(string externalId, string name)
    {
        ExternalId = externalId;
        Name = name;
    }
}
