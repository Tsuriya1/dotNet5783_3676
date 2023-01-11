using System;
using System.Collections.Specialized;

namespace Dal;

 internal sealed class DalList : IDal
{
    private DalList()
    {}
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
    public Iproduct Product => new DalProduct();
    public static IDal Instance { get; } = new DalList();
}
