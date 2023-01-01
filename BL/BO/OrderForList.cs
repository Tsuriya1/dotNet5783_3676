using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public struct OrderForList
    {
        public int ID { get; set; }
        public string? CustomerName { get; set; }
        public OrderStatus? status { get; set; }
        public int AmountOfItems { get; set; }
        public double TotalPrice { get; set; }

        public override string ToString() => $@"
        ID: {ID},
        Customer Name: {CustomerName},
        status: {status},
        Amount Of Items: {AmountOfItems},
        Total price: {TotalPrice}";
    }
}
