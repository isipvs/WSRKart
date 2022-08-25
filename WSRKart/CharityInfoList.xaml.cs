using System;
using System.Collections.Generic;
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
using static System.Net.Mime.MediaTypeNames;

namespace WSRKart
{
    /// <summary>
    /// Логика взаимодействия для CharityList.xaml
    /// </summary>
    public partial class CharityInfoList : Page
    {
        static private string ImagePath = "C:\\3 Kyrs\\Долги\\УП\\WSRKart\\WSRKart\\Images\\";

        DataSet dataSet = new DataSet();
        Charity_Adapter adapter = new Charity_Adapter();


        public CharityInfoList()
        {
            InitializeComponent();
            adapter.Fill(dataSet.Charity);

            foreach ( DataRow r in dataSet.Charity.Rows )
            {
                try
                {
                    r["Logo_Image"] = File.ReadAllBytes(ImagePath + r["Charity_Logo"].ToString());
                }
                catch (Exception ex) { }
            }

            lvwCharity.ItemsSource = dataSet.Charity.DefaultView;
            lvwCharity.SelectedIndex = 0;
            lvwCharity.SelectedValuePath = "ID_Сharity";
            /*
                        CharityGrid.ItemsSource = dataSet.Charity.DefaultView;
                        CharityGrid.DisplayMemberPath = "Charity_Name";
                        CharityGrid.SelectedValuePath = "ID_Сharity";
                        CharityGrid.SelectedIndex = 0;
            */
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
