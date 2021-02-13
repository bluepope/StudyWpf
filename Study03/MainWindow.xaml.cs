using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

using Study03.ViewModels;

namespace Study03
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new DirectoryStructureViewModel();
        }
    }
}
