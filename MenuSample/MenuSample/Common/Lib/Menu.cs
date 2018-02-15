using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MenuSample.Common.Lib
{
    /// <summary>
    /// Represents a menu tree.
    /// </summary>
    [XmlRoot("menu")]
    public class Menu
    {
        [XmlElement("item")]
        public List<MenuItem> Items { get; set; }
    }
}
