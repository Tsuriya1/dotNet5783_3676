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

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductItemListView.xaml
    /// </summary>
    public partial class ProductItemListView : Window
    {
        BO.Cart cart;
        ProductVM vm;
        bool add_click = false;
        public ProductItemListView(BO.Cart cart = null)
        {
            InitializeComponent();
            if(cart == null)
            {
                this.cart = new BO.Cart();
            }
            else
            {
                this.cart = cart;
            }
            
            vm = new ProductVM();
            vm.Mycart = this.cart;
            DataContext = vm;
        }

        private void add_Button_Click(object sender, RoutedEventArgs e)
        {
            new Cart.cartView(vm.Mycart).Show();
            add_click = true;
            Close();
        }

        private void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            if (add_click)
            {
                return;
            }
            new MainWindow().Show();
        }
    }
}
