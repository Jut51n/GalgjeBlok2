namespace Domain;

public struct GameStats
{
    public int AantalPotjes { get; set; }
    public int VerlorenPotjes { get; set; }
    public double VerkeerdeLettersGemiddeld { get; set; }
    public double AantalPogingenGemiddeld { get; set; }

    public GameStats(int aantal, int verloren, double verkeerdeLetters, double pogingen)
    {
        AantalPotjes = aantal;
        VerlorenPotjes = verloren;
        VerkeerdeLettersGemiddeld = verkeerdeLetters;
        AantalPogingenGemiddeld = pogingen;
    }
}
