using System;
using System.Collections.Specialized;
using DalFacade.DalApi;
using DalFacade.DO;
namespace Dal;

 sealed public class DalList : IDal
{
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
    public Iproduct Product => new DalProduct();
}
