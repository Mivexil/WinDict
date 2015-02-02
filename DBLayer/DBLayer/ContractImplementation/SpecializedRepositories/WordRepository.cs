using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Stachowski.WinDict.DBLayer.EFModel;
using Stachowski.WinDict.Interfaces;

namespace Stachowski.WinDict.DBLayer.ContractImplementation.SpecializedRepositories
{
    public class WordRepository : IWordRepository
    {
        private WinDictContext _ctx = WinDictContextFactory.Context.Value;
        private LanguageRepository _langRep = new LanguageRepository();

        internal IWord FromEntity(WordInDictionary entity)
        {
            Bitmap pic;
            try
            {
                using (var ms = new MemoryStream(entity.Image))
                {
                    var original = ((Bitmap) Image.FromStream(ms));
                    var copy = new Bitmap(original.Width, original.Height);
                    using (Graphics g = Graphics.FromImage(copy))
                    {
                        g.DrawImage(original, 0, 0, original.Width, original.Height);
                    }
                    pic = copy;                    
                }
            }
            catch (ArgumentException)
            {
                pic = null;
            }
            return new Word
            {
                EntityID = entity.ID,
                Picture = pic,
                Theme = entity.Theme,
                Definitions = entity.Definitions.ToDictionary(x => _langRep.FromEntity(x.Language), x => x.Word)
            };
        }

        internal WordInDictionary ToNewEntity(IWord word)
        {
            var definitions = new List<WordString>();
            foreach (var kvp in word.Definitions)
            {
                var lang = _langRep.FindEntity(kvp.Key);
                var ws = _ctx.WordStrings.FirstOrDefault(x => x.Language.ID == lang.ID && x.Word == kvp.Value);
                definitions.Add(ws ?? new WordString {Language = lang, Word = kvp.Value});
            }
            return new WordInDictionary
            {
                Theme = word.Theme,
                Image = (word.Picture == null ? null : (byte[]) (new ImageConverter()).ConvertTo(word.Picture, typeof (byte[]))),
                Definitions = definitions
            };
        }

        internal WordInDictionary FindEntity(IWord word)
        {
            var concrete = word as Word;
            if (concrete == null) throw new ArgumentException("Invalid language.");
            return _ctx.Words.Find(concrete.EntityID);
        }

        public IWord GetByDefinition(ILanguage language, string definition)
        {
            var lang = _langRep.FindEntity(language);
            var word =
                _ctx.Words.FirstOrDefault(x => x.Definitions.Any(y => y.Language.ID == lang.ID && y.Word == definition));
            if (word == null) return null;
            return FromEntity(word);
        }

        public IEnumerable<IWord> GetAll()
        {
            return _ctx.Words.Include("Definitions").Include("Definitions.Language").ToList().Select(FromEntity);
        }

        public IEnumerable<IWord> GetAllWithTheme(string theme)
        {
            return _ctx.Words.Include("Definitions").Include("Definitions.Language").Where(x => x.Theme == theme).ToList().Select(FromEntity);
        }

        public void UpdatePicture(IWord word, Bitmap newPicture)
        {
            var entity = FindEntity(word);
            entity.Image = (byte[]) (new ImageConverter()).ConvertTo(word.Picture, typeof (byte[]));
            _ctx.SaveChanges();
        }

        public void ChangeTheme(IWord word, string newTheme)
        {
            if (newTheme == null) throw new ArgumentNullException("newTheme");
            var entity = FindEntity(word);
            entity.Theme = newTheme;
            _ctx.SaveChanges();
        }

        public void ChangeDefinition(IWord word, ILanguage language, string newDefinition)
        {
            ChangeDefinition(word, language, newDefinition, true);
        }

        private void ChangeDefinition(IWord word, ILanguage language, string newDefinition, bool saveChanges)
        {
            if (newDefinition == null) throw new ArgumentNullException("newDefinition");
            var entity = FindEntity(word);
            var lang = _langRep.FindEntity(language);
            var definition = entity.Definitions.FirstOrDefault(x => x.Language.ID == lang.ID);
            if (definition == null)
            {
                definition = new WordString {Language = lang, Word = newDefinition};
                _ctx.WordStrings.Add(definition);
                entity.Definitions.Add(definition);
            }
            else
            {
                definition.Word = newDefinition;
            }
            if (saveChanges)
            {
                _ctx.SaveChanges();
            }
        }

        public void ChangeDefinition(IWord word, IDictionary<ILanguage, string> newDefinitions)
        {
            foreach (var kvp in newDefinitions)
            {
                ChangeDefinition(word, kvp.Key, kvp.Value, false);
            }
            _ctx.SaveChanges();
        }

        public void RemoveDefinition(IWord word)
        {
            var entity = FindEntity(word);
            foreach (var def in entity.Definitions)
            {
                entity.Definitions.Remove(def);
            }
            _ctx.SaveChanges();
        }

        public void RemoveDefinition(IWord word, ILanguage language)
        {
            var entity = FindEntity(word);
            var lang = _langRep.FindEntity(language);
            foreach (var def in entity.Definitions.Where(x => x.Language.ID == lang.ID))
            {
                entity.Definitions.Remove(def);
            }
            _ctx.SaveChanges();
        }

        public void RemoveDefinition(IWord word, IEnumerable<ILanguage> languages)
        {
            var entity = FindEntity(word);
            var langs = languages.Select(x => _langRep.FindEntity(x)).ToList();
            foreach (var def in entity.Definitions)
            {
                if (langs.Any(x => x.ID == def.Language.ID))
                {
                    entity.Definitions.Remove(def);
                }
            }
            _ctx.SaveChanges();
        }

        public void RemoveWord(IWord word)
        {
            var entity = FindEntity(word);
            if (entity != null) _ctx.Words.Remove(entity);
            _ctx.SaveChanges();
        }

        public void CreateWord(IDictionary<ILanguage, string> definitions, string theme, Bitmap picture)
        {
            var entity = ToNewEntity(new Word {Definitions = definitions, Theme = theme, Picture = picture});
            _ctx.Words.Add(entity);
            _ctx.SaveChanges();
        }
    }
}
