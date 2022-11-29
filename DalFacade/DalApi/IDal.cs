//using System;
//namespace DalFacade.DalApi
using DalFacade.DO;
using DalFacade.DalApi;
//{
public interface IDal
{
    IOrder Order { get; }
    IOrderItem OrderItem { get; }
    Iproduct Product { get; } 
}  
//}

