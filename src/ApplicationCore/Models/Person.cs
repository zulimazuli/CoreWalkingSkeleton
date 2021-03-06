﻿using CoreTemplate.ApplicationCore.Identity;

namespace CoreTemplate.ApplicationCore.Models
{
    public class Person
    { 

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
