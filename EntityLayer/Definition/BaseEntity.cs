using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Definition
{
    public class BaseEntity
    {
        public bool IsActive { get; set; }=true;
        public DateTime CreateDate { get; set; }= DateTime.Now;
    }
}
