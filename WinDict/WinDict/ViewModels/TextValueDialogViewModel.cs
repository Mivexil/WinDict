using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Stachowski.WinDict.Dialogs;
using Stachowski.WinDict.ViewModels.EventArgs;
using Stachowski.WinDict.ViewModels.General;

namespace Stachowski.WinDict.ViewModels
{
    public class TextValueDialogViewModel : DependencyObject, INotifyPropertyChanged
    {
        public TextValueDialogViewModel()
        {
            _okClickCommand = new RelayCommand(x =>
            {
                var eventArgs = new CancellableEventArgs(TextValueDialog.ConfirmedEvent);
                ((Window)x).RaiseEvent(eventArgs);
                if (eventArgs.Cancel)
                {
                    ((TextValueDialog) x).HandleInvalidData();
                }
                else
                {
                    ((Window)x).Close();
                }
            });
            _cancelClickCommand = new RelayCommand(x => ((Window)x).Close());
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

        public static DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof (string),
            typeof (TextValueDialogViewModel));

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof (string),
            typeof (TextValueDialogViewModel));

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
