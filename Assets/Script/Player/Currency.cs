using System;

[Serializable]
public class Currency
{
    public int coin;
    public int ruby;

    public Currency(int coin, int ruby)
    {
        this.coin = coin;
        this.ruby = ruby;
    }

    public Currency() { }

}
