using System;

namespace App.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchableAttribute : Attribute
    {
        public string Label { get; set; }
        public SearchableAttribute(string label) { Label = label; }
    }
}