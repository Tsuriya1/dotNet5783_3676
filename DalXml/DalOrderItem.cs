using DalFacade.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    internal class DalOrderItem : IOrderItem
    {
        string entity_name = @"OrdersItem";

        public int add(DalFacade.DO.OrderItem orderItem)
        {
            List<DalFacade.DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DalFacade.DO.OrderItem>(entity_name);
            XElement Config = XMLTools.LoadListFromXMLElement("Config");
            orderItem.ID = (int)Config.Element("OrderItemIdx");

            Config.Element("OrderItemIndex")?.SetValue(orderItem.ID+1);

            XMLTools.SaveListToXMLElement(Config, "Config");

            orderItemList.Add(orderItem);
            XMLTools.SaveListToXMLSerializer<DalFacade.DO.OrderItem>(orderItemList, entity_name);
            return orderItem.ID;
        }

        public DalFacade.DO.OrderItem get(int ID)
        {
            List<DalFacade.DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DalFacade.DO.OrderItem>(entity_name);
            var item = from orderItem in orderItemList where (orderItem.HasValue && orderItem.Value.ID == ID) select orderItem.Value;
            if (item != null && item.Count() > 0)
            {
                return item.First();
            }
            throw new DalFacade.DO.NotFoundException("orderItem not found");
            
        }
        public OrderItem getObject(Func<OrderItem, bool>? func)
        {
            List<DalFacade.DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DalFacade.DO.OrderItem>(entity_name);

            if (func == null)
            {
                throw new DalFacade.DO.NotFoundException("no filter parameter is given");
            }
            var item = from orderItem in orderItemList
                       where (orderItem.HasValue && func(orderItem.Value))
                       select orderItem.Value;
            if (item != null && item.Count() > 0)
            {
                return item.First();
            }
            throw new DalFacade.DO.NotFoundException("orderItem not found");


            
        }
        public DalFacade.DO.OrderItem get(int productId, int orderId)
        {
            List<DalFacade.DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DalFacade.DO.OrderItem>(entity_name);

            var item = from orderItem in orderItemList
                       where ((orderItem.HasValue) && (orderItem.Value.ProductId == productId) && (orderItem.Value.OrderId == orderId))
                       select orderItem.Value;
            if (item != null && item.Count() > 0)
            {
                return item.First();
            }
            throw new DalFacade.DO.NotFoundException("orderItem not found");
        }

        public IEnumerable<OrderItem> get(Func<OrderItem, bool>? func = null)
        {
            List<DalFacade.DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DalFacade.DO.OrderItem>(entity_name);

            IEnumerable<DalFacade.DO.OrderItem> orderItems;
            if (func == null)
            {
                orderItems = from orderItem in orderItemList
                             where (orderItem.HasValue)
                             select orderItem.Value;
                return orderItems;
            }
            orderItems = from orderItem in orderItemList
                         where (orderItem.HasValue && func(orderItem.Value))
                         select orderItem.Value;
            
            return orderItems;

        }


        public IEnumerable<OrderItem?> get(DalFacade.DO.Order order)
        {
            List<DalFacade.DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DalFacade.DO.OrderItem>(entity_name);

            int count;
            count = orderItemList.Count(x => ((x.HasValue) && (x.Value.OrderId == order.ID)));
            

            if (count == 0)
            {
                throw new DalFacade.DO.NotFoundException("orderItem not found");
            }

            IEnumerable<DalFacade.DO.OrderItem?> orderItems = orderItemList.Where(x => (x.HasValue && x.Value.OrderId == order.ID));
           
            return orderItems;

        }

        public void delete(int ID)
        {
            List<DalFacade.DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DalFacade.DO.OrderItem>(entity_name);

            int count = orderItemList.RemoveAll(x => (x.HasValue && x.Value.ID == ID));
            if (count == 0)
            {
                throw new DalFacade.DO.NotFoundException("orderItem not found");
            }
            XMLTools.SaveListToXMLSerializer<DalFacade.DO.OrderItem>(orderItemList, entity_name);



        }

        public void update(DalFacade.DO.OrderItem orderItem1)
        {
            List<DalFacade.DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DalFacade.DO.OrderItem>(entity_name);

            var item = from orderItem in orderItemList
                       where ((orderItem.HasValue) && (orderItem.Value.ID == orderItem1.ID))
                       select orderItem.Value;
            if (item != null && item.Count() > 0)
            {
                delete(orderItem1.ID);
                orderItemList.RemoveAll(x => (x.HasValue && x.Value.ID == orderItem1.ID));
                orderItemList.Add(orderItem1);
                XMLTools.SaveListToXMLSerializer<DalFacade.DO.OrderItem>(orderItemList, entity_name);
                return;
            }

            
            throw new DalFacade.DO.NotFoundException("orderItem not found");
        }


    }
}
