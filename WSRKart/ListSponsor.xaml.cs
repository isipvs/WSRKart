using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
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
    /// Логика взаимодействия для ListCharity.xaml
    /// </summary>
    public partial class ListSponsor : Page
    {
        DataSet dataSet = new DataSet();
        V_CHARITY_SUM_Adapter sumAdapter = new V_CHARITY_SUM_Adapter();

        public ListSponsor()
        {
            InitializeComponent();
            
            sumAdapter.Fill(dataSet.V_CHARITY_SUM);

            SponsorGrid.ItemsSource = dataSet.V_CHARITY_SUM.DefaultView;
            SponsorGrid.SelectedIndex = 0;
            SponsorGrid.SelectedValuePath = "ID_Сharity";

            NKolCharity.Text = SponsorGrid.Items.Count.ToString();

            decimal kolContributions = 0;

            foreach (DataRow r in dataSet.V_CHARITY_SUM.Rows)
            {
                decimal kol = 0;
                kol = (decimal)r["msum"];
                kolContributions += kol;
            }
            NKolContributions.Text = kolContributions.ToString();
        }

        private void OnSort(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)cbSort.SelectedItem;
            UITools.SortDataGrid(SponsorGrid, int.Parse(item.Tag.ToString()), ListSortDirection.Ascending);
        }
    }
}
