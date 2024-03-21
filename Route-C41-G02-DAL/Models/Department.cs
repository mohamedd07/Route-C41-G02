﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route_C41_G02_DAL.Models
{
    public class Department
    {
        public int Id { get; set; }  // PK For developer

        
        public string Name { get; set; } // Code for User
        
       
        public string Code { get; set; }
        [Display(Name = "Date Of Creation")]
        public DateTime DateOfCreation { get; set; }
    }
}
