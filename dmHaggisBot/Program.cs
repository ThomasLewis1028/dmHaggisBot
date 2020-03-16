using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using _Excel = Microsoft.Office.Interop.Excel;

namespace dmHaggisBot
{
    class Program
    {
        private static Random rand = new Random();
        private static readonly JObject prop = JObject.Parse(File.ReadAllText(@"C:\Users\Thomas Lewis\RiderProjects\dmHaggisBot\dmHaggisBot\properties.json"));
        private string token = (string) prop.GetValue("token");
        private static string firstLastName = (string) prop.GetValue("firstLastName");
        
        static void Main(string[] args)
        {
            // new DiscordBot().MainAsync().GetAwaiter().GetResult();

            while (true)
            {
                Console.Write("Would you like to create a (C)haracter, (P)lanet, or (J)ob: ");
                string sel = Console.ReadLine();

                if (sel == null)
                {
                    continue;
                }
                else if (sel.ToUpper() == "C")
                {
                    Excel personSheet = new Excel(firstLastName);
                    personSheet.PickSheet(rand.Next(1,2));
                    string name = personSheet.ReadCell();
                    personSheet.PickSheet(3);
                    name += (" " + personSheet.ReadCell());
                    Person person = new Person(name);
                    
                    Console.WriteLine(person.Name);
                    
                }
            }
        }
    }
}