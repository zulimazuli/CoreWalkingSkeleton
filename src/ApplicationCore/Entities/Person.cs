using CoreTemplate.ApplicationCore.Entities;
using CoreTemplate.ApplicationCore.Identity;

namespace CoreTemplate.ApplicationCore.Models
{
    public class Person : BaseEntity<int>
    { 

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
