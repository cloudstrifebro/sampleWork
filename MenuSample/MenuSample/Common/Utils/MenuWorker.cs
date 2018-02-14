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
    public static class MenuWorker
    {
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

        public static Menu Navigate(string url, Menu menu, bool isFound = false)
        {
            if (string.IsNullOrWhiteSpace(url) || menu == null) return menu;

            while (!isFound)
            {
                if (menu == null) return menu;                
                
                foreach(var item in menu.Items)
                {
                    if (item.Path.Value.ToUpperInvariant() == url.ToUpperInvariant())
                    {
                        isFound = true;
                        item.IsActive = true;
                        item.ParentMenu = menu;                        
                    }
                    else if(item.SubMenu != null && !isFound)
                    {
                        item.ParentMenu = menu;
                        item.ParentNode = item;
                        item.SubMenu =  Navigate(url, item.SubMenu, isFound);

                        //check if children are active
                        if(item.SubMenu != null && item.SubMenu.Items.Any(m => m.IsActive))
                        {
                            item.IsActive = true;
                        }
                        var result = item;
                    }
                }

                isFound = true;
            }

            return menu;
        }

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

        private static string FormatParent(MenuItem item)
        {
            var activeText = item.IsActive ? "ACTIVE" : "";
            return $"{item.DisplayName}, {item.Path.Value} {activeText}\n";
        }

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
