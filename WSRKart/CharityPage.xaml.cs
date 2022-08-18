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
using WSRKart.DataSetTableAdapters;

namespace WSRKart
{
    /// <summary>
    /// Логика взаимодействия для CharityPage.xaml
    /// </summary>
    public partial class CharityPage : Page
    {
        DataSet dataSet = new DataSet();
        Charity_Adapter adapter = new Charity_Adapter();

        public CharityPage()
        {
            InitializeComponent();
            adapter.Fill(dataSet.Charity);
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
