using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface Iorder
    {
        public IEnumerable<OrderForList> GetOrder();
        public Order getOrderDetails(int ID);
        public Order updateShipping(int ID);
        public Order updateSupply (int ID);
        public OrderTracking OrderTrack(int ID);
        //public Order updateOrder (int ID, int ProductID);
    }
}
