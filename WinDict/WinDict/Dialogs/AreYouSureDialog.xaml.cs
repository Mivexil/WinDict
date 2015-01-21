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
using System.Windows.Shapes;
using WinDict.ViewModels.EventArgs;
using WinDict.ViewModels.General;

namespace WinDict.Dialogs
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
