//using System;
//namespace DalFacade.DalApi

// mediation between data and logic layers insted of direct access to data.
using DalFacade.DO;
public interface IDal
{
    IOrder Order { get; }
    IOrderItem OrderItem { get; }
    Iproduct Product { get; } 
}  

