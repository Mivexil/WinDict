using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stachowski.WinDict.Interfaces
{
    public interface ILanguageRepository
    {
        ILanguage GetByName(string name);
        IEnumerable<ILanguage> GetAllLanguages();
        void AddLanguage(string name);
        void DeleteLanguage(ILanguage language);
        void DeleteLanguage(string name);
        void RenameLanguage(ILanguage language, string newName);
    }
}
