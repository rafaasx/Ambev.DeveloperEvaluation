namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateCategoryCommand
{
    public string ExternalId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public CreateCategoryCommand(string externalId, string name)
    {
        ExternalId = externalId;
        Name = name;
    }
}
