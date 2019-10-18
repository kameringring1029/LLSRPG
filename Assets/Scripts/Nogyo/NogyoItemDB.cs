using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * アイテムのデータベース
 * シングルトンにしました
 */

public sealed class NogyoItemDB{

    // 唯一のインスタンス
    private static NogyoItemDB instance = new NogyoItemDB(); 

   public Dictionary<string, NogyoItem> db { get; }

    // インスタンスのゲッタ
    public static NogyoItemDB getinstance()
    {
        return instance;
    }
    
    // コンストラクタ
    private NogyoItemDB()
    {
        db = new Dictionary<string, NogyoItem>();

        db.Add("Seed_GMary", 
            new NogyoItem("Seed_GMary", "ゴールドマリーのたね","わーお",NogyoItem.NogyoItemGroup.Seed, 100, -1,Produce.PRODUCE_TYPE.GMary));
        db.Add("Seed_WClover", 
            new NogyoItem("Seed_WClover", "シログメクサのたね", "くろーばー", NogyoItem.NogyoItemGroup.Seed, 100, -1, Produce.PRODUCE_TYPE.WClover));
        db.Add("Harv_GMary", 
            new NogyoItem("Harv_GMary", "ゴールドマリー","わお",NogyoItem.NogyoItemGroup.Flower, 500, -1,Produce.PRODUCE_TYPE.GMary));
        db.Add("Harv_WClover", 
            new NogyoItem("Harv_WClover", "シログメクサ","くろーばー",NogyoItem.NogyoItemGroup.Flower, 500, -1,Produce.PRODUCE_TYPE.WClover));
        db.Add("Water_Normal",
            new NogyoItem("Water_Normal", "お水", "どこの水でしょう", NogyoItem.NogyoItemGroup.Water, 100));
        db.Add("Chemi_Normal",
            new NogyoItem("Chemi_Normal", "肥料", "どこの肥料でしょう", NogyoItem.NogyoItemGroup.Chemi, 100));

    }

    /*
     * ProduceTypeからアイテム情報を取得
     */
    public NogyoItem getItemFromPType(NogyoItem.NogyoItemGroup group, Produce.PRODUCE_TYPE ptype)
    {
        foreach(KeyValuePair<string, NogyoItem> pair in db)
        {
            if (pair.Value.group == group && pair.Value.producetype == ptype)
                return db[pair.Key];
        }

        return null;
    }


    /*
    public NogyoItem.NogyoItemGroup getGroupFromProdState(Produce prod)
    {
        switch (prod.status)
        {
            case Produce.PRODUCE_STATE.Harvest:
                if()
                return NogyoItem.NogyoItemGroup.Harvest;

            default:
                return NogyoItem.NogyoItemGroup.Seed;
        }
    }
    */
}
