namespace App.Views
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            btnUndo = new Button();
            btnSave = new Button();
            btnRedo = new Button();
            lbRevisions = new ListBox();
            btnManageNotes = new Button();
            btnManageCategories = new Button();
            cbCategories = new ComboBox();
            txtContent = new TextBox();
            btnDelete = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnUndo
            // 
            btnUndo.BackColor = SystemColors.ControlLight;
            btnUndo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnUndo.ForeColor = Color.Tomato;
            btnUndo.Location = new Point(113, 379);
            btnUndo.Name = "btnUndo";
            btnUndo.Size = new Size(75, 23);
            btnUndo.TabIndex = 1;
            btnUndo.Text = "↩ Undo";
            btnUndo.UseVisualStyleBackColor = false;
            btnUndo.Click += btnUndo_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = SystemColors.ControlLight;
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.ForeColor = Color.FromArgb(0, 192, 0);
            btnSave.Location = new Point(474, 379);
            btnSave.Name = "btnSave";
            btnSave.RightToLeft = RightToLeft.No;
            btnSave.Size = new Size(135, 23);
            btnSave.TabIndex = 3;
            btnSave.Text = "📝 Save to Database";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnRedo
            // 
            btnRedo.BackColor = SystemColors.ControlLight;
            btnRedo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRedo.ForeColor = SystemColors.MenuHighlight;
            btnRedo.Location = new Point(30, 379);
            btnRedo.Name = "btnRedo";
            btnRedo.Size = new Size(77, 23);
            btnRedo.TabIndex = 4;
            btnRedo.Text = "↪ Redo\r\n";
            btnRedo.UseVisualStyleBackColor = false;
            btnRedo.Click += btnRedo_Click;
            // 
            // lbRevisions
            // 
            lbRevisions.FormattingEnabled = true;
            lbRevisions.Location = new Point(630, 34);
            lbRevisions.Name = "lbRevisions";
            lbRevisions.Size = new Size(158, 379);
            lbRevisions.TabIndex = 5;
            // 
            // btnManageNotes
            // 
            btnManageNotes.BackColor = SystemColors.ControlLight;
            btnManageNotes.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnManageNotes.Location = new Point(393, 52);
            btnManageNotes.Name = "btnManageNotes";
            btnManageNotes.Size = new Size(75, 23);
            btnManageNotes.TabIndex = 6;
            btnManageNotes.Text = "Notes";
            btnManageNotes.UseVisualStyleBackColor = false;
            btnManageNotes.Click += btnManageNotes_Click;
            // 
            // btnManageCategories
            // 
            btnManageCategories.BackColor = SystemColors.ControlLight;
            btnManageCategories.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnManageCategories.Location = new Point(145, 52);
            btnManageCategories.Name = "btnManageCategories";
            btnManageCategories.Size = new Size(75, 23);
            btnManageCategories.TabIndex = 7;
            btnManageCategories.Text = "Categories";
            btnManageCategories.UseVisualStyleBackColor = false;
            // 
            // cbCategories
            // 
            cbCategories.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCategories.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            cbCategories.FormattingEnabled = true;
            cbCategories.Location = new Point(18, 52);
            cbCategories.Name = "cbCategories";
            cbCategories.Size = new Size(121, 23);
            cbCategories.TabIndex = 9;
            // 
            // txtContent
            // 
            txtContent.Location = new Point(18, 81);
            txtContent.Multiline = true;
            txtContent.Name = "txtContent";
            txtContent.ScrollBars = ScrollBars.Vertical;
            txtContent.Size = new Size(591, 281);
            txtContent.TabIndex = 10;
            txtContent.Text = "Example text here . . .";
            // 
            // btnDelete
            // 
            btnDelete.BackColor = SystemColors.ControlLight;
            btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDelete.ForeColor = Color.IndianRed;
            btnDelete.Location = new Point(474, 52);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(135, 23);
            btnDelete.TabIndex = 11;
            btnDelete.Text = "🗑 Delete A Note...";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.Location = new Point(662, 12);
            label1.Name = "label1";
            label1.Size = new Size(91, 19);
            label1.TabIndex = 12;
            label1.Text = "Logs history";
            label1.Click += label1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(btnDelete);
            Controls.Add(txtContent);
            Controls.Add(cbCategories);
            Controls.Add(btnManageCategories);
            Controls.Add(btnManageNotes);
            Controls.Add(lbRevisions);
            Controls.Add(btnRedo);
            Controls.Add(btnSave);
            Controls.Add(btnUndo);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnUndo;
        private Button btnSave;
        private Button btnRedo;
        private ListBox lbRevisions;
        private Button btnManageNotes;
        private Button btnManageCategories;
        private ComboBox cbCategories;
        private TextBox txtContent;
        private Button btnDelete;
        private Label label1;
    }
}
