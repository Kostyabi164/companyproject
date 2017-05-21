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
using System.Data.OleDb;
using System.Data;

namespace WpfMakeev2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Константин\Desktop\WpfMakeev2\WpfMakeev2\bin\Debug\sysadmin.accdb");
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from problems", con);
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds, "problems");
            datagrid1.ItemsSource = ds.Tables["problems"].DefaultView;
            con.Close();
        }
    }
}
