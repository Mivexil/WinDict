using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stachowski.WinDict.DBLayer;
using Stachowski.WinDict.DBLayer.ContractImplementation.SpecializedRepositories;
using Stachowski.WinDict.DBLayer.EFModel;
using Stachowski.WinDict.Interfaces;

namespace DBLayer.Tests
{
    [TestClass]
    public class UnitTest1
    {

        private static ILanguageRepository _langRep;
        private static IWordRepository _wordRep;
        private static IWordStatisticsRepository _wsRep;
        private static IUserRepository _userRep;
        private static WinDictContext _ctx;

        [ClassInitialize]
        public static void Init(TestContext t)
        {
            _langRep = new LanguageRepository();
            _wordRep = new WordRepository();
            _wsRep = new StatisticsRepository();
            _userRep = new UserRepository();
            _ctx = WinDictContextFactory.Context.Value;
        }

        [TestCleanup]
        public void Clear()
        {
            _ctx.Database.ExecuteSqlCommand("DELETE FROM [WordStatisticsPerUsers]");
            _ctx.Database.ExecuteSqlCommand("DELETE FROM [WordStrings]");
            _ctx.Database.ExecuteSqlCommand("DELETE FROM [WordInDictionaries]");
            _ctx.Database.ExecuteSqlCommand("DELETE FROM [Users]");
            _ctx.Database.ExecuteSqlCommand("DELETE FROM [Languages]");
            _ctx.SaveChanges();
        }

        [TestMethod]
        public void CreateLanguage()
        {
            _langRep.AddLanguage("langA");
            var l = _langRep.GetByName("langA");
            Assert.IsNotNull(l);
            Assert.AreEqual("langA", l.Name);
        }

        [TestMethod]
        public void CreateWord()
        {
            _langRep.AddLanguage("langB");
            _langRep.AddLanguage("langC");
            _langRep.AddLanguage("langD");
            _wordRep.CreateWord(new Dictionary<ILanguage, String>
            {
                {_langRep.GetByName("langB"), "wordInLangB"},
                {_langRep.GetByName("langC"), "wordInLangC"},
                {_langRep.GetByName("langD"), "wordInLangD"}
            }, "Music", new Bitmap(Image.FromFile("C:\\Users\\Public\\Pictures\\Sample Pictures\\Penguins.jpg")));
            var result = _wordRep.GetByDefinition(_langRep.GetByName("langB"), "wordInLangB");
            Assert.IsNotNull(result);
            Assert.AreEqual("Music", result.Theme);
            Assert.IsTrue(result.Definitions.Any(x => x.Key.Name == "langD" && x.Value == "wordInLangD"));
        }

        [TestMethod]
        public void AddStatistics()
        {
            _userRep.AddUser("Stefan");
            _langRep.AddLanguage("langFrom");
            _langRep.AddLanguage("langTo");
            _wordRep.CreateWord(new Dictionary<ILanguage, String>
            {
                {_langRep.GetByName("langFrom"), "F"},
                {_langRep.GetByName("langTo"), "T"}
            }, "", null);
            _wsRep.AddTry(_wordRep.GetByDefinition(_langRep.GetByName("langFrom"), "F"),
                _userRep.GetByNick("Stefan"),
                _langRep.GetByName("langFrom"),
                _langRep.GetByName("langTo"),
                true);
            _wsRep.AddTry(_wordRep.GetByDefinition(_langRep.GetByName("langFrom"), "F"),
                _userRep.GetByNick("Stefan"),
                _langRep.GetByName("langFrom"),
                _langRep.GetByName("langTo"),
                true);
            _wsRep.AddTry(_wordRep.GetByDefinition(_langRep.GetByName("langFrom"), "F"),
                _userRep.GetByNick("Stefan"),
                _langRep.GetByName("langFrom"),
                _langRep.GetByName("langTo"),
                false);
            var stats = _wsRep.GetStatistics(_userRep.GetByNick("Stefan"),
                _wordRep.GetByDefinition(_langRep.GetByName("langFrom"), "F"),
                _langRep.GetByName("langFrom"),
                _langRep.GetByName("langTo"));
            Assert.IsNotNull(stats);
            Assert.AreEqual(3, stats.Tries);
            Assert.AreEqual(2, stats.Successes);
        }

        [TestMethod]
        public void AddStatisticsThenRemoveUser()
        {
            _userRep.AddUser("Andrzej");
            _langRep.AddLanguage("langFrom");
            _langRep.AddLanguage("langTo");
            _wordRep.CreateWord(new Dictionary<ILanguage, String>
            {
                {_langRep.GetByName("langFrom"), "F"},
                {_langRep.GetByName("langTo"), "T"}
            }, "", null);
            _wsRep.AddTry(_wordRep.GetByDefinition(_langRep.GetByName("langFrom"), "F"),
                _userRep.GetByNick("Andrzej"),
                _langRep.GetByName("langFrom"),
                _langRep.GetByName("langTo"),
                true);
            _wsRep.AddTry(_wordRep.GetByDefinition(_langRep.GetByName("langFrom"), "F"),
                _userRep.GetByNick("Andrzej"),
                _langRep.GetByName("langFrom"),
                _langRep.GetByName("langTo"),
                true);
            _wsRep.AddTry(_wordRep.GetByDefinition(_langRep.GetByName("langFrom"), "F"),
                _userRep.GetByNick("Andrzej"),
                _langRep.GetByName("langFrom"),
                _langRep.GetByName("langTo"),
                false);
            _userRep.DeleteUser(_userRep.GetByNick("Andrzej"));
            var stats = _wsRep.GetAllStatistics().Where(x => x.User.Nick == "Andrzej");
            Assert.IsFalse(stats != null && stats.Any());
        }
    }
}
