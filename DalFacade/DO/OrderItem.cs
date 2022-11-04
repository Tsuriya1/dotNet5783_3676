

namespace DalFacade.DO;

public struct OrderItem
{
    public int ID { get; set; }
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }

    public override string ToString() => $@"
        ID: {ID},
        Product ID: {ProductId} ,
        OrderId: {OrderId},
        Price: {Price},
        Amount: {Amount}";
}
