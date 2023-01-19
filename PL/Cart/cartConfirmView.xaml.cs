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

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for cartConfirmView.xaml
    /// </summary>
    public partial class cartConfirmView : Window
    {
        private cartVm vm;
        private BlApi.IBl? bl = BlApi.Factory.get();

        public cartConfirmView(cartVm vm1)
        {
            InitializeComponent();
            this.vm = vm1;
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
                Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
