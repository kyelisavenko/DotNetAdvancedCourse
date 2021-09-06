using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace SyncContextWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Console.WriteLine("Long operation on another thread");
                //Dispatcher.Invoke(() =>
                //    // This will occur on the UI Thread
                //    TextBlock.Text = "Operation finished");
                TextBlock.Text = "Operation finished";
            });
                //.Wait();
            Console.WriteLine("Do more stuff after the long operation is finished");
        }
    }
}
