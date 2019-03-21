using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appka1
{
    public class AutoBazar
    {
        private List<Auto> carList = new List<Auto>();



        public void LoadFile()
        {
            string line;
            using (StreamReader sr = new StreamReader("bazos.txt"))
            {
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    string[] loadFile = line.Split('\t');
                    carList.Add(new Auto(int.Parse(loadFile[0]))
                    {
                        Brand = loadFile[1],
                        TypeOfCar = loadFile[2],
                        Fuel = (FuelType)Enum.Parse(typeof(FuelType), loadFile[3]),
                        YearOfProd = int.Parse(loadFile[4]),
                        MileAge = int.Parse(loadFile[5]),
                        Price = double.Parse(loadFile[6]),
                        Doors = int.Parse(loadFile[7]),
                        City = loadFile[8],
                        Condition = bool.Parse(loadFile[9])
                    });
                }

            }


        }

        public AutoBazar()
        {
            LoadFile();
        }

        public int ReadCorrectIntValue()
        {

            bool isOk = true;
            int ret = 0;
            while (isOk)
            {
                isOk = int.TryParse(Console.ReadLine(), out ret);
                if (isOk)
                {
                    return ret;
                }
                Console.WriteLine("Zadaj číslo");
            }
            return ret;
        }

        public void Vypis()
        {
            foreach (var item in carList)
            {
                Console.WriteLine(item.DescribeMe());
            }
            Console.WriteLine(carList.Count);
        }


        public int GetNextId(List<Auto> carList)

        {
            if (carList.Last().Id > 0)
            {
                int lastId = carList.Last().Id;
                lastId++;
                return lastId;

            }
            else
            {
                int lastId = 1;
                return lastId;
            }

        }

        public void DeleteCar(int delCar)
        {
            for (int i = 0; i < carList.Count; i++)
            {
                if (carList[i].Id == delCar)
                {
                    carList.RemoveAt(i);
                    SaveToFile();
                    break;
                }

            }



        }

        public void UpdateCar(int updCar)
        {
            for (int i = 0; i < carList.Count; i++)
            {
                if (carList[i].Id == updCar)
                {
                    Console.WriteLine("Zmena : 1- Značka, 2-Typ auta, 3- Typ paliva,4- Rok výroby ,5- Najazdené km, 6- Cena, 7- Počet dverí, 8- Miesto predaja, 9- Havarované");
                    int cUpd = ReadCorrectIntValue();
                    bool correctValue = true;
                    switch (cUpd)
                    {
                        case 1:
                            Console.WriteLine("Ideš zmeniť značku " + carList[i].Brand + " na:");
                            carList[i].Brand = Console.ReadLine();
                            SaveToFile();
                            break;
                        case 2:
                            Console.WriteLine("Ideš zmeniť typ auta " + carList[i].TypeOfCar + " na:");
                            carList[i].TypeOfCar = Console.ReadLine();
                            SaveToFile();
                            break;
                        case 3:
                            Console.WriteLine("Ideš zmeniť typ paliva " + carList[i].Fuel + " na:");
                            carList[i].Fuel = ((FuelType)Enum.Parse(typeof(FuelType), Console.ReadLine()));
                            SaveToFile();
                            break;
                        case 4:
                            Console.WriteLine("Ideš zmeniť rok výroby " + carList[i].YearOfProd + " na:");

                            while (correctValue)
                            {
                                int yOfProd = ReadCorrectIntValue();
                                if (yOfProd > 1870 && yOfProd <= DateTime.Now.Year)
                                {
                                    carList[i].YearOfProd = yOfProd;
                                    correctValue = false;
                                }
                                else { Console.WriteLine("Rok musí byť od 1870 až po aktuálny rok"); }
                            }
                            SaveToFile();
                            break;
                        case 5:
                            Console.WriteLine("Ideš zmeniť najazdené kilometre " + carList[i].MileAge + " na: (POZOR-je to protizákonné)");

                            while (correctValue)
                            {


                                int mAge = ReadCorrectIntValue();
                                if (mAge >= 0)
                                {
                                    carList[i].MileAge = mAge;
                                    correctValue = false;
                                }
                                else { Console.WriteLine("Kilometre nemôžu byť mínusová hodnota"); }
                            }
                            SaveToFile();
                            break;
                        case 6:
                            Console.WriteLine("Ideš zmeniť cenu " + carList[i].Price + " na: ");

                            while (correctValue)
                            {
                                if (!double.TryParse(Console.ReadLine(), out double prc))
                                {
                                    Console.WriteLine("Zadaj číselnú hodnotu");
                                    continue;
                                }
                                if (prc > 0)
                                {
                                    carList[i].Price = prc;
                                    correctValue = false;
                                }
                                else { Console.WriteLine("Cena musí byť minimále 1cent (0.01€)"); }
                            }
                            SaveToFile();
                            break;
                        case 7:
                            Console.WriteLine("Ideš zmeniť počet dverí " + carList[i].Doors + " na:");
                            carList[i].Doors =CorrectDoors ();
                            SaveToFile();
                            break;

                        case 8:
                            Console.WriteLine("Ideš zmeniť mesto predaja " + carList[i].City + " na:");
                            carList[i].City = Console.ReadLine();
                            SaveToFile();
                            break;
                    }
                    break;
                }
            }
        }
        public int CorrectDoors()
        {
            while (true)
            {
                int door = ReadCorrectIntValue();
                if (door >= 2 && door <= 5)
                {
                    int correctDoor = door;
                    return correctDoor;
                }
                else
                {
                    Console.WriteLine("Počet dverí môže byť 2,3,4 alebo 5");
                }
            }
        }

        bool correct = true;
        public void AddCar()
        {



            Auto car = new Auto(GetNextId(carList));

            Console.WriteLine("Zadaj výrobcu");
            car.Brand = Console.ReadLine();

            Console.WriteLine("Zadaj typ auta");
            car.TypeOfCar = Console.ReadLine();

            Console.Write("Zadaj typ paliva Benzin, Nafta, Plyn, Elektricke alebo Hybrid");
            car.Fuel = (FuelType)Enum.Parse(typeof(FuelType), Console.ReadLine());

            Console.WriteLine("Zadaj rok výroby");
            car.YearOfProd = ReadCorrectIntValue();

            Console.WriteLine("Zadaj najazdené km");
            car.MileAge = ReadCorrectIntValue();


            Console.WriteLine("Zadaj cenu");
            car.Price = double.Parse(Console.ReadLine());

            Console.WriteLine("Zadaj počet dverí");            
            car.Doors = CorrectDoors();

            Console.WriteLine("Je auto havarované");
            car.Condition = bool.Parse(Console.ReadLine());

            Console.WriteLine("Zadaj mesto predaja");
            car.City = Console.ReadLine();

            carList.Add(car);
            SaveToFile();



        }

        public void SaveToFile()
        {
            File.Delete("bazos.txt");
            foreach (var item in carList)
            {
                File.AppendAllText("bazos.txt", item.DescribeMe() + "\n");
            }
        }



    }
}
