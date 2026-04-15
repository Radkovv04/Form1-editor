using App.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace App.Views.Views
{
    public static class FilterGenerator
    {
        public static void GenerateFilters<T>(FlowLayoutPanel panel, Dictionary<string, string> filterDictionary, Action onSearch)
        {
            panel.Controls.Clear(); // Това трие btnSearch, което е ОК, ако не го ползваш (търсенето ти е автоматично)
            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                var attr = prop.GetCustomAttribute<SearchableAttribute>();
                if (attr != null)
                {
                    Label lbl = new Label
                    {
                        Text = "Search by " + attr.Label + ":", // Добавяме "Search by" за яснота
                        AutoSize = true,
                        Margin = new Padding(10, 15, 0, 0)
                    };
                    TextBox txt = new TextBox
                    {
                        Name = "txt_" + prop.Name,
                        Width = 150,
                        Margin = new Padding(5, 12, 0, 0)
                    };

                    txt.TextChanged += (s, e) => {
                        filterDictionary[prop.Name] = txt.Text;
                        onSearch?.Invoke();
                    };

                    panel.Controls.Add(lbl);
                    panel.Controls.Add(txt);
                }
            }
        }
    }
}