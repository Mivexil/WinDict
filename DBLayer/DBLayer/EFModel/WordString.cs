using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stachowski.WinDict.DBLayer.EFModel
{
    internal class WordString
    {
        public virtual int ID { get; set; }
        [Required]
        public virtual Language Language { get; set; }
        [Required]
        public virtual string Word { get; set; }
    }
}
