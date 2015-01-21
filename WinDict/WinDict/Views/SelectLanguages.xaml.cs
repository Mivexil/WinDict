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
using WinDict.ViewModels.ObjectsViewModels;

namespace WinDict.Views
{
    /// <summary>
    /// Interaction logic for SelectLanguages.xaml
    /// </summary>
    public partial class SelectLanguages : UserControl
    {

        public static readonly RoutedEvent ConfirmedEvent = EventManager.RegisterRoutedEvent("Confirmed",
            RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (SelectLanguages));

        public event RoutedEventHandler Confirmed
        {
            add { AddHandler(ConfirmedEvent, value); }
            remove { RemoveHandler(ConfirmedEvent, value); }
        }

        public LanguageViewModel PrimaryLanguage
        {
            get { return ((SelectLanguagesViewModel) DataContext).PrimaryLanguage; }
            set { ((SelectLanguagesViewModel)DataContext).PrimaryLanguage = value; }
        }

        public LanguageViewModel SecondaryLanguage
        {
            get { return ((SelectLanguagesViewModel)DataContext).SecondaryLanguage; }
            set { ((SelectLanguagesViewModel)DataContext).SecondaryLanguage = value; }
        }

        public string Category
        {
            get { return ((SelectLanguagesViewModel) DataContext).Category; }
            set { ((SelectLanguagesViewModel) DataContext).Category = value; }
        }

        public SelectLanguages()
        {
            InitializeComponent();
        }

        public void HandleInvalidData()
        {
            var currentBrush = NextButton.Background;
            NextButton.Background = new SolidColorBrush(Colors.LightPink);
            SelectionChangedEventHandler _onSelectionChanged = null;
            _onSelectionChanged = (s, e) =>
            {
                NextButton.Background = currentBrush;
                LangFromComboBox.SelectionChanged -= _onSelectionChanged;
                LangToComboBox.SelectionChanged -= _onSelectionChanged;
                CategoryComboBox.SelectionChanged -= _onSelectionChanged;
            };
            LangFromComboBox.SelectionChanged += _onSelectionChanged;
            LangToComboBox.SelectionChanged += _onSelectionChanged;
            CategoryComboBox.SelectionChanged += _onSelectionChanged;
        }
    }
}
