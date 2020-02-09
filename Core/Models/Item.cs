using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTemplate.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
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
