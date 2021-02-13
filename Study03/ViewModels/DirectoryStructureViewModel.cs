using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


using BluePope.Lib.Wpf;

using Study03.Directorys.Data;

namespace Study03.ViewModels
{
    public class DirectoryStructureViewModel : ModelBase
    {
        #region Public Properties

        /// <summary>
        /// A list of all directories on the machine
        /// </summary>
        public ObservableCollection<DirectoryItem> Items { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DirectoryStructureViewModel()
        {
            // Get the logical drives
            var children = System.IO.DriveInfo.GetDrives();
            
            // Create the view models from the data
            this.Items = new ObservableCollection<DirectoryItem>(children.Select(drive => new DirectoryItem(drive.Name)).ToList());
        }

        #endregion
    }
}
