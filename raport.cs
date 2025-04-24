public class ElementRaportu
{
    public string Nazwa { get; set; }
    public string Szczegoly { get; set; }
}

public class Raport
{
    public string Tytul { get; set; }
    public DateTime DataStworzenia { get; set; }
    public List<ElementRaportu> Elementy { get; set; } = new();
}
