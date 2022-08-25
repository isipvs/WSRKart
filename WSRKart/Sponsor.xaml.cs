using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
    /// Логика взаимодействия для Sponsor.xaml
    /// </summary>
    public partial class Sponsor : Page
    {
        private int mSum = 0;

        public Sponsor()
        {
            InitializeComponent();
            DataSet dataSet = new DataSet();
            V_Spon_For_Adapter adapter = new V_Spon_For_Adapter();
            adapter.Fill(dataSet.V_Spon_For);

            cbxRacers.ItemsSource = dataSet.V_Spon_For.DefaultView;
            cbxRacers.DisplayMemberPath = "Racer_Name";
            cbxRacers.SelectedValuePath = "ID_Registration";

            txtSum.Text = "50";

            ChnageSum(+1);
        }

        private void cbxRacers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object selectedValue = ((DataRowView)cbxRacers.SelectedItem)["Charity_Name"];
            txbCharity.Text = selectedValue.ToString();
        }

        private void ChnageSum(int sign) 
        {
            try
            {
                int d = int.Parse(txtSum.Text);
                d *= sign;
                mSum += d;
                lblSum.Content = "$ " + mSum.ToString();
            }
            catch (Exception ex) {
                MessageBox.Show( "Ошибка: " + ex.Message, null, MessageBoxButton.OK, MessageBoxImage.Error );
            }
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            ChnageSum(+1);
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            ChnageSum(-1);
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            try {

                Control[] controls = new Control[] {
                    txbName, cbxRacers, txbKart, txbNumKart, dayes, years, txbCvc 
                };

                foreach (Control c in controls)
                    UITools.ChekReq(c);

                decimal d = mSum;

                Sponsorship_Adapter a = new Sponsorship_Adapter();
                a.Insert_Spon(
                    txbName.Text, d, (int)cbxRacers.SelectedValue
                );

                NavigationService.Navigate(new ProofSponsor());
            }
            catch (Exception ex) {
                MessageBox.Show("Ошибка: " + ex.Message, null, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NUM_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            UITools.Num_PreviewTextInput(e);
        }
    }
}
