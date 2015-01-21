using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Stachowski.WinDict.Interfaces;

namespace WinDict.AssemblyBinder
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
