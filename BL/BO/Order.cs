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
        public string? CustumerName { get; set; }
        public string? CustumerEmail { get; set; }
        public string? CustumerAdress { get; set; }
        public DateTime? OrderDate { get; set; }
        public OrderStatus? Status { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public IEnumerable<DalFacade.DO.OrderItem?>? Items { get; set; }
        public double TotalPrice { get; set; }

        public override string ToString()
        {
            string order = $@"
                    ID: {ID},
                    CustumerName: {CustumerName} ,
                    CustumerEmail: {CustumerEmail},
                    CustumerAdress: {CustumerAdress},
                    Status: {Status},
                    ";
            if (OrderDate == null){
                order += @$"OrderDate: no date found ,
                    ";
            }
            else
            {
                order += $@"OrderDate: {OrderDate} ,
                    ";
            }
            if (PaymentDate == null)
            {
                order += @$"PaymentDate: no date found ,
                    ";
            }
            else
            {
                order += $@"PaymentDate: {PaymentDate} ,
                    ";
            }
            if (DeliveryDate == null)
            {
                order += @$"DeliveryDate: no date found ,
                    ";
            }
            else
            {
                order += $@"DeliveryDate: {DeliveryDate} ,
                    ";
            }
            if (ShipDate == null)
            {
                order += @$"ShipDate: no date found ,
                    ";
            }
            else
            {
                order += $@"ShipDate: {ShipDate} ,
                    ";
            }
            order += $@"Total Price: {TotalPrice}";
            
            return order;
            
        }
    }
}
