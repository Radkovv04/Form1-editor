using App.Models.Attributes;
using System.Collections.Generic;

namespace App.Models.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Searchable("Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
    }
}