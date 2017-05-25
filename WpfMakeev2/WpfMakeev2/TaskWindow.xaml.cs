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
    /// Логика взаимодействия для TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        OleDbCommand cmd = new OleDbCommand();
        OleDbConnection cn = new OleDbConnection();        
        public TaskWindow()
        {
            InitializeComponent();
        }

        private void ButtonProblem_Click(object sender, RoutedEventArgs e)
        {
            cn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Project_Kostya\WpfMakeev2\WpfMakeev2\bin\Debug\sysadmin.accdb";
            cmd.Connection = cn;
            if (description.Text != "")
            {
                string q = "INSERT INTO problems (dateopen,description,FIOIT,dateclose) VALUES ('" + date.DisplayDate.ToString() + "','" + description.Text.ToString() + "','" + fio.Text.ToString() + "',NULL)";
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

        private void CloseForm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
