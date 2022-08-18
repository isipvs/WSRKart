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
    /// Логика взаимодействия для CharityList.xaml
    /// </summary>
    public partial class CharityList : Page
    {
        DataSet dataSet = new DataSet();
        Charity_Adapter adapter = new Charity_Adapter();

        public CharityList()
        {
            InitializeComponent();
            adapter.Fill(dataSet.Charity);
            CharityGrid.ItemsSource = dataSet.Charity.DefaultView;
            CharityGrid.DisplayMemberPath = "Charity_Name";
            CharityGrid.SelectedValuePath = "ID_Сharity";
            CharityGrid.SelectedIndex = 0;
        }

        private void AddCharity_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CharityPage());
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
