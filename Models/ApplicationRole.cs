﻿using Microsoft.AspNetCore.Identity;

namespace BankingApp.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
