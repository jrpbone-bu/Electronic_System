namespace electronics
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F);
            label1.Location = new Point(140, 169);
            label1.Name = "label1";
            label1.Size = new Size(288, 37);
            label1.TabIndex = 0;
            label1.Text = "PASSWORD RECOVERY";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F);
            label2.Location = new Point(188, 206);
            label2.Name = "label2";
            label2.Size = new Size(191, 28);
            label2.TabIndex = 1;
            label2.Text = "Reset your password";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(164, 234);
            label3.Name = "label3";
            label3.Size = new Size(240, 15);
            label3.TabIndex = 2;
            label3.Text = "Enter your email to recieve reset instructions";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(16, 32);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(288, 23);
            textBox1.TabIndex = 3;
            textBox1.Text = "Email Address";
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 10F);
            button1.Location = new Point(64, 75);
            button1.Name = "button1";
            button1.Size = new Size(191, 31);
            button1.TabIndex = 4;
            button1.Text = "Send Reset Link";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(112, 112);
            button2.Name = "button2";
            button2.Size = new Size(91, 23);
            button2.TabIndex = 5;
            button2.Text = "Back to Login";
            button2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Location = new Point(124, 261);
            panel1.Name = "panel1";
            panel1.Size = new Size(320, 164);
            panel1.TabIndex = 6;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(562, 611);
            Controls.Add(panel1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form2";
            Text = "Password Recovery";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox1;
        private Button button1;
        private Button button2;
        private Panel panel1;
    }
}