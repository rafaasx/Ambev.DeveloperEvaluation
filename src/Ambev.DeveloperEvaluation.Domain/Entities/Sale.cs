using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    public string SaleNumber { get; private set; }
    public DateTime SaleDate { get; private set; }
    public decimal TotalAmount { get; private set; }
    public bool IsCancelled { get; private set; }
    public Guid CustomerId { get; private set; }
    public string CustomerName { get; private set; }
    public Guid BranchId { get; private set; }
    public string BranchName { get; private set; }

    private readonly List<SaleItem> _products = new();
    public IReadOnlyCollection<SaleItem> Products => _products.AsReadOnly();

    protected Sale() { }

    public Sale(string saleNumber, DateTime saleDate, Guid customerId, string customerName, Guid branchId, string branchName)
    {
        Id = Guid.NewGuid();
        SaleNumber = saleNumber;
        SaleDate = saleDate;
        CustomerId = customerId;
        CustomerName = customerName;
        BranchId = branchId;
        BranchName = branchName;
        IsCancelled = false;
    }

    public void Cancel() => IsCancelled = true;

    private void CalculateTotal()
    {
        TotalAmount = _products.Sum(i => i.TotalItemAmount);
    }

    public void AddItem(SaleItem item)
    {
        _products.Add(item);
        CalculateTotal();
        _domainEvents.Add(new SaleModified(Id, TotalAmount));
    }

    public void AddItems(IEnumerable<SaleItem> items) => items.ToList().ForEach(item => AddItem(item));

    public void Cancel(string reason)
    {
        IsCancelled = true;
        _domainEvents.Add(new SaleCancelled(Id, reason));
    }
}
