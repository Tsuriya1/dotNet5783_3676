using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Simulator
{
    public static class Simulator
    {
        private static readonly BlApi.IBl? bl = BlApi.Factory.get();
        private static volatile bool active;
        private static Random rnd = new Random();

        //new event that notify that the status changed that get funcs that get order time and int
        public delegate void NotifyDate(BO.Order? order, DateTime date,int delay);
        public static event NotifyDate reportUpdateDate;

        public static void registerUpdateDate(NotifyDate observer)
        {
            reportUpdateDate+=observer;
        }
        public static void unRegisterUpdateDate(NotifyDate observer)
        {
            reportUpdateDate -= observer;
        }

        //event that notify to all his listeneres that the sim complate his work
        public delegate void Notify();
        public static event Notify reportTaskCompleted;

        public static void registerTaskCompleted(Notify observer)
        {
            reportTaskCompleted += observer;
        }
        public static void unRegisterreportTaskCompleted(Notify observer)
        {
            reportTaskCompleted -= observer;
        }

        public static event Notify reportSimCompleted;

        public static void registerSimCompleted(Notify observer)
        {
            reportSimCompleted += observer;
        }
        public static void unRegisterreportSimCompleted(Notify observer)
        {
            reportSimCompleted -= observer;
        }

        //the func go over all the order by oldest date time and update the status for each one
        static private void run_sim()
        {
            //while the sim is work
            while (active)
            {
                ///get new order
                int? order_id= bl.Order.getOldestOrder();
                if (order_id.HasValue)
                {
                    //update the status by get random number of seconds 
                    BO.Order order =  bl.Order.getOrderDetails(order_id.Value);
                    int delay_time = rnd.Next(3,11);
                    DateTime new_date = DateTime.Now.AddSeconds(delay_time);
                    reportUpdateDate?.Invoke(order, new_date,delay_time);
                    Thread.Sleep(delay_time*1000);
                    reportTaskCompleted?.Invoke();
                    //update the data base.
                    if(order.ShipDate == null)
                    {
                        bl.Order.updateShipping(order_id.Value);
                    }
                    else
                    {
                        bl.Order.updateSupply(order_id.Value);
                    }

                }
                else
                {
                    reportUpdateDate?.Invoke(null, new DateTime(),0);
                }
                Thread.Sleep(1000);
            }
        }

        //the func create new thread that changes orders status
        static public void activate()
        {
            new Thread(() =>
            {
                active = true;
                run_sim();
                reportSimCompleted?.Invoke();
            }).Start();
        }

        //finish sim windows.
        public static void stop_simulation()
        {
            active = false;
        }
    }
}
