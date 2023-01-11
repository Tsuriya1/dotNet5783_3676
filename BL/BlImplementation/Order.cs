using BlApi;
using BO;
//using Dal;
using DalFacade.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    internal class Order : BlApi.Iorder

    {
        IDal? Dal = DalApi.Factory.Get();
       // private IDal Dal = new DalList();
        IEnumerable<OrderForList?> Iorder.GetOrder()
        {

            IEnumerable<DalFacade.DO.Order> orders = Dal.Order.get();

            List<OrderForList?> list_of_ordersForList = new List<OrderForList?>();
            foreach (DalFacade.DO.Order order in orders)
            {

                OrderForList orderForList = new OrderForList();
                orderForList.ID = order.ID;
                orderForList.CustomerName = order.CustumerName;
                IEnumerable<DalFacade.DO.OrderItem?> temp = Dal.OrderItem.get(order);
                orderForList.AmountOfItems = temp.Count();
                orderForList.status = OrderStatus.Confirmed;        // "confirmed" is the default value
                int sumOfAmount = 0;
                double sumOfprice = 0;
                foreach (DalFacade.DO.OrderItem item in temp)
                {
                    sumOfAmount += item.Amount;
                    double itemPrice = item.Price * item.Price;
                    sumOfprice += itemPrice;
                }
                orderForList.AmountOfItems = sumOfAmount;
                orderForList.TotalPrice = sumOfprice;
                list_of_ordersForList.Add(orderForList);
            }
            return list_of_ordersForList;
        }
        public BO.Order getOrderDetails(int ID)
        {
            if (ID > 0)
            {
                DalFacade.DO.Order order;
                try
                {
                    order = Dal.Order.get(ID);
                }
                catch (DalFacade.DO.NotFoundException e)
                {
                    throw new NotFoundError("Order not found", e);
                }
                BO.Order BOorder = new BO.Order();
                BOorder.ID = ID;
                BOorder.CustumerName = order.CustumerName;
                BOorder.CustumerEmail = order.CustumerEmail;
                BOorder.CustumerAdress = order.CustumerAdress;
                BOorder.OrderDate = order.OrderDate;
                BOorder.Status = OrderStatus.Confirmed;          // "confirmed" is the default value (??)
                BOorder.DeliveryDate = order.DeliveryDate;
                BOorder.ShipDate = order.ShipDate;
                BOorder.PaymentDate = null;
                BOorder.Items = Dal.OrderItem.get(order);
                double totlaPrice = 0; 
                foreach(DalFacade.DO.OrderItem item in BOorder.Items)
                {
                    totlaPrice += item.Price;
                }
                BOorder.TotalPrice = totlaPrice;
                return BOorder;
            }
            throw new NotValidValue("order Id < 0");
        }
        public BO.Order updateShipping(int ID)
        {
            DalFacade.DO.Order order;
            try
            {
                order = Dal.Order.get(ID);  
            }
            catch (DalFacade.DO.NotFoundException e)
            {
                throw new NotFoundError ("order not found",e);
            }
            if (order.ShipDate > DateTime.Now)
            {
                BO.Order BOorder = getOrderDetails(ID);
                order.ShipDate = DateTime.Now;
                BOorder.ShipDate = DateTime.Now;
                try
                {
                    Dal.Order.update(order);
                }
                catch (NotFoundError e)
                {
                    throw new NotFoundError ("order not found, update failed",e);
                }
                return BOorder;

            }
                throw new StatusError("the order is already shipped");
        }
        public BO.Order updateSupply(int ID)
        {
            DalFacade.DO.Order order;
            try
            {
                order = Dal.Order.get(ID);
            }
            catch (DalFacade.DO.NotFoundException e)
            {
                throw new NotFoundError ("order not found",e);
            }
            if (order.DeliveryDate < DateTime.Now)
            {
                BO.Order BOorder = getOrderDetails(ID);
                if(BOorder.Status != OrderStatus.provided)
                {
                    order.DeliveryDate = DateTime.Now;
                    BOorder.DeliveryDate = DateTime.Now;
                    try
                    {
                        Dal.Order.update(order);
                    }
                    catch (DalFacade.DO.NotFoundException e)
                    {
                        throw new NotFoundError ("order not found, update failed",e);
                    }
                    return BOorder;
                }
                throw new StatusError ("the order has already provided");
            }
            throw new StatusError ("the order has not shipped yet");
        }
        public OrderTracking OrderTrack(int ID)
        {
            OrderTracking orderTracking = new OrderTracking();
            BO.Order BOorder = new BO.Order();
            try
            {
                BOorder = getOrderDetails(ID);
            }
            catch (DalFacade.DO.NotFoundException e)

            {
                throw new NotFoundError ("order not found");
            }
            orderTracking.Status = BOorder.Status;
            orderTracking.ID = BOorder.ID;
            return orderTracking;
        }  
    }
}