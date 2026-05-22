using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace electronics
{
    public partial class Form2 : Form
    {
        private readonly AccountRepository accountRepository = new();
        private readonly TextBox txtNewPassword = new();
        private readonly TextBox txtConfirmPassword = new();
        private readonly Label lblNewPassword = new();
        private readonly Label lblConfirmPassword = new();

        public Form2()
        {
            InitializeComponent();
            ConfigurePasswordRecoveryForm();
        }

        private void ConfigurePasswordRecoveryForm()
        {
            textBox1.Text = string.Empty;
            textBox1.PlaceholderText = "Email Address";

            lblNewPassword.AutoSize = true;
            lblNewPassword.Location = new Point(16, 64);
            lblNewPassword.Text = "New Password";

            txtNewPassword.Location = new Point(16, 84);
            txtNewPassword.Size = new Size(288, 23);
            txtNewPassword.UseSystemPasswordChar = true;

            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Location = new Point(16, 116);
            lblConfirmPassword.Text = "Confirm Password";

            txtConfirmPassword.Location = new Point(16, 136);
            txtConfirmPassword.Size = new Size(288, 23);
            txtConfirmPassword.UseSystemPasswordChar = true;

            button1.Location = new Point(64, 174);
            button1.Text = "Change Password";
            button1.Click += button1_Click;

            button2.Location = new Point(112, 211);
            button2.Click += button2_Click;

            panel1.Size = new Size(320, 250);
            panel1.Controls.Add(lblNewPassword);
            panel1.Controls.Add(txtNewPassword);
            panel1.Controls.Add(lblConfirmPassword);
            panel1.Controls.Add(txtConfirmPassword);

            label3.Text = "Enter your email and new password";
        }

        private async void button1_Click(object? sender, EventArgs e)
        {
            string email = textBox1.Text.Trim();
            string password = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Enter your account email address.", "Password Recovery", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Enter your new password.", "Password Recovery", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Password Recovery", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            button1.Enabled = false;
            try
            {
                bool accountExists = await accountRepository.EmailExistsAsync(email);
                if (!accountExists)
                {
                    MessageBox.Show("No account was found for that email address.", "Password Recovery", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool updated = await accountRepository.ResetPasswordAsync(email, password);
                if (!updated)
                {
                    MessageBox.Show("Unable to update the account password.", "Password Recovery", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(
                    "Password has been changed. You can now log in with your new password.",
                    "Password Recovery",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to process password recovery.\n\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button1.Enabled = true;
            }
        }

        private void button2_Click(object? sender, EventArgs e)
        {
            Close();
        }
    }
}
