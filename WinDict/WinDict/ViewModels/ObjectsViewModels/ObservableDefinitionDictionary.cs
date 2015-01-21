using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stachowski.WinDict.Interfaces;
using WinDict.ViewModels.General;

namespace WinDict.ViewModels.ObjectsViewModels
{
    public class ObservableDefinitionDictionary : ObservableDictionary<LanguageViewModel, string>
    {
        private IWord _word;
        private IWordRepository _wordRep = AssemblyBinder.AssemblyBinder.WordRepository;
        public ObservableDefinitionDictionary(IWord word)
            : base(
                (word.Definitions.Select(
                    kvp => new KeyValuePair<LanguageViewModel, string>(new LanguageViewModel(kvp.Key), kvp.Value))
                    .ToDictionary(x => x.Key, x => x.Value)))
        {
            _word = word;
        }

        protected override bool AddEntry(LanguageViewModel key, string value)
        {
            if (_word != null) _wordRep.ChangeDefinition(_word, key.BackingObject, value);
            return base.AddEntry(key, value);
        }

        protected override bool ClearEntries()
        {
            throw new NotSupportedException("Definitions cannot be removed.");
        }

        protected override bool RemoveEntry(LanguageViewModel key)
        {
            throw new NotSupportedException("Definitions cannot be removed.");
        }

        protected override bool SetEntry(LanguageViewModel key, string value)
        {
            if (_word != null) _wordRep.ChangeDefinition(_word, key.BackingObject, value);
            return base.SetEntry(key, value);
        }
    }
}
