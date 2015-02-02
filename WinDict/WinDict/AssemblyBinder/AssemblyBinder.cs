using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Stachowski.WinDict.Interfaces;

namespace Stachowski.WinDict.AssemblyBinder
{
    public static class AssemblyBinder
    {
        private static Assembly _loadedAssembly;
        private static string _assemblyName;
        private static string _namespaceName;
        private static string _langRepTypename;
        private static string _statsRepTypename;
        private static string _userRepTypename;
        private static string _wordRepTypename;

        public static IWordRepository WordRepository = GetWordRepository();
        public static IUserRepository UserRepository = GetUserRepository();
        public static IWordStatisticsRepository StatisticsRepository = GetWordStatisticsRepository();
        public static ILanguageRepository LanguageRepository = GetLanguageRepository();

        public static void CheckAndSeedDatabase()
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains("SeedDatabase") &&
                ConfigurationManager.AppSettings["SeedDatabase"] == "True" &&
                !WordRepository.GetAll().Any() &&
                !UserRepository.GetAllUsers().Any() &&
                !StatisticsRepository.GetAllStatistics().Any() &&
                !LanguageRepository.GetAllLanguages().Any())
            {
                LanguageRepository.AddLanguage("English");
                LanguageRepository.AddLanguage("Polish");
                UserRepository.AddUser("Andrzej");
                UserRepository.AddUser("Stefan");
                UserRepository.AddUser("Bogdan");
                var eng = LanguageRepository.GetByName("English");
                var pol = LanguageRepository.GetByName("Polish");
                WordRepository.CreateWord(new Dictionary<ILanguage, string>
                {
                    {eng, "Cow"},
                    {pol, "Krowa"}
                }, "Animals", null);
                WordRepository.CreateWord(new Dictionary<ILanguage, string>
                {
                    {eng, "Pig"},
                    {pol, "Świnia"}
                }, "Animals", null);
                WordRepository.CreateWord(new Dictionary<ILanguage, string>
                {
                    {eng, "Dog"},
                    {pol, "Pies"}
                }, "Animals", null);
                WordRepository.CreateWord(new Dictionary<ILanguage, string>
                {
                    {eng, "Cat"},
                    {pol, "Kot"}
                }, "Animals", null);
                WordRepository.CreateWord(new Dictionary<ILanguage, string>
                {
                    {eng, "Carrot"},
                    {pol, "Marchewka"}
                }, "Vegetables", null);
                WordRepository.CreateWord(new Dictionary<ILanguage, string>
                {
                    {eng, "Cabbage"},
                    {pol, "Kapusta"}
                }, "Vegetables", null);
                WordRepository.CreateWord(new Dictionary<ILanguage, string>
                {
                    {eng, "Tomato"},
                    {pol, "Pomidor"}
                }, "Vegetables", null);
            }
        }

        public static void Refresh()
        {
            WordRepository = GetWordRepository();
            UserRepository = GetUserRepository();
            StatisticsRepository = GetWordStatisticsRepository();
            LanguageRepository = GetLanguageRepository();
        }

        private static void TestAndReload(string repSettingInFile, ref string repSettingInClass)
        {
            var assName = ConfigurationManager.AppSettings["DBLayerAssembly"];
            if (assName != _assemblyName)
            {
                _loadedAssembly = Assembly.LoadFrom(assName);
                _assemblyName = assName;
            }
            _namespaceName = ConfigurationManager.AppSettings["RepositoriesNamespace"];
            repSettingInClass = ConfigurationManager.AppSettings[repSettingInFile];
        }

        private static IWordRepository GetWordRepository()
        {
            TestAndReload("WordRepositoryType", ref _wordRepTypename);
            var type = _loadedAssembly.GetType(String.Format("{0}.{1}", _namespaceName, _wordRepTypename));
            return (IWordRepository)(type.GetConstructor(new Type[] {}).Invoke(new object[] {}));
        }

        private static ILanguageRepository GetLanguageRepository()
        {
            TestAndReload("LanguageRepositoryType", ref _langRepTypename);
            var type = _loadedAssembly.GetType(String.Format("{0}.{1}", _namespaceName, _langRepTypename));
            return (ILanguageRepository)(type.GetConstructor(new Type[] { }).Invoke(new object[] { }));
        }

        private static IWordStatisticsRepository GetWordStatisticsRepository()
        {
            TestAndReload("StatisticsRepositoryType", ref _statsRepTypename);
            var type = _loadedAssembly.GetType(String.Format("{0}.{1}", _namespaceName, _statsRepTypename));
            return (IWordStatisticsRepository)(type.GetConstructor(new Type[] { }).Invoke(new object[] { }));
        }

        private static IUserRepository GetUserRepository()
        {
            TestAndReload("UserRepositoryType", ref _userRepTypename);
            var type = _loadedAssembly.GetType(String.Format("{0}.{1}", _namespaceName, _userRepTypename));
            return (IUserRepository)(type.GetConstructor(new Type[] { }).Invoke(new object[] { }));
        }
    }
}
