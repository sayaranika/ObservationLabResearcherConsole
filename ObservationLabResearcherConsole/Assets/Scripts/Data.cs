[System.Serializable]
public class Data
{
    public string message;
    public string action;

    public Data(string message)
    {
        action = "sendmessage";
        this.message = message;
    }
}