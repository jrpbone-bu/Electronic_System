using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace electronics
{
    public partial class Form4 : Form
    {
        private readonly Button btnUserManagement = new();
        private readonly Button btnLogout = new();

        public Form4()
        {
            InitializeComponent();
            ConfigureDashboard();
        }

        private void ConfigureDashboard()
        {
            label2.Text = $"Welcome, {AppSession.CurrentAccount?.FullName ?? "User"}!";

            btnUserManagement.Font = new Font("Segoe UI", 12F);
            btnUserManagement.Location = new Point(842, 83);
            btnUserManagement.Name = "btnUserManagement";
            btnUserManagement.Size = new Size(125, 34);
            btnUserManagement.TabIndex = 9;
            btnUserManagement.Text = "USERS";
            btnUserManagement.UseVisualStyleBackColor = true;
            btnUserManagement.Click += btnUserManagement_Click;
            Controls.Add(btnUserManagement);

            btnLogout.Font = new Font("Segoe UI", 12F);
            btnLogout.Location = new Point(37, ClientSize.Height - 58);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(125, 34);
            btnLogout.TabIndex = 10;
            btnLogout.Text = "LOGOUT";
            btnLogout.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            Controls.Add(btnLogout);

            button1.Click += button1_Click;
            button3.Click += button3_Click;
        }

        private void button1_Click(object? sender, EventArgs e)
        {
            using Form5 reportGenerator = new();
            reportGenerator.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object? sender, EventArgs e)
        {
            using Form3 about = new();
            about.ShowDialog(this);
        }

        private void btnUserManagement_Click(object? sender, EventArgs e)
        {
            using AccountManagementForm accountManagementForm = new();
            accountManagementForm.ShowDialog(this);
        }

        private void btnLogout_Click(object? sender, EventArgs e)
        {
            AppSession.CurrentAccount = null;
            Close();
        }
    }
}
