using System;
using System.Windows;
using Stachowski.WinDict.ViewModels.EventArgs;
using Stachowski.WinDict.ViewModels.ObjectsViewModels;
using Stachowski.WinDict.Views;
using Menu = Stachowski.WinDict.Views.Menu;

namespace Stachowski.WinDict
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
        public RoutedEventHandler OnLanguageSelectedForTesting;

        private Menu _menu = new Menu { SelectedMenuItem = ViewModels.MenuItem.Learn };
        private UserSelect _userSelect = new UserSelect();
        private SelectLanguages _selectLanguages = new SelectLanguages();
        private Define _define = new Define();
        private Learn _learn = new Learn();
        private Test _test = new Test();

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
                    case ViewModels.MenuItem.Test:
                        _selectLanguages.Confirmed += OnLanguageSelectedForTesting;
                        ControlContainer.Children.Clear();
                        ControlContainer.Children.Add(_selectLanguages);
                        break;
                    case ViewModels.MenuItem.Define:
                        ControlContainer.Children.Clear();
                        ControlContainer.Children.Add(_define);
                        break;
                    case ViewModels.MenuItem.Logout:
                        CurrentUser = null;
                        MenuContainer.Children.Clear();
                        ControlContainer.Children.Clear();
                        ControlContainer.Children.Add(_userSelect);
                        break;
                }
            };
            OnLanguageSelectedForLearning = (s, e) =>
            {
                if (_selectLanguages.PrimaryLanguage == null ||
                    _selectLanguages.SecondaryLanguage == null ||
                    String.IsNullOrWhiteSpace(_selectLanguages.Category) ||
                    Equals(_selectLanguages.PrimaryLanguage, _selectLanguages.SecondaryLanguage))
                {
                    ((CancellableEventArgs) e).Cancel = true;
                    return;
                }
                _learn.PrimaryLanguage = _selectLanguages.PrimaryLanguage;
                _learn.SecondaryLanguage = _selectLanguages.SecondaryLanguage;
                _learn.CurrentUser = CurrentUser;
                _learn.Theme = _selectLanguages.Category;
                ControlContainer.Children.Clear();
                ControlContainer.Children.Add(_learn);
            };
            OnLanguageSelectedForTesting = (s, e) =>
            {
                if (_selectLanguages.PrimaryLanguage == null ||
                    _selectLanguages.SecondaryLanguage == null ||
                    String.IsNullOrWhiteSpace(_selectLanguages.Category) ||
                    Equals(_selectLanguages.PrimaryLanguage, _selectLanguages.SecondaryLanguage))
                {
                    ((CancellableEventArgs)e).Cancel = true;
                    return;
                }
                _test.CurrentUser = CurrentUser;
                _test.PrimaryLanguage = _selectLanguages.PrimaryLanguage;
                _test.SecondaryLanguage = _selectLanguages.SecondaryLanguage;
                _test.Theme = _selectLanguages.Category;
                ControlContainer.Children.Clear();
                ControlContainer.Children.Add(_test);
            };
            InitializeComponent();
            _userSelect.UserSelected += OnUserSelected;
            _menu.SelectedMenuItemChanged += OnMenuItemChanged;
            ControlContainer.Children.Add(_userSelect);
            
        }
    }
}
