using System.Linq;

namespace Dal;

using DalFacade.DO;
using System.Collections.Generic;
using System.Security.Cryptography;

internal class DalOrder : IOrder
{
    public int add(DalFacade.DO.Order order)
    {
        order.ID = Dal.DataSource.Config.getOrderId();
        DataSource.ordersList.Add(order);
        return order.ID;
    }

    public DalFacade.DO.Order get(int ID)
    {
        int len = DataSource.ordersList.Count;
        var orders = from DalFacade.DO.Order order1 in DataSource.ordersList
                    where order1.ID == ID
                    select order1;
        if(orders!= null && orders.Count() > 0)
        {
            return orders.First();
        }
        throw new DalFacade.DO.NotFoundException("order not found");
    }
    public Order getObject(Func<Order, bool>? func)
    {
        int len = DataSource.ordersList.Count;
        var orders = from Order order in DataSource.ordersList
                     where func(order)
                     select order;
        if (orders != null && orders.Count() > 0)
        {
            return orders.First();
        }

        throw new DalFacade.DO.NotFoundException("order not found");
    }
    public IEnumerable<Order> get(Func<Order, bool>? func = null)
    {
        IEnumerable<DalFacade.DO.Order> orders;
        
        if( func != null)
        {
            orders = from order in DataSource.ordersList where (order.HasValue && func(order.Value)) select order.Value;
        }
        else
        {
            orders = from order in DataSource.ordersList where (order.HasValue) select order.Value;

        }

        return orders;

    }

    public void delete(int ID)
    {
        int count = DataSource.ordersList.RemoveAll(x => x.HasValue && x.Value.ID == ID);
        if (count== 0)
        {
            throw new DalFacade.DO.NotFoundException("order not found");
        }
    }

    public void update(DalFacade.DO.Order order)
    {
        var orderToUpdate = from order1 in DataSource.ordersList where order1.Value.ID == order.ID select order1.Value;
        if (orderToUpdate != null && orderToUpdate.Count() > 0)
        {
            
            delete(order.ID);
            DataSource.ordersList.Add(order);
            return;
        }

        throw new DalFacade.DO.NotFoundException("order not found");
    }

   
}

