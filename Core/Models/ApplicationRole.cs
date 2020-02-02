using Microsoft.AspNetCore.Identity;
using System;
// ReSharper disable ClassNeverInstantiated.Global

namespace CoreTemplate.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
