using App.Models.Attributes;
using System;
using System.Collections.Generic;

namespace App.Models.Models
{
    public class Note
    {
        public int Id { get; set; }
        [Searchable("Title")]
        public string Title { get; set; }
        [Searchable("Content")]
        public string Content { get; set; }
        public DateTime LastModified { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }

    public class NoteRevision
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public string Content { get; set; } = "";
        public DateTime SavedAt { get; set; } = DateTime.Now;

    }
}