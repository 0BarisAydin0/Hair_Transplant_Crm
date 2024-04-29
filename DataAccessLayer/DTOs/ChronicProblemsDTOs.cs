using EntityLayer.Definition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class ChronicProblemsDTOs
    {
       public ChronicProblems chronicProblems;

       public List<ChronicProblems> chronicProblemsList;

        public bool Add {  get; set; }
        public bool Update {  get; set; }
        
        public int ChronicProblemsID { get; set; }

   
        public string Title { get; set; }
    }
}
