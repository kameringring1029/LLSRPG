using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet{

    public int money { get; private set; }

    public Wallet(int money)
    {
        this.money = money;
    }

    /*
     * come : 増減額
     */
    public void comeMoney(int come)
    {
        money += come;
    }
}
