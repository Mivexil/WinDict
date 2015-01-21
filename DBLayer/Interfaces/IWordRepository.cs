using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stachowski.WinDict.Interfaces
{
    public interface IWordRepository
    {
        IWord GetByDefinition(ILanguage language, string definition);
        IEnumerable<IWord> GetAll();
        IEnumerable<IWord> GetAllWithTheme(string theme);
        void UpdatePicture(IWord word, Bitmap newPicture);
        void ChangeTheme(IWord word, string newTheme);
        void ChangeDefinition(IWord word, ILanguage language, string newDefinition);
        void ChangeDefinition(IWord word, IDictionary<ILanguage, string> newDefinitions);
        void RemoveDefinition(IWord word);
        void RemoveDefinition(IWord word, ILanguage language);
        void RemoveDefinition(IWord word, IEnumerable<ILanguage> languages);
        void RemoveWord(IWord word);
        void CreateWord(IDictionary<ILanguage, string> definitions, string theme, Bitmap picture);
    }
}
