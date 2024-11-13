namespace Marathon.models;

public class ResultCollection
{
    public result[] results { get; set; }
}

public class result
{
    public string placment { get; set; }
    public string name { get; set; }
    public string time { get; set; }

    public string detail
    {
        get
        {
            return $"{placment} Place       Time:{time}";
        }
    }
}