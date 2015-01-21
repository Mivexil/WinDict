using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Stachowski.WinDict.Interfaces
{
    public interface IWordStatisticsRepository
    {
        void AddTry(IWord word, IUser user, ILanguage from, ILanguage to, bool isSuccess);
        IEnumerable<IWordStatistics> GetAllStatistics();
        IEnumerable<IWordStatistics> GetStatisticsByUser(IUser user);
        IEnumerable<IWordStatistics> GetStatisticsByUserAndWord(IUser user, IWord word);
        IWordStatistics GetStatistics(IUser user, IWord word, ILanguage from, ILanguage to);
    }
}