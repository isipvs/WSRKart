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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WSRKart
{
    /// <summary>
    /// Логика взаимодействия для ManagementRacer.xaml
    /// </summary>
    public partial class ListControlRacer : Page
    {
        DataSet dataSet = new DataSet();
        public ListControlRacer()
        {
            InitializeComponent();
        }

        private void OnEdit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProfilReacer());
        }

        private void OnRefresh(object sender, RoutedEventArgs e)
        {

        }
    }
}
