using System;

[Serializable]
public class Currency
{
    public int gold;
    public int ruby;

    public Currency(int coin, int ruby)
    {
        this.gold = coin;
        this.ruby = ruby;
    }

    public Currency() { }

}
