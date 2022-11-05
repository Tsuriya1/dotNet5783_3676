
namespace DalFacade.DO;

public struct Order
{
    public int ID { get; set; }
    public string CustumerName { get; set; }
    public string CustumerEmail { get; set; }
    public string CustumerAdress { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public DateTime ShipDate { get; set; }

    public override string ToString() => $@"
        ID: {ID},
        CustumerName: {CustumerName} ,
        CustumerEmail: {CustumerEmail},
        CustumerAdress: {CustumerAdress},
        OrderDate: {OrderDate},
        DeliveryDate: {DeliveryDate},
        ShipDate: {ShipDate}";
}
