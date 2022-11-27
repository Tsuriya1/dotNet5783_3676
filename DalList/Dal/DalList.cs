using System;
using System.Collections.Specialized;
using DalApi;
using DO;

namespace DalList;
namespace Dal;

 sealed public class DalList : IDal
{
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
    public IProduct Product => new DalProduct();
}
