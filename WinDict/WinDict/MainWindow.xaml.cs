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
using WinDict.ViewModels.EventArgs;
using WinDict.ViewModels.ObjectsViewModels;
using WinDict.Views;
using Menu = WinDict.Views.Menu;

namespace WinDict
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static DependencyProperty CurrentUserProperty = DependencyProperty.Register("CurrentUser",
            typeof (UserViewModel), typeof (MainWindow));

        public UserViewModel CurrentUser
        {
            get { return (UserViewModel) GetValue(CurrentUserProperty); }
            set { SetValue(CurrentUserProperty, value); }
        }

        public RoutedEventHandler OnUserSelected;
        public RoutedEventHandler OnMenuItemChanged;
        public RoutedEventHandler OnLanguageSelectedForLearning;

        private Menu _menu = new Menu { SelectedMenuItem = ViewModels.MenuItem.Learn };
        private UserSelect _userSelect = new UserSelect();
        private SelectLanguages _selectLanguages = new SelectLanguages();

        public MainWindow()
        {
            OnUserSelected = (s, e) =>
            {
                if (_userSelect.SelectedUser == null) return;
                CurrentUser = _userSelect.SelectedUser;
                ControlContainer.Children.Clear();
                _menu.CurrentUser = CurrentUser;
                MenuContainer.Children.Add(_menu);
            };
            OnMenuItemChanged = (s, e) =>
            {
                switch (_menu.SelectedMenuItem)
                {
                    case ViewModels.MenuItem.Learn:
                        _selectLanguages.Confirmed += OnLanguageSelectedForLearning;
                        ControlContainer.Children.Clear();
                        ControlContainer.Children.Add(_selectLanguages);
                        break;
                    //todo ...
                }
            };
            OnLanguageSelectedForLearning = (s, e) =>
            {
                if (_selectLanguages.PrimaryLanguage == null ||
                    _selectLanguages.SecondaryLanguage == null ||
                    String.IsNullOrWhiteSpace(_selectLanguages.Category) ||
                    _selectLanguages.PrimaryLanguage == _selectLanguages.SecondaryLanguage)
                {
                    ((CancellableEventArgs) e).Cancel = true;
                    return;
                }
                //todo...
            };

            InitializeComponent();
            _userSelect.UserSelected += OnUserSelected;
            _menu.SelectedMenuItemChanged += OnMenuItemChanged;
            ControlContainer.Children.Add(_userSelect);
            
        }
    }
}
