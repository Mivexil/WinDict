using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Stachowski.WinDict.Interfaces;
using Stachowski.WinDict.ViewModels.ObjectsViewModels;

namespace Stachowski.WinDict.ViewModels.ObjectsCollections
{
    public class ObservableWordCollection : ObservableCollection<WordViewModel>
    {
        public ObservableWordCollection() : base()
        {
        }

        public ObservableWordCollection(List<WordViewModel> l) : base(l)
        {
        }

        public ObservableWordCollection(IEnumerable<WordViewModel> ie) : base(ie)
        {
        }

        private IWordRepository _wordRep = AssemblyBinder.AssemblyBinder.WordRepository;
        public void Add(IDictionary<LanguageViewModel, string> definitions, string theme = "", Bitmap picture = null)
        {
            _wordRep.CreateWord(definitions.ToDictionary(x => x.Key.BackingObject, x => x.Value), theme, picture);
            Add(new WordViewModel(_wordRep.GetByDefinition(definitions.ToList()[0].Key.BackingObject, definitions.ToList()[0].Value)));
        }

        public new void Remove(WordViewModel word)
        {
            _wordRep.RemoveWord(word.BackingObject);
            base.Remove(word);
        }
    }
}
