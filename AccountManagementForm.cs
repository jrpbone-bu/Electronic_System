using System.ComponentModel;

namespace electronics;

public sealed class AccountManagementForm : Form
{
    private readonly AccountRepository accountRepository = new();
    private readonly BindingList<Account> accounts = new();
    private readonly DataGridView grid = new();
    private readonly TextBox txtSearch = new();
    private readonly TextBox txtUsername = new();
    private readonly TextBox txtPassword = new();
    private readonly TextBox txtConfirmPassword = new();
    private readonly TextBox txtFullName = new();
    private readonly TextBox txtEmail = new();
    private readonly ComboBox cmbRole = new();
    private readonly CheckBox chkActive = new();
    private readonly Button btnAdd = new();
    private readonly Button btnUpdate = new();
    private readonly Button btnActivate = new();
    private readonly Button btnInactivate = new();
    private readonly Button btnClear = new();

    private int? selectedAccountId;

    public AccountManagementForm()
    {
        Text = "User Management";
        StartPosition = FormStartPosition.CenterParent;
        Size = new Size(1210, 760);
        MinimumSize = new Size(1090, 700);
        BackColor = Color.FromArgb(245, 247, 250);

        BuildLayout();
        Load += AccountManagementForm_Load;
    }

    private void BuildLayout()
    {
        Label title = new()
        {
            Text = "User Management",
            Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold),
            Location = new Point(28, 22),
            Size = new Size(320, 40)
        };

        Label searchLabel = new()
        {
            Text = "Account List / Search",
            Location = new Point(32, 82),
            Size = new Size(160, 22)
        };

        txtSearch.Location = new Point(32, 108);
        txtSearch.Size = new Size(420, 25);
        txtSearch.PlaceholderText = "Search username, full name, email, or role";
        txtSearch.TextChanged += async (_, _) => await LoadAccountsAsync();

        grid.Location = new Point(32, 146);
        grid.Size = new Size(740, 520);
        grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
        grid.AutoGenerateColumns = false;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.ReadOnly = true;
        grid.MultiSelect = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.DataSource = accounts;
        grid.SelectionChanged += grid_SelectionChanged;
        grid.CellClick += grid_CellClick;

        grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Account.Id), HeaderText = "ID", Width = 55 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Account.Username), HeaderText = "Username", Width = 110 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Account.FullName), HeaderText = "Full Name", Width = 170 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Account.Email), HeaderText = "Email", Width = 210 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Account.Role), HeaderText = "Role", Width = 85 });
        grid.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = nameof(Account.IsActive), HeaderText = "Active", Width = 90 });

        Panel editor = new()
        {
            Location = new Point(802, 82),
            Size = new Size(330, 590),
            Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom,
            BackColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle
        };

        AddField(editor, "Username", txtUsername, 18, false);
        AddField(editor, "Password", txtPassword, 80, true);
        AddField(editor, "Confirm Password", txtConfirmPassword, 142, true);
        AddField(editor, "Full Name", txtFullName, 204, false);
        AddField(editor, "Email", txtEmail, 266, false);

        Label roleLabel = new() { Text = "Role", Location = new Point(22, 328), Size = new Size(120, 22) };
        cmbRole.Location = new Point(22, 352);
        cmbRole.Size = new Size(280, 25);
        cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbRole.Items.AddRange(new object[] { "Admin", "User" });
        cmbRole.SelectedIndex = 1;

        chkActive.Text = "Active account";
        chkActive.Checked = true;
        chkActive.Location = new Point(22, 390);
        chkActive.Size = new Size(180, 24);

        btnAdd.Text = "Add Account";
        btnAdd.Location = new Point(22, 428);
        btnAdd.Size = new Size(132, 34);
        btnAdd.Click += btnAdd_Click;

        btnUpdate.Text = "Update Profile";
        btnUpdate.Location = new Point(170, 428);
        btnUpdate.Size = new Size(132, 34);
        btnUpdate.Click += btnUpdate_Click;

        btnActivate.Text = "Activate";
        btnActivate.Location = new Point(22, 474);
        btnActivate.Size = new Size(132, 34);
        btnActivate.Click += async (_, _) => await SetSelectedAccountStatusAsync(true);

        btnInactivate.Text = "Inactivate";
        btnInactivate.Location = new Point(170, 474);
        btnInactivate.Size = new Size(132, 34);
        btnInactivate.Click += async (_, _) => await SetSelectedAccountStatusAsync(false);

        btnClear.Text = "Clear";
        btnClear.Location = new Point(22, 520);
        btnClear.Size = new Size(280, 34);
        btnClear.Click += (_, _) => ClearForm();

        editor.Controls.AddRange(new Control[] { roleLabel, cmbRole, chkActive, btnAdd, btnUpdate, btnActivate, btnInactivate, btnClear });
        Controls.AddRange(new Control[] { title, searchLabel, txtSearch, grid, editor });
    }

    private static void AddField(Control parent, string labelText, TextBox textBox, int top, bool password)
    {
        Label label = new() { Text = labelText, Location = new Point(22, top), Size = new Size(120, 22) };
        textBox.Location = new Point(22, top + 24);
        textBox.Size = new Size(280, 25);
        textBox.UseSystemPasswordChar = password;
        parent.Controls.Add(label);
        parent.Controls.Add(textBox);
    }

    private async void AccountManagementForm_Load(object? sender, EventArgs e)
    {
        await LoadAccountsAsync();
    }

    private async Task LoadAccountsAsync()
    {
        try
        {
            List<Account> results = await accountRepository.SearchAsync(txtSearch.Text);
            accounts.Clear();
            foreach (Account account in results)
            {
                accounts.Add(account);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Unable to load accounts.\n\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void grid_SelectionChanged(object? sender, EventArgs e)
    {
        LoadSelectedAccountIntoEditor();
    }

    private void grid_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        grid.Rows[e.RowIndex].Selected = true;
        grid.CurrentCell = grid.Rows[e.RowIndex].Cells[Math.Max(e.ColumnIndex, 0)];
        LoadSelectedAccountIntoEditor();
    }

    private void LoadSelectedAccountIntoEditor()
    {
        if (grid.CurrentRow?.DataBoundItem is not Account account)
        {
            return;
        }

        selectedAccountId = account.Id;
        txtUsername.Text = account.Username;
        txtUsername.Enabled = true;
        txtPassword.Text = string.Empty;
        txtConfirmPassword.Text = string.Empty;
        txtPassword.PlaceholderText = "Leave blank to keep current password";
        txtConfirmPassword.PlaceholderText = "Re-enter new password";
        txtFullName.Text = account.FullName;
        txtEmail.Text = account.Email;
        cmbRole.SelectedItem = account.Role;
        chkActive.Checked = account.IsActive;
    }

    private async void btnAdd_Click(object? sender, EventArgs e)
    {
        if (!ValidateForm(requirePassword: true))
        {
            return;
        }

        try
        {
            await accountRepository.AddAsync(
                txtUsername.Text,
                txtPassword.Text,
                txtFullName.Text,
                txtEmail.Text,
                cmbRole.Text,
                chkActive.Checked);
            ClearForm();
            await LoadAccountsAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Unable to add account.\n\n{ex.Message}", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnUpdate_Click(object? sender, EventArgs e)
    {
        if (selectedAccountId is null)
        {
            MessageBox.Show("Select an account to update.", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!ValidateForm(requirePassword: false))
        {
            return;
        }

        try
        {
            await accountRepository.UpdateAsync(
                selectedAccountId.Value,
                txtUsername.Text,
                txtFullName.Text,
                txtEmail.Text,
                cmbRole.Text,
                chkActive.Checked,
                string.IsNullOrWhiteSpace(txtPassword.Text) ? null : txtPassword.Text);
            await LoadAccountsAsync();
            PromptLogoutIfCurrentAccountWasInactivated(selectedAccountId.Value, chkActive.Checked);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Unable to update account.\n\n{ex.Message}", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task SetSelectedAccountStatusAsync(bool isActive)
    {
        if (selectedAccountId is null)
        {
            MessageBox.Show("Select an account first.", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            await accountRepository.SetActiveAsync(selectedAccountId.Value, isActive);
            await LoadAccountsAsync();
            PromptLogoutIfCurrentAccountWasInactivated(selectedAccountId.Value, isActive);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Unable to update account status.\n\n{ex.Message}", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void PromptLogoutIfCurrentAccountWasInactivated(int accountId, bool isActive)
    {
        if (isActive || AppSession.CurrentAccount?.Id != accountId)
        {
            return;
        }

        DialogResult result = MessageBox.Show(
            "Your account has been set to inactive. Confirm logout to return to the login screen.",
            "Confirm Logout",
            MessageBoxButtons.OKCancel,
            MessageBoxIcon.Warning);

        if (result != DialogResult.OK)
        {
            return;
        }

        AppSession.CurrentAccount = null;
        Close();
        Owner?.Close();
    }

    private bool ValidateForm(bool requirePassword)
    {
        if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
            string.IsNullOrWhiteSpace(txtFullName.Text) ||
            string.IsNullOrWhiteSpace(txtEmail.Text) ||
            string.IsNullOrWhiteSpace(cmbRole.Text))
        {
            MessageBox.Show("Complete username, full name, email, and role.", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        if (requirePassword && string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            MessageBox.Show("Enter a password for the new account.", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        if (!string.IsNullOrWhiteSpace(txtPassword.Text) || !string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Password and confirm password do not match.", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        return true;
    }

    private void ClearForm()
    {
        selectedAccountId = null;
        txtUsername.Enabled = true;
        txtUsername.Text = string.Empty;
        txtPassword.Text = string.Empty;
        txtConfirmPassword.Text = string.Empty;
        txtPassword.PlaceholderText = string.Empty;
        txtConfirmPassword.PlaceholderText = string.Empty;
        txtFullName.Text = string.Empty;
        txtEmail.Text = string.Empty;
        cmbRole.SelectedIndex = 1;
        chkActive.Checked = true;
        grid.ClearSelection();
    }
}
