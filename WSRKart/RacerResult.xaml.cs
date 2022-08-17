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
    /// Логика взаимодействия для MyResult.xaml
    /// </summary>
    public partial class RacerResult : Page
    {
        DataSet dataSet = new DataSet();
        Prev_Result_Adapter adapter;

        private int GetRacerId()
        {
            return (int)App.Current.Properties["ID_Racer"];
        }

        public RacerResult()
        {
            InitializeComponent();
            adapter = new Prev_Result_Adapter();
            adapter.FillBy_Racer(dataSet.v_prev_result, GetRacerId() );
            ResultGrid.ItemsSource = dataSet.v_prev_result.DefaultView;
            ResultGrid.SelectionMode = DataGridSelectionMode.Single;
            ResultGrid.SelectedValuePath = "Pos";
            ResultGrid.CanUserAddRows = false;
            ResultGrid.CanUserDeleteRows = false;

            Racer_Adapter ra = new Racer_Adapter();
            ra.FillById(dataSet.Racer, GetRacerId());

            NGender.Text = dataSet.Racer.Rows[0]["Gender"].ToString();
            int age = (int)dataSet.Racer.Rows[0]["age"];

            /*
                   CASE  
                       WHEN Racer_Age BETWEEN 18 AND 29   THEN 1
                       WHEN Racer_Age BETWEEN 30 AND 39   THEN 2
                       WHEN Racer_Age BETWEEN 48 AND 55   THEN 3
                       WHEN Racer_Age BETWEEN 56 AND 70   THEN 4
                       WHEN Racer_Age BETWEEN 70 AND 1000 THEN 5
                       ELSE 0
                   END AS cat

             */

            string cat = null;

            if (age >= 18 && age <= 29)
                cat = "18-29";
            else if (age >= 30 && age <= 39)
                cat = "30-39";
            else if (age >= 40 && age <= 55)
                cat = "40-55";
            else if (age >= 56 && age <= 70)
                cat = "56-70";
            else if (age >= 70 && age <= 1000)
                cat = "70 и более";


            NAgeCategory.Text = cat;
            //ra.Adapter
            //ra.Adapter.
            //ra["Gender"];
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void OnResulLastRace(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ResultLastRace());
        }
    }
}
