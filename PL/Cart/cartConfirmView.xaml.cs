using PL.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for cartConfirmView.xaml
    /// </summary>
    public partial class cartConfirmView : Window
    {
        private cartVm vm;
        bool check_out_click = false;
        Product.ProductItemListView? itemListView;
        private BlApi.IBl? bl = BlApi.Factory.get();

        public cartConfirmView(cartVm vm1, Product.ProductItemListView? itemList=null)
        {
            InitializeComponent();
            this.vm = vm1;
            itemListView = itemList;
            DataContext = vm1;
            vm.TotalPrice = vm.Cart.TotalPrice;
        }

        private void Checkout_button(object sender, RoutedEventArgs e)
        {
            try
            {
                
                vm.Cart.CustomerName = vm.CustomerName;
                vm.Cart.CustomerAddress = vm.CustomerAddress;
                vm.Cart.CustomerEmail = vm.CustomerEmail;
                bl.Cart.Confirm(vm.Cart);
                MessageBox.Show("We got your order :)" );
                check_out_click = true;
                if (itemListView != null)
                {
                    itemListView.Close();
                }
                Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            if (check_out_click)
            {
                new MainWindow().Show();
                return;
            }
            new cartView(vm.Cart).Show();
        }
    }
}
