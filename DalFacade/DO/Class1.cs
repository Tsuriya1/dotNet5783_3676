
namespace DO;

public struct Product
{
    private int ID { get; set; }
    private string Name { get; set; }
    private int Category { get; set; }
    private double Price { get; set; }
    private int InStock { get; set; }
    

    public override string ToString() => $@"
        Product ID={ID}: {Name},
        category - {Category},
        Price: {Price},
        Amount in stock: {InStock}";
}
