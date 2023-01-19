﻿using System;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for manager.xaml
    /// </summary>
    public partial class manager : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.get();
        public manager()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Product.ProductListWindow().Show();
            Close();
        }
        private void Order_button(object sender, RoutedEventArgs e)
        {
            new Order.OrdersView().Show();
            Close();
        }
    }
}
