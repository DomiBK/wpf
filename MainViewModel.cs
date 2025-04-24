public class MainViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Raport> Raporty { get; set; } = new();
    public Raport AktualnyRaport { get; set; } = new();

    public ICommand DodajElementCommand { get; }
    public ICommand ZapiszRaportCommand { get; }
    public ICommand EksportujPDFCommand { get; }

    public MainViewModel()
    {
        DodajElementCommand = new RelayCommand(DodajElement);
        ZapiszRaportCommand = new RelayCommand(ZapiszRaport);
        EksportujPDFCommand = new RelayCommand(EksportujPDF);
        WczytajRaporty();
    }

    private void DodajElement()
    {
        AktualnyRaport.Elementy.Add(new ElementRaportu { Nazwa = "Nowy", Szczegoly = "Szczegóły" });
        OnPropertyChanged(nameof(AktualnyRaport));
    }

    private void ZapiszRaport()
    {
        Raporty.Add(AktualnyRaport);
        ZapiszDoPliku();
        AktualnyRaport = new();
        OnPropertyChanged(nameof(AktualnyRaport));
    }

    private void EksportujPDF()
    {
        PDFExporter.Export(AktualnyRaport);
    }

    private void ZapiszDoPliku()
    {
        File.WriteAllText("raporty.json", JsonSerializer.Serialize(Raporty));
    }

    private void WczytajRaporty()
    {
        if (File.Exists("raporty.json"))
        {
            var json = File.ReadAllText("raporty.json");
            Raporty = JsonSerializer.Deserialize<ObservableCollection<Raport>>(json);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
