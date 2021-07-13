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

        // constructor
       public XML (string path_)
        {
            path = path_;
        }


        // Parse XML and load the data into class properties
       public void Parse_XML()
       {
         int lineNumber = 1 ;
         int columnNumber = 0 ;
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
                    correctResult+= "<" + tag_name + tag_attribute + ">"+  Environment.NewLine ;
                    Console.WriteLine(correctResult);
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
                    correctResult += tag_value + (tag_value !="" ? Environment.NewLine:"");
                    Console.WriteLine(correctResult);

                }
                    // closing tag 
                          


            
                else if (currentRead == '<' && rdr.Peek() == '/') {
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
                         errorLine = "Error: Tag not closed at Line "+ lineNumber + "Col." + columnNumber;
                        //currentError.Text = errorLine;

                    }
                    correctResult += "</" + validationStack.Peek() + ">";
                    Console.WriteLine(correctResult);
                    Tag poped_node = validationStack.Pop();                 
                    //Root Node if empty stack. 
                    if (validationStack.Count == 0)
                    {
                        this.root_tags.Add(poped_node);
                    }
                    else {
                        Tag lastOpenedNode = validationStack.Peek();
                        lastOpenedNode.Childs.Add(poped_node);
                    }
                    // child to some parent 
                    //Console.WriteLine(tag_name + " tag >  has been detected ");
                }

               

            }
        }
    }


       public string FormatXML()
       {
           // Format the XML and retun a string with XML formated.
           String result = "";
        int levels = 0 ;
           for (int j = 0; j <  root_tags.Count; j++)
           {
              result +="<"+ root_tags[j].TagName +" "+ root_tags[j].attributes +">"+Environment.NewLine;
                levels = 0;

                formating_childs(root_tags[j]);  
              
                result +="</"+ root_tags[j].TagName + ">" + Environment.NewLine;
           }
       
        void formating_childs(Tag mytag )
        {
             for(int u =0 ;u < levels ; u++)
                result +="      ";
              if(levels != 0)
                result +="<"+ mytag.TagName +" "+ mytag.attributes +">"+Environment.NewLine;
          
                if (mytag.Childs.Count != 0)
                {
                    levels ++ ;

                    for (int j = 0; j < mytag.Childs.Count; j++)
                    {
                        
                        formating_childs(mytag.Childs[j] );
                    }
                   
                     levels -- ;
                    for(int u =0 ;u < levels ; u++)
                         result +="      ";
                  
                  if(levels != 0)
                     result +="</"+ mytag.TagName + ">" + Environment.NewLine;   
                         
                     
                }
                  
              else if (mytag.TagValue != null)
               {
                    string spaces = null ;
                     for(int u =0 ;u < levels + 1 ; u++)
                        { //result +="      ";
                        spaces+="      ";
                        }
                      
                        int position = 0 ; 
                        string temp = null ;   
                        string temp2 = null ;
                        int y = 0 ;
                        position = mytag.TagValue.IndexOf(Environment.NewLine);
                           while(position != -1)
                            {
                               if (y==0)
                            {
                            position = mytag.TagValue.IndexOf(Environment.NewLine);
                            temp = mytag.TagValue.Substring(position + 2);
                            temp2 = mytag.TagValue.Substring(0,position); 
                            } 
                            if(temp == Environment.NewLine)
                                 result += spaces +mytag.TagValue.Substring(0,position);
                                 else
                                 result += spaces + temp2+ Environment.NewLine;
                                  
                           position =  temp.IndexOf(Environment.NewLine);
                           if (position != -1)
                           {
                            temp2 = temp.Substring(0,position); 
                            temp = temp.Substring(position + 2);
                           }
                                  y = 1 ;
                           }
                    
                          if (y==0)
                         result += spaces + mytag.TagValue + Environment.NewLine;
                  
                   
                    for(int u =0 ;u < levels ; u++)
                         result +="      ";
                  if(levels != 0)
                     result +="</"+ mytag.TagName + ">" + Environment.NewLine; 
                
                       
                }



        }
       
        
        /*using (FileStream fs = File.Create(path))     
        {
                   byte[] info = new UTF8Encoding(true).GetBytes(result);
                    fs.Write(info, 0, info.Length);
        }
        */

      /*       int lineNumber = 1 ;
         int columnNumber = 0 ;
         FileStream _inputStream = new FileStream(this.path, FileMode.Open, FileAccess.Read);
         using (StreamReader rdr = new StreamReader(_inputStream))
        {            
            int currentRead;
            Tag currentParent = null;
            Tag currentNode = null;
            
            while ((currentRead = rdr.Read()) >= 0)
            {
                char currentChar = Convert.ToChar(currentRead);
                    result +=  currentChar ;
                columnNumber++;
                if (currentChar == '\n')
                {
                    lineNumber++;
                    columnNumber = 0;
                }
                // Xml Version line
                if ((currentChar == '<') && (rdr.Peek() == '?'))
                {
                             while (rdr.Peek() != '>')
                    { 
                        result += Convert.ToChar(rdr.Read());
                        columnNumber++;
                    }
                                 result += Environment.NewLine;
                }
                // Tag Detection  
                if (currentRead == '<' && rdr.Peek() != '/')
                {
                    
                    while (rdr.Peek() != '>')
                    { 
                        result += Convert.ToChar(rdr.Read());
                        columnNumber++;
                    }

                   result += Environment.NewLine;
                }
               
                else if (currentRead == '<' && rdr.Peek() == '/') {
                    rdr.Read();
                    string tag_name = "";
                    while (rdr.Peek() != '>')
                    {
                        tag_name += Convert.ToChar(rdr.Read());
                        columnNumber++;
                    }
                    rdr.Read();
                    if (tag_name != validationStack.Peek().TagName) {
                        Console.WriteLine("Error: Tag not closed at Line "+ lineNumber + "Col." + columnNumber);
                         errorLine = "Error: Tag not closed at Line "+ lineNumber + "Col." + columnNumber;
                        //currentError.Text = errorLine;

                    }
                    Tag poped_node = validationStack.Pop();                 
                    //Root Node if empty stack. 
                    if (validationStack.Count == 0)
                    {
                        this.root_tags.Add(poped_node);
                    }
                    else {
                        Tag lastOpenedNode = validationStack.Peek();
                        lastOpenedNode.Childs.Add(poped_node);
                    }
                    // child to some parent 
                    //Console.WriteLine(tag_name + " tag >  has been detected ");
                }

                else if (currentRead != '<' && currentRead != '\r' && currentRead != '\n')
                {
                    string tag_value = ""+Convert.ToChar(currentRead);
                    while (rdr.Peek() != '<')
                    {
                       tag_value += Convert.ToChar(rdr.Read());

                    }
                
                    currentNode.TagValue = tag_value;
                }
            }
        }
    }*/
           return result;
       }

       public string ConvertToJson() {
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