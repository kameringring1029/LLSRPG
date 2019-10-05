using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * セーブデータ用のクラス
 */

public class PlayerData
{
    public int day { get; private set; }
    public Wallet wallet;
    public Dictionary<BalconyState.BALCONY, BalconyState> balconies;
    public ItemBox itembox;

    public PlayerData()
    {
        day = 0;
        wallet = new Wallet(1000);
        balconies = new Dictionary<BalconyState.BALCONY, BalconyState>();
        itembox = new ItemBox();
    }


    /*
     * 新規に農園を用意
     */
    public void createBalcony(BalconyState.BALCONY name, List<int[]> plantpos)
    {
        balconies.Add(name, new BalconyState(plantpos));
    }

}
