using DalFacade.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    internal class DalOrder : IOrder
    {
        string entity_name = @"Orders"; 
        public int add(DalFacade.DO.Order order)
        {
            List< DalFacade.DO.Order?> ordersList = XMLTools.LoadListFromXMLSerializer<DalFacade.DO.Order>(entity_name);
            XElement Config = XMLTools.LoadListFromXMLElement("Config");
            order.ID = (int)Config.Element("OrderIdx");

            Config.Element("OrderItemIndex")?.SetValue(order.ID + 1);

            XMLTools.SaveListToXMLElement(Config, "Config");
            ordersList.Add(order);
            XMLTools.SaveListToXMLSerializer<DalFacade.DO.Order>(ordersList, entity_name);
            return order.ID;
        }

        public DalFacade.DO.Order get(int ID)
        {
            List<DalFacade.DO.Order?> ordersList = XMLTools.LoadListFromXMLSerializer<DalFacade.DO.Order>(entity_name);

            int len = ordersList.Count;
            var orders = from DalFacade.DO.Order order1 in ordersList
                         where order1.ID == ID
                         select order1;
            if (orders != null && orders.Count() > 0)
            {
                return orders.First();
            }
            throw new DalFacade.DO.NotFoundException("order not found");
        }
        public Order getObject(Func<Order, bool>? func)
        {
            List<DalFacade.DO.Order?> ordersList = XMLTools.LoadListFromXMLSerializer<DalFacade.DO.Order>(entity_name);

            int len = ordersList.Count;
            var orders = from Order order in ordersList
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
            List<DalFacade.DO.Order?> ordersList = XMLTools.LoadListFromXMLSerializer<DalFacade.DO.Order>(entity_name);

            IEnumerable<DalFacade.DO.Order> orders;

            if (func != null)
            {
                orders = from order in ordersList where (order.HasValue && func(order.Value)) select order.Value;
            }
            else
            {
                orders = from order in ordersList where (order.HasValue) select order.Value;

            }

            return orders;

        }

        public void delete(int ID)
        {
            List<DalFacade.DO.Order?> ordersList = XMLTools.LoadListFromXMLSerializer<DalFacade.DO.Order>(entity_name);

            int count = ordersList.RemoveAll(x => x.HasValue && x.Value.ID == ID);
            if (count == 0)
            {
                throw new DalFacade.DO.NotFoundException("order not found");
            }
            XMLTools.SaveListToXMLSerializer<DalFacade.DO.Order>(ordersList, entity_name);

        }

        public void update(DalFacade.DO.Order order)
        {
            List<DalFacade.DO.Order?> ordersList = XMLTools.LoadListFromXMLSerializer<DalFacade.DO.Order>(entity_name);

            var orderToUpdate = from order1 in ordersList where order1.Value.ID == order.ID select order1.Value;
            if (orderToUpdate != null && orderToUpdate.Count() > 0)
            {

                delete(order.ID);
                ordersList.RemoveAll(x => x.HasValue && x.Value.ID == order.ID);
                ordersList.Add(order);
                XMLTools.SaveListToXMLSerializer<DalFacade.DO.Order>(ordersList, entity_name);
                return;
            }

            throw new DalFacade.DO.NotFoundException("order not found");
        }


    }
}
