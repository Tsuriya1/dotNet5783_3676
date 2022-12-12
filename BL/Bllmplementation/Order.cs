using BO;
using Dal;
using DalFacade.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bllmplementation
{
    internal class Order : BlApi.Iorder

    {
        private IDal Dal = new DalList();
        public IEnumerable<OrderForList> GetOrder()
        {

            IEnumerable<DalFacade.DO.Order> orders = Dal.Order.get();

            List<OrderForList> list_of_ordersForList = new List<OrderForList>();
            foreach (DalFacade.DO.Order order in orders)
            {

                OrderForList orderForList = new OrderForList();
                orderForList.ID = order.ID;
                orderForList.CustomerName = order.CustumerName;
                IEnumerable<DalFacade.DO.OrderItem> temp = Dal.OrderItem.get(order);
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
                catch (Exception e)
                {
                    throw new Exception("order not found");
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
                // what about "payment date"? check page 10 of general instructions
                BOorder.Items = Dal.OrderItem.get(order);
                double totlaPrice = 0; 
                foreach(DalFacade.DO.OrderItem item in BOorder.Items)
                {
                    totlaPrice += item.Price;
                }
                BOorder.TotalPrice = totlaPrice;
                return BOorder;
            }
            throw new Exception("order Id < 0");
        }
        public BO.Order updateShipping(int ID)
        {
            DalFacade.DO.Order order;
            try
            {
                order = Dal.Order.get(ID);  
            }
            catch(Exception e)
            {
                throw new Exception ("order not found");
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
                catch (Exception e)
                {
                    throw new Exception("order not found, update failed");
                }
                return BOorder;

            }
                throw new Exception("the order is already shipped");
        }
        public BO.Order updateSupply(int ID)
        {
            DalFacade.DO.Order order;
            try
            {
                order = Dal.Order.get(ID);
            }
            catch (Exception e)
            {
                throw new Exception("order not found");
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
                    catch (Exception e)
                    {
                        throw new Exception("order not found, update failed");
                    }
                    return BOorder;
                }
                throw new Exception("the order has already provided");
            }
            throw new Exception("the order has not shipped yet");
        }
        public OrderTracking OrderTrack(int ID)
        {
            OrderTracking orderTracking = new OrderTracking();
            BO.Order BOorder = new BO.Order();
            try
            {
            BOorder = getOrderDetails(ID);
            }
            catch (Exception e)
            {
                throw new Exception("order not found");
            }
            orderTracking.Status = BOorder.Status;
            orderTracking.ID = BOorder.ID;
            return orderTracking;
        }
        public Order updateOrder(int ID, int ProductID, int amount)
        {
            DalFacade.DO.Order orderToUpdate;
            try
            {
                orderToUpdate = Dal.Order.get(ID);
                BO.Order BOorder = new BO.Order();
            }
            catch (Exception e)
            {
                throw new Exception("item not found");
            }
            for(int i=0; i<orderToUpdate.)

            


        }
    }
}