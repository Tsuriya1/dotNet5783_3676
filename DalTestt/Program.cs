using System;
using DalFacade.DO;

namespace Dal
{
    public class testClass
    {

        private static DalProduct dalProduct = new DalProduct();
        private DalOrder dalOrder = new DalOrder();
        private DalOrderItem dalOrderItem = new DalOrderItem();

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

                Console.WriteLine(dalProduct.add(product));
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


        }

        private static void actionOrder()
        {

        }

        private static void actionOrderItem()
        {

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

