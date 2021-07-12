using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace xml_read
{
  

    class TagAttribute
    {
        public string Name;
        public string Value;
        public List<Tag> child = new List<Tag>();
        private string attributeName;
        private string attributeContent;
        private Tag element;

        public TagAttribute(string name_, string value_ = "")
        {
            Name = name_;
            Value = value_;

        }

        public TagAttribute(string attributeName, string attributeContent, Tag element)
        {
            // TODO: Complete member initialization
            this.attributeName = attributeName;
            this.attributeContent = attributeContent;
            this.element = element;
        }
    }
}