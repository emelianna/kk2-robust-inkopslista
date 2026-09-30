// One item on the shopping list.
class Item
{
    public string Name { get; set; }
    public int Price { get; set; }

    public Item(string name, int price) //Först string, sen int
    {
        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Name} - {Price} kr";
    }
}
