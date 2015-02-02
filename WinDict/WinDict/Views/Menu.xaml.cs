using System.Windows;
using System.Windows.Controls;
using Stachowski.WinDict.ViewModels;
using Stachowski.WinDict.ViewModels.EventArgs;
using Stachowski.WinDict.ViewModels.ObjectsViewModels;
using MenuItem = Stachowski.WinDict.ViewModels.MenuItem;

namespace Stachowski.WinDict.Views
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {

        public UserViewModel CurrentUser
        {
            get { return ((MenuViewModel) DataContext).CurrentUser; }
            set { ((MenuViewModel)DataContext).CurrentUser = value; }
        }

        public MenuItem SelectedMenuItem
        {
            get { return ((MenuViewModel) DataContext).SelectedMenuItem; }
            set { ((MenuViewModel)DataContext).SelectedMenuItem = value; }
        }

        public static readonly RoutedEvent SelectedMenuItemChangedEvent =
            EventManager.RegisterRoutedEvent("SelectedMenuItemChanged", RoutingStrategy.Bubble,
                typeof (RoutedEventHandler), typeof (Menu));

        public event RoutedEventHandler SelectedMenuItemChanged
        {
            add { AddHandler(SelectedMenuItemChangedEvent, value); }
            remove { RemoveHandler(SelectedMenuItemChangedEvent, value); }
        }

        public Menu()
        {
            InitializeComponent();
            ((MenuViewModel) DataContext).PropertyChanged +=
                (s, e) =>
                    RaiseEvent(new MenuItemChangedEventArgs(SelectedMenuItemChangedEvent,
                        ((MenuViewModel) DataContext).SelectedMenuItem));
        }
    }
}
