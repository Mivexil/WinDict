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
using WinDict.ViewModels;

namespace WinDict.Dialogs
{
    /// <summary>
    /// Interaction logic for NewUserDialog.xaml
    /// </summary>
    public partial class TextValueDialog : Window
    {
        public static readonly RoutedEvent ConfirmedEvent = EventManager.RegisterRoutedEvent("Confirmed",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(TextValueDialog));

        public string Value
        {
            get { return ((TextValueDialogViewModel) DataContext).Value; }
            set { ((TextValueDialogViewModel)DataContext).Value = value; }
        }

        public string Text
        {
            get { return ((TextValueDialogViewModel)DataContext).Text; }
            set { ((TextValueDialogViewModel)DataContext).Text = value; }
        }

        public event RoutedEventHandler Confirmed
        {
            add { AddHandler(ConfirmedEvent, value); }
            remove { RemoveHandler(ConfirmedEvent, value); }
        }

        public void HandleInvalidData()
        {
            var currentBrush = AddNewUserTextBox.BorderBrush;
            this.AddNewUserTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            TextChangedEventHandler _onTextChanged = null;
            _onTextChanged = (s, e) =>
            {
                AddNewUserTextBox.BorderBrush = currentBrush;
                AddNewUserTextBox.TextChanged -= _onTextChanged;
            };
            this.AddNewUserTextBox.TextChanged += _onTextChanged;
        }

        public TextValueDialog()
        {
            InitializeComponent();
        }
    }
}
