using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Stachowski.WinDict.Dialogs;
using Stachowski.WinDict.Interfaces;
using Stachowski.WinDict.Properties;
using Stachowski.WinDict.ViewModels.EventArgs;
using Stachowski.WinDict.ViewModels.General;
using Stachowski.WinDict.ViewModels.ObjectsCollections;
using Stachowski.WinDict.ViewModels.ObjectsViewModels;
using Stachowski.WinDict.Views;

namespace Stachowski.WinDict.ViewModels
{
    public class UserSelectViewModel : DependencyObject, INotifyPropertyChanged
    {
        private IUserRepository _userRep = AssemblyBinder.AssemblyBinder.UserRepository;

        private ObservableUserCollection _allUsers;
        public ObservableUserCollection AllUsers
        {
            get
            {
                return _allUsers ?? (_allUsers = new ObservableUserCollection(_userRep.GetAllUsers().Select(x => new UserViewModel(x))));
            }
        }

        public static readonly DependencyProperty SelectedUserProperty = DependencyProperty.Register("SelectedUser",
            typeof(UserViewModel), typeof(UserSelectViewModel));

        public UserViewModel SelectedUser
        {
            get { return (UserViewModel)GetValue(SelectedUserProperty); }
            set { SetValue(SelectedUserProperty, value); }
        }

        private ICommand _addUserClickCommand;
        public ICommand AddUserClick
        {
            get { return _addUserClickCommand; }
        }

        private ICommand _submitClickCommand;
        public ICommand SubmitClick
        {
            get { return _submitClickCommand; }
        }

        public UserSelectViewModel()
        {
            _addUserClickCommand = new RelayCommand(x =>
            {
                var dialog = new TextValueDialog {Owner = Application.Current.MainWindow, Text="New user name:" };
                dialog.Confirmed += (s, e) =>
                {
                    try
                    {
                        AllUsers.Add(dialog.Value);
                    }
                    catch (Exception)
                    {
                        ((CancellableEventArgs)e).Cancel = true;
                    }
                };
                dialog.ShowDialog();
            });
            _submitClickCommand = new RelayCommand(x => ((UserSelect) x).RaiseEvent(new RoutedEventArgs(UserSelect.ConfirmedEvent)));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
