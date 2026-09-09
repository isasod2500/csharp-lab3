using System;
using System.ComponentModel.Design;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks.Dataflow;

namespace laboration3
{

    class EntryArray
    {
        //Deklarerar lista som ska styra formatet på lagringen av objekten.
        public List<Entry> entries = new List<Entry>();
    }

    class Entry
    {

        //Deklarerar objekt
        public string Name { get; set; }

        public string Message { get; set; }
    }

    class Guestbook
    {
        static void Main(string[] args)
        {

            //Här skapas array i form av EntryArray
            EntryArray entryArray = new EntryArray();

            //Kontroll att filen finns och att den inte är tom.
            if (File.Exists("guestbook.txt"))
            {
                string json = File.ReadAllText("guestbook.txt");
                if(!string.IsNullOrWhiteSpace(json))
                {
                    entryArray.entries = JsonSerializer.Deserialize<List<Entry>>(json);
                }
                
            }


           
            //Så länge true = true körs allt. För att undvika att appen stänger sig när ett alternativ är färdiggjort.
            while (true)
            {
                //Rensar konsoll, detta sker vid start (då egentligen onödigt) och efter att alternativen körts.
                Console.Clear();

                //Formatering av linjer.
                Console.WriteLine("VÄLKOMMEN TILL VINTERGATANS GÄSTBOK");

                Console.WriteLine("1. Skapa inlägg");
                Console.WriteLine("2. Radera inlägg");
                Console.WriteLine();
                Console.WriteLine("ESC. Stäng gästbook");
                Console.WriteLine();
                Console.WriteLine();



                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();

                //Om entryarray är tom, skriv då att gästboken är tom.
                if (entryArray.entries.Count == 0)
                {
                    Console.WriteLine("Gästboken är tom!");
                }
                else
                {   //Annars printa samtliga.
                    for (int i = 0; i < entryArray.entries.Count; i++)
                    {
                        var entry = entryArray.entries[i];
                        Console.WriteLine($"[{i}] {entry.Name}: {entry.Message}");
                    }
                }


                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Välj alternativ: ");
                //Jag ville att alternativen skulle börja utan att man tryckte på enter. True här gör att ens knapptryck inte syns i konsollen.
                var userInput = Console.ReadKey(true);

                Console.Clear();

                //Switchcase för knapptrycken.
                switch (userInput.Key)
                {
                    //Alternativ 1
                    case ConsoleKey.D1:

                        //Så länge username är tom kommer den be om namn.
                        string userName = "";
                        while (string.IsNullOrWhiteSpace(userName))
                        {
                            Console.Write("Ditt namn: ");
                            userName = Console.ReadLine();
                        }

                        //Så länge meddelandet är tomt kommer den be om meddelande.
                        string userMessage = "";
                        while (string.IsNullOrWhiteSpace(userMessage))
                        {
                            Console.Write("Ditt meddelande: ");
                            userMessage = Console.ReadLine();
                        }

                        
                        //Skapa objekt med lyckat input
                        var entry = new Entry
                        {
                            Name = userName,
                            Message = userMessage
                        };

                        //Lägg till nytt objekt i entryarray
                        entryArray.entries.Add(entry);

                        //Serialisera samtliga objekt till JSON
                        var jsonAdd = JsonSerializer.Serialize(entryArray.entries);

                        //Skriv den nyligen serialiserade arrayen till guestbook.txt
                        File.WriteAllText("guestbook.txt", jsonAdd);
                        break;

                    //Alternativ 2
                    case ConsoleKey.D2:

                        Console.Write("Välj indexnummer att radera: ");

                        //If-sats för att bedöma inputten. Omvandla input (string) till int (index för delete) och om den är mindre än 0, eller lika stor eller större än längden, neka input.
                        if (int.TryParse(Console.ReadLine(), out int delete))
                            if (delete < 0 || delete >= entryArray.entries.Count)
                            {
                                Console.WriteLine("Angivet nummer utanför listan. Ange ett giltigt indexnummer");
                            }
                            else
                            { //Annars ta bort motsvarande indexnummer, kör samma serialisering för att skriva över den gamla informationen och spara till fil.
                                entryArray.entries.RemoveAt(delete);

                                var jsonDel = JsonSerializer.Serialize(entryArray.entries);

                                File.WriteAllText("guestbook.txt", jsonDel);
                            }

                        else
                        {
                            Console.WriteLine("Du måste ange ett nummer");
                        }
                        break;

                    //Alternativ ESC
                    case ConsoleKey.Escape:
                        //Stänger konsollapp.
                        return;

                }

            }

        }
    }
}