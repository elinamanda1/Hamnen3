using System;
using System.Collections.Generic;
using System.Text;

namespace Hamnen
{
    class MotorBoat : Boat
    {
        int MaxHorsePower { get; }

        public MotorBoat()
        {
            Type = "Motorbåt";
            Space = 1;
            DaysInHarbor = 3;
            Identity = "M-" + RandomizeIdentity();
            Weight = RandomizeNumbers(200, 3001);
            MaxSpeed = RandomizeNumbers(0, 61);
            MaxHorsePower = RandomizeNumbers(10, 1001);
            Value = 1;
            HasBeenLoweredToday = false;
            Info = false;
        }

        public override string ToString()
        {
            return $"\t{Type}\t{Identity}\t\t{Weight} kg\t\t{MaxSpeed} knop\t\t\tHästkrafter: {MaxHorsePower} hk";
        }
    }
}
