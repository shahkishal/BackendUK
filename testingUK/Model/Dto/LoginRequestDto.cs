﻿using System.ComponentModel.DataAnnotations;

namespace testingUK.Model.Dto
{
    public class LoginRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; } 
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
