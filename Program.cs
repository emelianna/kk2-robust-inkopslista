ShoppingList list = new ShoppingList("items.txt");
list.Load();
while (true)
{
    Console.WriteLine();
    list.Print();
    Console.WriteLine();
    Console.WriteLine("1. Lägg till vara");
    Console.WriteLine("2. Ta bort vara");
    Console.WriteLine("3. Spara");
    Console.WriteLine("4. Sök vara");
    Console.WriteLine("5. Avsluta");
    Console.Write("Välj: ");

    int choice;
    int price;
    int number;

    while (!int.TryParse(Console.ReadLine(), out choice))
    {
        Console.Write("Skriv en siffra mellan 1-5: ");
    }

    if (choice == 1)
    {
        Console.Write("Namn: ");
        string name = Console.ReadLine();

        Console.Write("Pris: ");

        while (!int.TryParse(Console.ReadLine(), out price) || price < 0) //Om inte ett positivt heltal skrivs så för användaren meddelande om det. Annars läggs det till direkt
        {
            Console.Write("Skriv ett giltigt, positivt heltal: ");
        }
        try
        {
            if (!list.Add(new Item(name, price)))
            {
                Console.WriteLine("Du är utanför budget, varan kan inte läggas till.");
            }
        }

        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Priset får inte vara negativt");
        }

        catch (ArgumentException)
        {
            Console.WriteLine("Namnet får inte vara tomt.");
        }

    }
    else if (choice == 2)
    {
        Console.Write("Nummer: ");

        while (!int.TryParse(Console.ReadLine(), out number))
        {
            Console.Write("Skriv siffra för vilken vara du vill ta bort: ");
        }
        try
        {
            list.RemoveAt(number);
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine($"Det finns ingen vara med nummer {number}.");
        }
    }
    else if (choice == 3)
    {
        list.Save();
    }
    else if (choice == 4)
    {
        Console.Write("Namn att söka efter: ");
        string wanted = Console.ReadLine();
        Item found = list.Find(wanted);

        if (found == null)
        {
            Console.WriteLine("Varan finns inte i listan.");
        }
        else
        {
            Console.WriteLine($"Hittade: {found}");
        }
    }
    else if (choice == 5)
    {
        break;
    }
}
