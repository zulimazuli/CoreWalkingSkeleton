using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTemplate.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string CustomTag { get; set; }
         
        //[ForeignKey("PersonalInfo")]
        public int? PersonalInformationId { get; set; }
        public virtual PersonalInformation PersonalInfo { get; set; }
    }
}
