﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class CheckCreateResultViewModel
    {
        public string MessageType { get; set; }
        public string DuplicatePatientID { get; set; }
    }
}
