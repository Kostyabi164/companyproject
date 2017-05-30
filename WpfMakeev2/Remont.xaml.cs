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
using System.Windows.Shapes;
using System.Data.OleDb;
using System.Data;

namespace WpfMakeev2
{
    /// <summary>
    /// Логика взаимодействия для Remont.xaml
    /// </summary>
    public partial class Remont : Window
    {
        OleDbCommand cmd = new OleDbCommand();
        OleDbConnection cn = new OleDbConnection();
        public Remont()
        {
            InitializeComponent();
        }

        private void btnCancelRemont_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnEditStatus_Click(object sender, RoutedEventArgs e)
        {
            if (cmbRemont.Text.Length > 0)
            {
                cn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=sysadmin.accdb";
                cmd.Connection = cn;
                string q = "UPDATE computers SET status='" + cmbRemont.Text.ToString() + "' WHERE computerID=" + WpfMakeev2.MainWindow.selectitem4.ToString();                
                execsql(q);               
                this.Close();
            }
        }        

        private void execsql(String q)
        {
            try
            {
                cn.Open();
                cmd.CommandText = q;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception e)
            {
                cn.Close();
                MessageBox.Show(e.Message.ToString());
            }
        }
    }
}
