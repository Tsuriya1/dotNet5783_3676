using System.Diagnostics;
using DalFacade.DO;
using DalFacade.DalApi;

namespace Dal;



//namespace Dal; System.Collections.Generic;
internal static class DataSource
{
    static internal int readOnly;

    static internal List<DalFacade.DO.Order> ordersList;
    //static internal DalFacade.DO.Order[] ordersList = new DalFacade.DO.Order[100];

    static internal List<DalFacade.DO.OrderItem> orderItemList = new List<DalFacade.DO.OrderItem>();
    //static internal DalFacade.DO.OrderItem[] ordersItemList = new DalFacade.DO.OrderItem[200];

    static internal List<DalFacade.DO.Product> productList;
    //static internal DalFacade.DO.Product[] productList = new DalFacade.DO.Product[50];

    internal static class Config{
        //static internal int orderInx = 0;
        //static internal int productInx = 0;
        //static internal int orderItemInx = 0;
        static private int currentOrderID = 0;
        static private int currentOrderItemID = 0;

        static public int getOrderId()
        {
            int cur = currentOrderID;
            currentOrderID++;
            return cur;
        }

        static public int getOrderItemId()
        {
            int cur = currentOrderItemID;
            currentOrderItemID++;
            return cur;
        }
    }

    static DataSource()
    {
        s_Initialize();
    }

    static private void addProduct(DalFacade.DO.Product product)
    {
        DalProduct dalProduct = new DalProduct();
        dalProduct.add(product);
    }

    static private void addOrder(DalFacade.DO.Order order)
    {
        DalOrder dalOrder = new DalOrder();
        dalOrder.add(order);
    }

    static private void addOrderItem(DalFacade.DO.OrderItem item)
    {
        DalOrderItem dalOrderItem = new DalOrderItem();
        dalOrderItem.add(item);
    }


    static private void createProducts()
    {
        int firstId = 100000;
        string[] names = {"toyota corolla","mini minor","mercedes jeep","ford mustang","hyunday i25","toyota motorcycle","kia picanto","honda civic","honda motorcycle","audi jeep" };
        DalFacade.DO.Category[] categories = {DalFacade.DO.Category.Family,DalFacade.DO.Category.Mini,DalFacade.DO.Category.Exclusive,DalFacade.DO.Category.Sport,DalFacade.DO.Category.Family,DalFacade.DO.Category.Motorcycle,DalFacade.DO.Category.Mini,DalFacade.DO.Category.Family,DalFacade.DO.Category.Motorcycle,DalFacade.DO.Category.Exclusive};
        double[] prices = {140000,70000,450000,200000,110000,40000,80000,110000,55000,350000 };
        int[] inStocks = {4,8,10,5,5,7,9,11,6,8};

        for(int i = 0; i < 10; i++) {
            DalFacade.DO.Product product = new DalFacade.DO.Product();
            product.ID = firstId+i;
            product.Name = names[i];
            product.Category = categories[i];
            product.Price = prices[i];
            product.InStock = inStocks[i];
            addProduct(product);
            
        }
     
    }

    static private void createOrders()
    {
        int firstId = 1;
        string[] names = {"avi","dani","lior","roni","reut","tal","ron","ben","david","dan","yafa","kai","shon","nir","lili","bar","oz","or","dov","nir" };
        string[] Emails = { "avi@gmail.com", "dani@gmail.com", "lior@gmail.com", "roni@gmail.com", "reut@gmail.com", "tal@gmail.com", "ron@gmail.com", "ben@gmail.com", "david@gmail.com", "dan@gmail.com", "yafa@gmail.com", "kai@gmail.com", "shon@gmail.com", "nir@gmail.com", "lili@gmail.com", "bar@gmail.com", "oz@gmail.com", "or@gmail.com", "dov@gmail.com", "nir@gmail.com" };
    
        string[] adress = {"tel aviv","jerusalem","modiin","jaffa","yavne"};
        DateTime orderDate = new DateTime(2021, 5, 1, 8, 30, 52);
        DateTime shipDate = new DateTime(2021, 7, 1, 8, 30, 52);
        DateTime deliveryDate = new DateTime(2021, 10, 1, 8, 30, 52);
        int numOrders = 20;
        DalFacade.DO.Order[] orders = new DalFacade.DO.Order[numOrders];

        
        for (int i = 0; i < numOrders; i++)
        {
            orders[i].ID = firstId;
            orders[i].CustumerName = names[i];
            orders[i].CustumerEmail = Emails[i];
            orders[i].CustumerAdress = adress[i % 5];
            orders[i].OrderDate = orderDate;
            orders[i].ShipDate = DateTime.MinValue;
            orders[i].DeliveryDate = DateTime.MinValue;


        }
        Random rnd = new Random();
        for (int i = 0; i < numOrders * 0.8; i++)
        {
            orders[i].ShipDate = orders[i].OrderDate+ new TimeSpan(rnd.Next(1,48), 0, 0);
        }

        for (int i = 0; i < numOrders * 0.8*0.6; i++)
        {
            orders[i].DeliveryDate = orders[i].ShipDate+ new TimeSpan(rnd.Next(1, 48), 0, 0);
        }

        for(int i = 0; i < 20; i++)
        {
            addOrder(orders[i]);
        }
    }


    static private void createOrdersItem()
    {
        int firstId = 1;
        int numoforderItems = 60;
        for (int i = 0; i < numoforderItems / 2; i++)
        {
            DalFacade.DO.OrderItem orderItems = new DalFacade.DO.OrderItem();
            orderItems.ID = firstId;
            orderItems.OrderId = ordersList[i].ID;
            orderItems.ProductId = productList[0].ID;
            orderItems.Price = productList[0].Price;
            orderItems.Amount = 1;

            addOrderItem(orderItems);

            orderItems.ID = firstId;
            orderItems.OrderId = ordersList[i].ID;
            orderItems.ProductId = productList[1].ID;
            orderItems.Price = productList[1].Price;
            orderItems.Amount = 2;
            addOrderItem(orderItems);
        }
    }



    static private void s_Initialize()
    {
        createProducts();
        createOrders();
        createOrdersItem();
    }
        
   
}

