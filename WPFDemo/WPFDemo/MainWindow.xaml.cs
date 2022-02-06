using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;
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

namespace WPFDemo
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable dt = new DataTable();
        Thread t1; 
        Thread t2;
        Thread t3;
        
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dt.Columns.Add("NO");
            dt.Columns.Add("THREAD NAME");
            dt.Columns.Add("THREAD STATE");
            dataGridLog.DataContext = dt.DefaultView;
            t1 = new Thread(new ThreadStart(doldur1));
            t2 = new Thread(new ThreadStart(doldur2));
            t3 = new Thread(new ThreadStart(doldur3));
        }

        private void BtnDeactivate_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            MessageBox.Show("Clicked deactivate");
        }

        private void BtnActivate_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Clicked activate");
        }

        List<Log> Logs = new List<Log>();

        [Obsolete]
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            t1.Abort();
            t2.Abort();
            t3.Abort();
        }

        [Obsolete]
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            t1.Start();
            Thread.Sleep(600);
            t2.Start();
            Thread.Sleep(600);
            t3.Start();
            Thread.Sleep(600);
            DataRow dr = dt.NewRow();
            dr[0] = "1";
            dr[1] = "Thread 1";
            dr[2] = "Active";
            dt.Rows.Add(dr);
            
        }


        private void doldur1()
        {
            while (true)
            {
                Random random = new Random();
                int number = random.Next(1, 100);
                prgBar1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate () { prgBar1.Value = number; });
                Thread.Sleep(10);
            }


        }

        private void doldur2()
        {
            while (true)
            {
                Random random = new Random();
                int number = random.Next(1, 100);
                prgBar2.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate () { prgBar2.Value = number; });
                Thread.Sleep(10);
            }
        }

        private void doldur3()
        {
            while (true)
            {
                Random random = new Random();
                int number = random.Next(1, 100);
                prgBar2.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate () { prgBar3.Value = number; });
                Thread.Sleep(10);
            }
        }



        public class Log
        {
            public int no { get; set; }
            public string threadName { get; set; }
            public string threadState { get; set; }
        }

        private void dataGridLog_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            MessageBox.Show("Clicked");
        }
    }
}
