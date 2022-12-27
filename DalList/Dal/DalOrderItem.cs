namespace Dal;
using DalFacade.DO;

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
            if (DataSource.orderItemList[i].ID == ID)
            {
                return Dal.DataSource.orderItemList[i];
            }
        }
        throw new DalFacade.DO.NotFoundException("orderItem not found");
    }

    public DalFacade.DO.OrderItem get(int productId, int orderId)
    {

        for (int i = 0; i < Dal.DataSource.orderItemList.Count; i++)
        {
            if ((Dal.DataSource.orderItemList[i].ProductId == productId)&&(Dal.DataSource.orderItemList[i].OrderId == orderId))
            {
                return Dal.DataSource.orderItemList[i];
            }
        }
        throw new DalFacade.DO.NotFoundException("orderItem not found");
    }

    public IEnumerable<DalFacade.DO.OrderItem> get()
    {
        List<DalFacade.DO.OrderItem> orderItems = new List<DalFacade.DO.OrderItem>();
        for (int i = 0; i < Dal.DataSource.orderItemList.Count; i++)
        {
            orderItems.Insert(i, DataSource.orderItemList[i]);
        }
        return orderItems;

    }


    public IEnumerable< OrderItem> get(DalFacade.DO.Order order)
    {
        int count = 0;
        for (int i = 0; i < Dal.DataSource.orderItemList.Count; i++)
        {

            if (Dal.DataSource.orderItemList[i].OrderId == order.ID)
            {
                count ++;
            }
        }

        if(count == 0)
        {
            throw new DalFacade.DO.NotFoundException("orderItem not found");
        }
        DalFacade.DO.OrderItem[] orderItems = new DalFacade.DO.OrderItem[count];
        int inx = 0;
        for (int i = 0; i < Dal.DataSource.orderItemList.Count; i++)
        {
            if (Dal.DataSource.orderItemList[i].OrderId == order.ID)
            {
                orderItems[inx] = Dal.DataSource.orderItemList[i];
                inx++;
            }
        }
        return orderItems;

    }

    public void delete(int ID)
    {
        for (int i = 0; i < Dal.DataSource.orderItemList.Count; i++)
        {
            if (Dal.DataSource.orderItemList[i].ID == ID)
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
            if (Dal.DataSource.orderItemList[i].ID == orderItem.ID)
            {
                Dal.DataSource.orderItemList.Insert(i,orderItem);
                return;
            }
        }
        throw new DalFacade.DO.NotFoundException("orderItem not found");
    }
    

}
