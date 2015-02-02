using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Stachowski.WinDict.Dialogs;
using Stachowski.WinDict.ViewModels;
using Stachowski.WinDict.ViewModels.EventArgs;
using Stachowski.WinDict.ViewModels.ObjectsViewModels;

namespace Stachowski.WinDict.Views
{
    /// <summary>
    /// Interaction logic for UserSelect.xaml
    /// </summary>
    public partial class UserSelect : UserControl
    {
        public UserSelect()
        {
            InitializeComponent();
        }

        public UserViewModel SelectedUser
        {
            get { return ((UserSelectViewModel) DataContext).SelectedUser; }
            set { ((UserSelectViewModel) DataContext).SelectedUser = value; }
        }

        public static readonly RoutedEvent ConfirmedEvent = EventManager.RegisterRoutedEvent("UserSelected",
            RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (UserSelect));

        public event RoutedEventHandler UserSelected
        {
            add { AddHandler(ConfirmedEvent, value); }
            remove { RemoveHandler(ConfirmedEvent, value); }
        }

        private void KeyDownListbox(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (UsersListBox.SelectedItem != null)
                {
                    var user = (UserViewModel) UsersListBox.SelectedItem;
                    var dialog = new AreYouSureDialog {Owner = Application.Current.MainWindow};
                    dialog.Confirmed += (s, v) =>
                    {
                        try
                        {
                            ((UserSelectViewModel) DataContext).AllUsers.Remove(user);
                        }
                        catch (Exception)
                        {
                            ((CancellableEventArgs)v).Cancel = true;
                        }
                    };
                    dialog.ShowDialog();
                }
            }
        }
    }
}
