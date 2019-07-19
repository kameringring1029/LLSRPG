using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Information;
using General;


/*
 * WebSocketのタスクスタックアイテム用クラス 
 */
public class WSStackItem
{
    public WSITEMSORT sort;
    public mapinfo map;

    public WSStackItem(WSITEMSORT sort=WSITEMSORT.NONE, string option="")
    {
        this.sort = sort;

        if(sort == WSITEMSORT.ESTROOM) // ルーム確立時にmap情報を格納
        {
              map = JsonUtility.FromJson<mapinfo>(option);
        }
    }
}
