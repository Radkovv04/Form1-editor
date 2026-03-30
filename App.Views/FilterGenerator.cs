using App.Models.Attributes;
using System.Reflection;
using System.Windows.Forms;

namespace App.Views
{
    public static class FilterGenerator
    {
        public static void GenerateFilters<T>(FlowLayoutPanel panel, Dictionary<string, string> filterDictionary)
        {
            panel.Controls.Clear();
            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                var attr = prop.GetCustomAttribute<SearchableAttribute>();
                if (attr != null)
                {
                    Label lbl = new Label { Text = attr.Label, AutoSize = true };
                    TextBox txt = new TextBox { Name = "txt_" + prop.Name, Width = 150 };
                    txt.TextChanged += (s, e) => {
                        filterDictionary[prop.Name] = txt.Text;
                    };

                    panel.Controls.Add(lbl);
                    panel.Controls.Add(txt);
                }
            }
        }
    }
}