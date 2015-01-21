using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Stachowski.WinDict.DBLayer.EFModel
{
    internal class WordInDictionary : IEFModel
    {
        public WordInDictionary()
        {
            Definitions = new Collection<WordString>();
        }
        public virtual int ID { get; set; }
        public virtual string Theme { get; set; }
        public virtual byte[] Image { get; set; }
        public virtual ICollection<WordString> Definitions { get; set; }
    }
}
