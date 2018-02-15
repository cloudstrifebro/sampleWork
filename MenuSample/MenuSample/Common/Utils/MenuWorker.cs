using MenuSample.Common.Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MenuSample.Common.Utils
{
    /// <summary>
    /// Worker class that is used to simulate various Menu operations.
    /// </summary>
    public static class MenuWorker
    {
        /// <summary>
        /// Gets a menu from a given XML file.
        /// </summary>
        /// <param name="path">The fully-qualified path for the XML file.</param>
        /// <returns>Returns a Menu.</returns>
        public static Menu GetMenu(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new FileNotFoundException("Invalid path specified. Check the filename and try again.");

            var serializer = new XmlSerializer(typeof(Menu));
            using (var reader = new StreamReader(path))
            {
                return (Menu)serializer.Deserialize(reader);               
            }
        }

        /// <summary>
        /// Navigates through a menu.  
        /// </summary>
        /// <param name="url">The url to search for.</param>
        /// <param name="menu">The menu containing the URL to search for.</param>
        /// <returns>Returns an updated menu.</returns>
        public static Menu Navigate(string url, Menu menu)
        {
            //guard used to determine if there is a submenu
            if (string.IsNullOrWhiteSpace(url) || menu == null) return menu;                
                
                foreach(var item in menu.Items)
                {
                    //check if the url exists
                    if (item.Path.Value.ToUpperInvariant() == url.ToUpperInvariant())
                    {
                        item.IsActive = true;
                    }
                    //check if url is matched elsewhere in the tree
                    if(item.SubMenu != null)
                    {
                        //if url doesn't exist, recurse through menu until base case or url found. 
                        item.SubMenu =  Navigate(url, item.SubMenu);

                        //check if children are active
                        if(item.SubMenu != null && item.SubMenu.Items.Any(m => m.IsActive))
                        {
                            item.IsActive = true;
                        }
                    }
                }
         
            return menu;
        }

        /// <summary>
        /// Prints a menu.
        /// </summary>
        /// <param name="menu">The menu to print.</param>
        /// <param name="isChild">Flag which determines if this Menu is a child of another menu.</param>
        /// <param name="depth">The depth of this menu or sub-menu</param>
        /// <returns>Returns a string which contains the menu structure.</returns>
        public static string PrintMenu(Menu menu, bool isChild = false, int depth = 0)
        {
            if (menu == null) return "";
            var result = "";

            foreach(var menuItem in menu.Items)
            {
                var parentMenu = "";

                if (isChild)
                {
                    parentMenu = FormatChild(menuItem, depth);
                }
                else
                {
                    parentMenu = FormatParent(menuItem);
                    depth = 0;
                }

                //if a submenu exists, recurse through the submenu and format if necessary
                if(menuItem.SubMenu != null && menuItem.SubMenu.Items.Any())
                {
                    depth++;
                    foreach (var subItem in menuItem.SubMenu.Items)
                    {
                        var subText = FormatChild(subItem, depth);
                        parentMenu += $"{subText}";
                        if (subItem.SubMenu != null && subItem.SubMenu.Items.Any())
                        {
                            depth++;
                            parentMenu += PrintMenu(subItem.SubMenu, true, depth);
                        }
                    }
                }

                result += parentMenu;
            }

            return result;
        }

        /// <summary>
        /// Formats a parent menu item as a string.
        /// </summary>
        /// <param name="item">A parent menu item.</param>
        /// <returns>Returns the output of a parent menu item.</returns>
        private static string FormatParent(MenuItem item)
        {
            var activeText = item.IsActive ? "ACTIVE" : "";
            return $"{item.DisplayName}, {item.Path.Value} {activeText}\n";
        }

        /// <summary>
        /// Formats a child menu item as a string.
        /// </summary>
        /// <param name="item">A child menu item.</param>
        /// <param name="depth">The depth of the child menu item.</param>
        /// <returns>Returns the output of a child menu item.</returns>
        private static string FormatChild(MenuItem item, int depth = 0)
        {
            var activeText = item.IsActive ? "ACTIVE" : "";
            var indentText = "";

            for(int i = 0; i < depth; i++)
            {
                indentText += "\t";
            }

            return $"{indentText}{item.DisplayName}, {item.Path.Value} {activeText}\n";
        }
    }
}
