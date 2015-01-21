using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stachowski.WinDict.DBLayer.EFModel
{
    internal class User : IEFModel
    {
        public virtual int ID { get; set; }
        [Required(ErrorMessage="No nickname provided")]
        public virtual string Nick { get; set; }
    }
}
