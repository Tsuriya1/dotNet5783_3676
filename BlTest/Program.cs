
//namespace Dal;
using DalFacade;
using Dal;
using System.Collections.Generic;

enum ItemOrderMenu { Add = 1, View, ViewAll, Update, Delete, itemByOrderAndProduct, itemListByOrder };
enum Actions { Add = 1, View, ViewAll, Update, Delete, };
enum StoreMenu { Exit, Product, Order, OrderItem };


class Program
{
    private static IDal? dalList = DalApi.Factory.Get();
    private static StoreMenu menuChoice;
    static void Main(string[] args)
    {

        Console.WriteLine(@"Welcome to the Car Shop");
        do
        {
            Console.WriteLine(@"For any action on Products press 1");
            Console.WriteLine(@"For any action on Orders press 2");
            Console.WriteLine(@"For any action on Order Items press 3");
            Console.WriteLine(@"To exit this menu press 0");
            StoreMenu.TryParse(Console.ReadLine(), out menuChoice);
            try
            {
                switch (menuChoice)
                {
                    case StoreMenu.Product:
                        ProductMenu();
                        break;
                    case StoreMenu.Order:
                        OrderMenu();
                        break;
                    case StoreMenu.OrderItem:
                        OrderItemMenu();
                        break;
                    case StoreMenu.Exit:
                        return;
                    default:
                        throw new Exception("invalid key was pressed. press any key to continue...");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        } while (menuChoice != StoreMenu.Exit);
    }

    static void ProductMenu()
    {
        Console.WriteLine(@"To add a product please press 1");
        Console.WriteLine(@"To display a product please press 2");
        Console.WriteLine(@"To display all products please press 3");
        Console.WriteLine(@"To update a product please press 4");
        Console.WriteLine(@"To delete a product please press 5");

        Actions productCode;
        Actions.TryParse(Console.ReadLine(), out productCode);
        switch (productCode)
        {
            case Actions.Add:
                int id;
                Console.WriteLine(@"Enter the new product ID number");
                int.TryParse(Console.ReadLine(), out id);
                Console.WriteLine(@"Enter the title of the new item");
                string title;
                title = Console.ReadLine();
                Console.WriteLine(@"Enter the category you want this item to belong to");
                Console.WriteLine(
                $@"
                Sport = 0
                family =  1
                mini = 2
                Motorcycle = 3
                Exclusive = 4 ");
                int catgory;
                int.TryParse(Console.ReadLine(), out catgory);

                Console.WriteLine(@"Enter the price of added item");
                double addPrice;
                double.TryParse(Console.ReadLine(), out addPrice);
                int addAmount;
                Console.WriteLine(@"Enter the amount in stock of the added item");
                int.TryParse(Console.ReadLine(), out addAmount);

                DalFacade.DO.Product addedProduct = new DalFacade.DO.Product()
                {
                    ID = id,
                    Name = title,
                    Category = (DalFacade.DO.Category)catgory,
                    Price = addPrice,
                    InStock = addAmount
                };
                int addedProductId = dalList.Product.add(addedProduct);
                Console.WriteLine("The Product id: {0}", addedProductId);
                break;
            case Actions.View:
                Console.WriteLine(@"Enter the asked product ID number");
                int.TryParse(Console.ReadLine(), out id);
                DalFacade.DO.Product productToShow = new DalFacade.DO.Product();
                productToShow = dalList.Product.get(id);
                Console.WriteLine(productToShow);
                break;
            case Actions.ViewAll:
                IEnumerable<DalFacade.DO.Product> productsList = dalList.Product.get();
                foreach (var item in productsList)
                {
                    Console.WriteLine(item);
                }

                break;
            case Actions.Update:
                Console.WriteLine(@"Enter the ID number of the product you want to update");
                int.TryParse(Console.ReadLine(), out id);
                DalFacade.DO.Product oldProduct = dalList.Product.get(id);
                Console.WriteLine(oldProduct);
                Console.WriteLine(@"Enter the updated name");
                title = Console.ReadLine();
                Console.WriteLine(@"Enter the updated category");
                Console.WriteLine(
                $@"
                Sport = 0
                family =  1
                mini = 2
                Motorcycle = 3
                Exclusive = 4 ");
                int tmpcatgory;
                int.TryParse(Console.ReadLine(), out tmpcatgory);
                Console.WriteLine(@"Enter The updated price");
                double tmpPrice2;
                double.TryParse(Console.ReadLine(), out tmpPrice2);
                Console.WriteLine(@"Enter the updated amount in stok");
                int.TryParse(Console.ReadLine(), out addAmount);
                DalFacade.DO.Product Update = new DalFacade.DO.Product()
                {
                    ID = id,
                    Name = title,
                    Category = (DalFacade.DO.Category)tmpcatgory,
                    Price = tmpPrice2,
                    InStock = addAmount
                };
                dalList.Product.update(Update);
                break;
            case Actions.Delete:
                Console.WriteLine(@"Enter the ID number of the product you want to remove");
                int.TryParse(Console.ReadLine(), out id);
                dalList.Product.delete(id);
                break;
            default:
                break;
        }
    }
    static void OrderMenu()
    {
        Console.WriteLine(@"To add an Order please press 1");
        Console.WriteLine(@"To display an order please press 2");
        Console.WriteLine(@"To display all orders please press 3");
        Console.WriteLine(@"To update an order please press 4");
        Console.WriteLine(@"To delete an order please press 5");

        Actions orderCode;
        int id;
        string customeName;
        string email;
        string adress;
        DateTime orderCreate;
        DateTime orderShip;
        DateTime delivery;
        Actions.TryParse(Console.ReadLine(), out orderCode);
        switch (orderCode)
        {
            case Actions.Add:
                Console.WriteLine(@"Enter the Customers name you want to add");
                customeName = Console.ReadLine();
                Console.WriteLine(@"Enter the Customers Email Address you want to add");
                email = Console.ReadLine();
                Console.WriteLine(@"Enter the Customers adress you want to add");
                adress = Console.ReadLine();
                orderCreate = DateTime.Now;
                DalFacade.DO.Order addedOrder = new DalFacade.DO.Order()
                {
                    
                    ID = 0,
                    CustumerAdress = adress,
                    CustumerEmail = email,
                    CustumerName = customeName,
                    OrderDate = orderCreate,
                    ShipDate = DateTime.MinValue,
                    DeliveryDate = DateTime.MinValue
                };
                int addedOrderId = dalList.Order.add(addedOrder);
                Console.WriteLine("The Order id: {0}", addedOrderId);
                break;
            case Actions.View:
                Console.WriteLine(@"Enter the ID number of the order that you want to see its details");
                while (!int.TryParse(Console.ReadLine(), out id)) ;
                DalFacade.DO.Order OrderToShow = new DalFacade.DO.Order();
                OrderToShow = dalList.Order.get(id);
                Console.WriteLine(OrderToShow);
                break;
            case Actions.ViewAll:
                IEnumerable<DalFacade.DO.Order> productsList = dalList.Order.get();
                foreach (var item in productsList)
                {
                    Console.WriteLine(item);
                }
                break;
            case Actions.Update:
                int y = 0;
                Console.WriteLine(@"Enter the ID number of the order that you want to update");
                int.TryParse(Console.ReadLine(), out id);
                DalFacade.DO.Order oldOrder = dalList.Order.get(id);
                Console.WriteLine(oldOrder);
                Console.WriteLine(@"Enter the updated customer name");
                customeName = Console.ReadLine();
                Console.WriteLine(@"Enter the updated customer Email Address");
                email = Console.ReadLine();
                Console.WriteLine(@"Enter the updated customer address");
                adress = Console.ReadLine();
                Console.WriteLine(@"Enter the updated order creation date");
                DateTime.TryParse(Console.ReadLine(), out orderCreate);
                Console.WriteLine(@"Enter the new ship date");
                DateTime.TryParse(Console.ReadLine(), out orderShip);
                Console.WriteLine(@"Enter the updated delivery date");
                DateTime.TryParse(Console.ReadLine(), out delivery);

                DalFacade.DO.Order updateOrder = new DalFacade.DO.Order()
                {
                    ID = id,
                    CustumerAdress = adress,
                    CustumerEmail = email,
                    CustumerName = customeName,
                    OrderDate = orderCreate,
                    ShipDate = orderShip,
                    DeliveryDate = delivery
                };
                dalList.Order.update(updateOrder);
                break;
            case Actions.Delete:
                Console.WriteLine(@"Enter the ID number of the order you want to remove");
                int.TryParse(Console.ReadLine(), out id);
                dalList.Order.delete(id);
                break;
            default:
                break;
        }
    }
    static void OrderItemMenu()
    {
        Console.WriteLine(@"To add an Order Item please press 1");
        Console.WriteLine(@"To display an order Item please press 2");
        Console.WriteLine(@"To display all order Items please press 3");
        Console.WriteLine(@"To update an order Item please press 4");
        Console.WriteLine(@"To delete an order Item please press 5");

        ItemOrderMenu OrderItem;
        int id;
        int productId;
        int orderId;
        int Amount;
        double price;
        ItemOrderMenu.TryParse(Console.ReadLine(), out OrderItem);
        switch (OrderItem)
        {
            case ItemOrderMenu.Add:

                Console.WriteLine(@"Enter the ID number of the product you want to add");
                while (!int.TryParse(Console.ReadLine(), out productId)) ;
                Console.WriteLine(@"Enter the Id number of the order that you want to add");
                while (!int.TryParse(Console.ReadLine(), out orderId)) ;
                Console.WriteLine(@"Enter the amount that you want to add");
                while (!int.TryParse(Console.ReadLine(), out Amount)) ;
                Console.WriteLine(@"Enter the price of product");
                while (!double.TryParse(Console.ReadLine(), out price)) ;

                DalFacade.DO.OrderItem addedOrderItem = new DalFacade.DO.OrderItem()
                {
                    ID = 0,///the function add give him id OI
                    ProductId = productId,
                    OrderId = orderId,
                    Amount = Amount,
                    Price = price
                };
                int addedOrderId = dalList.OrderItem.add(addedOrderItem);
                Console.WriteLine("The Order item id: {0}", addedOrderId);
                break;
            case ItemOrderMenu.View:
                Console.WriteLine(@"Enter the ID number of Order Item that you want to see its details");
                while (!int.TryParse(Console.ReadLine(), out orderId)) ;
                DalFacade.DO.OrderItem OrderItemToShow = new DalFacade.DO.OrderItem();
                OrderItemToShow = dalList.OrderItem.get(orderId);
                Console.WriteLine(OrderItemToShow);
                break;
            case ItemOrderMenu.ViewAll:
                IEnumerable<DalFacade.DO.OrderItem> OrdersItemList = dalList.OrderItem.get();
                foreach (var item in OrdersItemList)
                {
                    Console.WriteLine(item);
                }
                break;
            case ItemOrderMenu.Update:
                Console.WriteLine(@"Enter the ID number of the Order that you want to update");
                while (!int.TryParse(Console.ReadLine(), out orderId)) ;
                DalFacade.DO.OrderItem oldItemOrder = dalList.OrderItem.get(orderId);
                Console.WriteLine(oldItemOrder);

                Console.WriteLine(@"Enter the ID number of the product you want to update");
                while (!int.TryParse(Console.ReadLine(), out productId)) ;
                Console.WriteLine(@"Enter the ID number of the order you want to update");
                while (!int.TryParse(Console.ReadLine(), out orderId)) ;
                Console.WriteLine(@"Enter the updated amount");
                while (!int.TryParse(Console.ReadLine(), out Amount)) ;
                Console.WriteLine(@"Enter the updated price");
                while (!double.TryParse(Console.ReadLine(), out price)) ;
                DalFacade.DO.OrderItem updateItemOrder = new DalFacade.DO.OrderItem()
                {
                    ID = (int)oldItemOrder.ID,
                    ProductId = productId,
                    OrderId = orderId,
                    Amount = Amount,
                    Price = price
                };
                dalList.OrderItem.update(updateItemOrder);
                break;
            case ItemOrderMenu.Delete:
                Console.WriteLine(@"Enter the ID number of the Order you want to remove");
                int.TryParse(Console.ReadLine(), out orderId);
                dalList.OrderItem.delete(orderId);
                break;
            case ItemOrderMenu.itemByOrderAndProduct:
                Console.WriteLine(@"Enter the ID number of the Order you want to display");
                int.TryParse(Console.ReadLine(), out orderId);
                Console.WriteLine(@"Enter the ID number of the product you want to display");
                int.TryParse(Console.ReadLine(), out productId);
                DalFacade.DO.OrderItem itemToShowByProductAndOrder = dalList.OrderItem.get(orderId, productId);
                Console.WriteLine(itemToShowByProductAndOrder);
                break;
            case ItemOrderMenu.itemListByOrder:
                Console.WriteLine(@"Enter the ID number of the Order you want to see Item Orders related to");
                int.TryParse(Console.ReadLine(), out orderId);
                DalFacade.DO.Order myorder = dalList.Order.get(orderId);
                IEnumerable<DalFacade.DO.OrderItem?> ordersItemsList = dalList.OrderItem.get(myorder);
                foreach (DalFacade.DO.OrderItem item in ordersItemsList)
                    Console.WriteLine(item);
                break;
            default:
                break;
        }
    }
}
