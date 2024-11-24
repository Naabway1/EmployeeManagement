using EmployeeManagement.ViewModel;
using EmployeeManagement.View;
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

namespace EmployeeManagement
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = MainVM.Instance;
        }


        //Добавил, чтобы потестить как это работает, но получилось настолько неплохо, что я готов это оставить, аххаха
        private void Frame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            LoadingBoarder.Visibility = Visibility.Visible;
            LoadingProgressBar.Visibility = Visibility.Visible;
        }

        private async void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            for (int i = 0; i <= 100; i += 10)
            {
                LoadingProgressBar.Value = i;
                await Task.Delay(50);
            }
            LoadingProgressBar.Visibility = Visibility.Collapsed;
            LoadingBoarder.Visibility = Visibility.Collapsed;
        }
    }
}
