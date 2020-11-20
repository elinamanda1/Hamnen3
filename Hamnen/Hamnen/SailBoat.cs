using System;
using System.Collections.Generic;
using System.Text;

namespace Hamnen
{
    class SailBoat: Boat
    {
        int Length { get; }

        public SailBoat()
        {
            Type = "Segelbåt";
            Space = 2;
            DaysInHarbor = 4;
            Identity = "S-" + RandomizeIdentity();
            Weight = RandomizeNumbers(800, 6001);
            MaxSpeed = RandomizeNumbers(0, 13);
            Length = RandomizeNumbers(10, 61);
            Value = 1;
        }
        public override string ToString()
        {
            return $"\t{Type}\t{Identity}\t\t{Weight} kg\t\t{MaxSpeed} knop\t\t\tLängd: {Length} fot";
        }
    }
}
