using System;
using System.Collections.Specialized;

namespace Dal;

 sealed public class DalList : IDal
{
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
    public Iproduct Product => new DalProduct();
}
