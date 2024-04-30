using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class ReminderDatesDTO
    {
        public bool? Add { get; set; }
        public bool? Update { get; set; }
        public int? ReminderDateID { get; set; }
        public int? RemindDayCount { get; set; }
        public string? RemindDayCountName { get; set; }



    }
}
