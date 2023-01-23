using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    sealed internal class DalXml : IDal
    {
        private DalXml()
        { }
        public IOrder Order { get; } = new DalOrder();
        public IOrderItem OrderItem { get; } = new DalOrderItem();
        public Iproduct Product { get; } = new DalProduct();
        public static IDal Instance { get; } = new DalXml();
    }
}
