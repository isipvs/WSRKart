using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для ResultLastRace.xaml
    /// </summary>
    public partial class ResultLastRace : Page
    {
        /*
         
        select count (1), 
        CONVERT  (varchar, avg(datediff(second,0,racetime) / 60), 1) AvgTime,
        min(racetime), max(racetime),
        (select count (1) from racer )from result where id_event = 1
         */

        DataSet dataSet = new DataSet();
        v_prev_result_Adapter adapter;

        public ResultLastRace()
        {
            InitializeComponent();
            adapter = new v_prev_result_Adapter();
            ResultGrid.ItemsSource = dataSet.v_prev_result.DefaultView;
            ResultGrid.SelectionMode = DataGridSelectionMode.Single;
            ResultGrid.SelectedValuePath = "BidNumber";
            ResultGrid.CanUserAddRows = false;
            ResultGrid.CanUserDeleteRows = false;

            GenderEx_Adapter ga = new GenderEx_Adapter();
            ga.Fill(dataSet.GenderEx);
            CbGender.ItemsSource = dataSet.GenderEx.DefaultView;
            CbGender.DisplayMemberPath = "Gender_Name";
            CbGender.SelectedValuePath = "ID_Gender";
            CbGender.SelectedIndex = 0;

            Event_Adapter ea = new Event_Adapter();
            ea.Fill(dataSet.Event);

            CbSob.ItemsSource = dataSet.Event.DefaultView;
            CbSob.DisplayMemberPath = "Event_Name";
            CbSob.SelectedValuePath = "ID_Event";
            CbSob.SelectedIndex = 0;

            Event_Type_Adapter eta = new Event_Type_Adapter();
            eta.Fill(dataSet.Event_Type);

            CbTypeRace.ItemsSource = dataSet.Event_Type.DefaultView;
            CbTypeRace.DisplayMemberPath = "Event_Type_Name";
            CbTypeRace.SelectedValuePath = "ID_Event_type";
            CbTypeRace.SelectedIndex = 0;
        }

        /** */
        private void GetInfo(int EventId)
        {
            SqlCommand sqlSelect = new SqlCommand();
            sqlSelect.CommandText = "select count(1) reg_count, avg( datediff( SECOND, 0, racetime ) ) avg_time, (select count (1) from racer ) racer_count from[result] where id_event = @id_event";
            sqlSelect.CommandType = System.Data.CommandType.Text;
            sqlSelect.Parameters.AddWithValue("id_event", EventId);
            sqlSelect.Connection = adapter.Connection;

            if (sqlSelect.Connection.State != System.Data.ConnectionState.Open)
                sqlSelect.Connection.Open();

            int[] ret = null;

            using (SqlDataReader reader = sqlSelect.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        ret = new int[3];
                        ret[0] = Convert.ToInt32(reader.GetValue(0));
                        ret[1] = Convert.ToInt32(reader.GetValue(1));
                        ret[2] = Convert.ToInt32(reader.GetValue(2));
                    }
                }

                sqlSelect.Connection.Close();
            }

            if (ret == null)
            {
                NAllRacer.Content = null;
                NFinishAllRacer.Content = null;
                NAvgTime.Content = null;
            }
            else
            {
                NAllRacer.Content = ret[2].ToString();
                NFinishAllRacer.Content = ret[0].ToString();
                NAvgTime.Content = string.Format("{0:D2}:{0:D2}", ret[1] / 60, ret[1] % 60 );
            }
        }

        /** */
        private void OnSearch(object sender, RoutedEventArgs e)
        {
            try
            {
                ComboBoxItem Item = (ComboBoxItem)CbCategory.SelectedItem;
                string s = (string)Item.Tag;
                string[] sa = s.Split(',');

                adapter.FillBy_Params(
                    dataSet.v_prev_result,
                    (string)CbGender.SelectedValue,
                    (int)CbSob.SelectedValue,
                    (string)CbTypeRace.SelectedValue,
                    int.Parse(sa[0]),
                    int.Parse(sa[1])
                                        );

                GetInfo((int)CbSob.SelectedValue);
            }
            catch (Exception ex) {
                MessageBox.Show("Ошибка: " + ex.Message, null, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
