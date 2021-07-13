using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace xml_read
{
  
    // class Tag
    class Tag 
    {
       public Tag Parent; 
       public string TagName;
       public string TagValue;
       public bool isClosed;
       public List<Tag> Childs = new List<Tag>();
       public string attributes;
       //public List<TagAttribute> Attributes = new List<TagAttribute>();

       public Tag(string name, Tag parent=null ) 
       {
           TagName = name;
           Parent = parent;
           TagValue = "";
       }

       public Tag()
       {
            TagValue = null;
            attributes = null ;
           // TODO: Complete member initialization
           TagValue = "";
           attributes = null;
       }


    }

}