namespace electronics
{
    partial class Form3
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
            richTextBox1 = new RichTextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            button1 = new Button();
            button2 = new Button();
            label7 = new Label();
            label8 = new Label();
            panel1 = new Panel();
            label9 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(134, 107);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(199, 169);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(211, 182);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 1;
            label1.Text = "(LOGO)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13F);
            label2.Location = new Point(348, 167);
            label2.Name = "label2";
            label2.Size = new Size(134, 25);
            label2.TabIndex = 2;
            label2.Text = "Electronic Shop";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(349, 197);
            label3.Name = "label3";
            label3.Size = new Size(111, 15);
            label3.TabIndex = 3;
            label3.Text = "Information System";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(134, 289);
            label4.Name = "label4";
            label4.Size = new Size(72, 15);
            label4.TabIndex = 4;
            label4.Text = "Version 1.0.0";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(134, 304);
            label5.Name = "label5";
            label5.Size = new Size(251, 15);
            label5.TabIndex = 5;
            label5.Text = "Managing your Electronics Business Efficiently";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(134, 353);
            label6.Name = "label6";
            label6.Size = new Size(475, 15);
            label6.TabIndex = 6;
            label6.Text = "Electronic Shop Information System helps you manage inventory, sales, and reports with ";
            label6.Click += label6_Click;
            // 
            // button1
            // 
            button1.Location = new Point(193, 424);
            button1.Name = "button1";
            button1.Size = new Size(122, 23);
            button1.TabIndex = 7;
            button1.Text = "Learn More";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(376, 424);
            button2.Name = "button2";
            button2.Size = new Size(144, 23);
            button2.TabIndex = 8;
            button2.Text = "Contact Support";
            button2.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(134, 377);
            label7.Name = "label7";
            label7.Size = new Size(392, 15);
            label7.TabIndex = 9;
            label7.Text = "ease. Enhance your business productivity with our user-friendly platform.";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 25F);
            label8.Location = new Point(211, 48);
            label8.Name = "label8";
            label8.Size = new Size(309, 46);
            label8.TabIndex = 10;
            label8.Text = "About the Program";
            // 
            // panel1
            // 
            panel1.Controls.Add(label9);
            panel1.Controls.Add(richTextBox1);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Location = new Point(41, 50);
            panel1.Name = "panel1";
            panel1.Size = new Size(676, 513);
            panel1.TabIndex = 11;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(206, 183);
            label9.Name = "label9";
            label9.Size = new Size(47, 15);
            label9.TabIndex = 11;
            label9.Text = "(LOGO)";
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(769, 611);
            Controls.Add(panel1);
            Name = "Form3";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "About Us";
            Load += Form3_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox richTextBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button button1;
        private Button button2;
        private Label label7;
        private Label label8;
        private Panel panel1;
        private Label label9;
    }
}