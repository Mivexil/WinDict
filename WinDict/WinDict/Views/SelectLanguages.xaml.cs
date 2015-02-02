using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Stachowski.WinDict.ViewModels;
using Stachowski.WinDict.ViewModels.ObjectsViewModels;

namespace Stachowski.WinDict.Views
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
