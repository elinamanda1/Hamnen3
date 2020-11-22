using System;
using System.Collections.Generic;
using System.Text;

namespace Hamnen
{
    class RowBoat : Boat
    {
        int MaxPassengers { get; }

        public RowBoat()
        {
            Type = "Roddbåt";
            DaysInHarbor = 1;
            Identity = "R-" + RandomizeIdentity();
            Weight = RandomizeNumbers(100, 301);
            MaxSpeed = RandomizeNumbers(0, 4);
            MaxPassengers = RandomizeNumbers(1, 7);
            Value = 0.5;
            HasBeenLoweredToday = false;
            Info = false;
    }
        public override string ToString()
        {
            return $"\t{Type}\t\t{Identity}\t\t{Weight} kg\t\t{MaxSpeed} knop\t\t\tMax antal passagerare: {MaxPassengers}";
        }        
    }
}
