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

            for (int i = 0; i < arrayCity.Length; i++)
            {
                string[] name = arrayCity[i].Split('=');
                string[] popden = name[1].Split(',');

                cities[i].Name = name[0];
                cities[i].Population = int.Parse(popden[0]);
                cities[i].Dencity = int.Parse(popden[1]);

            }

            int mostPop = 0;
            string nameMostPop = "";

            foreach (var s in cities)
            {                
                if (mostPop < s.Population)
                {
                    mostPop = s.Population;
                    nameMostPop = s.Name;
                }
            }

            string nameLongName = "";
            int lenghtName = 0;

            foreach (var s in cities)
            {
                if (lenghtName < s.Name.Length)
                {
                    nameLongName = s.Name;
                    lenghtName = s.Name.Length;
                }
            }

            Console.WriteLine($" Most populated:{nameMostPop} ({mostPop} people)");
            Console.WriteLine($" Longest name:{nameLongName} ({lenghtName} letters)");
            Console.WriteLine($" Density:");
            double tempValue = 0.00;

            foreach (var s in cities)
            {
                tempValue = Convert.ToDouble(s.Population) / Convert.ToDouble(s.Dencity);
                Console.WriteLine($"         {s.Name} - {tempValue.ToString().Substring(0,4)}");
            }


            // Console.WriteLine($"{mostPop}  {nameMostPop}\n");
            Console.ReadLine();
        }


    }
}
