namespace KKC_Test_2
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

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.playerListBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.EP5 = new System.Windows.Forms.ComboBox();
            this.EP4 = new System.Windows.Forms.ComboBox();
            this.EP3 = new System.Windows.Forms.ComboBox();
            this.EP2 = new System.Windows.Forms.ComboBox();
            this.EP1 = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.uploadBTN = new System.Windows.Forms.Button();
            this.turnBTN = new System.Windows.Forms.Button();
            this.importBTN = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(250, 68);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 31);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // playerListBox
            // 
            this.playerListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.playerListBox.FormattingEnabled = true;
            this.playerListBox.Location = new System.Drawing.Point(14, 16);
            this.playerListBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.playerListBox.Name = "playerListBox";
            this.playerListBox.Size = new System.Drawing.Size(138, 28);
            this.playerListBox.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(14, 108);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(158, 240);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Actions";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1.1",
            "1.2",
            "2.1",
            "2.2",
            "3.1",
            "3.2",
            "4.1",
            "4.2"});
            this.comboBox1.Location = new System.Drawing.Point(56, 69);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(138, 28);
            this.comboBox1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Turn";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.EP5);
            this.groupBox2.Controls.Add(this.EP4);
            this.groupBox2.Controls.Add(this.EP3);
            this.groupBox2.Controls.Add(this.EP2);
            this.groupBox2.Controls.Add(this.EP1);
            this.groupBox2.Location = new System.Drawing.Point(178, 108);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(158, 240);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "EP";
            // 
            // EP5
            // 
            this.EP5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EP5.FormattingEnabled = true;
            this.EP5.Items.AddRange(new object[] {
            "Linguistics",
            "Arithmetics",
            "Rhetoric & Logic",
            "Archives",
            "Sympathy",
            "Physicking",
            "Alchemy",
            "Artificery",
            "Naming"});
            this.EP5.Location = new System.Drawing.Point(13, 184);
            this.EP5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EP5.Name = "EP5";
            this.EP5.Size = new System.Drawing.Size(138, 28);
            this.EP5.TabIndex = 4;
            // 
            // EP4
            // 
            this.EP4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EP4.FormattingEnabled = true;
            this.EP4.Items.AddRange(new object[] {
            "Linguistics",
            "Arithmetics",
            "Rhetoric & Logic",
            "Archives",
            "Sympathy",
            "Physicking",
            "Alchemy",
            "Artificery",
            "Naming"});
            this.EP4.Location = new System.Drawing.Point(13, 145);
            this.EP4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EP4.Name = "EP4";
            this.EP4.Size = new System.Drawing.Size(138, 28);
            this.EP4.TabIndex = 3;
            // 
            // EP3
            // 
            this.EP3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EP3.FormattingEnabled = true;
            this.EP3.Items.AddRange(new object[] {
            "Linguistics",
            "Arithmetics",
            "Rhetoric & Logic",
            "Archives",
            "Sympathy",
            "Physicking",
            "Alchemy",
            "Artificery",
            "Naming"});
            this.EP3.Location = new System.Drawing.Point(13, 107);
            this.EP3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EP3.Name = "EP3";
            this.EP3.Size = new System.Drawing.Size(138, 28);
            this.EP3.TabIndex = 2;
            // 
            // EP2
            // 
            this.EP2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EP2.FormattingEnabled = true;
            this.EP2.Items.AddRange(new object[] {
            "Linguistics",
            "Arithmetics",
            "Rhetoric & Logic",
            "Archives",
            "Sympathy",
            "Physicking",
            "Alchemy",
            "Artificery",
            "Naming"});
            this.EP2.Location = new System.Drawing.Point(13, 68);
            this.EP2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EP2.Name = "EP2";
            this.EP2.Size = new System.Drawing.Size(138, 28);
            this.EP2.TabIndex = 1;
            // 
            // EP1
            // 
            this.EP1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EP1.FormattingEnabled = true;
            this.EP1.Items.AddRange(new object[] {
            "Linguistics",
            "Arithmetics",
            "Rhetoric & Logic",
            "Archives",
            "Sympathy",
            "Physicking",
            "Alchemy",
            "Artificery",
            "Naming"});
            this.EP1.Location = new System.Drawing.Point(13, 29);
            this.EP1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EP1.Name = "EP1";
            this.EP1.Size = new System.Drawing.Size(138, 28);
            this.EP1.TabIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(343, 140);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(163, 24);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "University Next Turn";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(343, 173);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(129, 24);
            this.checkBox2.TabIndex = 7;
            this.checkBox2.Text = "Imre Next Turn";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.uploadBTN);
            this.groupBox3.Controls.Add(this.turnBTN);
            this.groupBox3.Controls.Add(this.importBTN);
            this.groupBox3.Location = new System.Drawing.Point(569, 81);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(250, 174);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // uploadBTN
            // 
            this.uploadBTN.Location = new System.Drawing.Point(24, 134);
            this.uploadBTN.Name = "uploadBTN";
            this.uploadBTN.Size = new System.Drawing.Size(154, 29);
            this.uploadBTN.TabIndex = 2;
            this.uploadBTN.Text = "Upload Data";
            this.uploadBTN.UseVisualStyleBackColor = true;
            this.uploadBTN.Click += new System.EventHandler(this.uploadBTN_Click);
            // 
            // turnBTN
            // 
            this.turnBTN.Location = new System.Drawing.Point(24, 87);
            this.turnBTN.Name = "turnBTN";
            this.turnBTN.Size = new System.Drawing.Size(154, 29);
            this.turnBTN.TabIndex = 1;
            this.turnBTN.Text = "Process Turn";
            this.turnBTN.UseVisualStyleBackColor = true;
            this.turnBTN.Click += new System.EventHandler(this.turnBTN_Click);
            // 
            // importBTN
            // 
            this.importBTN.Location = new System.Drawing.Point(24, 31);
            this.importBTN.Name = "importBTN";
            this.importBTN.Size = new System.Drawing.Size(154, 29);
            this.importBTN.TabIndex = 0;
            this.importBTN.Text = "Import Data";
            this.importBTN.UseVisualStyleBackColor = true;
            this.importBTN.Click += new System.EventHandler(this.importBTN_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 600);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.playerListBox);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private ComboBox playerListBox;
        private GroupBox groupBox1;
        private ComboBox comboBox1;
        private Label label1;
        private GroupBox groupBox2;
        private ComboBox EP5;
        private ComboBox EP4;
        private ComboBox EP3;
        private ComboBox EP2;
        private ComboBox EP1;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private GroupBox groupBox3;
        private Button turnBTN;
        private Button importBTN;
        private Button uploadBTN;
    }
}