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
    public class InfectiousDisease: BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InfectiousDiseaseID { get; set; }

        [Required]

        [DisplayName("Bulaşıcı Hastalık")]
        public string Title { get; set; }
    }
}
