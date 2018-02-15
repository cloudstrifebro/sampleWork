using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MenuSample.Common.Lib;
using System.Data;
using System.Xml.Linq;
using System.Xml.Xsl;
using MenuSample.Common.Utils;
using System.Xml.Serialization;

namespace MenuSample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var filePath = args[0];
                var url = args[1];

                var menu = MenuWorker.GetMenu(filePath);
                var updatedMenu = MenuWorker.Navigate(url, menu);
                var output = MenuWorker.PrintMenu(updatedMenu);

                Console.WriteLine(output);
            }
            catch(IndexOutOfRangeException ex)
            {
                Console.WriteLine($"Invalid number of arguments provided.  Please enter a valid path for your first argument and a valid navigation path for the second argument. Exception message: {ex.Message}");     
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
