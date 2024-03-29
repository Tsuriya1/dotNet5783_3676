﻿using BlApi;
using BO;
//using Dal;
using DalFacade.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    internal class Order : BlApi.Iorder

    {
        IDal? Dal = DalApi.Factory.Get();
        // private IDal Dal = new DalList();

        OrderForList? convert2_order_list(DalFacade.DO.Order order)
        {
            OrderForList orderForList = new OrderForList();
            orderForList.ID = order.ID;
            orderForList.CustomerName = order.CustumerName;
            IEnumerable<DalFacade.DO.OrderItem?> temp = Dal.OrderItem.get(order);
            orderForList.AmountOfItems = temp.Count();
            orderForList.status = getStatus(order);        // "confirmed" is the default value
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
            return orderForList;
        }
        
        IEnumerable<OrderForList?> Iorder.GetOrder()
        {

            IEnumerable<DalFacade.DO.Order> orders = Dal.Order.get();

            //List<OrderForList?> list_of_ordersForList = new List<OrderForList?>();
            /*foreach(DalFacade.DO.Order item in orders)
            {
                convert2_order_list(item);
            }*/
            var list_of_ordersForList = from DalFacade.DO.Order order in orders
                                        let orderForList = convert2_order_list(order)
                                        select orderForList;
            return list_of_ordersForList;
        }
        
        private OrderStatus getStatus(DalFacade.DO.Order order)
        {
            if (order.DeliveryDate != null)
            {
                return OrderStatus.provided;
            }else if(order.ShipDate != null)
            {
                return OrderStatus.sent;

            }
            else
            {
                return OrderStatus.Confirmed;
            }
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
                BOorder.Status = getStatus(order);          // "confirmed" is the default value (??)
                BOorder.DeliveryDate = order.DeliveryDate;
                BOorder.ShipDate = order.ShipDate;
                BOorder.PaymentDate = null;
                BOorder.Items = Dal.OrderItem.get(order);
                double totlaPrice = 0;
                totlaPrice = BOorder.Items.Sum(x => x.Value.Price);
                /**
                foreach (DalFacade.DO.OrderItem item in BOorder.Items)
                {
                    totlaPrice += item.Price;
                }
                **/
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
            var date = DateTime.Now;
            if (order.ShipDate == null || (order.ShipDate > DateTime.Now))
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
            if (order.ShipDate != null && (order.ShipDate < DateTime.Now ))
            {
                //order.DeliveryDate == null || (order.DeliveryDate < DateTime.Now )
                BO.Order BOorder = getOrderDetails(ID);
                if(order.DeliveryDate == null || (order.DeliveryDate > DateTime.Now))
                {
                    order.DeliveryDate = DateTime.Now;
                    BOorder.DeliveryDate = DateTime.Now;
                    BOorder.Status = OrderStatus.provided;
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
            orderTracking.description = new List<Tuple<DateTime?, string?>?>();
            if(BOorder.OrderDate < DateTime.Now )
            {
                orderTracking.description.Add(new Tuple<DateTime?, string?>(BOorder.OrderDate, "order was created"));
            }
            if(BOorder.PaymentDate < DateTime.Now)
            {
                orderTracking.description.Add(new Tuple<DateTime?, string?>(BOorder.PaymentDate, "get payment"));
            }
            if (BOorder.DeliveryDate < DateTime.Now)
            {
                orderTracking.description.Add(new Tuple<DateTime?, string?>(BOorder.DeliveryDate, "order was delivery"));
            }
            if (BOorder.ShipDate < DateTime.Now)
            {
                orderTracking.description.Add(new Tuple<DateTime?, string?>(BOorder.ShipDate, "order was shipped"));
            }
            return orderTracking;
        }

        public OrderForList GetOrderForList(int ID)
        {
            IEnumerable<DalFacade.DO.Order> orders = Dal.Order.get();

            var list_of_ordersForList = from DalFacade.DO.Order order in orders
                                        where order.ID == ID    
                                        let orderForList = convert2_order_list(order)
                                        select orderForList;
            if (list_of_ordersForList.Any())
            {
                return list_of_ordersForList.First().Value;
            }
            throw new NotFoundError("order not found");
        }

        public int? getOldestOrder()
        {
            IEnumerable<DalFacade.DO.Order> orders = Dal.Order.get();
            int id_older = -1;
            DateTime minTime = DateTime.Now;
            foreach (DalFacade.DO.Order order in orders)
            {
                if (order.DeliveryDate != null)
                {
                    continue;
                }
                if (order.ShipDate != null && order.ShipDate < minTime)
                {
                    id_older = order.ID;
                    minTime = order.ShipDate.Value;
                }else if (order.OrderDate!=null && order.OrderDate < minTime)
                {
                    id_older = order.ID;
                    minTime = order.OrderDate.Value;
                }
            }
            if(id_older != -1)
            {
                return id_older;    
            }
            return null;

        }
    }
}