using SrezShend.Moduel;
using SrezShend.Pages;
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

namespace SrezShend
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            FrameObj.frameMain = FrmMain;

            FrameObj.frameMain.Navigate(new PageMain());
            btnBack.Visibility = Visibility.Hidden;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameObj.frameMain.GoBack();
        }

        private void FrmMain_ContentRendered(object sender, EventArgs e)
        {
            if (FrameObj.frameMain.CanGoBack)
            {
                btnBack.Visibility = Visibility.Visible;
            }
            else
                btnBack.Visibility = Visibility.Hidden;
        }
    }
}
