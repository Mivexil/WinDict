using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stachowski.WinDict.DBLayer.EFModel;
using Stachowski.WinDict.Interfaces;

namespace Stachowski.WinDict.DBLayer.ContractImplementation.SpecializedRepositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private WinDictContext _ctx = WinDictContextFactory.Context.Value;

        internal ILanguage FromEntity(EFModel.Language entity)
        {
            return new Language { EntityID = entity.ID, Name = entity.Name };
        }

        internal EFModel.Language ToNewEntity(ILanguage language)
        {
            return new EFModel.Language { Name = language.Name };
        }

        internal EFModel.Language FindEntity(ILanguage language)
        {
            var concrete = language as Language;
            if (concrete == null) throw new ArgumentException("Invalid language.");
            return _ctx.Languages.Find(concrete.EntityID);
        }

        public ILanguage GetByName(string name)
        {
            var language = _ctx.Languages.FirstOrDefault(x => x.Name == name);
            if (language == null) return null;
            return FromEntity(language);
        }

        public IEnumerable<ILanguage> GetAllLanguages()
        {
            return _ctx.Languages.ToList().Select(FromEntity);
        }

        public void AddLanguage(string name)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentException("Name cannot be empty.");
            if (_ctx.Languages.Any(x => x.Name == name)) throw new ArgumentException("Language already exists.");
            _ctx.Languages.Add(new EFModel.Language {Name = name});
            _ctx.SaveChanges();
        }

        public void DeleteLanguage(ILanguage language)
        {
            var lEntity = FindEntity(language);
            if (lEntity != null) _ctx.Languages.Remove(lEntity);
            _ctx.SaveChanges();
        }

        public void DeleteLanguage(string name)
        {
            var lEntity = _ctx.Languages.FirstOrDefault(x => x.Name == name);
            if (lEntity != null) _ctx.Languages.Remove(lEntity);
            _ctx.SaveChanges();
        }

        public void RenameLanguage(ILanguage language, string newName)
        {
            if (newName == null) throw new ArgumentNullException("newName");
            var lEntity = FindEntity(language);
            if (lEntity == null) throw new ArgumentException("Invalid language.");
            lEntity.Name = newName;
            _ctx.SaveChanges();
        }
    }
}
