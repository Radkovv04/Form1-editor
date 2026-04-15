using App.ViewModels;
using System;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using App.Models.Models;
using App.Views.Views;

namespace App.Views
{
    public class BaseDynamicForm : Form
    {
    }

    public partial class DynamicListForm<T> : BaseDynamicForm where T : class
    {
        private readonly DynamicListViewModel<T> _viewModel;
        public T SelectedItem { get; private set; }
        private CheckedListBox clbColumns;
        private Button btnSelect;

        public DynamicListForm(int? initialCategoryId = null)
        {
            InitializeComponent();

            _viewModel = new DynamicListViewModel<T>();

            SetupSelectButton();
            SetupColumnSelector();

            FilterGenerator.GenerateFilters<T>(pnlFilters, _viewModel.SearchFilters, () => {
                _viewModel.SearchCommand.Execute(null);
                RefreshGrid();
            });

            if (initialCategoryId.HasValue && typeof(T) == typeof(Note))
            {
                _viewModel.SearchFilters["CategoryId"] = initialCategoryId.Value.ToString();
                _viewModel.SearchCommand.Execute(null);
            }

            dgvData.DataSource = _viewModel.Items;

            dgvData.CellFormatting += (s, e) => {
                if (e.Value == null || e.RowIndex < 0) return;
                var colName = dgvData.Columns[e.ColumnIndex].Name;
                if (colName == "Category" && e.Value is Category cat) { e.Value = cat.Name; e.FormattingApplied = true; }
                if (colName == "Notes" && e.Value is IEnumerable<Note> notes)
                {
                    var titles = notes.Select(n => n.Title).ToList();
                    e.Value = titles.Any() ? string.Join(", ", titles) : "(No notes)";
                    e.FormattingApplied = true;
                }
            };

            dgvData.DataBindingComplete += (s, e) => {
                if (dgvData.Columns.Contains("Revisions")) dgvData.Columns["Revisions"].Visible = false;

                if (clbColumns.Items.Count == 0) PopulateColumnSelector();
            };

            dgvData.SelectionChanged += (s, e) => {
                if (dgvData.CurrentRow?.DataBoundItem is T selected) SelectedItem = selected;
            };
        }

        private void RefreshGrid()
        {
            dgvData.DataSource = _viewModel.Items;
        }

        private void SetupSelectButton()
        {
            btnSelect = new Button
            {
                Text = "Select & Close",
                Dock = DockStyle.Bottom,
                Height = 45,
                BackColor = System.Drawing.Color.LightGreen,
                Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Bold)
            };

            btnSelect.Click += (s, e) => {
                if (SelectedItem != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please select an item from the list.");
                }
            };

            this.Controls.Add(btnSelect);
        }

        private void SetupColumnSelector()
        {
            Label lbl = new Label { Text = " ", Dock = DockStyle.Top, Height = 20 };
            clbColumns = new CheckedListBox { Dock = DockStyle.Right, Width = 150 };
            clbColumns.ItemCheck += (s, e) => {
                string colName = clbColumns.Items[e.Index].ToString();
                if (dgvData.Columns.Contains(colName))
                    dgvData.Columns[colName].Visible = (e.NewValue == CheckState.Checked);
            };
            this.Controls.Add(clbColumns);
            this.Controls.Add(lbl);
        }

        private void PopulateColumnSelector()
        {
            clbColumns.Items.Clear();
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                if (col.Name != "Revisions")
                {
                    clbColumns.Items.Add(col.Name, col.Visible);
                }
            }
        }

    }
}