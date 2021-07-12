using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using xml_read;

namespace XML_Checker_Proj
{
    public partial class Form1 : Form
    {
        XML xml_file ;
        public Form1()
        {
            InitializeComponent();
        }


       
       

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /* Button1_XML_Import_New_File */
        private void Import_XML_File_Click(object sender, EventArgs e)
        {
            string path = textBox2_XML_Path.Text;

            var XmlDoc = File.ReadAllText(path);

            /* Check if the file is not empty, else assert an error */
            if (XmlDoc != "")
            {
                /* Load the file Content into the textBox1 */
                textBox1_XML_Contents.Text = XmlDoc;

                /* Disable Import Button and Enable Other buttons */
                Button1_Import_XML_file.Enabled = false;
                Button2_Remove_XML_file.Enabled = true;
                Button3_Check_Errors.Enabled    = true;
                Button4_Correct_Errors.Enabled  = true;
                Button5_Compress.Enabled        = true;
                parse_xml_button.Enabled = true;
                format_xml_button.Enabled = true;


                /* Disable tetBox2_XML_Path */
                textBox2_XML_Path.Enabled = false;
                xml_file = new XML(path);
            }


            else
            {
                MessageBox.Show("Incorrect Path or the file you entered is empty!");
            }
        }


        /* Button2_XML_Remove_File *///
        private void button1_Click_1(object sender, EventArgs e)
        {
            /* Empty textBox1_XML_Content */
            textBox1_XML_Contents.Text = "";

            /* Enable Button1_Import_XML_file and Disable Other buttons */
            Button1_Import_XML_file.Enabled = true;
            Button2_Remove_XML_file.Enabled = false;
            Button3_Check_Errors.Enabled = false;
            Button4_Correct_Errors.Enabled = false;
            Button5_Compress.Enabled = false;

            /* Enable tetBox2_XML_Path */
            textBox2_XML_Path.Enabled = true;
            textBox2_XML_Path.Text = "";

        }


        /* Button3_Check_Errors  */
        private void button2_Click(object sender, EventArgs e)
        {
            xml_file.Parse_XML(); 
            Console.WriteLine(xml_file.root_tags.Count);
            //readxml.parsing_file();
            xml_file.print();
        }


        /* Button4_Correct_Errors *///
        private void button3_Click_1(object sender, EventArgs e)
        {
            
        }

        /* Button5_Compress */
        private void button4_Click_1(object sender, EventArgs e)
        {
            string compressed = xml_file.Trim();
            this.output_txt_box.Text = compressed;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
        /* Button4_Correct_Errors */
        //
        private void format_xml_click(object sender, EventArgs e)
        {
            string formatted = xml_file.FormatXML();
            this.output_txt_box.Text = formatted;

        }
   
     

       

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2_XML_Path.Text = openFileDialog1.FileName;
            }  
        }
    }
}
