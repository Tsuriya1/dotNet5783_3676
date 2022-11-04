namespace Dal;

public class DalOrderItem
{
    public int add(DalFacade.DO.OrderItem orderItem)
    {
        orderItem.ID = Dal.DataSource.Config.getOrderItemId();
        Dal.DataSource.ordersItemList[Dal.DataSource.Config.orderItemInx] = orderItem;
        Dal.DataSource.Config.orderInx++;
        return orderItem.ID;
    }

    public DalFacade.DO.OrderItem get(int ID)
    {

        for (int i = 0; i < Dal.DataSource.Config.orderItemInx; i++)
        {
            if (Dal.DataSource.ordersItemList[i].ID == ID)
            {
                return Dal.DataSource.ordersItemList[i];
            }
        }
        throw new Exception("orderItem not found");
    }

    public DalFacade.DO.OrderItem get(DalFacade.DO.Product product, DalFacade.DO.Order order)
    {

        for (int i = 0; i < Dal.DataSource.Config.orderItemInx; i++)
        {
            if ((Dal.DataSource.ordersItemList[i].ProductId == product.ID)&&(Dal.DataSource.ordersItemList[i].OrderId == order.ID))
            {
                return Dal.DataSource.ordersItemList[i];
            }
        }
        throw new Exception("orderItem not found");
    }

    public DalFacade.DO.OrderItem[] get()
    {
        DalFacade.DO.OrderItem[] orderItems = new DalFacade.DO.OrderItem[Dal.DataSource.Config.orderItemInx];
        for (int i = 0; i < Dal.DataSource.Config.orderItemInx; i++)
        {
            orderItems[i] = Dal.DataSource.ordersItemList[i];
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


        DalFacade.DO.OrderItem[] orderItems = new DalFacade.DO.OrderItem[count];
        int inx = 0;
        for (int i = 0; i < count; i++)
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
