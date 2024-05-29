using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class ScopeDTOs
    {
        
        public int ScopeID { get; set; }

        public string Title { get; set; }
        public bool Add { get; set; }
        public bool Update { get; set; }
    }
}
