using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Study02
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
            foreach (var drive in Directory.GetLogicalDrives())
            {
                var item = new TreeViewItem();

                item.Header = drive;
                item.Tag = drive;

                item.Items.Add(null);
                item.Expanded += Folder_Expanded;

                FolderView.Items.Add(item);
            }
        }

        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;

            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            item.Items.Clear();

            string fullPath = (string)item.Tag;

            var dirList = new List<string>();
            try
            {
                string[] dirs = Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    dirList.AddRange(dirs);

                dirList.ForEach((dir) =>
                {
                    var subItem = new TreeViewItem();
                    subItem.Header = Path.GetFileName(dir);
                    subItem.Tag = dir;

                    subItem.Items.Add(null);
                    subItem.Expanded += Folder_Expanded;

                    item.Items.Add(subItem);
                });

                string[] files = Directory.GetFiles(fullPath);
                
                if (files.Length > 0)
                    dirList.AddRange(files);

                foreach(string filePath in files)
                {
                    var subItem = new TreeViewItem();
                    subItem.Header = Path.GetFileName(filePath);
                    subItem.Tag = filePath;

                    item.Items.Add(subItem);
                }
            }
            catch { }
        }
    }
}
