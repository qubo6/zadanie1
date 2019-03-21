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
            

            AutoBazar autoBazar = new AutoBazar();
            bool exit = true;

            while (exit)
            {
                Console.WriteLine("********************");
                Console.WriteLine("1    -Pridaj auto");
                Console.WriteLine("2    -Vypis zoznamu aut");
                Console.WriteLine("3    -Vymaz auto");
                Console.WriteLine("4    -Zmena parametrov");
                Console.WriteLine("0    -Vypni program");
                Console.WriteLine("********************");

                if (!int.TryParse(Console.ReadLine(), out int key))
                {
                    Console.WriteLine("Vyber spravnu možnosť");
                    continue;
                }


                switch (key)
                {
                    case 1:

                        autoBazar.AddCar();
                        
                        break;
                    case 2:
                        
                        autoBazar.Vypis();
                        
                        break;
                    case 3:
                        Console.WriteLine("Zadaj Id auta na odstránenie zo zoznamu");
                        if (!int.TryParse(Console.ReadLine(), out int delCar))
                        {
                            Console.WriteLine("Zadaj číslo");
                            continue;
                        }

                        autoBazar.DeleteCar(delCar);
                        
                        break;
                    case 4:
                        Console.WriteLine("Zadaj Id auta, ktoré chces upraviť");
                        if (!int.TryParse(Console.ReadLine(), out int updCar))
                        {
                            Console.WriteLine("Zadaj číslo");
                            continue;
                        }
                        autoBazar.UpdateCar(updCar);
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
