using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using Stachowski.WinDict.ViewModels;
using Stachowski.WinDict.ViewModels.ObjectsViewModels;

namespace Stachowski.WinDict.Dialogs
{
    /// <summary>
    /// Interaction logic for NewWordDialog.xaml
    /// </summary>
    public partial class NewWordDialog : Window
    {
        public NewWordDialog()
        {
            InitializeComponent();
        }

        public static readonly RoutedEvent ConfirmedEvent = EventManager.RegisterRoutedEvent("Confirmed",
            RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (NewWordDialog));

        public event RoutedEventHandler Confirmed
        {
            add { AddHandler(ConfirmedEvent, value); }
            remove { RemoveHandler(ConfirmedEvent, value); }
        }

        public ObservableCollection<LanguageAndDefinition> Definitions
        {
            get { return ((NewWordDialogViewModel) DataContext).Definitions; }
            set { ((NewWordDialogViewModel) DataContext).Definitions = value; }
        }

        public string Theme
        {
            get { return ((NewWordDialogViewModel)DataContext).Theme; }
            set { ((NewWordDialogViewModel)DataContext).Theme = value; }
        }

        public string PicturePath
        {
            get { return ((NewWordDialogViewModel)DataContext).PicturePath; }
            set { ((NewWordDialogViewModel)DataContext).PicturePath = value; }
        }

        public WordViewModel EditedWord
        {
            get { return ((NewWordDialogViewModel)DataContext).EditedWord; }
            set { ((NewWordDialogViewModel)DataContext).EditedWord = value; }
        }
        public void HandleInvalidData(string cancelledField)
        {
            var currentBrush = DefinitionsGrid.BorderBrush;
            DefinitionsGrid.BorderBrush = new SolidColorBrush(Colors.Red);
            /*TextBox offendingControl;
            switch (cancelledField)
            {
                case "Definitions":
                    offendingControl = DefinitionGrid;
                    break;
                case "Theme":
                    offendingControl = ThemeTextBox;
                    break;
                case "PicturePath":
                    offendingControl = PicturePathTextBox;
                    break;
                default:
                    return;
            }
            var currentBrush = offendingControl.BorderBrush;
            offendingControl.BorderBrush = new SolidColorBrush(Colors.Red);
            TextChangedEventHandler _onTextChanged = null;
            _onTextChanged = (s, e) =>
            {
                offendingControl.BorderBrush = currentBrush;
                offendingControl.TextChanged -= _onTextChanged;
            };
            offendingControl.TextChanged += _onTextChanged;*/
        }
    }
}
