namespace Domain;

public class Speler
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public List<stats>? Stats { get; set; }

    public Speler(string username)
    {
        UserName = username;
    }

    public Speler()
    {
    }
}
