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
            new NogyoItem("Seed_GMary", "ゴールドマリーのたね","わーお",NogyoItem.NogyoItemGroup.Seed, 100, -1, new NogyoItemStatus(1,0,1,0,2,0), Produce.PRODUCE_TYPE.GMary));
        db.Add("Seed_WClover", 
            new NogyoItem("Seed_WClover", "シログメクサのたね", "シャジクソウ属の多年草。 別名、クローバー。 原産地はヨーロッパ。 花期は春から秋。", NogyoItem.NogyoItemGroup.Seed, 100, -1, new NogyoItemStatus(1, 0,1,0,2,0), Produce.PRODUCE_TYPE.WClover));
        db.Add("Harv_GMary", 
            new NogyoItem("Harv_GMary", "ゴールドマリー","わお",NogyoItem.NogyoItemGroup.Flower, 500, -1, new NogyoItemStatus(1, 1,2,1,3,1), Produce.PRODUCE_TYPE.GMary));
        db.Add("Harv_WClover", 
            new NogyoItem("Harv_WClover", "シログメクサ","くろーばー",NogyoItem.NogyoItemGroup.Flower, 500, -1, new NogyoItemStatus(1, 3,1,1,1,2), Produce.PRODUCE_TYPE.WClover));
        db.Add("Harv_Mikan",
            new NogyoItem("Harv_Mikan", "ニカン", "常緑低木に実るやつ。鉢植えでも育つしムシも付きづらいしで意外と手軽なんだよね。皮も色々使えてべんりだよ。かんかん。『純潔』『親愛』", NogyoItem.NogyoItemGroup.Fruit, 500, -1, new NogyoItemStatus(2, 5, 1, 4, 1, 2), Produce.PRODUCE_TYPE.Mikan));
        db.Add("Harv_SBerry",
            new NogyoItem("Harv_SBerry", "ストローベリー", "すとろー", NogyoItem.NogyoItemGroup.Fruit, 500, -1, new NogyoItemStatus(1, 3, 1, 5, 1, 2), Produce.PRODUCE_TYPE.SBerry));
        db.Add("Harv_Apple",
            new NogyoItem("Harv_Apple", "ナップル", "おりんご", NogyoItem.NogyoItemGroup.Fruit, 500, -1, new NogyoItemStatus(1, 3, 1, 5, 1, 2), Produce.PRODUCE_TYPE.SBerry));
        db.Add("Harv_Grape",
            new NogyoItem("Harv_Grape", "グレイブ", "ドリルっぽい", NogyoItem.NogyoItemGroup.Fruit, 500, -1, new NogyoItemStatus(1, 3, 1, 5, 1, 2), Produce.PRODUCE_TYPE.SBerry));
        db.Add("Water_Normal",
            new NogyoItem("Water_Normal", "お水", "どこの水でしょう", NogyoItem.NogyoItemGroup.Water, 0, 0, new NogyoItemStatus(1, 0,0,0,0,0)));
        db.Add("Chemi_Normal",
            new NogyoItem("Chemi_Normal", "肥料", "どこの肥料でしょう", NogyoItem.NogyoItemGroup.Chemi, 100, -1, new NogyoItemStatus(1, 1,1,1,1,1)));

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
