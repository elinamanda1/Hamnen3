using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;


namespace Hamnen
{
    abstract class Boat // Abstrakt klass då den enbart ska fungera som basklass
    {
        //Protected, är tillgänglig i denna klass och i subklasser
        public string Type { get; set; }
        public int Space { get; set; }
        public int DaysInHarbor { get; set; }
        public string Identity { get; set; }
        public int Weight { get; set; }
        public int MaxSpeed { get; set; }
        public double Value { get; set; }

        static Random random = new Random();

        protected static string RandomizeIdentity() 
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, 3)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        protected static int RandomizeNumbers(int minimum, int maximum)
        {
            return random.Next(minimum, maximum);
        }
        public override string ToString()
        {
            return "Boat info";
        }
       
       
    }
}
