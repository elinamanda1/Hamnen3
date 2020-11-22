using System;
using System.Collections.Generic;
using System.Text;

namespace Hamnen
{
    
    class Dock
    {
        public Boat[] Boats = new Boat[2];
        public int Spot;
        public string name;
        public double valueDock
        {
            get;
        }
        public Dock(int i)
        {
            Spot = i;

        }
        public bool IsEmpty()
        {
            if(Boats[0]== null && Boats[1] == null)
            {
                return true;
            }

            return false;         
           
        }
        public void MakeNull(Dock dock)
        {
            dock.Boats[0] = null;
            dock.Boats[1] = null;
        }

       
    }
}

