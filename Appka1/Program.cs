using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appka1
{
    class Program
    {
        static void Main(string[] args)
        {

            ///vytvorenie inštancie typu autobazar
            AutoBazar autoBazar = new AutoBazar();
            bool exit = true;

            while (exit)
            {
                
                Console.WriteLine("********************");
                Console.WriteLine("1    -Pridaj auto");
                Console.WriteLine("2    -Vypis zoznamu aut");
                Console.WriteLine("3    -Vymaz auto");
                Console.WriteLine("4    -Zmena parametrov");
                Console.WriteLine("5    -Uloženie dát do súboru");
                Console.WriteLine("0    -Vypni program");
                Console.WriteLine("********************");
                switch (autoBazar.ReadCorrectIntValue())
                {
                    case 1:
                        
                        autoBazar.AddCar();
                        Console.WriteLine("Auto bolo pridané");
                        break;
                    case 2:
                        
                        autoBazar.Vypis();
                        break;
                    case 3:
                        Console.WriteLine("Zadaj Id auta na odstránenie zo zoznamu");
                        autoBazar.DeleteCar(autoBazar.ReadCorrectIntValue());
                        Console.WriteLine("Auto bolo odstranene");
                        break;
                    case 4:
                        Console.WriteLine("Zadaj Id auta, ktoré chces upraviť");
                        autoBazar.UpdateCar(autoBazar.ReadCorrectIntValue());
                        Console.WriteLine("Auto bolo upravené");
                        break;
                    case 5:
                        autoBazar.SaveToFile();
                        Console.WriteLine("Data boli uložene do suboru");
                        break;
                    case 0:
                        exit = false;
                        break;

                }

            }

            Console.ReadLine();
        }



    }
}
