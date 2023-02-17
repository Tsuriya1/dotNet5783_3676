using PL.Product;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for cartView.xaml
    /// </summary>
    public partial class cartView : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.get();
        private cartVm vm;
        bool confirm_was_click = false;
        BO.Cart cart;
        Product.ProductItemListView? productItemList;

        public cartView(BO.Cart cart1,Product.ProductItemListView? productItemListView=null)
        {
            InitializeComponent();
            cart = cart1;
            productItemList = productItemListView;
            vm = new Cart.cartVm(cart1);
            DataContext = vm;
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (vm.SelectedItem == null)
                {
                    MessageBox.Show("no item selected");
                    return;
                }
                bl.Cart.deleteItemFromCart(cart, vm.SelectedItem.Value.ID);
                vm.OrderItems = new ObservableCollection<BO.OrderItem?>(cart.Items);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private void update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(vm.SelectedItem == null)
                {
                    MessageBox.Show("no item selected");
                    return;
                }
                bl.Cart.Update(cart, vm.SelectedItem.Value.ProductId,vm.Amount);
                vm.OrderItems = new ObservableCollection<BO.OrderItem?>(cart.Items);

            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private void confirm_Click(object sender, RoutedEventArgs e)
        {
            new Cart.cartConfirmView(this.vm, productItemList).Show();
            confirm_was_click = true;
            Close();
        }

        private void DataWindow_Closing(object sender, CancelEventArgs e)
        {

            if (confirm_was_click)
            {
                return;
            }
            new Product.ProductItemListView(this.cart).Show();
        }

    }
}
