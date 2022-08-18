using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для PolzList.xaml
    /// </summary>
    public partial class PolzList : Page
    {

        DataSet dataSet = new DataSet();
        Role_Adapter ra = new Role_Adapter();
        User_Adapter ua = new User_Adapter();

        public PolzList()
        {
            InitializeComponent();
            ra = new Role_Adapter();
            ra.Fill(dataSet.Role);

            /**
            adapter.FillBy_Params(
                    dataSet.v_prev_result,
                    (string)CbGender.SelectedValue,
                    (int)CbSob.SelectedValue,
                    (string)CbTypeRace.SelectedValue,
                    int.Parse(sa[0]),
                    int.Parse(sa[1])
                                        );
             */

            CbRole.ItemsSource = dataSet.Role.DefaultView;
            CbRole.DisplayMemberPath = "Role_Name";
            CbRole.SelectedValuePath = "ID_Role";
            CbRole.SelectedIndex = 0;

            //(DataSet.UserDataTable dataTable, string ID_role, string txt)
            
            UserGrid.ItemsSource = dataSet.User.DefaultView;
            UserGrid.SelectionMode = DataGridSelectionMode.Single;
            UserGrid.SelectedValuePath = "ID_user";
            UserGrid.CanUserAddRows = false;
            UserGrid.CanUserDeleteRows = false;

            GetCountRacer();

            OnUpdate(null, null);
        }

        private void OnEdit(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)UserGrid.SelectedItem;
            if( dataRowView != null )
                NavigationService.Navigate(new AddUser(dataRowView.Row.Field<int>("ID_user")));
        }

        private void OnUpdate(object sender, RoutedEventArgs e)
        {
            string s = txSearch.Text;
            if (string.IsNullOrEmpty(s))
                s = null;
            else
                s = "%" + s + "%";
            ua.FillBy_Fltr(dataSet.User, CbRole.SelectedValue.ToString(), s);
        }

        private void GetCountRacer()
        {
            SqlCommand sqlSelect  = new SqlCommand();
            sqlSelect.CommandText = "select count (1) nr from racer";
            sqlSelect.CommandType = System.Data.CommandType.Text;
            sqlSelect.Connection  = ua.Connection;

            if (sqlSelect.Connection.State != System.Data.ConnectionState.Open)
                sqlSelect.Connection.Open();

            int ret = 0;

            using (SqlDataReader reader = sqlSelect.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        ret = Convert.ToInt32(reader.GetValue(0));
                    }
                }

                sqlSelect.Connection.Close();
            }

            txtKolPilots.Text = ret.ToString();

        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddUser(0));
        }
    }
}
