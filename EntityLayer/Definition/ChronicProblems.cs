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
    public class ChronicProblems
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChronicProblemsID { get; set; }

        [DisplayName("Kronik Hastalık")]
        public string Title { get; set; }
    }
}
