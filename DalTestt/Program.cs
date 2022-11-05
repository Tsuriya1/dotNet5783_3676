using System;
using System.Globalization;
using DalFacade.DO;

namespace Dal
{
    public class testClass
    {

        private static DalProduct dalProduct = new DalProduct();
        private static DalOrder dalOrder = new DalOrder();
        private static DalOrderItem dalOrderItem = new DalOrderItem();

        private static int convertIntPos(string num)
        {
            int ret;
            bool success = int.TryParse(num, out ret);
            if (!success)
            {
                Console.WriteLine("not a number");
                return -1;
            }
            return ret;
        }

        private static double convertDoublePos(string num)
        {
            double ret;
            bool success = double.TryParse(num, out ret);
            if (!success)
            {
                Console.WriteLine("not a number");
                return -1;
            }
            return ret;
        }


        private static void aProduct()
        {
            DalFacade.DO.Product product = new DalFacade.DO.Product();
            Console.WriteLine("enter ID");
            string IDStr = Console.ReadLine();
            int ID = convertIntPos(IDStr);
            if (ID == -1)
            {
                return;
            }

            if (ID < 100000)
            {
                Console.WriteLine("not a valid id");

                return;
            }
            product.ID = ID;

            Console.WriteLine("enter name");
            string name = Console.ReadLine();
            product.Name = name;

            Console.WriteLine("enter price");
            string priceStr = Console.ReadLine();
            double price = convertDoublePos(priceStr);
            if (price == -1)
            {
                return;
            }
            product.Price = price;

            Console.WriteLine("enter category:" +
                "Sport,\n        Family,\n        Mini,\n        Motorcycle,\n        Exclusive");

            string cat = Console.ReadLine();
            if (cat == "Sport")
            {
                product.Category = DalFacade.DO.Category.Sport;
            }
            else if (cat == "Family")
            {
                product.Category = DalFacade.DO.Category.Family;
            }
            else if (cat == "Mini")
            {
                product.Category = DalFacade.DO.Category.Mini;
            }
            else if (cat == "Motorcycle")
            {
                product.Category = DalFacade.DO.Category.Motorcycle;
            }
            else
            {
                product.Category = DalFacade.DO.Category.Exclusive;
            }

            Console.WriteLine("enter amount in stock");
            string stockStr = Console.ReadLine();
            int stock = convertIntPos(stockStr);
            if (stock == -1)
            {
                return;
            }

            if (stock < 0)
            {
                Console.WriteLine("amount less than a 0");

                return;
            }
            product.InStock = stock;

            try
            {

                Console.WriteLine(dalProduct.add(product));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        private static void dProduct(int ID)
        {
            DalFacade.DO.Product product = new DalFacade.DO.Product();
            
         
            product.ID = ID;

            Console.WriteLine("enter name");
            string name = Console.ReadLine();
            product.Name = name;

            Console.WriteLine("enter price");
            string priceStr = Console.ReadLine();
            int price = convertIntPos(priceStr);
            if (price == -1)
            {
                return;
            }
            product.Price = price;

            Console.WriteLine("enter category:" +
                "Sport,\n        Family,\n        Mini,\n        Motorcycle,\n        Exclusive");

            string cat = Console.ReadLine();
            if (cat == "Sport")
            {
                product.Category = DalFacade.DO.Category.Sport;
            }
            else if (cat == "Family")
            {
                product.Category = DalFacade.DO.Category.Family;
            }
            else if (cat == "Mini")
            {
                product.Category = DalFacade.DO.Category.Mini;
            }
            else if (cat == "Motorcycle")
            {
                product.Category = DalFacade.DO.Category.Motorcycle;
            }
            else
            {
                product.Category = DalFacade.DO.Category.Exclusive;
            }

            Console.WriteLine("enter amount in stock");
            string stockStr = Console.ReadLine();
            int stock = convertIntPos(stockStr);
            if (stock == -1)
            {
                return;
            }

            if (stock < 0)
            {
                Console.WriteLine("amount less than a 0");

                return;
            }
            product.InStock = stock;

            try
            {

                dalProduct.update(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }



        private static void actionProduct()
        {
            Console.WriteLine("enter a letter");
            Console.WriteLine(" a. add");
            Console.WriteLine(" b.get by ID");
            Console.WriteLine(" c.get all Products");
            Console.WriteLine(" d.update Product");
            Console.WriteLine(" e.delete Product");
            string option = Console.ReadLine();

            if (option == "a")
            {
                aProduct();
            }else if (option == "b")
            {
                
                try
                {
                    Console.WriteLine("enter ID");
                    string IDStr = Console.ReadLine();
                    int ID = convertIntPos(IDStr);
                    if (ID == -1)
                    {
                        return;
                    }
                    Console.WriteLine(dalProduct.get(ID));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }else if (option == "c")
            {
                DalFacade.DO.Product[] products = dalProduct.get();
                foreach(DalFacade.DO.Product p in products)
                {
                    Console.WriteLine(p);
                }
            }
            else if (option == "d")
            {
                try
                {
                    Console.WriteLine("enter ID");
                    string IDStr = Console.ReadLine();
                    int ID = convertIntPos(IDStr);
                    if (ID == -1)
                    {
                        return;
                    }
                    Console.WriteLine(dalProduct.get(ID));
                    dProduct(ID);


                }catch(Exception e)
                {
                    return;
                }
            }
            else if (option == "e")
            {
                try
                {
                    Console.WriteLine("enter ID");
                    string IDStr = Console.ReadLine();
                    int ID = convertIntPos(IDStr);
                    if (ID == -1)
                    {
                        return;
                    }
                    dalProduct.delete(ID);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }


        }

        


        private static DateTime convertDate(string dateStr)
        {
            CultureInfo cult;
            DateTimeStyles styles;
            DateTime result;
            cult = CultureInfo.CreateSpecificCulture("en-US");
            styles = DateTimeStyles.None;
            if(DateTime.TryParse(dateStr, cult,styles,out result))
            {
                return result;
            }
            else
            {
                throw new Exception("unable to convert to date time");
            }
        }

        private static void aOrder()
        {
            DalFacade.DO.Order order = new DalFacade.DO.Order();
            Console.WriteLine("enter ID");
            string IDStr = Console.ReadLine();
            int ID = convertIntPos(IDStr);
            if (ID == -1)
            {
                return;
            }

        
            order.ID = ID;

            Console.WriteLine("enter customer name");
            string name = Console.ReadLine();
            order.CustumerName = name;

            Console.WriteLine("enter customer email");
            string email = Console.ReadLine();
            order.CustumerEmail = email;

            Console.WriteLine("enter customer adress");
            string adress = Console.ReadLine();
            order.CustumerAdress = adress;

            Console.WriteLine("enter order date");
            string dateStr = Console.ReadLine();
            try { order.OrderDate = convertDate(dateStr); }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            Console.WriteLine("enter ship date");
            dateStr = Console.ReadLine();
            try { order.ShipDate = convertDate(dateStr); }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            Console.WriteLine("enter delivery date");
            dateStr = Console.ReadLine();
            try { order.DeliveryDate = convertDate(dateStr); }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            Console.WriteLine(dalOrder.add(order));
        }


        private static void dOrder(int ID)
        {
            DalFacade.DO.Order order = new DalFacade.DO.Order();
            order.ID = ID;

            Console.WriteLine("enter customer name");
            string name = Console.ReadLine();
            order.CustumerName = name;

            Console.WriteLine("enter customer email");
            string email = Console.ReadLine();
            order.CustumerEmail = email;

            Console.WriteLine("enter customer adress");
            string adress = Console.ReadLine();
            order.CustumerAdress = adress;

            Console.WriteLine("enter order date");
            string dateStr = Console.ReadLine();
            try { order.OrderDate = convertDate(dateStr); }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("enter ship date");
            dateStr = Console.ReadLine();
            try { order.ShipDate = convertDate(dateStr); }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("enter delivery date");
            dateStr = Console.ReadLine();
            try { order.DeliveryDate = convertDate(dateStr); }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            dalOrder.update(order);
        }

        private static void actionOrder()
        {
            Console.WriteLine("enter a letter");
            Console.WriteLine(" a. add");
            Console.WriteLine(" b.get by ID");
            Console.WriteLine(" c.get all orders");
            Console.WriteLine(" d.update order");
            Console.WriteLine(" e.delete order");
            string option = Console.ReadLine();
            if (option == "a") {
                aOrder();
            }
            else if (option == "b") {
                try
                {
                    Console.WriteLine("enter ID");
                    string IDStr = Console.ReadLine();
                    int ID = convertIntPos(IDStr);
                    if (ID == -1)
                    {
                        return;
                    }
                    Console.WriteLine(dalOrder.get(ID));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else if (option == "c")
            {
                DalFacade.DO.Order[] orders = dalOrder.get();
                foreach (DalFacade.DO.Order o in orders)
                {
                    if (o.CustumerName != null)
                    {
                        Console.WriteLine(o);
                    }
                }

            }
            else if (option == "d")
            {
                try
                {
                    Console.WriteLine("enter ID");
                    string IDStr = Console.ReadLine();
                    int ID = convertIntPos(IDStr);
                    if (ID == -1)
                    {
                        return;
                    }
                    Console.WriteLine(dalOrder.get(ID));
                    dOrder(ID);


                }
                catch (Exception e)
                {
                    return;
                }
            }
            else {
                try
                {
                    Console.WriteLine("enter ID");
                    string IDStr = Console.ReadLine();
                    int ID = convertIntPos(IDStr);
                    if (ID == -1)
                    {
                        return;
                    }
                    dalOrder.delete(ID);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private static void aOrderItem()
        {
            DalFacade.DO.OrderItem orderItem = new DalFacade.DO.OrderItem();
            Console.WriteLine("enter ID");
            string IDStr = Console.ReadLine();
            int ID = convertIntPos(IDStr);
            if (ID == -1)
            {
                return;
            }
            orderItem.ID = ID;

            Console.WriteLine("enter product ID");
            IDStr = Console.ReadLine();
            int productID = convertIntPos(IDStr);
            if (productID == -1)
            {
                return;
            }
            DalFacade.DO.Product product;
            try
            {
                product =  dalProduct.get(productID);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;

            }
            orderItem.ProductId = productID;
            orderItem.Price = product.Price;

            Console.WriteLine("enter order ID");
            IDStr = Console.ReadLine();
            int orderID = convertIntPos(IDStr);
            if (orderID == -1)
            {
                return;
            }
            try
            {
                dalOrder.get(orderID);
                orderItem.OrderId = orderID;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;

            }

            Console.WriteLine("enter amount");
            string amountStr = Console.ReadLine();
            int amount = convertIntPos(amountStr);
            if (amount == -1)
            {
                return;
            }
            orderItem.Amount = amount;
            product.InStock = product.InStock - amount;
            if (product.InStock < 0)
            {
                Console.WriteLine("out of stock");
                return;
            }
            if (product.InStock == 0)
            {
                try
                {
                    dalProduct.delete(product.ID);
                }
                catch
                {
                    return;
                }
            }
            try {

                dalProduct.update(product);
            }
            catch (Exception e)
            {
                return;

            }

            dalOrderItem.add(orderItem);


        }


        private static void dOrderItem(int ID,DalFacade.DO.OrderItem oldOrderItem)
        {
            DalFacade.DO.OrderItem orderItem = new DalFacade.DO.OrderItem();
            orderItem.ID = ID;

            Console.WriteLine("enter product ID");
            string IDStr = Console.ReadLine();
            int productID = convertIntPos(IDStr);
            if (productID == -1)
            {
                return;
            }
            DalFacade.DO.Product product;
            try
            {
                product = dalProduct.get(productID);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;

            }
            orderItem.ProductId = productID;
            orderItem.Price = product.Price;

            Console.WriteLine("enter order ID");
            IDStr = Console.ReadLine();
            int orderID = convertIntPos(IDStr);
            if (orderID == -1)
            {
                return;
            }
            try
            {
                dalOrder.get(orderID);
                orderItem.OrderId = orderID;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;

            }

            Console.WriteLine("enter amount");
            string amountStr = Console.ReadLine();
            int amount = convertIntPos(amountStr);
            if (amount == -1)
            {
                return;
            }
            orderItem.Amount = amount;
            int diff = oldOrderItem.Amount - orderItem.Amount;
            product.InStock += diff;

            if (product.InStock < 0)
            {
                Console.WriteLine("out of stock");
                return;
            }
            if (product.InStock == 0)
            {
                try
                {
                    dalProduct.delete(product.ID);
                }
                catch
                {
                    return;
                }
            }
            try
            {

                dalProduct.update(product);
            }
            catch (Exception e)
            {
                return;

            }

            dalOrderItem.update(orderItem);


        }



        private static void actionOrderItem()
        {
            Console.WriteLine("enter a letter");
            Console.WriteLine(" a. add");
            Console.WriteLine(" b.get by ID");
            Console.WriteLine(" c.get all order Items");
            Console.WriteLine(" d.update order Item");
            Console.WriteLine(" e.delete order Item");
            Console.WriteLine(" f.get by order Item id and order id");
            Console.WriteLine(" g.get by order id");
            string option = Console.ReadLine();
            if (option == "a")
            {
                aOrder();
            }
            else if (option == "b")
            {
                try
                {
                    Console.WriteLine("enter ID");
                    string IDStr = Console.ReadLine();
                    int ID = convertIntPos(IDStr);
                    if (ID == -1)
                    {
                        return;
                    }
                    Console.WriteLine(dalOrderItem.get(ID));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else if (option == "c")
            {
                DalFacade.DO.OrderItem[] orderItems = dalOrderItem.get();
                foreach (DalFacade.DO.OrderItem o in orderItems)
                {
                    Console.WriteLine(o);
                }

            }
            else if (option == "d")
            {
                try
                {
                    Console.WriteLine("enter ID");
                    string IDStr = Console.ReadLine();
                    int ID = convertIntPos(IDStr);
                    if (ID == -1)
                    {
                        return;
                    }
                    DalFacade.DO.OrderItem orderItem = dalOrderItem.get(ID);
                    Console.WriteLine(orderItem);
                    dOrderItem(ID,orderItem);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return;
                }
            }
            else if(option=="e")
            {
                try
                {
                    Console.WriteLine("enter ID");
                    string IDStr = Console.ReadLine();
                    int ID = convertIntPos(IDStr);
                    if (ID == -1)
                    {
                        return;
                    }
                    dalOrderItem.delete(ID);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return;
                }
            }
            else if (option == "f")
            {
                Console.WriteLine("enter product ID");
                string IDStr = Console.ReadLine();
                int productID = convertIntPos(IDStr);
                if (productID == -1)
                {
                    return;
                }
                Console.WriteLine("enter order ID");
                IDStr = Console.ReadLine();
                int orderID = convertIntPos(IDStr);
                if (orderID == -1)
                {
                    return;
                }
                try
                {
                    DalFacade.DO.Product product = dalProduct.get(productID);
                    DalFacade.DO.Order order = dalOrder.get(orderID);
                    Console.WriteLine(dalOrderItem.get(product, order));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return;
                }
            }
            else if (option == "g")
            {
                Console.WriteLine("enter order ID");
                string IDStr = Console.ReadLine();
                int orderID = convertIntPos(IDStr);
                if (orderID == -1)
                {
                    return;
                }
                try
                {
                    DalFacade.DO.Order order = dalOrder.get(orderID);
                    DalFacade.DO.OrderItem[] orderItems = dalOrderItem.get(order);
                    foreach (DalFacade.DO.OrderItem o in orderItems)
                    {
                        Console.WriteLine(o);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return;
                }
            }
        }



        static public void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("enter a number");
                Console.WriteLine(" 0. exit");
                Console.WriteLine(" 1.checks Product");
                Console.WriteLine(" 2.checks Order");
                Console.WriteLine(" 3.checks OrderItem");
                int option;
                string optionString = Console.ReadLine();
                bool success = int.TryParse(optionString, out option);
                if (success)
                {
                    switch (option)
                    {
                        case 0:
                            return;
                        case 1:
                            actionProduct();
                            break;
                        case 2:
                            actionOrder();
                            break;
                        case 3:
                            actionOrderItem();
                            break;
                        default:
                            Console.WriteLine("option is not valid, choose a number between 0-3");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("option is not valid, choose a number between 0-3");

                }
            }

        }
    }
}

