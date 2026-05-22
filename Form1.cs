namespace electronics
{
    public partial class Form1 : Form
    {
        private readonly AccountRepository accountRepository = new();

        public Form1()
        {
            InitializeComponent();
            ConfigureLogo();
            textBox2.UseSystemPasswordChar = true;
            button1.Click += button1_Click;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            Load += Form1_Load;
        }

        private void ConfigureLogo()
        {
            string logoPath = Path.Combine(AppContext.BaseDirectory, "logo.png");
            if (!File.Exists(logoPath))
            {
                logoPath = Path.Combine(Application.StartupPath, "logo.png");
            }

            if (!File.Exists(logoPath))
            {
                return;
            }

            richTextBox1.Visible = false;
            label3.Visible = false;
            label4.Visible = false;

            PictureBox logo = new()
            {
                Location = richTextBox1.Location,
                Size = richTextBox1.Size,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White
            };

            using FileStream stream = File.OpenRead(logoPath);
            using MemoryStream memory = new();
            stream.CopyTo(memory);
            memory.Position = 0;
            using Image sourceLogo = Image.FromStream(memory);
            logo.Image = new Bitmap(sourceLogo);

            panel1.Controls.Add(logo);
            logo.BringToFront();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void Form1_Load(object? sender, EventArgs e)
        {
            try
            {
                await accountRepository.InitializeAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to connect to MySQL or initialize the accounts table.\n\n{ex.Message}",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void button1_Click(object? sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Enter your username and password.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            button1.Enabled = false;
            try
            {
                Account? account = await accountRepository.AuthenticateAsync(username, password);
                if (account is null)
                {
                    MessageBox.Show("Invalid username/password, or the account is inactive.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                AppSession.CurrentAccount = account;
                textBox2.Text = string.Empty;
                Hide();

                Form4 dashboard = new();
                dashboard.FormClosed += (_, _) =>
                {
                    if (AppSession.CurrentAccount is null)
                    {
                        Show();
                        textBox1.Focus();
                    }
                    else
                    {
                        Close();
                    }
                };
                dashboard.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login failed.\n\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button1.Enabled = true;
            }
        }

        private void linkLabel1_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();

            Form2 recovery = new();
            recovery.FormClosed += (_, _) =>
            {
                Show();
                textBox1.Focus();
            };
            recovery.Show();
        }
    }
}
