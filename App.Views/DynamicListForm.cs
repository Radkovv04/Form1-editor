using App.ViewModels;
using App.Models;
using System;
using System.Windows.Forms;

namespace App.Views
{
    // 1. The Main Generic Form
    public partial class DynamicListForm<T> : BaseDynamicForm where T : class
    {
        private readonly DynamicListViewModel<T> _viewModel;

        // This is the property Form1 looks for!
        public T SelectedItem { get; private set; }

        public DynamicListForm()
        {
            InitializeComponent();
            _viewModel = new DynamicListViewModel<T>();

            // 1. MANUALLY CREATE THE BUTTON
            // Since it's not in the designer, we make it here:
            Button btnSelect = new Button
            {
                Text = "Select & Close",
                Dock = DockStyle.Bottom,
                Height = 45,
                BackColor = System.Drawing.Color.LightGreen,
                Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Bold)
            };

            // 2. ADD IT TO THE FORM
            this.Controls.Add(btnSelect);

            // 3. GENERATE FILTERS
            FilterGenerator.GenerateFilters<T>(pnlFilters, _viewModel.SearchFilters);

            // 4. BIND DATA
            dgvData.DataSource = _viewModel.Items;

            // 5. SEARCH LOGIC
            btnSearch.Click += (s, e) => {
                _viewModel.SearchCommand.Execute(null);
                dgvData.DataSource = null;
                dgvData.DataSource = _viewModel.Items;
            };

            // 6. SELECTION LOGIC
            dgvData.SelectionChanged += (s, e) => {
                if (dgvData.CurrentRow?.DataBoundItem is T selected)
                {
                    SelectedItem = selected;
                }
            };

            // 7. SELECT BUTTON CLICK LOGIC
            btnSelect.Click += (s, e) => {
                if (SelectedItem != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please select an item from the list first.");
                }
            };
        }
    }

    // 2. The "Designer Trick" Class
    // This MUST stay simple so the WinForms Designer doesn't crash.
    public class BaseDynamicForm : Form
    {
        // Keep this empty. The actual logic lives in the Generic class above.
    }
}