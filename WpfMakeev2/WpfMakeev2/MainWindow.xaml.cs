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
        OleDbCommand cmd = new OleDbCommand();
        OleDbConnection cn = new OleDbConnection();
        string selectitem = "0";
        public MainWindow()
        {
            InitializeComponent();            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Project_Kostya\WpfMakeev2\WpfMakeev2\bin\Debug\sysadmin.accdb");
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select number as 'Номер',dateopen as 'Дата открытия',description as 'Описание проблемы',FIOIT as 'Ответственный ИТ',dateclose as 'Дата закрытия проблемы' from problems",con);
            //OleDbDataAdapter da = new OleDbDataAdapter("select * from problems", con);
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds, "problems");
            datagrid1.ItemsSource = ds.Tables["problems"].DefaultView;
            con.Close();            
        }

        private void OpenProblem_Click(object sender, RoutedEventArgs e)
        {
            TaskWindow taskWindow = new TaskWindow();
            taskWindow.Show();
        }

        private void datagrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
                DataRowView row = (DataRowView)datagrid1.SelectedItems[0];
                selectitem = row["number"].ToString();           
        }       

        private void ButtonCloseTicket_Click(object sender, RoutedEventArgs e)
        {
            if (selectitem != "0")
            {
                cn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Project_Kostya\WpfMakeev2\WpfMakeev2\bin\Debug\sysadmin.accdb";
                cmd.Connection = cn;                
                string q = "UPDATE problems SET dateclose='"+ DateTime.Today.ToString() + "' WHERE number='"+selectitem+"'";
                MessageBox.Show(q);
                execsql(q);                
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

        private void TabItem_Initialized(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Project_Kostya\WpfMakeev2\WpfMakeev2\bin\Debug\sysadmin.accdb");
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select number as 'Номер',dateopen as 'Дата открытия',description as 'Описание проблемы',fioit as 'Ответственный ИТ',dateclose as 'Дата закрытия проблемы' from problems", con);
            //OleDbDataAdapter da = new OleDbDataAdapter("select * from problems", con);
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds, "problems");
            datagrid1.ItemsSource = ds.Tables["problems"].DefaultView;
            con.Close();
        }

        private void tabComputers_Initialized(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Project_Kostya\WpfMakeev2\WpfMakeev2\bin\Debug\sysadmin.accdb");
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select computerID as 'Номер',compname as 'Имя компьютера',status as 'Статус',dep as 'Отдел организации',organization as 'Организация',fio as 'ФИО Сотрудника' from computers", con);
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds, "computers");
            datagrid4.ItemsSource = ds.Tables["computers"].DefaultView;
            con.Close();
        }

        private void btnAddComputer_Click(object sender, RoutedEventArgs e)
        {
            AddCompoter addCompoter = new AddCompoter();
            addCompoter.Show();
        }
    }
}
