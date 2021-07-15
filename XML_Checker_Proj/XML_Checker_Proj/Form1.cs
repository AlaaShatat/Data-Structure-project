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
using System.Text.RegularExpressions;
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
            textBox1.Text = "";
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
                button3.Enabled = true;
                button4.Enabled = true;
                button2.Enabled = true;
                button5.Enabled = true;

                /* Disable tetBox2_XML_Path */
                textBox2_XML_Path.Enabled = true;
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
            output_txt_box.Text = "";


        }


        /* Button3_Check_Errors  */
        private void button2_Click(object sender, EventArgs e)
        {
            this.output_txt_box.Text = "";
            xml_file.correction();
            string errorText = xml_file.errorLine;
            this.output_txt_box.Text = xml_file.errorString;
            //bool errorCounter = Regex.IsMatch(errorText, @"^[a-zA-Z]+$");
            //if (errorCounter == 0 ) 
            //{
            //    textBox1.Text = "No error has been detected";
            //}
            //else 
            //{
            //    textBox1.Text = errorText;
            //}
            textBox1.Text = errorText;

            Console.WriteLine(xml_file.root_tags.Count);
            //readxml.parsing_file();
            xml_file.print();
            
            
        }


        /* Button4_Correct_Errors *///
        private void button3_Click_1(object sender, EventArgs e)
        {
            this.output_txt_box.Text = "";
            //xml_file.Parse_XML();
            //string corrected = xml_file.correctResult;
            string corrected = xml_file.correction();
            string errorText = xml_file.errorLine;
            textBox1.Text = errorText;


            this.output_txt_box.Text = corrected;
        }

        /* Button5_Compress */
        private void button4_Click_1(object sender, EventArgs e)
        {
            this.output_txt_box.Text = "";
            ////////////// For Compress & Decompress
            List<List<int>> compress_indexes = new List<List<int>>();
            List<string> dictionary = new List<string>();
            //StreamReader xmlString = new StreamReader(textBox2_XML_Path.Text);
            //string line ;
            //string fileRead = "";
            //while ((line = xmlString.ReadLine()) != null) 
            //{
            //    fileRead += line;
            //}
            string compressed = xml_file.Compress();
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
            this.output_txt_box.Text = "";
            xml_file.Parse_XML();
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
        // jason
        private void button4_Click(object sender, EventArgs e)
        {
            this.output_txt_box.Text = "";
            xml_file.Parse_XML();
            string converted = xml_file.ConvertToJson();
            this.output_txt_box.Text = converted;


        }
        // minify file 
        private void button3_Click(object sender, EventArgs e)
        {
            this.output_txt_box.Text = "";
            string trimmed = xml_file.Trim();
            this.output_txt_box.Text = trimmed;
        }
        // decompress
        private void button5_Click(object sender, EventArgs e)
        {
            this.output_txt_box.Text = "";
            string decompressed = xml_file.Decompress();
            this.output_txt_box.Text = decompressed;
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    outputFileSave.Text = openFileDialog1.FileName;
            //}
            //string path1 = textBox2_XML_Path.Text;
            //string path2 = Path.Combine(path1, "temp1");

            //// Create directory temp1 if it doesn't exist
            //Directory.CreateDirectory(path2+"1");
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
            SaveFileDialog1.ShowDialog(); 
            SaveFileDialog1.InitialDirectory = @"C:\";
            SaveFileDialog1.Title = "Save text Files";
            SaveFileDialog1.DefaultExt = "txt";
            SaveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*|XML files (*.xml)|*.xml|JSON files (*.json)|*.json";  
           // saveFileDialog1.CheckFileExists = true;      
            //saveFileDialog1.CheckPathExists = true;   
            if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
            {      
            outputFileSave.Text = SaveFileDialog1.FileName; 
                string filePath = outputFileSave.Text;
                xml_file.storeOutput(filePath, output_txt_box.Text);
            }
            
        }
    }
}
