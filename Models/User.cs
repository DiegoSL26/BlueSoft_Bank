﻿namespace Bluesoft_Bank.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
