using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WinDict.Annotations;
using WinDict.ViewModels.General;
using WinDict.ViewModels.ObjectsViewModels;

namespace WinDict.ViewModels
{
    
    public class MenuViewModel : DependencyObject, INotifyPropertyChanged
    {

        public MenuViewModel()
        {
            _buttonClickedCommand = new RelayCommand(x =>
            {
                SelectedMenuItem = (MenuItem) x;
            });
        }

        public static readonly DependencyProperty SelectedMenuItemProperty = DependencyProperty.Register("SelectedMenuItem",
            typeof (MenuItem), typeof (MenuViewModel));

        public MenuItem SelectedMenuItem
        {
            get { return ((MenuItem) GetValue(SelectedMenuItemProperty)); }
            set 
            { 
                SetValue(SelectedMenuItemProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty CurrentUserProperty = DependencyProperty.Register("CurrentUser",
            typeof (UserViewModel), typeof (MenuViewModel));

        public UserViewModel CurrentUser
        {
            get { return (UserViewModel) GetValue(CurrentUserProperty); }
            set 
            { 
                SetValue(CurrentUserProperty, value);
                OnPropertyChanged();
            }
        }

        private ICommand _buttonClickedCommand;
        public ICommand ButtonClicked
        {
            get { return _buttonClickedCommand; }
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
