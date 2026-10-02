// Holds the items and takes care of loading and saving them.
class ShoppingList
{
    private List<Item> items = new List<Item>();
    private string path;
    private int budgetLimit = 1000; //Budgettaket


    public ShoppingList(string path)
    {
        this.path = path;
    }

    public bool Add(Item item)    //Om priset på varan tillsammans med redan köpta varor ryms inom budget läggs varan till. Annars skickas värde false till Program.cs och meddelande skrivs ut
    {
        if (Total() + item.Price <= budgetLimit)
        {
            items.Add(item);
            return true;

        }
        return false;
    }

    // Removes the item the user sees as number 1, 2, 3 ...
    public void RemoveAt(int number)
    {
        items.RemoveAt(number - 1);
    }

    // Adds up the price of every item on the list.
    public int Total()
    {
        int sum = 0;

        for (int i = 0; i < items.Count; i++)
        {
            sum += items[i].Price;
        }

        return sum;
    }

    // Looks up an item by its name. Returns null if there is no such item.
    public Item Find(string name)
    {
        foreach (Item item in items)
        {
            if (item.Name == name)
            {
                return item;
            }
        }

        return null;
    }

    public void Print()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i]}"); //items[i] är ett Itemobjekt. I Item.cs finns override string ToString() som bestämmer hur utskriften ska se ut
        }

        Console.WriteLine($"Totalt: {Total()} kr. Du har {budgetLimit - Total()} kvar att handla för.");
    }

    // Writes one item per line, as "price;name".
    public void Save()
    {
        List<string> lines = new List<string>();

        foreach (Item item in items)
        {
            lines.Add($"{item.Price};{item.Name}");
        }

        try
        {
            File.WriteAllText(path, string.Join("\r\n", lines) + "\r\n"); //sätter ihop alla rader till en lång text och lägger \r\n mellan varje rad. \r\n på slutet gör att filen slutar med en tom rad. path visar vilken fil detta gäller. WriteAllText skapar en fil om det inte finns någon redan. Den skapar ingen ny vid problem med befintlig. 
            Console.WriteLine("Listan är sparad.");
        }
        catch (IOException ex) //Fel vid läsning eller skrivning av filer
        {
            Console.WriteLine($"Listan kunde inte sparas: {ex.Message}"); //Tar med C# egen beskrivning av vad som gick fel i ex.Message
        }

        catch (UnauthorizedAccessException) //Du har inte tillåtelse att skriva i filen, kan tex vara skrivskyddad
        {
            Console.WriteLine("Du saknar behörighet till filen listan sparas i, listan gick inte att spara.");
        }


    }

    // Reads the file back into the list.
    public void Load()
    {
        try //försöker läsa in filen och lägga till varorna
        {
            string text = File.ReadAllText(path); //items.txt läses och innehållet returneras som en sträng
            string[] lines = text.Split('\n'); //arrayen lines skapas med delar av den långa strängen ovan, i rader vid varje \n. Ser tex ut så här: 15;Mjölk\r. \r hänger med från Save. \n tas bort när den delar

            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line)) //nedan görs bara med rader med innehåll, ej tomma rader
                {
                    string[] parts = line.Split(';'); //en rad i taget delas upp vid varje ;. "15;Mjölk\r" blir ["15", "Mjölk\r"]. ; tas bort när den delar
                    items.Add(new Item(parts[1].Trim(), int.Parse(parts[0]))); //ett nytt item bestående av del på index 1 och index 0 skapas. parts[1] är en string (Mjölk\r) och parts[0] görs om till int från string (15). Trim används för att ta bort \r i slutet av parts[1]
                }
            }
        }
        catch (FileNotFoundException) //om filen saknas hoppar programmet hit, skriver ut ett meddelande och fortsätter med en tom lista
        {
            Console.WriteLine("Ingen sparad lista hittades. Du börjar med en tom lista.");
        }
    }
}