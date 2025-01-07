﻿using System.ComponentModel.DataAnnotations;

namespace NoteApi__.NET_.DTO
{
    public class UserRegistrationDto
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }


        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

    }
}
