using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Stachowski.WinDict.Dialogs;
using Stachowski.WinDict.Interfaces;
using Stachowski.WinDict.ViewModels.EventArgs;
using Stachowski.WinDict.ViewModels.General;
using Stachowski.WinDict.ViewModels.ObjectsCollections;
using Stachowski.WinDict.ViewModels.ObjectsViewModels;

namespace Stachowski.WinDict.ViewModels
{
    public class DefineViewModel : DependencyObject
    {

        private ILanguageRepository _langRep = AssemblyBinder.AssemblyBinder.LanguageRepository;
        private IWordRepository _wordRep = AssemblyBinder.AssemblyBinder.WordRepository;
        
        public DefineViewModel()
        {
            LanguageList =
                new ObservableCollection<LanguageViewModel>(
                    _langRep.GetAllLanguages().Select(x => new LanguageViewModel(x)));
            WordList = new ObservableWordCollection(_wordRep.GetAll().Select(x => new WordViewModel(x)));
            _editWordClickedCommand = new RelayCommand(x =>
            {
                var dialog = new NewWordDialog { Owner = Application.Current.MainWindow, EditedWord = CurrentWord };
                dialog.Confirmed += (s, e) =>
                {
                    var def = dialog.Definitions;
                    foreach (var d in def)
                    {
                        if (String.IsNullOrEmpty(d.Definition))
                        {
                            ((NewWordEventArgs)e).Cancel = true;
                            ((NewWordEventArgs)e).CancelledField = "Definitions";
                        }
                    }
                    Bitmap bmp;
                    try
                    {
                        bmp = (Bitmap)Image.FromFile(dialog.PicturePath);
                    }
                    catch (Exception)
                    {
                        bmp = null;
                    }
                    _wordRep.ChangeDefinition(CurrentWord.BackingObject,
                        dialog.Definitions.ToDictionary(y => y.Language.BackingObject, y => y.Definition));
                    _wordRep.ChangeTheme(CurrentWord.BackingObject, dialog.Theme);
                    if (bmp != null) _wordRep.UpdatePicture(CurrentWord.BackingObject, bmp);
                    WordList = new ObservableWordCollection(_wordRep.GetAll().Select(y => new WordViewModel(y)));
                };
                dialog.ShowDialog();
            });
            _newWordClickedCommand = new RelayCommand(x =>
            {
                var dialog = new NewWordDialog {Owner = Application.Current.MainWindow};
                dialog.Confirmed += (s, e) =>
                {
                    var def = dialog.Definitions;
                    foreach (var d in def)
                    {
                        if (String.IsNullOrEmpty(d.Definition))
                        {
                            ((NewWordEventArgs)e).Cancel = true;
                            ((NewWordEventArgs)e).CancelledField = "Definitions";
                        }
                    }
                    var defDictionary = def.ToDictionary(y => y.Language, y => y.Definition);
                    Bitmap bmp;
                    try
                    {
                        bmp = (Bitmap) Image.FromFile(dialog.PicturePath);
                    }
                    catch (Exception)
                    {
                        bmp = null;
                    }
                    WordList.Add(defDictionary, dialog.Theme, bmp);
                };
                dialog.ShowDialog();
            });
            _newLanguageClickedCommand = new RelayCommand(x =>
            {
                var dialog = new TextValueDialog {Text = "Add new language"};
                dialog.Confirmed += (s, e) =>
                {
                    if (LanguageList.Select(y => y.Name).Contains(dialog.Value))
                    {
                        ((CancellableEventArgs)e).Cancel = true;
                        return;
                    }
                    _langRep.AddLanguage(dialog.Value);
                    var lang = new LanguageViewModel(_langRep.GetByName(dialog.Value));
                    foreach (var w in WordList)
                    {
                        w.Definitions[lang] = "N/A";
                    }
                    LanguageList.Add(lang);
                };
                dialog.ShowDialog();
            });
            
        }

        public static readonly DependencyProperty PrimaryLanguageProperty =
            DependencyProperty.Register("PrimaryLanguage", typeof (LanguageViewModel), typeof (DefineViewModel));

        public LanguageViewModel PrimaryLanguage
        {
            get { return (LanguageViewModel) GetValue(PrimaryLanguageProperty); }
            set { SetValue(PrimaryLanguageProperty, value); }
        }

        public static readonly DependencyProperty WordListProperty =
            DependencyProperty.Register("WordList", typeof (ObservableWordCollection), typeof (DefineViewModel));

        public ObservableWordCollection WordList
        {
            get { return (ObservableWordCollection) GetValue(WordListProperty); }
            set { SetValue(WordListProperty, value); }
        }

        public static readonly DependencyProperty CurrentWordProperty = DependencyProperty.Register("CurrentWord",
            typeof (WordViewModel), typeof (DefineViewModel));

        public WordViewModel CurrentWord
        {
            get { return (WordViewModel) GetValue(CurrentWordProperty); }
            set { SetValue(CurrentWordProperty, value); }
        }

        public static readonly DependencyProperty LanguageListProperty =
            DependencyProperty.Register("LanguageList", typeof (ObservableCollection<LanguageViewModel>), typeof (DefineViewModel));

        public ObservableCollection<LanguageViewModel> LanguageList
        {
            get { return (ObservableCollection<LanguageViewModel>)GetValue(LanguageListProperty); }
            set { SetValue(LanguageListProperty, value); }
        }

        public static readonly DependencyProperty CurrentDefinitionProperty =
            DependencyProperty.Register("CurrentDefinition", typeof (string), typeof (DefineViewModel));

        public string CurrentDefinition
        {
            get { return (string) GetValue(CurrentDefinitionProperty); }
            set { SetValue(CurrentDefinitionProperty, value); }
        }

        private ICommand _editWordClickedCommand;

        public ICommand EditWordClicked
        {
            get { return _editWordClickedCommand; }
        }

        private ICommand _newWordClickedCommand;

        public ICommand NewWordClicked
        {
            get { return _newWordClickedCommand; }
        }

        private ICommand _newLanguageClickedCommand;

        public ICommand NewLanguageClicked
        {
            get { return _newLanguageClickedCommand; }
        }
    }
}
