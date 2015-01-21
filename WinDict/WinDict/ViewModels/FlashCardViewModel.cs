using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WinDict.ViewModels.General;
using WinDict.ViewModels.ObjectsViewModels;

namespace WinDict.ViewModels
{
    public class FlashCardViewModel : DependencyObject
    {

        public FlashCardViewModel()
        {
            _flipClickCommand = new RelayCommand(x =>
            {
                if (Flippable) Flip();
            });
        }

        public static readonly DependencyProperty FlippableProperty = DependencyProperty.Register("Flippable",
            typeof (bool), typeof (FlashCardViewModel));

        public bool Flippable
        {
            get { return (bool) GetValue(FlippableProperty); }
            set { SetValue(FlippableProperty, value); }
        }

        public static readonly DependencyProperty WordProperty = DependencyProperty.Register("Word",
            typeof (WordViewModel), typeof (FlashCardViewModel));

        public WordViewModel Word
        {
            get { return (WordViewModel) GetValue(WordProperty); }
            set { SetValue(WordProperty, value); }
        }

        public static readonly DependencyProperty PrimaryLanguageProperty =
            DependencyProperty.Register("PrimaryLanguage", typeof (LanguageViewModel), typeof (FlashCardViewModel));

        public LanguageViewModel PrimaryLanguage
        {
            get { return (LanguageViewModel) GetValue(PrimaryLanguageProperty); }
            set { SetValue(PrimaryLanguageProperty, value); }
        }

        public static readonly DependencyProperty SecondaryLanguageProperty =
            DependencyProperty.Register("SecondaryLanguage", typeof(LanguageViewModel), typeof(FlashCardViewModel));

        public LanguageViewModel SecondaryLanguage
        {
            get { return (LanguageViewModel)GetValue(SecondaryLanguageProperty); }
            set { SetValue(SecondaryLanguageProperty, value); }
        }

        public static readonly DependencyProperty FlippedProperty =
            DependencyProperty.Register("Flipped", typeof (bool), typeof (FlashCardViewModel));

        public bool Flipped
        {
            get { return (bool) GetValue(FlippedProperty); }
            set { SetValue(FlippedProperty, value); }
        }

        private ICommand _flipClickCommand;
        public ICommand FlipClick
        {
            get { return _flipClickCommand; }
        }

        public void Flip()
        {
            Flipped = !Flipped;
        }
    }
}
