using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public struct Order
    {
        public int ID { get; set; }
        public string CustumerName { get; set; }
        public string CustumerEmail { get; set; }
        public string CustumerAdress { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime ShipDate { get; set; }
        public OrderItem Items { get; set; }
        public double TotalPrice { get; set; }

        public override string ToString() => $@"
        ID: {ID},
        CustumerName: {CustumerName} ,
        CustumerEmail: {CustumerEmail},
        CustumerAdress: {CustumerAdress},
        OrderDate: {OrderDate},
        Status: {Status},
        PaymentDate: {PaymentDate},
        DeliveryDate: {DeliveryDate},
        ShipDate: {ShipDate},
        Items: {Items},
        Total Price: {TotalPrice}";
    }
}
