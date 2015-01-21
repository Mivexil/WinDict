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
using WinDict.ViewModels;
using WinDict.ViewModels.EventArgs;
using WinDict.ViewModels.ObjectsViewModels;
using MenuItem = WinDict.ViewModels.MenuItem;

namespace WinDict.Views
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
