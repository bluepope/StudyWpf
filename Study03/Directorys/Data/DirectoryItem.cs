using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Linq;

using BluePope.Lib.Wpf;
using System.Windows.Input;

namespace Study03.Directorys.Data
{
    public class DirectoryItem : ModelBase
    {
        public DirectoryItemType Type { get => GetValue<DirectoryItemType>(); set => SetValue(value); }

        public string FullPath { get => GetValue<string>(); set => SetValue(value); }

        [DependsOnProperties("FullPath")]
        public string Name => Type == DirectoryItemType.Drive ? FullPath : Path.GetFileName(this.FullPath);

        #region Public Properties

        [DependsOnProperties("FullPath")]
        public string ImageName { get => GetValue<string>(); set => SetValue(value); }

        /// <summary>
        /// A list of all children contained inside this item
        /// </summary>
        public ObservableCollection<DirectoryItem> Children { get => GetValue<ObservableCollection<DirectoryItem>>(); set => SetValue(value); }

        /// <summary>
        /// Indicates if this item can be expanded
        /// </summary>
        public bool CanExpand { get { return this.Type != DirectoryItemType.File; } }

        /// <summary>
        /// Indicates if the current item is expanded or not
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return this.Children?.Count(f => f != null) > 0;
            }
            set
            {
                // If the UI tells us to expand...
                if (value == true)
                {
                    // Find all children
                    Expand();
                }
                // If the UI tells us to close
                else
                    this.ClearChildren();
            }
        }

        #endregion

        #region Public Commands

        /// <summary>
        /// The command to expand this item
        /// </summary>
        public ICommand ExpandCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="fullPath">The full path of this item</param>
        public DirectoryItem(string fullPath)
        {
            // Create commands
            this.ExpandCommand = new RelayCommand(Expand);

            // Set path and type
            this.FullPath = fullPath;

            this.Type = FullPath.Length <= 3 ? DirectoryItemType.Drive : System.IO.Directory.Exists(FullPath) ? DirectoryItemType.Folder : DirectoryItemType.File;
            this.ImageName = Type == DirectoryItemType.Drive ? "drive" : (Type == DirectoryItemType.File ? "file" : (IsExpanded ? "folder-open" : "folder-closed"));

            // Setup the children as needed
            this.ClearChildren();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Removes all children from the list, adding a dummy item to show the expand icon if required
        /// </summary>
        private void ClearChildren()
        {
            // Clear items
            this.Children = new ObservableCollection<DirectoryItem>();

            // Show the expand arrow if we are not a file
            if (this.Type != DirectoryItemType.File)
                this.Children.Add(null);
        }

        #endregion

        /// <summary>
        ///  Expands this directory and finds all children
        /// </summary>
        private void Expand()
        {
            // We cannot expand a file
            if (this.Type == DirectoryItemType.File)
                return;

            var children = DirectoryItem.GetSubItem(this.FullPath);
            this.Children = new ObservableCollection<DirectoryItem>(children.ToList());
        }

        private static List<DirectoryItem> GetSubItem(string fullPath)
        {
            var list = Directory.GetDirectories(fullPath).ToList();
            list.AddRange(Directory.GetFiles(fullPath));
            
            return list.Select(p => new DirectoryItem(p)).ToList();
            
        }
    }
}
