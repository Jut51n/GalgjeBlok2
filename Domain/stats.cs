namespace Domain;

public class stats
{
    public int Id { get; set; }
    public DateTime datetime { get; set; }
    public bool Won { get; set; }
    public int Tries { get; set; }
    public int WrongLettersGuessed { get; set; }
    public int SpelerId { get; set; }
    public virtual Speler? Speler { get; set; }

    public stats()
    {

    }

    public stats(bool won, int tries, int wrongLetters, Speler speler)
    {
        datetime = DateTime.Now;
        Won = won;
        Tries = tries;
        WrongLettersGuessed = wrongLetters;
        SpelerId = speler.Id;
    }
}


