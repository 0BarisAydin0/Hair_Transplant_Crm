using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Definition;

namespace DataAccessLayer.DTOs
{
    public class InfectiousDiseasesDTOs
    {
        public InfectiousDisease InfectiousDisease;

        public List<InfectiousDisease> InfectiousDiseaseList;
        public int InfectiousDiseaseID { get; set; }
        public string Title { get; set; }
        public bool Add { get; set; }
        public bool Update { get; set; }

    }
}
