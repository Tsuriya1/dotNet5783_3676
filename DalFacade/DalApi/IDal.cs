//using System;
//namespace DalFacade.DalApi
using DalFacade.DO;
//{
public interface IDal
{
    IOrder Order { get; }
    IOrderItem OrderItem { get; }
    Iproduct Product { get; } 
}  
//}

