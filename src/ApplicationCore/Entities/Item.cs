using System;
using System.ComponentModel.DataAnnotations;
using CoreTemplate.ApplicationCore.Enums;

namespace CoreTemplate.ApplicationCore.Entities
{
    public class Item : AuditableEntity
    {
        public int Id { get; set; }
        [ScaffoldColumn(false)]
        public Guid UniqueId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemPriority Priority { get; set; }
    }
}
