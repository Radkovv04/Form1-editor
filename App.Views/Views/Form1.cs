using App.Models.Models;
using App.ViewModels;
using System;
using System.Windows.Forms;

namespace App.Views
{
    public partial class Form1 : Form
    {
        private readonly MainViewModel _viewModel;
        private bool _isInternalChange = false;
        private string _lastSnapshot = "";

        public Form1()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();

            cbCategories.DataSource = _viewModel.AvailableCategories;
            cbCategories.DisplayMember = "Name";
            cbCategories.ValueMember = "Id";
            lbRevisions.DataSource = _viewModel.SessionLogs;

            txtContent.TextChanged += (s, e) =>
            {
                if (!_isInternalChange && txtContent.Focused)
                {
                    _viewModel.CurrentText = txtContent.Text;
                    _viewModel.RegisterChange(_lastSnapshot);
                    _lastSnapshot = txtContent.Text;
                }
            };

            btnSave.Click += (s, e) => {
                string newName = PromptForInput("Enter File Name to Save:", "Save New File");
                if (string.IsNullOrWhiteSpace(newName)) return;

                if (_viewModel.DoesTitleExist(newName))
                {
                    MessageBox.Show($"A file named '{newName}' already exists!", "Duplicate Name");
                    return;
                }

                int selectedCatId = 0;
                if (cbCategories.SelectedValue is int id && id > 0)
                {
                    selectedCatId = id;
                }
                else if (_viewModel.AvailableCategories.Any())
                {
                    selectedCatId = _viewModel.AvailableCategories.First().Id;
                }

                if (selectedCatId == 0)
                {
                    MessageBox.Show("Please create a category first!");
                    return;
                }

                _viewModel.CurrentTitle = newName;
                _viewModel.CurrentText = txtContent.Text;
                _viewModel.CurrentCategoryId = selectedCatId;

                _viewModel.SaveNote();
                _viewModel.LogAction($"SAVED: {newName}");
                MessageBox.Show("Saved successfully!");
            };

            btnUndo.Click += (s, e) =>
            {
                if (_viewModel.CanUndo)
                {
                    _isInternalChange = true;
                    txtContent.Text = _viewModel.Undo(txtContent.Text);
                    _viewModel.CurrentText = txtContent.Text;
                    _lastSnapshot = txtContent.Text;
                    _viewModel.LogAction("UNDO");
                    _isInternalChange = false;
                }
            };

            btnRedo.Click += (s, e) =>
            {
                if (_viewModel.CanRedo)
                {
                    _isInternalChange = true;
                    txtContent.Text = _viewModel.Redo(txtContent.Text);
                    _viewModel.CurrentText = txtContent.Text;
                    _lastSnapshot = txtContent.Text;
                    _viewModel.LogAction("REDO");
                    _isInternalChange = false;
                }
            };

            btnDelete.Click += (s, e) =>
            {
                string nameToDelete = PromptForInput("Enter the exact name of the file to delete:", "Delete Note");
                if (string.IsNullOrWhiteSpace(nameToDelete)) return;

                var confirm = MessageBox.Show($"Are you sure you want to permanently delete '{nameToDelete}'?",
                                              "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    bool success = _viewModel.DeleteNoteByName(nameToDelete);
                    if (success)
                    {
                        MessageBox.Show("Note deleted successfully.");
                        if (_viewModel.CurrentTitle.ToLower() == nameToDelete.ToLower())
                        {
                            _isInternalChange = true;
                            txtContent.Clear();
                            _viewModel.CurrentTitle = "";
                            _viewModel.CurrentNoteId = 0;
                            _lastSnapshot = "";
                            _isInternalChange = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Could not find a note named '{nameToDelete}'.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };

            lbRevisions.SelectedIndexChanged += (s, e) =>
            {
                if (lbRevisions.SelectedItem is ActionLogItem logItem && !_isInternalChange)
                {
                    _isInternalChange = true;
                    txtContent.Text = logItem.TextSnapshot;
                    _lastSnapshot = logItem.TextSnapshot;
                    _isInternalChange = false;
                }
            };

            btnManageNotes.Click += (s, e) =>
            {
                using (var noteManager = new DynamicListForm<Note>())
                {
                    if (noteManager.ShowDialog() == DialogResult.OK && noteManager.SelectedItem != null)
                    {
                        var selected = noteManager.SelectedItem;

                        _isInternalChange = true;
                        txtContent.Text = selected.Content;
                        _viewModel.CurrentText = selected.Content;
                        _viewModel.CurrentTitle = selected.Title;
                        _viewModel.CurrentNoteId = selected.Id;
                        _lastSnapshot = selected.Content;

                        _viewModel.LogAction($"LOADED: {selected.Title}");
                        _isInternalChange = false;
                    }
                }
            };
            btnManageCategories.Click += (s, e) => {
                using (var catManager = new DynamicListForm<Category>())
                {
                    if (catManager.ShowDialog() == DialogResult.OK)
                    {
                        _viewModel.LoadCategories();
                    }
                }
            };
        }



        private string PromptForInput(string labelText, string windowTitle)
        {
            Form prompt = new Form() { Width = 320, Height = 150, Text = windowTitle, StartPosition = FormStartPosition.CenterParent };
            Label lbl = new Label() { Left = 20, Top = 10, Text = labelText, Width = 280 };
            TextBox txt = new TextBox() { Left = 20, Top = 40, Width = 260 };
            Button btn = new Button() { Text = "OK", Left = 200, Top = 70, DialogResult = DialogResult.OK };
            prompt.Controls.Add(lbl); prompt.Controls.Add(txt); prompt.Controls.Add(btn);
            prompt.AcceptButton = btn;
            return prompt.ShowDialog() == DialogResult.OK ? txt.Text : "";
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {

        }

        private void btnRedo_Click(object sender, EventArgs e)
        {

        }

        private void btnManageNotes_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}