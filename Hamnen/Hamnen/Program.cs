using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Hamnen
{

    class Program
    {
        static int day = 1;
        static List<Dock> Docks = new List<Dock>(64);
        static List<Boat> RejectedBoats = new List<Boat>();
        static Random random = new Random();
      
        static void Main(string[] args)
        {
            CreateDocks(); //skapar 64 platser.

            while (true)
            {
                //Varje sak som händer i den här loopen kommer hända en gång per dag

                
                //RemoveBoats(); //Denna metod ska kolla vilka båtar som ska bort¨
                PaintScreen();

                var boats = CreateFiveMoreBoats(); //skapar 5 nya båtar

                foreach (var boat in boats) ///Skickar in en båt i taget
                {
                    if (boat is RowBoat)//Olika metoder för o lägga till roddbåtar och resterande.
                    {
                        FindFreeSpotRowBoat(boat as RowBoat);
                    }
                    else 
                    {
                        int spotIndex = FindFreeSpotAllBoats(boat.Space);
                        AddBoatToDock(spotIndex, boat);

                    }
                }

                PaintHarbor();

                //spara i fil

                Console.WriteLine($"Dag: {day}\n\nPress any key to switch to the next day");
                HarborInfo();
                Console.ReadKey();
                day++;

                Console.Clear();
            }
        }

        private static void CreateDocks()
        {
            for (int i = 0; i < 64; i++)
            {
                Docks.Add(new Dock(i));


            }
        }

      

        /*private static void RemoveBoats() //Ändra, funkar ej mnull
        {
            int a;
           for(int i = 0; i < Docks.Count; i++)
            {
                
                if(Docks[i] != null)
                {
                    
                    Console.WriteLine("Om dock inte är okänt värde så");
                    for(int y = 1; y < Docks[i].Boats.Length; y++)
                    {
                        Console.WriteLine("Inne in i båtarrayen");
                        a = Docks[i].Boats[y].DaysInHarbor;
                        //Console.WriteLine("Antal dagar kvar:" + Docks[i].Boats[y].DaysInHarbor);

                        if(Docks[i].Boats != null)
                        {
                            if (Docks[i].Boats[y] == null)
                            {
                                Console.WriteLine("Objekt sätts till null?");
                                Docks[i].Boats[y] = null;
                                Console.WriteLine();
                            }
                        }
                      
                    }
                }
            }
        }*/
        

        private static void PaintScreen()
        {
            Console.Clear();
            Console.WriteLine("Plats\tBåttyp\t\tNr\t\tVikt\t\tMaxhastighet\t\tÖvrigt");

        }

        public static List<Boat> CreateFiveMoreBoats() //skapar 5 båtar
        {
            List<Boat> boats = new List<Boat>();

            for (int i = 0; i < 5; i++)
            {
                int r = random.Next(0, 4);
                if (r == 0)
                {
                    RowBoat rb = new RowBoat();
                    boats.Add(rb);
                }
                if (r == 1)
                {
                    MotorBoat mb = new MotorBoat();
                    boats.Add(mb);
                }
                if (r == 2)
                {
                    SailBoat sb = new SailBoat();
                    boats.Add(sb);
                }
                if (r == 3)
                {
                    CargoShip cg = new CargoShip();
                    boats.Add(cg);
                }

            }
            return boats;
        }

        private static bool FindFreeSpotRowBoat(RowBoat rowBoat)//skickar in en roddbåt i taget.
        {
            foreach (Dock dock in Docks)//kollar en dock i taget 1-64(index 0-63)
            {
                for (int i = 0; i < dock.Boats.Length; i++) //
                {
                    if (dock.Boats[i] == null)
                    {
                        dock.Boats[i] = rowBoat;//= gör såhär och null i remove // lägger till rowboat
                        return true;
                    }
                }

            }
            RejectedBoats.Add(rowBoat); //funkar detta?
            return false;

        }

        private static int FindFreeSpotAllBoats(int boatSpace)//
        {
            int emptySpots = 0; 
            for (int i = 0; i < Docks.Count; i++)//0
            {
                if (Docks[i].IsEmpty() == true) //varje gång 
                {
                    emptySpots++;
                    if (boatSpace == emptySpots)
                    {
                        return i;//returnerar sista lediga index.
                    }
                }
                else
                {
                    emptySpots = 0;
                }
               
            }
            return 100; //ifall inga dock finns för båten.//glöm inte fånga upp dessa

        }
        private static void AddBoatToDock(int spot, Boat boat)//rätt sista index och båten.
        {
            int a = spot - boat.Space + 1; //Tar ut första lediga index 

            for (int i = 0; i < Docks.Count; i++)
            {
                if (i == a)
                {
                    for (int y = 0; y < Docks[i].Boats.Length; y++)
                    {
                        Docks[i].Boats[y] = boat; //fyller två platser med båten
                    }
                    a++;

                    if (a > spot) //stannar loopen när a = sista lediga index
                    {
                        break;
                    }
                }
            }
        }


        private static void PaintHarbor() //denna ska skriva ut alla båtar.
        {

            foreach (var dock in Docks)
            {
                Console.Write($"{dock.Spot}");
                foreach (Boat boat in dock.Boats)
                {
                    Console.WriteLine(boat);

                    if (!(boat is RowBoat))
                    {
                        break;
                    }
                }


            }

        }
        private static void HarborInfo()
        {
            Console.WriteLine(RejectedBoats.Count);
        }
    }
}


