namespace XML_Checker_Proj
{
    partial class Form1
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
            this.textBox1_XML_Contents = new System.Windows.Forms.TextBox();
            this.Button2_Remove_XML_file = new System.Windows.Forms.Button();
            this.Button3_Check_Errors = new System.Windows.Forms.Button();
            this.Button4_Correct_Errors = new System.Windows.Forms.Button();
            this.Button5_Compress = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.colorDialog2 = new System.Windows.Forms.ColorDialog();
            this.textBox2_XML_Path = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Button1_Import_XML_file = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.output_txt_box = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.parse_xml_button = new System.Windows.Forms.Button();
            this.format_xml_button = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.outputFileSave = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1_XML_Contents
            // 
            this.textBox1_XML_Contents.BackColor = System.Drawing.Color.Lavender;
            this.textBox1_XML_Contents.Enabled = false;
            this.textBox1_XML_Contents.Location = new System.Drawing.Point(27, 27);
            this.textBox1_XML_Contents.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1_XML_Contents.Multiline = true;
            this.textBox1_XML_Contents.Name = "textBox1_XML_Contents";
            this.textBox1_XML_Contents.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1_XML_Contents.Size = new System.Drawing.Size(458, 410);
            this.textBox1_XML_Contents.TabIndex = 4;
            this.textBox1_XML_Contents.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // Button2_Remove_XML_file
            // 
            this.Button2_Remove_XML_file.Enabled = false;
            this.Button2_Remove_XML_file.Location = new System.Drawing.Point(205, 451);
            this.Button2_Remove_XML_file.Margin = new System.Windows.Forms.Padding(2);
            this.Button2_Remove_XML_file.Name = "Button2_Remove_XML_file";
            this.Button2_Remove_XML_file.Size = new System.Drawing.Size(167, 32);
            this.Button2_Remove_XML_file.TabIndex = 5;
            this.Button2_Remove_XML_file.Text = "Remove XML File";
            this.Button2_Remove_XML_file.UseVisualStyleBackColor = true;
            this.Button2_Remove_XML_file.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Button3_Check_Errors
            // 
            this.Button3_Check_Errors.Enabled = false;
            this.Button3_Check_Errors.Location = new System.Drawing.Point(380, 626);
            this.Button3_Check_Errors.Margin = new System.Windows.Forms.Padding(2);
            this.Button3_Check_Errors.Name = "Button3_Check_Errors";
            this.Button3_Check_Errors.Size = new System.Drawing.Size(167, 32);
            this.Button3_Check_Errors.TabIndex = 6;
            this.Button3_Check_Errors.Text = "Check Errors";
            this.Button3_Check_Errors.UseVisualStyleBackColor = true;
            this.Button3_Check_Errors.Click += new System.EventHandler(this.button2_Click);
            // 
            // Button4_Correct_Errors
            // 
            this.Button4_Correct_Errors.Enabled = false;
            this.Button4_Correct_Errors.Location = new System.Drawing.Point(380, 662);
            this.Button4_Correct_Errors.Margin = new System.Windows.Forms.Padding(2);
            this.Button4_Correct_Errors.Name = "Button4_Correct_Errors";
            this.Button4_Correct_Errors.Size = new System.Drawing.Size(167, 32);
            this.Button4_Correct_Errors.TabIndex = 7;
            this.Button4_Correct_Errors.Text = "Correct Errors";
            this.Button4_Correct_Errors.UseVisualStyleBackColor = true;
            this.Button4_Correct_Errors.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // Button5_Compress
            // 
            this.Button5_Compress.Enabled = false;
            this.Button5_Compress.Location = new System.Drawing.Point(1125, 626);
            this.Button5_Compress.Margin = new System.Windows.Forms.Padding(2);
            this.Button5_Compress.Name = "Button5_Compress";
            this.Button5_Compress.Size = new System.Drawing.Size(167, 32);
            this.Button5_Compress.TabIndex = 8;
            this.Button5_Compress.Text = "Compress";
            this.Button5_Compress.UseVisualStyleBackColor = true;
            this.Button5_Compress.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // textBox2_XML_Path
            // 
            this.textBox2_XML_Path.BackColor = System.Drawing.Color.Lavender;
            this.textBox2_XML_Path.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textBox2_XML_Path.Location = new System.Drawing.Point(969, 27);
            this.textBox2_XML_Path.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2_XML_Path.Multiline = true;
            this.textBox2_XML_Path.Name = "textBox2_XML_Path";
            this.textBox2_XML_Path.Size = new System.Drawing.Size(176, 63);
            this.textBox2_XML_Path.TabIndex = 9;
            this.textBox2_XML_Path.Text = "XML file path";
            this.textBox2_XML_Path.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(30, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Input";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Button1_Import_XML_file
            // 
            this.Button1_Import_XML_file.Location = new System.Drawing.Point(11, 646);
            this.Button1_Import_XML_file.Margin = new System.Windows.Forms.Padding(2);
            this.Button1_Import_XML_file.Name = "Button1_Import_XML_file";
            this.Button1_Import_XML_file.Size = new System.Drawing.Size(167, 32);
            this.Button1_Import_XML_file.TabIndex = 12;
            this.Button1_Import_XML_file.Text = "Import New XML File";
            this.Button1_Import_XML_file.UseVisualStyleBackColor = true;
            this.Button1_Import_XML_file.Click += new System.EventHandler(this.Import_XML_File_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Silver;
            this.button1.Location = new System.Drawing.Point(969, 95);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "importFileDialogue";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.DarkGray;
            this.label2.Location = new System.Drawing.Point(966, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Path";
            // 
            // output_txt_box
            // 
            this.output_txt_box.BackColor = System.Drawing.Color.Lavender;
            this.output_txt_box.Enabled = false;
            this.output_txt_box.Location = new System.Drawing.Point(489, 27);
            this.output_txt_box.Margin = new System.Windows.Forms.Padding(2);
            this.output_txt_box.Multiline = true;
            this.output_txt_box.Name = "output_txt_box";
            this.output_txt_box.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.output_txt_box.Size = new System.Drawing.Size(449, 410);
            this.output_txt_box.TabIndex = 4;
            this.output_txt_box.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.DarkGray;
            this.label3.Location = new System.Drawing.Point(486, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Output";
            this.label3.Click += new System.EventHandler(this.label1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Lavender;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(27, 505);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(752, 90);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.DarkGray;
            this.label4.Location = new System.Drawing.Point(42, 466);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Errors";
            this.label4.Click += new System.EventHandler(this.label1_Click);
            // 
            // parse_xml_button
            // 
            this.parse_xml_button.Enabled = false;
            this.parse_xml_button.Location = new System.Drawing.Point(196, 646);
            this.parse_xml_button.Margin = new System.Windows.Forms.Padding(2);
            this.parse_xml_button.Name = "parse_xml_button";
            this.parse_xml_button.Size = new System.Drawing.Size(167, 32);
            this.parse_xml_button.TabIndex = 6;
            this.parse_xml_button.Text = "Parse XML";
            this.parse_xml_button.UseVisualStyleBackColor = true;
            this.parse_xml_button.Click += new System.EventHandler(this.button2_Click);
            // 
            // format_xml_button
            // 
            this.format_xml_button.Enabled = false;
            this.format_xml_button.Location = new System.Drawing.Point(576, 646);
            this.format_xml_button.Margin = new System.Windows.Forms.Padding(2);
            this.format_xml_button.Name = "format_xml_button";
            this.format_xml_button.Size = new System.Drawing.Size(167, 32);
            this.format_xml_button.TabIndex = 6;
            this.format_xml_button.Text = "Format XML";
            this.format_xml_button.UseVisualStyleBackColor = true;
            this.format_xml_button.Click += new System.EventHandler(this.format_xml_click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(937, 646);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(167, 32);
            this.button3.TabIndex = 14;
            this.button3.Text = "Minify";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(757, 646);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(167, 32);
            this.button4.TabIndex = 15;
            this.button4.Text = "Convert to Jason";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(1125, 662);
            this.button5.Margin = new System.Windows.Forms.Padding(2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(167, 32);
            this.button5.TabIndex = 16;
            this.button5.Text = "De-Compress";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // outputFileSave
            // 
            this.outputFileSave.BackColor = System.Drawing.Color.Lavender;
            this.outputFileSave.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.outputFileSave.Location = new System.Drawing.Point(969, 333);
            this.outputFileSave.Margin = new System.Windows.Forms.Padding(2);
            this.outputFileSave.Multiline = true;
            this.outputFileSave.Name = "outputFileSave";
            this.outputFileSave.Size = new System.Drawing.Size(176, 63);
            this.outputFileSave.TabIndex = 17;
            this.outputFileSave.Text = "output file path";
            this.outputFileSave.TextChanged += new System.EventHandler(this.textBox2_TextChanged_1);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Silver;
            this.button2.Location = new System.Drawing.Point(969, 414);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "Save File";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // Form1
            // 
            this.AccessibleName = "";
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1783, 805);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.outputFileSave);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Button1_Import_XML_file);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2_XML_Path);
            this.Controls.Add(this.Button5_Compress);
            this.Controls.Add(this.Button4_Correct_Errors);
            this.Controls.Add(this.format_xml_button);
            this.Controls.Add(this.parse_xml_button);
            this.Controls.Add(this.Button3_Check_Errors);
            this.Controls.Add(this.Button2_Remove_XML_file);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.output_txt_box);
            this.Controls.Add(this.textBox1_XML_Contents);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Location = new System.Drawing.Point(100, 100);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "XML_Proj";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1_XML_Contents;
        private System.Windows.Forms.Button Button2_Remove_XML_file;
        private System.Windows.Forms.Button Button3_Check_Errors;
        private System.Windows.Forms.Button Button4_Correct_Errors;
        private System.Windows.Forms.Button Button5_Compress;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ColorDialog colorDialog2;
        private System.Windows.Forms.TextBox textBox2_XML_Path;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Button1_Import_XML_file;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox output_txt_box;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button parse_xml_button;
        private System.Windows.Forms.Button format_xml_button;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox outputFileSave;
        private System.Windows.Forms.Button button2;
    }
}

