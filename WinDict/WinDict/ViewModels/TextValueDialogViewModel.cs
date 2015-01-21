using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Stachowski.WinDict.Interfaces;
using WinDict.Dialogs;
using WinDict.ViewModels.EventArgs;
using WinDict.ViewModels.General;

namespace WinDict.ViewModels
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
