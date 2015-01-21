using System.ComponentModel.DataAnnotations;

namespace Stachowski.WinDict.DBLayer.EFModel
{
    internal class WordStatisticsPerUser : IEFModel
    {
        public virtual int ID { get; set; }
        [Required]
        public virtual User User { get; set; }
        [Required]
        public virtual WordInDictionary Word { get; set; }
        //[Required]
        public virtual Language FromLanguage { get; set; }
        //[Required]
        public virtual Language ToLanguage { get; set; }
        [Required]
        public virtual int Tries { get; set; }
        [Required]
        public virtual int Successes { get; set; }
    }
}
