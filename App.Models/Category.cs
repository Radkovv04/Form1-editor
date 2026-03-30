using App.Models.Attributes;
using System.Collections.Generic;

namespace App.Models
{
    public class Category
    {
        public int Id { get; set; }

        [GridColumn("Category Name")]
        [Searchable("Search by Category Name...")]
        public string Name { get; set; } = "General";
        [GridColumn("Description")]
        [Searchable("Search by Description...")]
        public string Description { get; set; } = "";
        public List<Note> Notes { get; set; } = new();
    }
}