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

namespace Study01
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
            MessageBox.Show(txtDescription.Text);
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            chkAssembly.IsChecked = false;
            chkDrill.IsChecked = false;
            chkFiold.IsChecked = false;
            chkLaser.IsChecked = false;
            chkLathe.IsChecked = false;
            chkPlasma.IsChecked = false;
            chkPurchase.IsChecked = false;
            chkRoll.IsChecked = false;
            chkSaw.IsChecked = false;
            chkWeld.IsChecked = false;
        }
    }
}
