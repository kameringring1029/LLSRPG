using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NogyoGeneral;

/*
 * アイテムのデータクラス
 */
public class NogyoItem
{
    public enum NogyoItemGroup { Null, Seed, Flower, Fruit, Vegi, Soil, Chemi}

    public string id { get; }
    public string name { get; }
    public NogyoItemGroup group { get; }
    public Sprite sprite { get; }
    public string explain { get; }
    public int price_sell { get; }
    public int price_buy { get; }
    public Produce.PRODUCE_TYPE producetype { get; }

    public NogyoItem(string id, string name, string explain,  NogyoItemGroup group, int price_sell, int price_buy = -1, Produce.PRODUCE_TYPE producetype=Produce.PRODUCE_TYPE.Not)
    {
        this.id = id;
        this.name = name;
        this.explain = explain;
        this.group = group;
        this.price_sell = price_sell;
        this.price_buy = price_buy; if (price_buy == -1) price_buy = price_sell * 3; //特に買値設定されていなければ売値の3倍
        this.producetype = producetype;

        //sprite = Resources.Load<Sprite>("");
    }

}
