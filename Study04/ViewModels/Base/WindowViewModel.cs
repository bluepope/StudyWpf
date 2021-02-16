using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

using BluePope.Lib.Wpf;

using Study04.Windows;

namespace Study04.ViewModels.Base
{
    public class WindowViewModel : ModelBase
    {
        Window _window;

        WindowDockPosition _dockPosition = WindowDockPosition.Undocked;

        public double WindowMinimumWidth { get => GetValue<double>(); set => SetValue(value); }
        public double WindowMinimumHeight { get => GetValue<double>(); set => SetValue(value); }
        public bool Borderless => _window.WindowState == WindowState.Maximized || _dockPosition != WindowDockPosition.Undocked;
        public int ResizeBorder { get => GetValue<int>(); set => SetValue(value); }

        /// <summary>
        /// The size of the resize border around the window, taking into account the outer margin
        /// </summary>
        [DependsOnProperties(nameof(ResizeBorder), nameof(OuterMarginSize))]
        public Thickness ResizeBorderThickness => new Thickness(ResizeBorder + OuterMarginSize);

        /// <summary>
        /// The padding of the inner content of the main window
        /// </summary>
        [DependsOnProperties(nameof(ResizeBorder))]
        public Thickness InnerContentPadding => new Thickness(ResizeBorder);

        public int OuterMarginSize { get => _window.WindowState == WindowState.Maximized ? 0 : GetValue<int>(); set => SetValue(value); }
        
        [DependsOnProperties(nameof(OuterMarginSize))]
        public Thickness OuterMarginSizeThickness => new Thickness(OuterMarginSize);

        public int WindowRadius { get => Borderless ? 0 : GetValue<int>(); set => SetValue(value); }
        
        [DependsOnProperties(nameof(WindowRadius))]
        public CornerRadius WindowCornerRadius => new CornerRadius(WindowRadius);


        public int TitleHeight { get => GetValue<int>(); set => SetValue(value); }
        
        [DependsOnProperties(nameof(TitleHeight), nameof(ResizeBorder))]
        public GridLength TitleHeightGridLength => new GridLength(TitleHeight + ResizeBorder);


        public WindowViewModel(Window window)
        {
            _window = window;

            ResizeBorder = 6;
            OuterMarginSize = 5;
            WindowRadius = 10;
            TitleHeight = 46;

            _window.StateChanged += (s, e) =>
            {
                WindowResized();
            };

            // Create commands
            MinimizeCommand = new RelayCommand(() => _window.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => _window.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => _window.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(_window, GetMousePosition()));

            // Fix window resize issue
            var resizer = new WindowResizer(_window);

            // Listen out for dock changes
            resizer.WindowDockChanged += (dock) =>
            {
                // Store last position
                _dockPosition = dock;

                // Fire off resize events
                WindowResized();
            };
        }

        #region Commands

        /// <summary>
        /// The command to minimize the window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// The command to maximize the window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// The command to close the window
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// The command to show the system menu of the window
        /// </summary>
        public ICommand MenuCommand { get; set; }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Gets the current mouse position on the screen
        /// </summary>
        /// <returns></returns>
        private Point GetMousePosition()
        {
            // Position of the mouse relative to the window
            var position = Mouse.GetPosition(_window);

            // Add the window position so its a "ToScreen"
            return new Point(position.X + _window.Left, position.Y + _window.Top);
        }

        /// <summary>
        /// If the window resizes to a special position (docked or maximized)
        /// this will update all required property change events to set the borders and radius values
        /// </summary>
        private void WindowResized()
        {
            // Fire off events for all properties that are affected by a resize
            RaisePropertyChanged(nameof(Borderless));
            RaisePropertyChanged(nameof(ResizeBorderThickness));
            RaisePropertyChanged(nameof(OuterMarginSize));
            RaisePropertyChanged(nameof(OuterMarginSizeThickness));
            RaisePropertyChanged(nameof(WindowRadius));
            RaisePropertyChanged(nameof(WindowCornerRadius));
        }


        #endregion
    }
}
