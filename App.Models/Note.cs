using App.Models.Attributes;
using System;
using System.Collections.Generic;

namespace App.Models
{
    public class Note
    {
        public int Id { get; set; }

        [GridColumn("Note Title")]
        [Searchable("Search by Title...")]
        public string Title { get; set; } = "New Note";

        [GridColumn("Content Snippet")]
        public string Content { get; set; } = "";

        [GridColumn("Last Modified Date")]
        public DateTime LastModified { get; set; } = DateTime.Now;

        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public List<NoteRevision> Revisions { get; set; } = new();
    }

    public class NoteRevision
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public string Content { get; set; } = "";
        public DateTime SavedAt { get; set; } = DateTime.Now;
    }
}