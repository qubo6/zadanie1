using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appka1
{
    public enum FuelType
        {
            Benzin,
            Nafta,
            Plyn,
            Elektricke,
            Hybrid
        }
        public class Auto
        {
        




        
        public int Id { get; private set ; }
        
        public int YearOfProd { get; set; }
        public int MileAge { get; set; }
        public string Brand { get; set; }
        public string TypeOfCar { get; set; }
        public FuelType Fuel { get; set; }
        public double Price { get; set; }
        public string City { get; set; }
        public int Doors { get; set; }
        public bool Condition { get; set; }


        public Auto(int id)
          
        {
            this.Id = id;
            //this.Brand = Brand;
        }





        public string DescribeMe()
        {
            
            StringBuilder sb = new StringBuilder();
            //sb.AppendLine($" ");
            sb.Append($"{Id}\t");
            sb.Append($"{Brand}\t");
            sb.Append($"{TypeOfCar}\t");
            sb.Append($"{Fuel}\t");
            sb.Append($"{YearOfProd}\t");
            sb.Append($"{MileAge}\t");
            sb.Append($"{Price}\t");
            sb.Append($"{Doors}\t");
            sb.Append($"{City}\t");
            sb.Append($"{Condition}");
            
            return sb.ToString();
        }

        
    }
}
