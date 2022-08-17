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

            OnUpdate(null, null);

        }

        private void OnEdit(object sender, RoutedEventArgs e)
        {

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

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddUser());
        }
    }
}
