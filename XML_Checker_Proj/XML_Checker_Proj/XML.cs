using System;
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
        public string result = "";
        public int levels = 0; 
        public int no_of_lines=0;
        public List<List<int>> codeIndex = new List<List<int>>();
        public List<string> dictionary =new List<string>();
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
                           errorLine += "Error: Tag not closed at Line " + lineNumber + "Col." + columnNumber + Environment.NewLine;
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
                           errorLine += "Error: Tag not closed at Line " + lineNumber + "Col." + columnNumber + Environment.NewLine;
                       }
                       if (tag_name != validationStack.Peek().TagName)
                       {
                           // in case of missing values 
                           correctedFile += "</" + validationStack.Peek().TagName + ">" + Environment.NewLine;
                           correctedFile += "</" + tag_name + ">";
                           //Console.WriteLine("Error: Tag not closed at Line "+ lineNumber + "Col." + columnNumber);
                           errorLine += "Error: Tag not closed at Line " + lineNumber + "Col." + columnNumber + Environment.NewLine;
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
        //format
        public string FormatXML()
        {
            // Format the XML and retun a string with XML formated.
            String result = "";
            int levels = 0;


            void formating_childs(Tag mytag)
            {
                for (int u = 0; u < levels; u++)
                    result += "      ";
                if (levels != 0)
                    result += "<" + mytag.TagName + " " + mytag.attributes + ">" + Environment.NewLine;

                if (mytag.Childs.Count != 0)
                {
                    levels++;

                    for (int j = 0; j < mytag.Childs.Count; j++)
                    {

                        formating_childs(mytag.Childs[j]);
                    }

                    levels--;
                    for (int u = 0; u < levels; u++)
                        result += "      ";

                    if (levels != 0)
                        result += "</" + mytag.TagName + ">" + Environment.NewLine;


                }

                else if (mytag.TagValue != null)
                {
                    string spaces = null;
                    for (int u = 0; u < levels + 1; u++)
                    { //result +="      ";
                        spaces += "      ";
                    }

                    int position = 0;
                    string temp = null;
                    string temp2 = null;
                    int y = 0;
                    position = mytag.TagValue.IndexOf(Environment.NewLine);
                    while (position != -1)
                    {
                        if (y == 0)
                        {
                            position = mytag.TagValue.IndexOf(Environment.NewLine);
                            temp = mytag.TagValue.Substring(position + 2);
                            temp2 = mytag.TagValue.Substring(0, position);
                        }
                        if (temp == Environment.NewLine)
                            result += spaces + mytag.TagValue.Substring(0, position);
                        else
                            result += spaces + temp2 + Environment.NewLine;

                        position = temp.IndexOf(Environment.NewLine);
                        if (position != -1)
                        {
                            temp2 = temp.Substring(0, position);
                            temp = temp.Substring(position + 2);
                        }
                        y = 1;
                    }

                    if (y == 0)
                        result += spaces + mytag.TagValue + Environment.NewLine;


                    for (int u = 0; u < levels; u++)
                        result += "      ";
                    if (levels != 0)
                        result += "</" + mytag.TagName + ">" + Environment.NewLine;


                }



            }
            for (int jj = 0; jj < root_tags.Count; jj++)
            {
                result += "<" + root_tags[jj].TagName + " " + root_tags[jj].attributes + ">" + Environment.NewLine;
                levels = 0;

                formating_childs(root_tags[jj]);

                result += "</" + root_tags[jj].TagName + ">" + Environment.NewLine;
            }



            return result;
        }


        // jason
        public string ConvertToJson()
        {

            String result = "{" + Environment.NewLine;
            int levels = 0;
            string space = "";
            //main


            for (int j = 0; j < root_tags.Count - 1; j++)
            {
                levels = 0;
                if (root_tags[j].TagName == root_tags[j + 1].TagName)
                {
                    root_tags[j].has_sibling += 1;
                    root_tags[j + 1].has_sibling += 2;

                }

                if (j != 0 && root_tags[j].TagName != root_tags[j + 1].TagName && root_tags[j - 1].has_sibling >= 2)
                    result += "       " + "]" + Environment.NewLine;
                print(root_tags[j]);



            }

            if (root_tags.Count > 2 && root_tags[root_tags.Count - 2].TagName == root_tags[root_tags.Count - 3].TagName && root_tags[root_tags.Count - 1].has_sibling == 0)
                result += "       " + "]" + Environment.NewLine;

            print(root_tags[root_tags.Count - 1]);

            if (root_tags[root_tags.Count - 1].has_sibling >= 2)
                result += "       " + "]" + Environment.NewLine;


            void print(Tag mytag)
            {
                for (int u = 0; u < levels; u++)
                    space += "     ";

                if (mytag.has_sibling == 1)
                {
                    result += space + "\"" + mytag.TagName + "\":[" + Environment.NewLine;

                    if (mytag.attributes != null)
                    {
                        int position = mytag.attributes.IndexOf("=");
                        int position2 = 0;
                        string temp = mytag.attributes;
                        string temp2 = null;
                        while (temp.IndexOf("=") != -1)
                        {
                            position = temp.IndexOf("=");

                            result += space + "\"" + temp.Substring(0, position) + "\": ";

                            temp = temp.Substring(position + 1);
                            position = temp.IndexOf("=");

                            if (position == -1) //last attribute
                                result += temp.Substring(position + 1) + Environment.NewLine;
                            else
                            {
                                position2 = temp.IndexOf("\"");
                                temp2 = temp.Substring(position2 + 1); //after first =
                                position = temp2.IndexOf("\"");
                                result += space + temp.Substring(position2, position + 2) + "," + Environment.NewLine;
                                temp = temp2.Substring(position + 1); //take in consder space between attributes
                            }

                        }
                        result += ",";
                    }

                    if (mytag.Childs.Count != 0)
                    {

                        result += space + "{";
                        levels++;
                        for (int j = 0; j < mytag.Childs.Count - 1; j++) //to print childs
                        {

                            if (mytag.Childs[j].TagName == mytag.Childs[j + 1].TagName)
                            {
                                mytag.Childs[j].has_sibling += 1;
                                mytag.Childs[j + 1].has_sibling += 2;
                            }
                            if (j != 0 && mytag.Childs[j].TagName != mytag.Childs[j + 1].TagName && mytag.Childs[j - 1].has_sibling >= 2)
                            {
                                result += "       " + "]" + Environment.NewLine;
                            }
                            print(mytag.Childs[j]);
                            if (mytag.Childs.Count != 1)
                                result += "," + Environment.NewLine;

                        }

                        levels--;
                        space = null;
                        for (int u = 0; u < levels; u++)
                            space += "     ";

                        if (mytag.Childs.Count > 2 && mytag.Childs[mytag.Childs.Count - 2].TagName == mytag.Childs[mytag.Childs.Count - 3].TagName && root_tags[root_tags.Count - 1].has_sibling == 0)
                            result += "         " + "]" + Environment.NewLine;

                        print(mytag.Childs[mytag.Childs.Count - 1]);

                        if (mytag.Childs[mytag.Childs.Count - 1].has_sibling >= 2)
                            result += "         " + "]" + Environment.NewLine;


                        result += space + "}" + Environment.NewLine;


                    }

                    else if (mytag.TagValue != null)
                    {
                        result += "\"";
                        int position = 0;
                        string temp = null;
                        string temp2 = null;
                        int y = 0;
                        position = mytag.TagValue.IndexOf(Environment.NewLine);
                        while (position != -1)
                        {
                            if (y == 0)
                            {
                                position = mytag.TagValue.IndexOf(Environment.NewLine);
                                temp = mytag.TagValue.Substring(position + 2);
                                temp2 = mytag.TagValue.Substring(0, position);
                            }
                            if (temp == Environment.NewLine)
                                result += space + mytag.TagValue.Substring(0, position);
                            else
                                result += space + temp2 + Environment.NewLine;

                            position = temp.IndexOf(Environment.NewLine);
                            if (position != -1)
                            {
                                temp2 = temp.Substring(0, position);
                                temp = temp.Substring(position + 2);
                            }
                            y = 1;
                        }

                        if (y == 0)
                            result += mytag.TagValue;

                        result += "\"" + Environment.NewLine;
                    }


                }

                if (mytag.has_sibling >= 2)
                {
                    //   result += "," + Environment.NewLine;
                    if (mytag.attributes != null)
                    {
                        int position = mytag.attributes.IndexOf("=");
                        int position2 = 0;
                        string temp = mytag.attributes;
                        string temp2 = null;
                        while (temp.IndexOf("=") != -1)
                        {
                            position = temp.IndexOf("=");

                            result += space + "\"" + temp.Substring(0, position) + "\": ";

                            temp = temp.Substring(position + 1);
                            position = temp.IndexOf("=");

                            if (position == -1) //last attribute
                                result += temp.Substring(position + 1) + Environment.NewLine;
                            else
                            {
                                position2 = temp.IndexOf("\"");
                                temp2 = temp.Substring(position2 + 1); //after first =
                                position = temp2.IndexOf("\"");
                                result += temp.Substring(position2, position + 2) + "," + Environment.NewLine;
                                temp = temp2.Substring(position + 1); //take in consder space between attributes
                            }

                        }
                    }

                    if (mytag.Childs.Count != 0)
                    {
                        result += space + "{";
                        levels++;
                        for (int j = 0; j < mytag.Childs.Count - 1; j++) //to print childs
                        {

                            if (mytag.Childs[j].TagName == mytag.Childs[j + 1].TagName)
                            {
                                mytag.Childs[j].has_sibling += 1;
                                mytag.Childs[j + 1].has_sibling += 2;
                            }
                            if (j != 0 && mytag.Childs[j].TagName != mytag.Childs[j + 1].TagName && mytag.Childs[j - 1].has_sibling >= 2)
                                result += space + "]" + Environment.NewLine;

                            print(mytag.Childs[j]);
                            if (mytag.Childs.Count != 1)
                                result += "," + Environment.NewLine;

                        }
                        levels--;
                        space = null;
                        for (int u = 0; u < levels; u++)
                            space += "     ";

                        if (mytag.Childs.Count > 2 && mytag.Childs[mytag.Childs.Count - 2].TagName == mytag.Childs[mytag.Childs.Count - 3].TagName && root_tags[root_tags.Count - 1].has_sibling == 0)
                            result += space + "]" + Environment.NewLine;

                        print(mytag.Childs[mytag.Childs.Count - 1]);

                        if (mytag.Childs[mytag.Childs.Count - 1].has_sibling >= 2)
                            result += space + "]" + Environment.NewLine;


                        result += space + "}" + Environment.NewLine;

                    }

                    else if (mytag.TagValue != null)
                    {
                        result += space + "\"";
                        int position = 0;
                        string temp = null;
                        string temp2 = null;
                        int y = 0;
                        position = mytag.TagValue.IndexOf(Environment.NewLine);
                        while (position != -1)
                        {
                            if (y == 0)
                            {
                                position = mytag.TagValue.IndexOf(Environment.NewLine);
                                temp = mytag.TagValue.Substring(position + 2);
                                temp2 = mytag.TagValue.Substring(0, position);
                            }
                            if (temp == Environment.NewLine)
                                result += space + mytag.TagValue.Substring(0, position);
                            else
                                result += space + temp2 + Environment.NewLine;

                            position = temp.IndexOf(Environment.NewLine);
                            if (position != -1)
                            {
                                temp2 = temp.Substring(0, position);
                                temp = temp.Substring(position + 2);
                            }
                            y = 1;
                        }

                        if (y == 0)
                            result += mytag.TagValue;

                        result += "\"" + Environment.NewLine;
                    }

                }
                if (mytag.has_sibling == 0)
                {
                    if (mytag.attributes != null && mytag.Childs.Count != 0)
                    {
                        int position = mytag.attributes.IndexOf("=");
                        int position2 = 0;
                        string temp = mytag.attributes;
                        string temp2 = null;
                        while (temp.IndexOf("=") != -1)
                        {
                            position = temp.IndexOf("=");

                            result += space + "\"" + temp.Substring(0, position) + "\": ";

                            temp = temp.Substring(position + 1);
                            position = temp.IndexOf("=");

                            if (position == -1) //last attribute
                                result += temp.Substring(position + 1) + Environment.NewLine;
                            else
                            {
                                position2 = temp.IndexOf("\"");
                                temp2 = temp.Substring(position2 + 1); //after first =
                                position = temp2.IndexOf("\"");
                                result += temp.Substring(position2, position + 2) + "," + Environment.NewLine;
                                temp = temp2.Substring(position + 1); //take in consder space between attributes
                            }

                        }
                    }
                    if (mytag.Childs.Count != 0)
                    {
                        result += space + "\"" + mytag.TagName + "\":{" + Environment.NewLine;
                        for (int j = 0; j < mytag.Childs.Count - 1; j++) //to print childs
                        {
                            levels++;
                            if (mytag.Childs[j].TagName == mytag.Childs[j + 1].TagName)
                            {
                                mytag.Childs[j].has_sibling = 1;
                                mytag.Childs[j + 1].has_sibling = 2;
                            }
                            if (j != 0 && mytag.Childs[j].TagName != mytag.Childs[j + 1].TagName && mytag.Childs[j - 1].has_sibling >= 2)
                                result += space + "]" + Environment.NewLine;

                            print(mytag.Childs[j]);
                            if (mytag.Childs.Count != 1)
                                result += "," + Environment.NewLine;

                        }
                        levels--;
                        space = null;
                        for (int u = 0; u < levels; u++)
                            space += "    ";

                        if (mytag.Childs.Count > 2 && mytag.Childs[mytag.Childs.Count - 2].TagName == mytag.Childs[mytag.Childs.Count - 3].TagName && root_tags[root_tags.Count - 1].has_sibling == 0)
                            result += space + "]" + Environment.NewLine;

                        print(mytag.Childs[mytag.Childs.Count - 1]);

                        if (mytag.Childs[mytag.Childs.Count - 1].has_sibling >= 2)
                            result += space + "]" + Environment.NewLine;


                        result += space + "}" + Environment.NewLine;

                    }

                    else if (mytag.TagValue != null)
                    {
                        result += space + "\"" + mytag.TagName + "\":";
                        if (mytag.attributes != null)
                        {
                            int position = mytag.attributes.IndexOf("=");
                            int position2 = 0;
                            string temp = mytag.attributes;
                            string temp2 = null;
                            while (temp.IndexOf("=") != -1)
                            {
                                position = temp.IndexOf("=");

                                result += space + "\"" + temp.Substring(0, position) + "\": ";

                                temp = temp.Substring(position + 1);
                                position = temp.IndexOf("=");

                                if (position == -1) //last attribute
                                    result += temp.Substring(position + 1) + Environment.NewLine;
                                else
                                {
                                    position2 = temp.IndexOf("\"");
                                    temp2 = temp.Substring(position2 + 1); //after first =
                                    position = temp2.IndexOf("\"");
                                    result += temp.Substring(position2, position + 2) + "," + Environment.NewLine;
                                    temp = temp2.Substring(position + 1); //take in consder space between attributes
                                }

                            }
                        }




                        result += space + "\"";
                        int position3 = 0;
                        string temp3 = null;
                        string temp4 = null;
                        int z = 0;
                        position3 = mytag.TagValue.IndexOf(Environment.NewLine);
                        while (position3 != -1)
                        {
                            if (z == 0)
                            {
                                position3 = mytag.TagValue.IndexOf(Environment.NewLine);
                                temp3 = mytag.TagValue.Substring(position3 + 2);
                                temp4 = mytag.TagValue.Substring(0, position3);
                            }
                            if (temp3 == Environment.NewLine)
                                result += space + temp4;
                            else
                                result += space + temp4 + Environment.NewLine;

                            position3 = temp3.IndexOf(Environment.NewLine);
                            if (position3 != -1)
                            {
                                temp4 = temp3.Substring(0, position3);
                                temp3 = temp3.Substring(position3 + 2);
                            }
                            z = 1;
                        }

                        if (z == 0)
                            result += mytag.TagValue;

                        result += "\"" + Environment.NewLine;
                    }



                }

            }






            result += Environment.NewLine + "}";

            return result;
        }


        //trim
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

       public void SaveXMLTrimmed(string filepath_, string sotred)
       {
           // Save the current XML into a another file but trimmed. use the Trim function in this class
           String trimmed = this.Trim();

           // save to a file at the filepath_
           using (FileStream fs = File.Create(filepath_))
           {
               byte[] info = new UTF8Encoding(true).GetBytes(trimmed);
               fs.Write(info, 0, info.Length);
           }
           
       }
        // store function 
       public void storeOutput(string filepath_, string sotred) 
       {
           using (FileStream fs = File.Create(filepath_))
           {
               byte[] info = new UTF8Encoding(true).GetBytes(sotred);
               fs.Write(info, 0, info.Length);
           }
       }
       
        // compression 
       public int IsInTable(string searchList)
        {
            int index = dictionary.IndexOf(searchList);
            return index;
        }

        public  string Compress()
        {
            dictionary.Clear();
            codeIndex.Clear();
            List<string> lines = new List<string>();
            lines = File.ReadAllLines(path).ToList();

            no_of_lines = 0;
            foreach (string line in lines)
            {
                List<int> temp = new List<int>();
                int i = 0;
                string c = line[0].ToString();
                if ((IsInTable(c)) == -1)
                    dictionary.Add(c);

                string n = line[1].ToString();
                string cn = c + n;

                while (i < line.Length)
                {
                    int copycode = -1;
                    if (i != 0 && c != cn)
                    {
                        c = line[i].ToString();
                        if ((IsInTable(c)) == -1)
                            dictionary.Add(c);
                    }
                    if (i + 1 != line.Length)
                    {

                        n = line[i + 1].ToString();
                        cn = c + n;
                        if ((copycode = IsInTable(cn)) != -1)
                            c = cn;
                        else
                        {
                            copycode = IsInTable(c);
                            temp.Add(copycode);
                            dictionary.Add(cn);
                        }


                    }
                    i++;
                }
                temp.Add(IsInTable(c));

                no_of_lines++;

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
      
         public  string Decompress()
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
                for (int j = 0; j < codeIndex[i].Count; j++)
                {

                    str = str + dictionary[codeIndex[i][j]];

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