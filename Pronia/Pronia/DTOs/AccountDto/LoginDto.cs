﻿using System.ComponentModel.DataAnnotations;

namespace Pronia.DTOs.AccountDto
{
    public class LoginDto
    {
        [Required]
        public string UsernameOrEmail {  get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRemember {  get; set; }
    }
}
