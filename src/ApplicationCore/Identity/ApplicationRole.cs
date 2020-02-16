using System;
using Microsoft.AspNetCore.Identity;

// ReSharper disable ClassNeverInstantiated.Global

namespace CoreTemplate.ApplicationCore.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
