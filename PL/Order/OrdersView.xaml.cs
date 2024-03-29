﻿using PL.Product;
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

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrdersView.xaml
    /// </summary>
    public partial class OrdersView : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.get();

        public OrdersView()
        {
            InitializeComponent();
            OrderVM orderVM;
            orderVM = new OrderVM();
            this.DataContext = orderVM;
        }
        private void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            new MainWindow().Show();
        }

    }
}
