using System;
using System.ComponentModel.Design;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks.Dataflow;

namespace laboration3
{

    class EntryArray
    {
        public List<Entry> entries = new List<Entry>();
    }

    class Entry
    {


        public string Name { get; set; }

        public string Message { get; set; }
    }

    class Guestbook
    {
        static void Main(string[] args)
        {
            EntryArray entryArray = new EntryArray();

            if (File.Exists("guestbook.txt"))
            {
                string json = File.ReadAllText("guestbook.txt");
                if(!string.IsNullOrWhiteSpace(json))
                {
                    entryArray.entries = JsonSerializer.Deserialize<List<Entry>>(json);
                }
                
            }


           

            while (true)
            {
                Console.Clear();

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

                if (entryArray.entries.Count == 0)
                {
                    Console.WriteLine("Gästboken är tom!");
                }
                else
                {
                    for (int i = 0; i < entryArray.entries.Count; i++)
                    {
                        var entry = entryArray.entries[i];
                        Console.WriteLine($"[{i}] {entry.Name}: {entry.Message}");
                    }
                }


                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Välj alternativ: ");
                var userInput = Console.ReadKey(true);

                Console.Clear();

                switch (userInput.Key)
                {
                    case ConsoleKey.D1:
                        

                        string userName = "";
                        while (string.IsNullOrWhiteSpace(userName))
                        {
                            Console.Write("Ditt namn: ");
                            userName = Console.ReadLine();
                        }


                        string userMessage = "";
                        while (string.IsNullOrWhiteSpace(userMessage))
                        {
                            Console.Write("Ditt meddelande: ");
                            userMessage = Console.ReadLine();
                        }

                        

                        var entry = new Entry
                        {
                            Name = userName,
                            Message = userMessage
                        };

                        entryArray.entries.Add(entry);

                        var jsonAdd = JsonSerializer.Serialize(entryArray.entries);

                        File.WriteAllText("guestbook.txt", jsonAdd);
                        break;

                    case ConsoleKey.D2:

                        Console.Write("Välj indexnummer att radera: ");


                        if (int.TryParse(Console.ReadLine(), out int delete))
                            if (delete < 0 || delete >= entryArray.entries.Count)
                            {
                                Console.WriteLine("Angivet nummer utanför listan. Ange ett giltigt indexnummer");
                            }
                            else
                            {
                                entryArray.entries.RemoveAt(delete);

                                var jsonDel = JsonSerializer.Serialize(entryArray.entries);

                                File.WriteAllText("guestbook.txt", jsonDel);
                            }

                        else
                        {
                            Console.WriteLine("Du måste ange ett nummer");
                        }
                        break;

                    case ConsoleKey.Escape:
                        return;

                }

            }

        }
    }
}