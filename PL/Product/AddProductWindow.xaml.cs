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
using System.Windows.Shapes;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        private IBl bl = new Bl();

        public AddProductWindow()
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));

        }
        private double convertDoublePos(string num)
        {
            double ret;
            bool success = double.TryParse(num, out ret);
            if (!success)
            {
                Console.WriteLine("not a number");
                return -1;
            }
            return ret;
        }
        private int convertIntPos(string num)
        {
            int ret;
            bool success = int.TryParse(num, out ret);
            if (!success)
            {
                return -1;
            }
            return ret;
        }
        private void Add_Button_Click(object sender, RoutedEventArgs e) 
        {
            BO.Product product = new BO.Product();
            product.Category = (BO.Category)CategorySelector.SelectedItem;
            product.ID = convertIntPos(Id.Text);
            product.Name = Name.Text;
            product.Price = convertDoublePos(Price.Text);
            product.InStock = convertIntPos(In_Stock.Text);

            bl.Product.addProduct(product);
            new Product.ProductListWindow().Show();

            Close();    

        }
    }
}
