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
            // loading from file
            List<DalFacade.DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DalFacade.DO.OrderItem>(entity_name);
            XElement Config = XMLTools.LoadListFromXMLElement("Config");
            orderItem.ID = (int)Config.Element("OrderItemIdx");
            // saving and promoting the ID by 1
            Config.Element("OrderItemIndex")?.SetValue(orderItem.ID+1);

            XMLTools.SaveListToXMLElement(Config, "Config");

            orderItemList.Add(orderItem);
            XMLTools.SaveListToXMLSerializer<DalFacade.DO.OrderItem>(orderItemList, entity_name);
            return orderItem.ID;
        }
        // get by ID number
        public DalFacade.DO.OrderItem get(int ID)
        {
            // load from file
            List<DalFacade.DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DalFacade.DO.OrderItem>(entity_name);
            var item = from orderItem in orderItemList where (orderItem.HasValue && orderItem.Value.ID == ID) select orderItem.Value;
            if (item != null && item.Count() > 0)
            {
                return item.First();
            }
            throw new DalFacade.DO.NotFoundException("orderItem not found");
            
        }
        // get by boolian function
        public OrderItem getObject(Func<OrderItem, bool>? func)
        {
            List<DalFacade.DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DalFacade.DO.OrderItem>(entity_name);

            // no parameter is given for the boolian function
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
        // get by order ID number and product ID number
        public DalFacade.DO.OrderItem get(int productId, int orderId)
        {
            // load from the xml file
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
        // get by two fonctions
        public IEnumerable<OrderItem> get(Func<OrderItem, bool>? func = null)
        {
            // load from the xml file
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

        // get by an order
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

        // delete from the xml
        public void delete(int ID)
        {
            // find the asked order
            List<DalFacade.DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DalFacade.DO.OrderItem>(entity_name);
            // remove it
            int count = orderItemList.RemoveAll(x => (x.HasValue && x.Value.ID == ID));
            if (count == 0)
            {
                throw new DalFacade.DO.NotFoundException("orderItem not found");
            }
            // save back
            XMLTools.SaveListToXMLSerializer<DalFacade.DO.OrderItem>(orderItemList, entity_name);



        }
        // update (swiching between the new and old one)
        public void update(DalFacade.DO.OrderItem orderItem1)
        {
            List<DalFacade.DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DalFacade.DO.OrderItem>(entity_name);

            var item = from orderItem in orderItemList
                       where ((orderItem.HasValue) && (orderItem.Value.ID == orderItem1.ID))
                       select orderItem.Value;
            if (item != null && item.Count() > 0)
            {
                // removing the previous
                delete(orderItem1.ID);
                orderItemList.RemoveAll(x => (x.HasValue && x.Value.ID == orderItem1.ID));
                // adding the new
                orderItemList.Add(orderItem1);
                // save changes to xml file
                XMLTools.SaveListToXMLSerializer<DalFacade.DO.OrderItem>(orderItemList, entity_name);
                return;
            }

            
            throw new DalFacade.DO.NotFoundException("orderItem not found");
        }


    }
}
