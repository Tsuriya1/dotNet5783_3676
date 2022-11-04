using System;
namespace Dal
{
    public class testClass
    {

        private static DalProduct dalProduct = new DalProduct();
        private DalOrder dalOrder = new DalOrder();
        private DalOrderItem dalOrderItem = new DalOrderItem();

        private static void actionProduct()
        {
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

