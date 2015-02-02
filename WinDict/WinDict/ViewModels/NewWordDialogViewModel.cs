using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Stachowski.WinDict.Dialogs;
using Stachowski.WinDict.Interfaces;
using Stachowski.WinDict.ViewModels.EventArgs;
using Stachowski.WinDict.ViewModels.General;
using Stachowski.WinDict.ViewModels.ObjectsViewModels;

namespace Stachowski.WinDict.ViewModels
{
    public class NewWordDialogViewModel : DependencyObject
    {

        private ILanguageRepository _langRep = AssemblyBinder.AssemblyBinder.LanguageRepository;

        public NewWordDialogViewModel()
        {
            _okClickedCommand = new RelayCommand(x =>
            {
                var eventArgs = new NewWordEventArgs(NewWordDialog.ConfirmedEvent);
                ((Window) x).RaiseEvent(eventArgs);
                if (eventArgs.Cancel)
                {
                    ((NewWordDialog) x).HandleInvalidData(eventArgs.CancelledField);
                }
                else
                {
                    ((Window) x).Close();
                }
            });
            _cancelClickedCommand = new RelayCommand(x => ((Window) x).Close());
            if (EditedWord == null) 
            {
                Definitions =
                new ObservableCollection<LanguageAndDefinition>(
                    _langRep.GetAllLanguages()
                        .Select(x => new LanguageAndDefinition {Language = new LanguageViewModel(x), Definition = ""}));
            }
        }

        public static readonly DependencyProperty DefinitionsProperty = DependencyProperty.Register("Definitions", 
            typeof(ObservableCollection<LanguageAndDefinition>), typeof(NewWordDialogViewModel));

        public ObservableCollection<LanguageAndDefinition> Definitions
        {
            get { return (ObservableCollection<LanguageAndDefinition>) GetValue(DefinitionsProperty); }
            set { SetValue(DefinitionsProperty, value); }
        }


        public static readonly DependencyProperty ThemeProperty = DependencyProperty.Register("Theme",
            typeof(string), typeof(NewWordDialogViewModel));

        public string Theme
        {
            get { return (string)GetValue(ThemeProperty); }
            set { SetValue(ThemeProperty, value); }
        }

        public static readonly DependencyProperty PicturePathProperty = DependencyProperty.Register("PicturePath",
            typeof(string), typeof(NewWordDialogViewModel));

        public string PicturePath
        {
            get { return (string)GetValue(PicturePathProperty); }
            set { SetValue(PicturePathProperty, value); }
        }

        public static readonly DependencyProperty EditedWordProperty = DependencyProperty.Register("EditedWord",
            typeof (WordViewModel), typeof (NewWordDialogViewModel), new PropertyMetadata(OnEditedWordSet));

        private static void OnEditedWordSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((NewWordDialogViewModel) d).Definitions =
                new ObservableCollection<LanguageAndDefinition>(
                    ((WordViewModel) e.NewValue).Definitions.Select(
                        x => new LanguageAndDefinition {Language = x.Key, Definition = x.Value}));
            ((NewWordDialogViewModel)d).Theme = ((WordViewModel)e.NewValue).Theme;
        }

        public WordViewModel EditedWord
        {
            get { return (WordViewModel) GetValue(EditedWordProperty); }
            set { SetValue(EditedWordProperty, value); }
        }

        private ICommand _okClickedCommand;

        public ICommand OkClicked
        {
            get { return _okClickedCommand; }
        }

        private ICommand _cancelClickedCommand;

        public ICommand CancelClicked
        {
            get { return _cancelClickedCommand; }
        }
    }
}
