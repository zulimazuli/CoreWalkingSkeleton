using System;
using System.ComponentModel.DataAnnotations;
using CoreTemplate.ApplicationCore.Entities;

namespace CoreTemplate.ApplicationCore.Models
{
    public class Item : BaseEntity<int>
    {
        [ScaffoldColumn(false)]
        public Guid UniqueId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [EnumDataType(typeof(ItemPriority))]
        public ItemPriority Priority { get; set; }
    }

    public enum ItemPriority
    {
        High = 1,
        Medium = 2,
        Low = 3
    }
}
