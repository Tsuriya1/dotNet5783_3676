using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for orderTracking.xaml
    /// </summary>
    public partial class orderTracking : Window
    {

        OrderVM vm;
        private BlApi.IBl? bl = BlApi.Factory.get();
        BO.OrderTracking orderTrackingItem;
        public orderTracking(BO.OrderTracking orderTrackingItem)
        {
            InitializeComponent();

            vm = new OrderVM();
            DataContext = vm;
            vm.BO_Order =bl.Order.getOrderDetails(orderTrackingItem.ID);
            vm.ID = orderTrackingItem.ID;

            
            vm.OrderTrackingDesciption = new ObservableCollection<Tuple<DateTime?, string?>?>(orderTrackingItem.description);
            

            if (orderTrackingItem.Status.HasValue)
            {
                vm.Status = orderTrackingItem.Status.Value;
            }
            else
            {
                MessageBox.Show("there is no Status, please contact the menagere to fix it");
                Close();
                return;
            }
        }

        private void order_Click(object sender, RoutedEventArgs e)
        {
            new Order.orderForViewOnly(this.vm).Show();
        }
    }
}
