using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Stachowski.WinDict.DBLayer.EFModel;
using Stachowski.WinDict.Interfaces;

namespace Stachowski.WinDict.DBLayer.ContractImplementation.SpecializedRepositories
{
    public class StatisticsRepository : IWordStatisticsRepository
    {
        private WinDictContext _ctx = WinDictContextFactory.Context.Value;
        private UserRepository _userRep = new UserRepository();
        private LanguageRepository _langRep = new LanguageRepository();
        private WordRepository _wordRep = new WordRepository();

        internal IWordStatistics FromEntity(WordStatisticsPerUser entity)
        {
            return new WordStatistics
            {
                EntityID = entity.ID,
                Tries = entity.Tries,
                Successes = entity.Successes,
                FromLanguage = _langRep.FromEntity(entity.FromLanguage),
                ToLanguage = _langRep.FromEntity(entity.ToLanguage),
                User = _userRep.FromEntity(entity.User),
                Word = _wordRep.FromEntity(entity.Word)
            };
        }

        internal WordStatisticsPerUser ToNewEntity(IWordStatistics stats)
        {
            var langFromEntity = _langRep.FindEntity(stats.FromLanguage);
            var langToEntity = _langRep.FindEntity(stats.ToLanguage);
            var userEntity = _userRep.FindEntity(stats.User);
            var wordEntity = _wordRep.FindEntity(stats.Word);
            return new WordStatisticsPerUser
            {
                FromLanguage = langFromEntity,
                ToLanguage = langToEntity,
                User = userEntity,
                Word = wordEntity,
                Tries = stats.Tries,
                Successes = stats.Successes
            };
        }

        internal WordStatisticsPerUser FindEntity(IWordStatistics stats)
        {
            var concrete = stats as WordStatistics;
            if (concrete == null) throw new ArgumentException("Invalid statistics object.");
            return _ctx.Statistics.Find(concrete.EntityID);
        }

        public void AddTry(IWord word, IUser user, ILanguage from, ILanguage to, bool isSuccess)
        {
            var wordConcrete = word as Word;
            var userConcrete = user as User;
            var fromConcrete = from as Language;
            var toConcrete = to as Language;
            if (wordConcrete == null) throw new ArgumentException("Invalid word.");
            if (userConcrete == null) throw new ArgumentException("Invalid user.");
            if (fromConcrete == null || toConcrete == null) throw new ArgumentException("Invalid language.");

            var statisticsEntity = _ctx.Statistics.FirstOrDefault(x => x.User.ID == userConcrete.EntityID &&
                                                                       x.Word.ID == wordConcrete.EntityID &&
                                                                       x.FromLanguage.ID == fromConcrete.EntityID &&
                                                                       x.ToLanguage.ID == toConcrete.EntityID);
            if (statisticsEntity == null)
            {
                _ctx.Statistics.Add(
                    new WordStatisticsPerUser
                    {
                        FromLanguage = _ctx.Languages.Find(fromConcrete.EntityID),
                        ToLanguage = _ctx.Languages.Find(toConcrete.EntityID),
                        User = _ctx.Users.Find(userConcrete.EntityID),
                        Word = _ctx.Words.Find(wordConcrete.EntityID),
                        Tries = 1,
                        Successes = isSuccess ? 1 : 0
                    });
            }
            else
            {
                statisticsEntity.Tries += 1;
                if (isSuccess)
                {
                    statisticsEntity.Successes += 1;
                }
            }
            _ctx.SaveChanges();

        }

        public IEnumerable<IWordStatistics> GetAllStatistics()
        {
            return _ctx.Statistics.ToList().Select(FromEntity);

        }

        public IEnumerable<IWordStatistics> GetStatisticsByUser(IUser user)
        {
            var concreteUser = user as User;
            if (concreteUser == null) throw new ArgumentException("Invalid user.");
            return _ctx.Statistics.Where(x => x.User.ID == concreteUser.EntityID).Select(x => FromEntity(x)).ToList();
        }

        public IEnumerable<IWordStatistics> GetStatisticsByUserAndWord(IUser user, IWord word)
        {
            var concreteUser = user as User;
            if (concreteUser == null) throw new ArgumentException("Invalid user.");
            var concreteWord = word as Word;
            if (concreteWord == null) throw new ArgumentException("Invalid word.");
            return
                _ctx.Statistics
                    .Where(x => x.User.ID == concreteUser.EntityID && x.Word.ID == concreteWord.EntityID)
                    .Select(x => FromEntity(x)).ToList();
        }

        public IWordStatistics GetStatistics(IUser user, IWord word, ILanguage from, ILanguage to)
        {
            var wordConcrete = word as Word;
            var userConcrete = user as User;
            var fromConcrete = from as Language;
            var toConcrete = to as Language;
            if (wordConcrete == null) throw new ArgumentException("Invalid word.");
            if (userConcrete == null) throw new ArgumentException("Invalid user.");
            if (fromConcrete == null || toConcrete == null) throw new ArgumentException("Invalid language.");
            var stats = _ctx.Statistics.FirstOrDefault(x => x.User.ID == userConcrete.EntityID &&
                                                                       x.Word.ID == wordConcrete.EntityID &&
                                                                       x.FromLanguage.ID == fromConcrete.EntityID &&
                                                                       x.ToLanguage.ID == toConcrete.EntityID);
            if (stats == null)
            {
                stats =
                    _ctx.Statistics.Add(new WordStatisticsPerUser
                    {
                        User = _userRep.FindEntity(userConcrete),
                        Word = _wordRep.FindEntity(wordConcrete),
                        FromLanguage = _langRep.FindEntity(fromConcrete),
                        ToLanguage = _langRep.FindEntity(toConcrete),
                        Successes = 0,
                        Tries = 0
                    });
                _ctx.SaveChanges();
            };
            return FromEntity(stats);
        }
    }
}
