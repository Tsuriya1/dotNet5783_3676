using BlApi;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Manager_Click(object sender, RoutedEventArgs e)
        {
            new manager().Show();
            Close();

        }

        private void New_Order_Click(object sender, RoutedEventArgs e)
        {
            new Product.ProductItemListView().Show();
            Close();
        }

        private void Track_Your_Order_Click_2(object sender, RoutedEventArgs e)
        {
            new Order.enterOrderTracking().Show();
            Close();
        }

        private void simulation_Click(object sender, RoutedEventArgs e)
        {
            new simulatorWindow().Show();
        }
    }
}
