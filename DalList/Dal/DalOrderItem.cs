namespace Dal;

public class DalOrderItem
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
        throw new Exception("orderItem not found");
    }

    public DalFacade.DO.OrderItem get(DalFacade.DO.Product product, DalFacade.DO.Order order)
    {

        for (int i = 0; i < Dal.DataSource.orderItemList.Count; i++)
        {
            if ((Dal.DataSource.orderItemList[i].ProductId == product.ID)&&(Dal.DataSource.orderItemList[i].OrderId == order.ID))
            {
                return Dal.DataSource.orderItemList[i];
            }
        }
        throw new Exception("orderItem not found");
    }

    public List<DalFacade.DO.OrderItem> get()
    {
        List<DalFacade.DO.OrderItem> orderItems = new List<DalFacade.DO.OrderItem>();
        for (int i = 0; i < Dal.DataSource.orderItemList.Count; i++)
        {
            orderItems.Insert(i, DataSource.orderItemList[i]);
        }
        return orderItems;

    }


    public DalFacade.DO.OrderItem[] get(DalFacade.DO.Order order)
    {
        int count = 0;
        for (int i = 0; i < Dal.DataSource.Config.orderItemInx; i++)
        {

            if (Dal.DataSource.ordersItemList[i].OrderId == order.ID)
            {
                count ++;
            }
        }

        if(count == 0)
        {
            throw new Exception("order not found");
        }
        DalFacade.DO.OrderItem[] orderItems = new DalFacade.DO.OrderItem[count];
        int inx = 0;
        for (int i = 0; i < Dal.DataSource.Config.orderItemInx; i++)
        {
            if (Dal.DataSource.ordersItemList[i].OrderId == order.ID)
            {
                orderItems[inx] = Dal.DataSource.ordersItemList[i];
                inx++;
            }
        }
        return orderItems;

    }

    public void delete(int ID)
    {
        for (int i = 0; i < Dal.DataSource.Config.orderItemInx; i++)
        {
            if (Dal.DataSource.ordersItemList[i].ID == ID)
            {
                for (int j = i + 1; j < Dal.DataSource.Config.orderItemInx; j++)
                {
                    Dal.DataSource.ordersItemList[j - 1] = Dal.DataSource.ordersItemList[j];
                }
                Dal.DataSource.Config.orderItemInx--;
                return;
            }
        }
        throw new Exception("orderItem not found");

    }

    public void update(DalFacade.DO.OrderItem orderItem)
    {
        for (int i = 0; i < Dal.DataSource.Config.orderItemInx; i++)
        {
            if (Dal.DataSource.ordersItemList[i].ID == orderItem.ID)
            {
                Dal.DataSource.ordersItemList[i] = orderItem;
                return;
            }
        }
        throw new Exception("orderItem not found");
    }
}
