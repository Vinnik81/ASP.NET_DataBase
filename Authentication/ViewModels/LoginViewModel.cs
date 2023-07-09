﻿using System.ComponentModel.DataAnnotations;

namespace Authentication.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [MinLength(1)]
        public string Login { get; set; }
        [Required]
        [MinLength(1)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
