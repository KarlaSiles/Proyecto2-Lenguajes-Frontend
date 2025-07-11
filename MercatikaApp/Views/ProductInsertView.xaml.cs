﻿using MercatikaApp.ViewModel;
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

namespace MercatikaApp.Views
{
    /// <summary>
    /// ProductInsertView.xaml
    /// </summary>
    public partial class ProductInsertView : Window
    {
        public ProductInsertView()
        {
            InitializeComponent();

            Loaded += async (s, e) =>
            {
                if (DataContext is ProductViewModel vm)
                    await vm.LoadCategoriesAsync();
            };
        }
    }

}
