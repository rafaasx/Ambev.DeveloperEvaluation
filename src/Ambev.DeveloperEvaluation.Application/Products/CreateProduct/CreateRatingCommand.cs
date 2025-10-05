namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateRatingCommand
{
    public CreateRatingCommand()
    {
    }

    public CreateRatingCommand(double rate, int count)
    {
        Rate = rate;
        Count = count;
    }

    public double Rate { get; set; }
    public int Count { get; set; }
}