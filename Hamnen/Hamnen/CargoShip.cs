using System;
using System.Collections.Generic;
using System.Text;

namespace Hamnen
{
    class CargoShip: Boat
    {
        int Containers { get; }

        public CargoShip()
        {
            Type = "Lastfartyg";
            Space = 4;
            DaysInHarbor = 6;
            Identity = "L-" + RandomizeIdentity();
            Weight = RandomizeNumbers(3000, 20001);
            MaxSpeed = RandomizeNumbers(0, 21);
            Containers = RandomizeNumbers(0, 501);
            Value = 1;
        }
        public override string ToString()
        {
            return $"\t{Type}\t{Identity}\t\t{Weight}kg\t\t{MaxSpeed} knop\t\t\tContainers på båten: {Containers}";
        }

    }
}
