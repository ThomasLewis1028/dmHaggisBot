using System;
using System.IO;
using System.Linq;
using System.Numerics;
using Newtonsoft.Json.Linq;

namespace dmHaggisBot
{
    class Program
    {
        private static Random rand = new Random();
        private static readonly JObject prop = JObject.Parse(File.ReadAllText(@"C:\Users\Thomas Lewis\RiderProjects\dmHaggisBot\dmHaggisBot\properties.json"));
        // private string token = (string) prop.GetValue("token");
        private static string personData = (string) prop.GetValue("personData");
        private static string starData = (string) prop.GetValue("starData");
        
        static void Main(string[] args)
        {
            Excel personExcel = new Excel(personData);
            var personReader = personExcel.ReaderReturn(personData);
            
            Excel starExcel = new Excel(starData);
            var starReader = starExcel.ReaderReturn(starData);
            
            //new DiscordBot().MainAsync().GetAwaiter().GetResult();

            /* USE LATER
                var options =  {'c': self.character, 'j': self.job}
                options[sel]()
             */
            
            
            //Set up x/y grid and split the substring into X and Y values
            Console.Out.WriteLine("Insert your grid parameters (x y): ");
            string xyIn = Console.ReadLine();

            int x = 15,
                y = 15;
            
            if (xyIn != "")
            {
                x = Int32.Parse(xyIn.Split(" ")[0]);
                y = Int32.Parse(xyIn.Split(" ")[1]);
            }

            Grid grid = new Grid(x, y);
            
            while (true)
            {
                Console.Write("Would you like to create a (C)haracter, (S)ystem, or (J)ob: ");
                string sel = Console.ReadLine();
                
                //Check 
                if (sel == "")
                {
                    break;
                }
                
                if (sel.ToUpper() == "C")
                {
                    //Set sheet bounds inluding sheet# and row count.
                    var sheet = rand.Next(0, 2);
                    var firstLen = personReader.Tables[sheet].Rows.Count;
                    var lastLen = personReader.Tables[2].Rows.Count;
                    var colLen = personReader.Tables[3].Rows.Count;
                    var lenLen = personReader.Tables[4].Rows.Count;
                    var eyeLen = personReader.Tables[5].Rows.Count;
                    
                    //Create the specified object
                    Person person = new Person(personReader.Tables[sheet].Rows[rand.Next(0,firstLen-1)].ItemArray[0].ToString(), personReader.Tables[2].Rows[rand.Next(0,lastLen-1)].ItemArray[0].ToString());
                    person.Age = rand.Next(10, 95);
                    person.Gender = (Person.GenderEnum)sheet;
                    person.HairCol = personReader.Tables[3].Rows[rand.Next(0, colLen)].ItemArray[0].ToString();
                    person.HairStyle = personReader.Tables[4].Rows[rand.Next(0, lenLen)].ItemArray[0].ToString();
                    person.EyeCol = personReader.Tables[5].Rows[rand.Next(0, eyeLen)].ItemArray[0].ToString();
                    
                    Console.Out.WriteLine("\t{0}, {1}, {2}, {3} {4}, {5} Eyes", person.Name, person.Gender, person.Age, person.HairCol, person.HairStyle, person.EyeCol);
                }
                else if (sel.ToUpper() == "S")
                {
                    Console.Out.Write("How many Systems would you like to create? ");
                    int sc = Int32.Parse(Console.ReadLine());
                    Console.Out.Write("What is the maximum number of planets per system? ");
                    int pc = Int32.Parse(Console.ReadLine());
                    
                    //Set sheet bounds inluding sheet# and row count.
                    var starLen = starReader.Tables[0].Rows.Count;
                    var planLen = starReader.Tables[1].Rows.Count;

                    int sCount = 0;
                    while (sCount < sc)
                    {
                        Star star = new Star(
                            starReader.Tables[0].Rows[rand.Next(0, starLen - 1)].ItemArray[0].ToString(),
                            rand.Next(0, grid.X + 1), rand.Next(0, grid.Y + 1));

                        int pCount = 0;
                        int pMax = rand.Next(0, pc+1);
                        while (pCount < pMax)
                        {
                            Planet planet = new Planet(starReader.Tables[1].Rows[rand.Next(0, planLen - 1)].ItemArray[0].ToString());

                            star.Planets.Add(planet);
                            pCount++;
                        }

                        sCount++;

                        Console.Out.WriteLine("{0} - ({1}, {2})", star.Name, star.X, star.Y);
                        foreach (var p in star.Planets)
                        {
                            Console.Out.WriteLine("\t" + p.Name);
                        }
                    }
                }
                else if (sel.ToUpper() == "J")
                {
                    
                }
            }
        }
    }
}