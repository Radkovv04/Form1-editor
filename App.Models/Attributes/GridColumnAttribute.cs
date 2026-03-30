using System;

namespace App.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GridColumnAttribute : Attribute
    {
        public string HeaderText { get; }
        public GridColumnAttribute(string headerText)
        {
            HeaderText = headerText;
        }
    }
}