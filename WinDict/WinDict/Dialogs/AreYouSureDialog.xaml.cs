using System.Windows;
using System.Windows.Input;
using Stachowski.WinDict.ViewModels.General;

namespace Stachowski.WinDict.Dialogs
{
    /// <summary>
    /// Interaction logic for AreYouSureDialog.xaml
    /// </summary>
    public partial class AreYouSureDialog : Window
    {
        public AreYouSureDialog()
        {
            _okClickCommand = new RelayCommand(x =>
            {
                RaiseEvent(new RoutedEventArgs(ConfirmedEvent));
                Close();
            });
            _cancelClickCommand = new RelayCommand(x => Close());
            DataContext = this;
            InitializeComponent();
        }

        public static readonly RoutedEvent ConfirmedEvent = EventManager.RegisterRoutedEvent("Confirmed",
            RoutingStrategy.Bubble,
            typeof (RoutedEventHandler), typeof (AreYouSureDialog));

        public event RoutedEventHandler Confirmed
        {
            add { AddHandler(ConfirmedEvent, value); }
            remove { RemoveHandler(ConfirmedEvent, value); }
        }

        private ICommand _okClickCommand;

        public ICommand OKClick
        {
            get { return _okClickCommand; }
        }

        private ICommand _cancelClickCommand;
        public ICommand CancelClick
        {
            get { return _cancelClickCommand; }
        }
    }
}
