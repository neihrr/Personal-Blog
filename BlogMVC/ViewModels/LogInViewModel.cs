﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogMVC.ViewModels
{
    [Table("AspNetUsers")]

    public class LogInViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
