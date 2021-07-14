﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using System.Windows.Forms;

namespace xml_read
{
    class XML
    {
        // each xml file has list of xml Tag and each Tag has list of Childs Tags 
        public string path;
        public List <Tag> root_tags = new List<Tag>();
        public Stack <string> st1 = new Stack<string>();
        public Stack<Tag> validationStack = new Stack<Tag>();
        public TextBox currentError = new TextBox();
        public string errorLine;
        public string correctResult = "";

        // constructor
       public XML (string path_)
        {
            path = path_;
        }


        // Parse XML and load the data into class properties (finished)
       public void Parse_XML()
       {
           int lineNumber = 1;
           int columnNumber = 0;
           FileStream _inputStream = new FileStream(this.path, FileMode.Open, FileAccess.Read);
           using (StreamReader rdr = new StreamReader(_inputStream))
           {
               int currentRead;
               Tag currentParent = null;
               Tag currentNode = null;

               while ((currentRead = rdr.Read()) >= 0)
               {
                   char currentChar = Convert.ToChar(currentRead);
                   columnNumber++;
                   if (currentChar == '\n')
                   {
                       lineNumber++;
                       //Console.WriteLine("Line " + lineNumber + " " + columnNumber);
                       columnNumber = 0;
                   }
                   // Xml Version line
                   if ((currentChar == '<') && (rdr.Peek() == '?'))
                   {
                       rdr.ReadLine();
                   }
                   // Tag Detection  
                   if (currentRead == '<' && rdr.Peek() != '/')
                   {
                       string tag_name = "";
                       string tag_attribute = "";
                       while (rdr.Peek() != '>')
                       { // tag name with attributes 
                           // store tag name
                           tag_name += Convert.ToChar(rdr.Read());
                           columnNumber++;

                           // store tag attribute
                           if (rdr.Peek() == ' ')
                           {
                               while (rdr.Peek() != '>')
                               {
                                   tag_attribute += Convert.ToChar(rdr.Read());
                               }
                           }
                       }

                       rdr.Read();

                       currentNode = new Tag(tag_name, currentParent);
                       // attributes
                       currentNode.attributes = tag_attribute;
                       validationStack.Push(currentNode);
                       correctResult += "<" + tag_name + tag_attribute + ">" + Environment.NewLine;
                       //Console.WriteLine(correctResult);
                   }
                   // tag value 
                   else if (currentRead != '<' && currentRead != '\r' && currentRead != '\n')
                   {
                       string tag_value = "" + Convert.ToChar(currentRead);
                       while (rdr.Peek() != '<')
                       {
                           tag_value += Convert.ToChar(rdr.Read());
                       }
                       //Console.WriteLine(tag_value + " tag value  has been detected ");
                       currentNode.TagValue = tag_value;
                       correctResult += tag_value + (tag_value != "" ? Environment.NewLine : "");
                       //Console.WriteLine(correctResult);

                   }
                   // closing tag              
                   else if (currentRead == '<' && rdr.Peek() == '/')
                   {
                       rdr.Read();
                       string tag_name = "";
                       while (rdr.Peek() != '>')
                       {
                           tag_name += Convert.ToChar(rdr.Read());
                           columnNumber++;
                       }
                       rdr.Read();
                       if (tag_name != validationStack.Peek().TagName)
                       {
                           //Console.WriteLine("Error: Tag not closed at Line "+ lineNumber + "Col." + columnNumber);
                           errorLine = "Error: Tag not closed at Line " + lineNumber + "Col." + columnNumber;
                           //currentError.Text = errorLine;

                       }
                       correctResult += "</" + validationStack.Peek() + ">";
                       //Console.WriteLine(correctResult);
                       Tag poped_node = validationStack.Pop();
                       //Root Node if empty stack. 
                       if (validationStack.Count == 0)
                       {
                           this.root_tags.Add(poped_node);
                       }
                       else
                       {
                           Tag lastOpenedNode = validationStack.Peek();
                           lastOpenedNode.Childs.Add(poped_node);
                       }
                       // child to some parent 
                       //Console.WriteLine(tag_name + " tag >  has been detected ");
                   }



               }
           }
       }

       // correct result 
       public string correction()
       {
           string correctedFile = "";
           int lineNumber = 1;
           int columnNumber = 0;
           correctResult = "";
           List <string> lastused = new List<string>();
           string tagValueCopy = "";
           FileStream _inputStream = new FileStream(this.path, FileMode.Open, FileAccess.Read);
           using (StreamReader rdr = new StreamReader(_inputStream))
           {
               int currentRead;
               Tag currentParent = null;
               Tag currentNode = null;

               while ((currentRead = rdr.Read()) >= 0)
               {
                   char currentChar = Convert.ToChar(currentRead);
                   columnNumber++;
                   if (currentChar == '\n')
                   {
                       lineNumber++;
                       //Console.WriteLine("Line " + lineNumber + " " + columnNumber);
                       columnNumber = 0;
                       correctedFile += Environment.NewLine;
                   }
                   // Xml Version line
                   if ((currentChar == '<') && (rdr.Peek() == '?'))
                   {
                       string xmlLine= rdr.ReadLine();
                       correctedFile += xmlLine;

                   }
                   // Tag Detection  
                   if (currentRead == '<' && rdr.Peek() != '/')
                   {
                       int len = lastused.Count;
                       if (validationStack.Count !=0 && validationStack.Peek().TagValue != "" && lastused[len - 1] != "</")
                       {
                           correctedFile += "</" + validationStack.Peek().TagName + ">" + Environment.NewLine;
                           errorLine = "Error: Tag not closed at Line " + lineNumber + "Col." + columnNumber + Environment.NewLine;
                       } 
                       correctedFile += "<";
                       string tag_name = "";
                       string tag_attribute = "";
                       while (rdr.Peek() != '>')
                       { // tag name with attributes 
                           // store tag name
                           // check errors for tag name
                           while (rdr.Peek() == '<')
                           {
                               rdr.Read();
                           }
                           //if ()
                           tag_name += Convert.ToChar(rdr.Read());
                           columnNumber++;

                           // store tag attribute
                           if (rdr.Peek() == ' ')
                           {
                               while (rdr.Peek() != '>')
                               {
                                   tag_attribute += Convert.ToChar(rdr.Read());
                               }
                           }
                           
                       }
                       rdr.Read();
                       correctedFile += tag_name + tag_attribute;
                       correctedFile += ">";
                       lastused.Add("<");
                      // correctedFile += Environment.NewLine;
                       currentNode = new Tag(tag_name, currentParent);
                       // attributes
                       currentNode.attributes = tag_attribute;
                       validationStack.Push(currentNode);
                       correctResult += "<" + tag_name + tag_attribute + ">" + Environment.NewLine;
                       //Console.WriteLine(correctResult);
                   }
                   // tag value 
                   else if (currentRead != '<' && currentRead != '\r' && currentRead != '\n')
                   {
                       string tag_value = "" + Convert.ToChar(currentRead);
                       while (rdr.Peek() != '<')
                       {
                           tag_value += Convert.ToChar(rdr.Read());
                           //correctedFile += tag_value ;
                       }
                       //Console.WriteLine(tag_value + " tag value  has been detected ");
                       currentNode.TagValue = tag_value;
                       correctedFile += tag_value;
                       correctResult += tag_value + (tag_value != "" ? Environment.NewLine : "");
                       tagValueCopy = tag_value;
                       //Console.WriteLine(correctResult);

                   }
                   // closing tag              
                   else if (currentRead == '<' && rdr.Peek() == '/')
                   {
                       lastused.Add("</");
                       rdr.Read();
                       string tag_name = "";
                       while (rdr.Peek() != '>')
                       {
                           tag_name += Convert.ToChar(rdr.Read());
                           columnNumber++;
                       }
                       rdr.Read();
                       // error correction
                       if (validationStack.Count == 0) 
                       {
                           errorLine = "Error: Tag not closed at Line " + lineNumber + "Col." + columnNumber + Environment.NewLine;
                       }
                       if (tag_name != validationStack.Peek().TagName)
                       {
                           // in case of missing values 
                           correctedFile += "</" + validationStack.Peek().TagName + ">" + Environment.NewLine;
                           correctedFile += "</" + tag_name + ">";
                           //Console.WriteLine("Error: Tag not closed at Line "+ lineNumber + "Col." + columnNumber);
                           errorLine = "Error: Tag not closed at Line " + lineNumber + "Col." + columnNumber + Environment.NewLine;
                           //currentError.Text = errorLine;
                       }
                       else
                       {
                           correctedFile += "</" + tag_name + ">";
                           Console.WriteLine(correctedFile);
                       }
                       Console.WriteLine(correctedFile);
                       correctResult += "</" + validationStack.Peek().TagName + ">";
                       //Console.WriteLine(correctResult);
                       Tag poped_node = validationStack.Pop();
                       //Root Node if empty stack. 
                       if (validationStack.Count == 0)
                       {
                           this.root_tags.Add(poped_node);
                       }
                       else
                       {
                           Tag lastOpenedNode = validationStack.Peek();
                           lastOpenedNode.Childs.Add(poped_node);
                       }
                       // child to some parent 
                       //Console.WriteLine(tag_name + " tag >  has been detected ");
                   }
                   
               }
              
           }
           return correctedFile;
       }
    
       public string ConvertToJson()
       {
         return "result";

     
       }

       public string Trim()
       {
           // Pt5 remove the sapacs and the new lines from the xml and retrun a string with no new line or space.
           string XmlDoc = File.ReadAllText(path);
            XmlDoc.Trim();
           string[] charactersToReplace = new string[] { @"\t", @"\n", @"\r", " ", @"\r\n" ,Environment.NewLine};
           foreach (string s in charactersToReplace)
           {
               XmlDoc = XmlDoc.Replace(s, "");
           }
           return XmlDoc;
       }

       public void SaveXMLTrimmed(string filepath_)
       {
           // Save the current XML into a another file but trimmed. use the Trim function in this class
           String trimmed = this.Trim();

           // save to a file at the filepath_
           ;
       }
          public int IsInTable(List<string> dictionary, string searchList)
        {
            int index = dictionary.IndexOf(searchList);
            return index;
        }
        public  string Compress(string filepath, List<List<int>> codeIndex, List<string> dictionary,int* no_of_loines)
        {
            dictionary.Clear();
            List<string> lines = File.ReadAllLines(filepath).ToList();

            *no_of_lines = 0;
            foreach (string line in lines)
            {
                List<int> temp = new List<int>();
                int i = 0;
                string c = line[0].ToString();
                if ((IsInTable(dictionary, c)) == -1)
                    dictionary.Add(c);

                string n = line[1].ToString();
                string cn = c + n;

                while (i < line.Length)
                {
                    int copycode = -1;
                    if (i != 0 && c != cn)
                    {
                        c = line[i].ToString();
                        if ((IsInTable(dictionary, c)) == -1)
                            dictionary.Add(c);
                    }
                    if (i + 1 != line.Length)
                    {

                        n = line[i + 1].ToString();
                        cn = c + n;
                        if ((copycode = IsInTable(dictionary, cn)) != -1)
                            c = cn;
                        else
                        {
                            copycode = IsInTable(dictionary, c);
                            temp.Add(copycode);
                            dictionary.Add(cn);
                        }


                    }
                    i++;
                }
                temp.Add(IsInTable(dictionary, c));

                *no_of_lines++;

                codeIndex.Add(temp);
                
            }
            string After_compression = "";
            for (int i = 0; i < no_of_lines; i++)
            {
                string str = "";
                for (int j = 0; j < codeIndex[i].Count; j++)
                {
                    if (j == (codeIndex[i].Count - 1))
                    {
                      
                        str = str + codeIndex[i][j].ToString();// + ".";
                    }
                    else
                    {
                        str = str + codeIndex[i][j].ToString();//+ ",";
                        
                    }

                }

                After_compression += str;
                //str = str + Environment.NewLine;
                //lines2.Add(str);

            }

            return After_compression;
                
        }
      
         public static string Decompress(List<List<int>> compress_indexes, List<string> dictionary, int no_of_lines)
        {

            string decompressed_text = "";
            //StreamWriter file = new StreamWriter("aa.txt");
            //file.Write(ALL);
            //string filepath3 = @"aa.txt";
            //file.Close();
            //List<string> lines3 = File.ReadAllLines(filepath3).ToList();
            for (int i = 0; i < no_of_lines; i++)
            {
                string str = "";
                for (int j = 0; j < compress_indexes].Count; j++)
                {

                    str = str + dictionary[compress_indexes[i][j]];

                }
                //lines3.Add(str.ToString());
                decompressed_text += str.ToString() + Environment.NewLine;
            }

            //ALL += decompressed.ToString()+ Environment.NewLine;
            //File.WriteAllLines(filepath3, lines3);
            //dictionary.Clear();
            //file.Write(ALL);
            //file.Close();
            return decompressed_text;
        }

        // print result
        public void print ()
        {
            for (int i = 0; i < root_tags.Count(); i++ )
            {
                Console.WriteLine("the current Tag is: " + root_tags[i].TagName);
                for (int j = 0; j < root_tags[i].Childs.Count(); j++ )
                {
                    Console.WriteLine("the current Childs for this parent is: " + root_tags[i].Childs[j].TagName);
                }
            }
        }
    }
   
 
}