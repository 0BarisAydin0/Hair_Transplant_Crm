using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class CustomerDatabaseConfigs
    {
        public CustomerDatabaseConfigs()
        {
            CreatedDate = DateTime.Now;
            IsActive = true;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string DbName { get; set; }
        public string ConnectionString { get; set; }
        public int CustomerNumber { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
    }
}
