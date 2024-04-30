using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace EntityLayer.Definition
{
    public class Technique : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TechniqueID { get; set; }

        [Required]
        [DisplayName("Saç Ekim Tekniği")]
        public string Title { get; set; }
    }
}
