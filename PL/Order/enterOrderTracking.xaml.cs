using PL.Product;
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
    /// Interaction logic for enterOrderTracking.xaml
    /// </summary>
    public partial class enterOrderTracking : Window
    {
        OrderVM orderVM;
        private BlApi.IBl? bl = BlApi.Factory.get();

        public enterOrderTracking()
        {
            InitializeComponent();
            orderVM = new OrderVM();
            DataContext = orderVM;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderTracking tracking;
            try
            {
                tracking = bl.Order.OrderTrack(orderVM.ID);
            }
            catch(Exception ex)
            {
                MessageBox.Show("order id does not exist");
                orderVM.ID = 0;
                return;
            }

            new Order.orderTracking(tracking).Show();
            orderVM.ID = 0;

        }
    }
}
