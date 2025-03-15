public class Players
{

    private string playersName = "";
    private string playersColor;
    private int victoryPoints = 0;
    private int order;

    public Players(string name, string newColor)
    {
        this.playersName = name;
        this.playersColor = newColor;
    }

    public string getName()
    {
        return playersName;
    }

    public void setOrder(int newOrder)
    {
        order = newOrder;
    }

    public int getOrder()
    {
        return this.order;
    }

    public string getColor()
    {
        return playersColor;
    }

    public int getVictoryPoints()
    {
        return victoryPoints;
    }
}
