using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MenuSample.Common.Lib
{
    [XmlRoot("item")]
    public class MenuItem
    {
        [XmlElement("displayName")]
        public string DisplayName { get; set; }
        [XmlElement("path")]
        public ElementPath Path { get; set; }        
        [XmlElement("subMenu")]
        public Menu SubMenu { get; set; }
        public Menu ParentMenu { get; set; }
        public MenuItem ParentNode { get; set; }
        public bool IsActive { get; set; }
    }

    [XmlRoot("path")]
    public class ElementPath
    {
        [XmlAttribute("value")]
        public string Value { get; set; }
    }
}
