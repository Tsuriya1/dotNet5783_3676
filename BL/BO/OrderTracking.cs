using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public struct OrderTracking
    {
        public int ID { get; set; }
        public OrderStatus? Status { get; set; }
        public Tuple<DateTime?,string?>? description { get; set; }
        public override string ToString() => $@"
            ID: {ID} ,
            Status: {Status}";
    }
}
