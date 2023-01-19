using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for orderForViewOnly.xaml
    /// </summary>
    public partial class orderForViewOnly : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.get();
        void createItemString(OrderVM orderVM)
        {
            string itemString = "items:\n ";
            if (orderVM.BO_Order.Items != null)
            {
                foreach (var item in orderVM.BO_Order.Items)
                {
                    if (item != null)
                    {
                        itemString += item.ToString();
                        itemString += " ";
                    }

                }
                orderVM.StringItem = itemString;
            }
            
        }
        public orderForViewOnly(OrderVM orderVM)
        {
            InitializeComponent();
            createItemString(orderVM);
            DataContext = orderVM;

        }
    }
}
