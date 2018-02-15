using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MenuSample.Common.Lib
{
    /// <summary>
    /// Represents a menu item in a menu.
    /// </summary>
    [XmlRoot("item")]
    public class MenuItem
    {
        [XmlElement("displayName")]
        public string DisplayName { get; set; }
        [XmlElement("path")]
        public ElementPath Path { get; set; }        
        [XmlElement("subMenu")]
        public Menu SubMenu { get; set; }
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// Represents an element with a url path.
    /// </summary>
    [XmlRoot("path")]
    public class ElementPath
    {
        [XmlAttribute("value")]
        public string Value { get; set; }
    }
}
