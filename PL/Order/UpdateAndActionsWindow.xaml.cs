using BlApi;
using PL.Product;
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
    /// Interaction logic for UpdateAndActionsWindow.xaml
    /// </summary>
    public partial class UpdateAndActionsWindow : Window
    {
        OrderVM orderVM;
        private IDal Dal = DalApi.Factory.Get();
        BO.OrderForList order = new BO.OrderForList();
        private IBl bl = BlApi.Factory.get();


        public UpdateAndActionsWindow(int Id, OrderVM vm)
        {

            InitializeComponent();
            orderVM = vm;
            DataContext = vm;
            try
            {
                order = bl.Order.GetOrderForList(Id);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
            vm.Order= order;

        }
        

        private void updateShippingClick(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Order.updateShipping(order.ID);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void updateSupplyClick(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Order.updateSupply(order.ID);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }

}
