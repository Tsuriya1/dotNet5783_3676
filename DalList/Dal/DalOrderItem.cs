namespace Dal;
using DalFacade.DO;
using System;

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
    }
    public OrderItem getObject(Func<OrderItem, bool>? func)
    {
        for (int i = 0; i < DataSource.orderItemList.Count; i++)
        {
            if (DataSource.orderItemList[i].HasValue)
            {
                if (func(DataSource.orderItemList[i].Value))
                {
                    return Dal.DataSource.orderItemList[i].Value;
                }
            }
        }
        throw new DalFacade.DO.NotFoundException("orderItem not found");
    }
    public DalFacade.DO.OrderItem get(int productId, int orderId)
    {

        for (int i = 0; i < Dal.DataSource.orderItemList.Count; i++)
        {
            if ((Dal.DataSource.orderItemList[i].Value.ProductId == productId)&&(Dal.DataSource.orderItemList[i].Value.OrderId == orderId))
            {
                return Dal.DataSource.orderItemList[i].Value;
            }
        }
        throw new DalFacade.DO.NotFoundException("orderItem not found");
    }

    public IEnumerable<OrderItem> get(Func<OrderItem, bool>? func = null)
    {
        List<DalFacade.DO.OrderItem> orderItems = new List<DalFacade.DO.OrderItem>();
        for (int i = 0; i < Dal.DataSource.orderItemList.Count; i++)
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
        }
        return orderItems;

    }


    public IEnumerable< OrderItem?> get(DalFacade.DO.Order order)
    {
        int count = 0;
        for (int i = 0; i < Dal.DataSource.orderItemList.Count; i++)
        {

            if (Dal.DataSource.orderItemList[i].Value.OrderId == order.ID)
            {
                count ++;
            }
        }

        if(count == 0)
        {
            throw new DalFacade.DO.NotFoundException("orderItem not found");
        }
        List<DalFacade.DO.OrderItem?> orderItems = new List<DalFacade.DO.OrderItem?>();
        int inx = 0;
        for (int i = 0; i < Dal.DataSource.orderItemList.Count; i++)
        {
            if (Dal.DataSource.orderItemList[i].Value.OrderId == order.ID)
            {
                orderItems[inx] = Dal.DataSource.orderItemList[i].Value;
                inx++;
            }
        }
        return orderItems;

    }

    public void delete(int ID)
    {
        for (int i = 0; i < Dal.DataSource.orderItemList.Count; i++)
        {
            if (Dal.DataSource.orderItemList[i].Value.ID == ID)
            {
                DataSource.orderItemList.Remove(DataSource.orderItemList[i]);
                return;
            }
        }
        throw new DalFacade.DO.NotFoundException("orderItem not found");

    }

    public void update(DalFacade.DO.OrderItem orderItem)
    {
        for (int i = 0; i < Dal.DataSource.orderItemList.Count; i++)
        {
            if (Dal.DataSource.orderItemList[i].Value.ID == orderItem.ID)
            {
                Dal.DataSource.orderItemList.Insert(i,orderItem);
                return;
            }
        }
        throw new DalFacade.DO.NotFoundException("orderItem not found");
    }

    
}
