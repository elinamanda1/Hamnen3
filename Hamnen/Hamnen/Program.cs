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

                LowerDaysInHarbor(); //denna metod ska gå igenom alla båtar i hamnen om dom finns och ta bort -1 dag i DaysInHarbor per båt.
                ResetHasBeenLoweredToday(); //Går igenom alla båtarna i hamnen och sätter "hasBeenLoweredToday" == false
                RemoveBoats();//när DaysInHarbor är 0 Så tas de bort i denna metod.
                
                PaintScreen();

                
                var boats = CreateFiveMoreBoats(); //skapar 5 nya båtar
                 
                foreach (var boat in boats) ///Skickar in en båt i taget
                {
                    if (boat is RowBoat)//Olika metoder för o lägga till roddbåtar och resterande
                    {
                       FindFreeSpotRowBoat(boat as RowBoat);
                    }
                    else
                    {
                        int spotIndex = FindFreeSpotAllBoats(boat.Space);
                        if(spotIndex > 80) // Eftersom att den returnerar 100 om det ej finns plats så valde jag bara ngt tal över 63, så om den alltså egentligen
                            //är mer än (index) 63 så kommer den ej gå vidare till AddBoatTo Dock och istället lägga till båten i RejetedBoats
                        {
                            RejectedBoats.Add(boat);
                            break;
                        }
                        AddBoatToDock(spotIndex, boat);

                    }     
                  }

                PaintHarbor();

                //spara i fil

                Console.WriteLine($"Dag: {day}\n\nPress any key to switch to the next day\n");
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

        private static void LowerDaysInHarbor()
        {
           

            foreach (Dock dock in Docks)
            {
                
                foreach (Boat boat in dock.Boats) //kollar en halvplats 
                {
                    
                    if(boat != null) //om boatplatsen inte är null
                    {
                        if(boat.HasBeenLoweredToday == false)
                        {
                            
                            boat.DaysInHarbor--;
                            boat.HasBeenLoweredToday = true;
                        }
                    }
                }
            }
        }

        private static void ResetHasBeenLoweredToday()
        {
            foreach(Dock dock in Docks)
            {
                foreach(Boat boat in dock.Boats)
                {
                    if(boat != null)
                    {
                        boat.HasBeenLoweredToday = false;
                    }      
                }
            }
        }

        private static void RemoveBoats()
        {
            foreach(Dock dock in Docks)
            {
                foreach(Boat boat in dock.Boats)
                {
                    if(boat != null)
                    {
                        if(boat.DaysInHarbor == 0)
                        {
                            dock.MakeNull(dock);
                        }
                       
                    }
                }
            }
        }

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
          
            Console.WriteLine("Antal avvisade Båtar: " + RejectedBoats.Count);
            TotalInfo();
            ResetAllInfo();
          
            
        }
        private static void TotalInfo()//1:Totalvikt, 2:totalt antal lediga platser3:Medeltal av hastigheten
        {
            int totalWeight = 0;
            int totalFreeSpots = 0;
            int averageMaxSpeed = 0;
            int totalBoatsForAverage = 0;
            int AmountOfRowBoats = 0;
            int AmountOfMotorBoats = 0;
            int AmountOfSailBoats = 0;
            int AmountOfCargoShips = 0;


            foreach (Dock dock in Docks)
            {
                foreach (Boat boat in dock.Boats)
                {
                    if(boat != null)
                    {
                        if (boat.Info == false)
                        {
                            totalWeight += boat.Weight;
                            averageMaxSpeed += boat.MaxSpeed;
                            totalBoatsForAverage++;
                            boat.Info = true;
                            if (boat is RowBoat) AmountOfRowBoats++;
                            if (boat is MotorBoat) AmountOfMotorBoats++;
                            if (boat is SailBoat) AmountOfSailBoats++;
                            if (boat is CargoShip) AmountOfCargoShips++;
                        }
                    }
                    else if (boat == null )
                    {
                        totalFreeSpots +=1;
                        break;
                    }
                }

            }

            Console.WriteLine("Totalvikt av alla båtar i hamnen: " + totalWeight + "kg");
            Console.WriteLine("Antal lediga platser: " + totalFreeSpots);
            Console.WriteLine("Medeltal av båtarnas Maxhastighet(Avrundat): " + averageMaxSpeed/totalBoatsForAverage + " knop\n");
            Console.WriteLine("Antal roddbåtar: " + AmountOfRowBoats + "\tAntal motorbåtar: " + AmountOfMotorBoats);
            Console.WriteLine("Antal Segelbåtar : " + AmountOfSailBoats + "\tAntal Lastfartyg : " + AmountOfCargoShips);
        }

        private static void ResetAllInfo()
        {
            foreach (Dock dock in Docks)
            {
                foreach (Boat boat in dock.Boats)
                {
                    if (boat != null)
                    {
                        boat.Info = false;
                    }
                }
            }
        }

    }
}


