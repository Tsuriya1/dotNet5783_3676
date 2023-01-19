namespace Dal;
using DalFacade.DO;
using System;
using System.Security.Cryptography.X509Certificates;

internal class DalOrderItem : IOrderItem
{
    public int add(DalFacade.DO.OrderItem orderItem)
    {
        orderItem.ID = Dal.DataSource.Config.getOrderItemId();
        DataSource.orderItemList.Add(orderItem);
        return orderItem.ID;
    }

    public DalFacade.DO.OrderItem get(int ID)
    {
        var item =from orderItem in DataSource.orderItemList where (orderItem.HasValue && orderItem.Value.ID == ID) select orderItem.Value;
        if (item != null && item.Count() > 0)
        {
            return item.First();
        }
        throw new DalFacade.DO.NotFoundException("orderItem not found");
/*
            for (int i = 0; i < DataSource.orderItemList.Count; i++)
        {
            if (DataSource.orderItemList[i].HasValue)
            {
                if (DataSource.orderItemList[i].Value.ID == ID)
                {
                    return Dal.DataSource.orderItemList[i].Value;
                }
            }
        }
        throw new DalFacade.DO.NotFoundException("orderItem not found");
*/
    }
    public OrderItem getObject(Func<OrderItem, bool>? func)
    {
        if (func == null) {
            throw new DalFacade.DO.NotFoundException("no filter parameter is given");
        }
        var item = from orderItem in DataSource.orderItemList
                   where (orderItem.HasValue && func(orderItem.Value))
                   select orderItem.Value;
        if (item != null && item.Count() > 0)
        {
            return item.First();
        }
        throw new DalFacade.DO.NotFoundException("orderItem not found");


        /*for (int i = 0; i < DataSource.orderItemList.Count; i++)
        {
            if (DataSource.orderItemList[i].HasValue)
            {
                if (func(DataSource.orderItemList[i].Value))
                {
                    return Dal.DataSource.orderItemList[i].Value;
                }
            }
        }
        throw new DalFacade.DO.NotFoundException("orderItem not found");*/
    }
    public DalFacade.DO.OrderItem get(int productId, int orderId)
    {
        var item = from orderItem in DataSource.orderItemList
                   where ((orderItem.HasValue) && (orderItem.Value.ProductId == productId) && (orderItem.Value.OrderId == orderId))
                   select orderItem.Value;
        if (item != null && item.Count() > 0)
        {
            return item.First();
        }
        throw new DalFacade.DO.NotFoundException("orderItem not found");


        /*for (int i = 0; i < Dal.DataSource.orderItemList.Count; i++)
        {
            if ((Dal.DataSource.orderItemList[i].Value.ProductId == productId)&&(Dal.DataSource.orderItemList[i].Value.OrderId == orderId))
            {
                return Dal.DataSource.orderItemList[i].Value;
            }
        }
        throw new DalFacade.DO.NotFoundException("orderItem not found");*/
    }

    public IEnumerable<OrderItem> get(Func<OrderItem, bool>? func = null)
    {
        IEnumerable<DalFacade.DO.OrderItem> orderItems;
        if (func == null) 
        {
            orderItems = from orderItem in DataSource.orderItemList
                         where (orderItem.HasValue)
                         select orderItem.Value;
            return orderItems;
        }
        orderItems = from orderItem in DataSource.orderItemList
                     where (orderItem.HasValue && func(orderItem.Value))
                     select orderItem.Value;
        /*for (int i = 0; i < Dal.DataSource.orderItemList.Count; i++)
        {
            if (!DataSource.orderItemList[i].HasValue)
            {
                continue;
            }
            if (func != null)
            {
                if (func(DataSource.orderItemList[i].Value))
                {
                    orderItems.Insert(i, DataSource.orderItemList[i].Value);
                }
            }
            else
            {
                orderItems.Insert(i, DataSource.orderItemList[i].Value);
            }
            orderItems.Insert(i, DataSource.orderItemList[i].Value);
        }*/
        return orderItems;

    }


    public IEnumerable< OrderItem?> get(DalFacade.DO.Order order)
    {
        int count;
        count = Dal.DataSource.orderItemList.Count(x=> ((x.HasValue)&&( x.Value.OrderId == order.ID)));
        /*for (int i = 0; i < Dal.DataSource.orderItemList.Count; i++)
        {

            if (Dal.DataSource.orderItemList[i].Value.OrderId == order.ID)
            {
                count ++;
            }
        }*/

        if(count == 0)
        {
            throw new DalFacade.DO.NotFoundException("orderItem not found");
        }

        IEnumerable<DalFacade.DO.OrderItem?> orderItems = Dal.DataSource.orderItemList.Where(x => (x.HasValue && x.Value.OrderId == order.ID));
/*
        List<DalFacade.DO.OrderItem?> orderItems = new List<DalFacade.DO.OrderItem?>();
        int inx = 0;
        for (int i = 0; i < Dal.DataSource.orderItemList.Count; i++)
        {
            if (Dal.DataSource.orderItemList[i].Value.OrderId == order.ID)
            {
                orderItems[inx] = Dal.DataSource.orderItemList[i].Value;
                inx++;
            }
        }*/
        return orderItems;

    }

    public void delete(int ID)
    {
        int count = Dal.DataSource.orderItemList.RemoveAll(x => (x.HasValue && x.Value.ID == ID));
        if (count == 0)
        {
            throw new DalFacade.DO.NotFoundException("orderItem not found");
        }

        /*for (int i = 0; i < Dal.DataSource.orderItemList.Count; i++)
        {
            if (Dal.DataSource.orderItemList[i].Value.ID == ID)
            {
                DataSource.orderItemList.Remove(DataSource.orderItemList[i]);
                return;
            }
        }
        throw new DalFacade.DO.NotFoundException("orderItem not found");
        */

    }

    public void update(DalFacade.DO.OrderItem orderItem1)
    {
        var item = from orderItem in DataSource.orderItemList
                   where ((orderItem.HasValue) && (orderItem.Value.ID == orderItem1.ID))
                   select orderItem.Value;
        if (item != null && item.Count() > 0)
        {
            delete(orderItem1.ID);
            Dal.DataSource.orderItemList.Add(orderItem1);
            return;
        }

       /* for (int i = 0; i < Dal.DataSource.orderItemList.Count; i++)
        {
            if (Dal.DataSource.orderItemList[i].Value.ID == orderItem1.ID)
            {
                Dal.DataSource.orderItemList.Insert(i,orderItem1);
                return;
            }
        }*/
        throw new DalFacade.DO.NotFoundException("orderItem not found");
    }

    
}
