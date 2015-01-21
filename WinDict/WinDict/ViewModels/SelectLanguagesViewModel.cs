using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WinDict.ViewModels.EventArgs;
using WinDict.ViewModels.General;
using WinDict.ViewModels.ObjectsViewModels;
using WinDict.Views;

namespace WinDict.ViewModels
{
    public class SelectLanguagesViewModel : DependencyObject
    {

        public SelectLanguagesViewModel()
        {
            LanguageList =
                AssemblyBinder.AssemblyBinder.LanguageRepository.GetAllLanguages().Select(x => new LanguageViewModel(x)).ToList();

            if (LanguageList.Count > 0) PrimaryLanguage = LanguageList[0];
            if (LanguageList.Count > 1) SecondaryLanguage = LanguageList[1];

            var categoryList = new List<string> {"Easy", "Difficult", "All", ""};
            categoryList.AddRange(AssemblyBinder.AssemblyBinder.WordRepository.GetAll().Select(x => x.Theme).Distinct());
            CategoryList = categoryList;
            Category = "All";


            _nextClickedCommand = new RelayCommand(x =>
            {
                var eventArgs = new CancellableEventArgs(SelectLanguages.ConfirmedEvent);
                ((Control) x).RaiseEvent(eventArgs);
                if (eventArgs.Cancel)
                {
                    ((SelectLanguages) x).HandleInvalidData();
                }
            });
        }

        public static readonly DependencyProperty LanguageListProperty = DependencyProperty.Register("LanguageList",
            typeof (List<LanguageViewModel>), typeof (SelectLanguagesViewModel));

        public List<LanguageViewModel> LanguageList
        {
            get { return (List<LanguageViewModel>) GetValue(LanguageListProperty); }
            set { SetValue(LanguageListProperty, value); }
        }

        public static readonly DependencyProperty PrimaryLanguageProperty = DependencyProperty.Register("PrimaryLanguage",
            typeof (LanguageViewModel), typeof (SelectLanguagesViewModel));

        public LanguageViewModel PrimaryLanguage
        {
            get { return (LanguageViewModel) GetValue(PrimaryLanguageProperty); }
            set { SetValue(PrimaryLanguageProperty, value); }
        }

        public static readonly DependencyProperty SecondaryLanguageProperty = DependencyProperty.Register("SecondaryLanguage",
            typeof(LanguageViewModel), typeof(SelectLanguagesViewModel));

        public LanguageViewModel SecondaryLanguage
        {
            get { return (LanguageViewModel)GetValue(SecondaryLanguageProperty); }
            set { SetValue(SecondaryLanguageProperty, value); }
        }

        public static readonly DependencyProperty CategoryListProperty = DependencyProperty.Register("CategoryList",
            typeof (List<string>), typeof (SelectLanguagesViewModel));

        public List<string> CategoryList
        {
            get { return (List<string>) GetValue(CategoryListProperty); }
            set { SetValue(CategoryListProperty, value); }
        }

        public static readonly DependencyProperty CategoryProperty = DependencyProperty.Register("Category",
            typeof (string), typeof (SelectLanguagesViewModel));

        public string Category
        {
            get { return (string) GetValue(CategoryProperty); }
            set { SetValue(CategoryProperty, value); }
        }

        private ICommand _nextClickedCommand;
        public ICommand NextClicked
        {
            get { return _nextClickedCommand; }
        }
    }
}
