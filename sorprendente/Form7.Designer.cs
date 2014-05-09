namespace sorprendente
{
    partial class Form7
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cueTextBox3 = new sorprendente.CueTextBox();
            this.cueComboBox2 = new sorprendente.CueComboBox();
            this.cueComboBox1 = new sorprendente.CueComboBox();
            this.cueTextBox2 = new sorprendente.CueTextBox();
            this.cueTextBox1 = new sorprendente.CueTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cueTextBox3);
            this.groupBox1.Controls.Add(this.cueComboBox2);
            this.groupBox1.Controls.Add(this.cueComboBox1);
            this.groupBox1.Controls.Add(this.cueTextBox2);
            this.groupBox1.Controls.Add(this.cueTextBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(130, 159);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cueTextBox3
            // 
            this.cueTextBox3.CueText = "pic...";
            this.cueTextBox3.Location = new System.Drawing.Point(6, 125);
            this.cueTextBox3.Name = "cueTextBox3";
            this.cueTextBox3.PasswordChar = '*';
            this.cueTextBox3.ShortcutsEnabled = false;
            this.cueTextBox3.Size = new System.Drawing.Size(118, 20);
            this.cueTextBox3.TabIndex = 4;
            this.cueTextBox3.TabStop = false;
            this.cueTextBox3.MouseHover += new System.EventHandler(this.cueTextBox3_MouseHover);
            // 
            // cueComboBox2
            // 
            this.cueComboBox2.CueText = "character...";
            this.cueComboBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cueComboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cueComboBox2.FormattingEnabled = true;
            this.cueComboBox2.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.cueComboBox2.Location = new System.Drawing.Point(6, 98);
            this.cueComboBox2.Name = "cueComboBox2";
            this.cueComboBox2.Size = new System.Drawing.Size(118, 21);
            this.cueComboBox2.TabIndex = 3;
            this.cueComboBox2.TabStop = false;
            this.cueComboBox2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cueComboBox2_DrawItem);
            this.cueComboBox2.SelectedIndexChanged += new System.EventHandler(this.cueComboBox2_SelectedIndexChanged);
            this.cueComboBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cueComboBox2_KeyPress);
            // 
            // cueComboBox1
            // 
            this.cueComboBox1.CueText = "world...";
            this.cueComboBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cueComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cueComboBox1.FormattingEnabled = true;
            this.cueComboBox1.Items.AddRange(new object[] {
            "Scania",
            "Bera",
            "Broa",
            "Windia",
            "Khaini",
            "Bellocan",
            "Mardia",
            "Kradia",
            "Yellonde",
            "Demethos",
            "Galicia",
            "El Nido",
            "Zenith",
            "Arcania",
            "Chaos",
            "Nova",
            "Renegades"});
            this.cueComboBox1.Location = new System.Drawing.Point(6, 71);
            this.cueComboBox1.Name = "cueComboBox1";
            this.cueComboBox1.Size = new System.Drawing.Size(118, 21);
            this.cueComboBox1.TabIndex = 2;
            this.cueComboBox1.TabStop = false;
            this.cueComboBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cueComboBox1_DrawItem);
            this.cueComboBox1.SelectedIndexChanged += new System.EventHandler(this.cueComboBox1_SelectedIndexChanged);
            this.cueComboBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cueComboBox1_KeyPress);
            // 
            // cueTextBox2
            // 
            this.cueTextBox2.CueText = "password...";
            this.cueTextBox2.Location = new System.Drawing.Point(6, 45);
            this.cueTextBox2.Name = "cueTextBox2";
            this.cueTextBox2.PasswordChar = '*';
            this.cueTextBox2.ShortcutsEnabled = false;
            this.cueTextBox2.Size = new System.Drawing.Size(118, 20);
            this.cueTextBox2.TabIndex = 1;
            this.cueTextBox2.TabStop = false;
            this.cueTextBox2.MouseHover += new System.EventHandler(this.cueTextBox2_MouseHover);
            // 
            // cueTextBox1
            // 
            this.cueTextBox1.CueText = "username...";
            this.cueTextBox1.Location = new System.Drawing.Point(6, 19);
            this.cueTextBox1.Name = "cueTextBox1";
            this.cueTextBox1.PasswordChar = '*';
            this.cueTextBox1.ShortcutsEnabled = false;
            this.cueTextBox1.Size = new System.Drawing.Size(118, 20);
            this.cueTextBox1.TabIndex = 0;
            this.cueTextBox1.TabStop = false;
            this.cueTextBox1.MouseHover += new System.EventHandler(this.cueTextBox1_MouseHover);
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Location = new System.Drawing.Point(18, 192);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 24);
            this.button1.TabIndex = 1;
            this.button1.TabStop = false;
            this.button1.Text = "clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox1.Location = new System.Drawing.Point(18, 228);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(118, 24);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.TabStop = false;
            this.checkBox1.Text = "set";
            this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // Form7
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(154, 267);
            this.ControlBox = false;
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(170, 301);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(170, 223);
            this.Name = "Form7";
            this.Opacity = 0.8D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "login";
            this.TopMost = true;
            this.Click += new System.EventHandler(this.Form7_Click);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private CueTextBox cueTextBox1;
        private CueTextBox cueTextBox2;
        private CueComboBox cueComboBox1;
        private CueTextBox cueTextBox3;
        private CueComboBox cueComboBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}