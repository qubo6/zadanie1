using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Appka1
{
    public class AutoBazar
    {
        private List<Auto> carList = new List<Auto>();


        /// <summary>
        ///  V prípade ak súbor neexistuje metóda ho vytvorí. Načítanie dát zo súboru do reťazca, jeho rozbitie a následné priradenie hodnôt konkrétnym vlastnostiam auta. 
        /// </summary>
        public void LoadFile()
        {
            if (!File.Exists("bazos.txt"))
            {
                File.Create("bazos.txt").Close();
            }
           
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

       /// <summary>
       /// Konštruktor autobazaru - načítanie dát zo súboru
       /// </summary>
        public AutoBazar()
        {
            LoadFile();
        }
        /// <summary>
        /// Výpis každeho prvku v carliste
        /// </summary>
        public void Vypis()
        {
            foreach (var item in carList)
            {
                Console.WriteLine(item.DescribeMe());
            }            
        }

        /// <summary>
        /// získavanie posledného Id z carlistu, návratová hodnota je o 1 vyššia ako posledné Id - v prípade prázdneho súboru sa nastaví Id na hodnotu 1
        /// </summary>
        /// <param name="carList"></param>
        /// <returns></returns>
        public int GetNextId(List<Auto> carList)            
        {
            if (carList.Count > 0)
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
        /// <summary>
        /// Vymazanie položky z carlistu na základe Id- po vymazaní sa uložia nové dáta do súboru
        /// </summary>
        /// <param name="delCar"></param>
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
        /// <summary>
        /// Editovanie položky v carliste- po editovaní sa uložia nové dáta do súboru
        /// </summary>
        /// <param name="updCar"></param>
        public void UpdateCar(int updCar)
        {
            for (int i = 0; i < carList.Count; i++)
            {
                if (carList[i].Id == updCar)
                {
                    Console.WriteLine("Zmena : 1- Značka, 2-Typ auta, 3- Typ paliva,4- Rok výroby ,5- Najazdené km, 6- Cena, 7- Počet dverí, 8- Miesto predaja, 9- Havarované, 0-žiadna zmena");
                    int cUpd = ReadCorrectIntValue();
                   
                    switch (cUpd)
                    {
                        case 1:
                            Console.WriteLine("Ideš zmeniť značku " + carList[i].Brand + " na:");
                            carList[i].Brand = CorrectString();
                            SaveToFile();
                            break;
                        case 2:
                            Console.WriteLine("Ideš zmeniť typ auta " + carList[i].TypeOfCar + " na:");
                            carList[i].TypeOfCar = CorrectString();
                            SaveToFile();
                            break;
                        case 3:
                            Console.WriteLine("Ideš zmeniť typ paliva " + carList[i].Fuel + " na:(B-benzin, N-nafta, P-plyn, E-elektricke alebo H-hybrid) ");
                            carList[i].Fuel =CorrectFuel();
                            SaveToFile();
                            break;
                        case 4:
                            Console.WriteLine("Ideš zmeniť rok výroby " + carList[i].YearOfProd + " na:");
                            carList[i].YearOfProd = CorrectYear();
                            SaveToFile();
                            break;
                        case 5:
                            Console.WriteLine("Ideš zmeniť najazdené kilometre " + carList[i].MileAge + " na: (POZOR-je to protizákonné)");
                            carList[i].MileAge = CorrectMileAge();
                            SaveToFile();
                            break;
                        case 6:
                            Console.WriteLine("Ideš zmeniť cenu " + carList[i].Price + " na: ");
                            carList[i].Price = ReadCorrectDoubleValue();                           
                            SaveToFile();
                            break;
                        case 7:
                            Console.WriteLine("Ideš zmeniť počet dverí " + carList[i].Doors + " na:");
                            carList[i].Doors = CorrectDoors();
                            SaveToFile();
                            break;

                        case 8:
                            Console.WriteLine("Ideš zmeniť mesto predaja " + carList[i].City + " na:");
                            carList[i].City = CorrectString();
                            SaveToFile();
                            break;
                        case 9:
                            Console.WriteLine("Ideš zmeniť stav auta z (havarované) " +carList[i].Condition +"na (A-áno, N-nie)");
                            carList[i].Condition = CorrectCondition();
                            SaveToFile();
                            break;
                        case 0:
                            break;
                    }
                    break;
                }
            }
        }
        /// <summary>
        /// načítanie číselnej(int) hodnoty z konzoly - ošetrenie v prípade nezadania hodnoty - s návratovou hodnotou
        /// </summary>
        /// <returns></returns>
        public int ReadCorrectIntValue()
        {
            bool isOk = false;
            int ret = 0;
            while (!isOk)
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
        /// <summary>
        /// načítanie číselnej(double) hodnoty z konzoly - ošetrenie v prípade nezadania hodnoty - s návratovou hodnotou
        /// </summary>
        /// <returns></returns>
        public double ReadCorrectDoubleValue()
        {
            bool isOk = false;
            double ret = 0;
            while (!isOk)
            {
                isOk = double.TryParse(Console.ReadLine(), out ret);
                if (isOk)
                {
                    return ret;
                }
                Console.WriteLine("Zadaj cenu");
            }
            return ret;
        }
        /// <summary>
        /// Načítanie textu z konzoly - ošetrenie v prípade nezadania hodnoty - s návratovou hodnotou
        /// </summary>
        /// <returns></returns>
        private string CorrectString()
        {
            while (true)
            {
                string correctStr = Console.ReadLine();
                if (!string.IsNullOrEmpty(correctStr))
                {
                    return correctStr;
                }
                else
                { Console.WriteLine("Je potrebné zadať text"); }
            }
        }
        /// <summary>
        /// Metóoda ošetrujúca zadanie roku v správnom intervale - s návratovou hodnotou
        /// </summary>
        /// <returns></returns>
        private int CorrectYear()
        {
            while (true)
            {
                int yOfProd = ReadCorrectIntValue();
                if (yOfProd > 1870 && yOfProd <= DateTime.Now.Year)
                {
                    int correctYear = yOfProd;
                    return correctYear;
                }
                else { Console.WriteLine("Rok musí byť od 1870 až po aktuálny rok"); }
            }
        }
        /// <summary>
        /// Metóoda ošetrujúca zadanie počtu kilometrov- nepovoluje mínosovú hodnotu - s návratovou hodnotou
        /// </summary>
        /// <returns></returns>
        private int CorrectMileAge()
        {
            while (true)
            {
                int mAge = ReadCorrectIntValue();
                if (mAge >= 0)
                {
                    int correctmAge = mAge;
                    return correctmAge;
                }
                else { Console.WriteLine("Kilometre nemôžu byť mínusová hodnota"); }
            }
        }
        /// <summary>
        /// Metóoda ošetrujúca zadanie počtu dverí v správnom intervale - s návratovou hodnotou
        /// </summary>
        /// <returns></returns>
        private int CorrectDoors()
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
                { Console.WriteLine("Počet dverí môže byť 2,3,4 alebo 5"); }
            }
        }
        /// <summary>
        /// Metóoda ošetrujúca zadanie stavu auta - A-áno alebo N-nie - s návratovou hodnotou
        /// </summary>
        /// <returns></returns>
        private bool CorrectCondition()
        {
            while (true)
            {
                string value = Console.ReadLine().ToUpper();
                if (!string.IsNullOrEmpty(value))
                {
                    if (value== "A")
                    {
                        return true;
                    }
                    if (value == "N")
                    {
                        return false;
                    }
                }
                Console.WriteLine("Zadaj A-áno alebo N-nie");
            }
        }
        /// <summary>
        /// Metóoda ošetrujúca zadanie správneho názvu paliva - s návratovou hodnotou
        /// </summary>
        /// <returns></returns>
        private FuelType CorrectFuel()
        {

            while (true)
            {
                string correctFuel = Console.ReadLine().ToUpper();
                if (!string.IsNullOrEmpty(correctFuel))
                {
                    if (correctFuel=="B")
                    {
                        return FuelType.Benzin;
                    }
                    if (correctFuel == "N")
                    {
                        return FuelType.Nafta;
                    }
                    if (correctFuel == "E")
                    {
                        return FuelType.Elektricke;
                    }
                    if (correctFuel == "P")
                    {
                        return FuelType.Plyn;
                    }
                    if (correctFuel == "H")
                    {
                        return FuelType.Hybrid;
                    }
                }
                Console.WriteLine("B-benzin, N-nafta, P-plyn, E-elektricke alebo H-hybrid"); 
            }
        }


        /// <summary>
        /// V danej métode sa vytvorí inštacia typu auto, a pridá jednotlivé hodnoty pre vlastnosti auta, po pridaní sa auto vloží do carlistu a ten sa uloží do súboru
        /// </summary>
        public void AddCar()
        {
            Auto car = new Auto(GetNextId(carList));

            Console.WriteLine("Zadaj výrobcu");
            car.Brand = CorrectString();

            Console.WriteLine("Zadaj typ auta");
            car.TypeOfCar = CorrectString();

            Console.WriteLine("Zadaj typ paliva B-benzin, N-nafta, P-plyn, E-elektricke alebo H-hybrid");
            car.Fuel = CorrectFuel();

            Console.WriteLine("Zadaj rok výroby");
            car.YearOfProd = CorrectYear();

            Console.WriteLine("Zadaj najazdené km");
            car.MileAge = CorrectMileAge();

            Console.WriteLine("Zadaj cenu");
            car.Price = ReadCorrectDoubleValue();

            Console.WriteLine("Zadaj počet dverí");
            car.Doors = CorrectDoors();

            Console.WriteLine("Je auto havarované A-áno, N-nie");
            car.Condition = CorrectCondition();

            Console.WriteLine("Zadaj mesto predaja");
            car.City = CorrectString();

            carList.Add(car);
            SaveToFile();
        }
        /// <summary>
        /// Metóda najprv vymaže súbor a následne doňho vloží každú položku z carlistu
        /// </summary>
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
