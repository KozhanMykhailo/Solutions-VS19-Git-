using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controlOne
{
    struct City
    {
        public string Name;
        public int Dencity;
        public int Population;
    }

    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine($"Input data\n");
            var userInput = Console.ReadLine();

            string[] arrayCity = userInput.Split(';');
            int countArrayCity = arrayCity.Length;

            City[] cities = new City[countArrayCity];

            for(int i =0; i<arrayCity.Length;i++)
            {
                string[] name = arrayCity[i].Split('=');
                string[] popden = name[1].Split(',');

                cities[i].Name = name[0];
                cities[i].Population = int.Parse(popden[0]);
                cities[i].Dencity = int.Parse(popden[1]);

            }

            Console.WriteLine($"{cities[0].Name}  {cities[0].Population}\n");
            Console.ReadLine();
        }


    }
}
