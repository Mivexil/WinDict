using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stachowski.WinDict.DBLayer.EFModel
{
    internal class Language : IEFModel
    {
        public virtual int ID { get; set; }
        [Required(ErrorMessage = "No language name provided")]
        public virtual string Name { get; set; }
    }
}
