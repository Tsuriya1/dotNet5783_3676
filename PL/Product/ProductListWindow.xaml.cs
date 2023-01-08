using BlApi;
using BO;
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
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        // is it correct?
        private IBl bl = new Bl();
        public ProductListWindow()
        {
            InitializeComponent();
            ProductListReview.ItemsSource = bl.Product.GetProducts();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void ProductListReview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void add_Button_Click(object sender, RoutedEventArgs e) { 
            new Product.AddProductWindow().Show();
            Close();

        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Category choice = (BO.Category)CategorySelector.SelectedItem;
            List<ProductForList?> list= bl.Product.GetProductsByCategory(choice);
            List<ProductForList> listWithoutNull = new List<ProductForList>();
            for(int i = 0; i < list.Count; i++)
            {
                if (list[i].HasValue)
                {
                    listWithoutNull.Add(list[i].Value);
                }
            }
            ProductListReview.ItemsSource = listWithoutNull;

        }

        private void ProductListReview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ProductListReview.SelectedItems.Count == 1)
            {//display the text of selected item

                BO.ProductForList selectedProduct =  (BO.ProductForList)ProductListReview.SelectedItems[0];
                int id = selectedProduct.ID;
                new Product.UpdateAndActionsWindow(id).Show();
                Close();
                //ProductListReview.ItemsSource = bl.Product.GetProducts();


            }

        }

    }

}
