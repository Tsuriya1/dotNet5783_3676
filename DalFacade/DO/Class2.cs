
using System.Diagnostics;
using System.Xml.Linq;

namespace DO;

public struct OrderItem
{
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }

    public override string ToString() => $@"
        Product ID: {ProductId} ,
        OrderId: {OrderId},
        Price: {Price},
        Amount: {Amount}";
}
