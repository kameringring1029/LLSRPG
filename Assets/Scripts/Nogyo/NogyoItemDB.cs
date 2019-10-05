using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NogyoItemDB{

   public Dictionary<string, NogyoItem> db { get; }
    
    public NogyoItemDB()
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

    }

    public NogyoItem getItemFromPType(NogyoItem.NogyoItemGroup group, Produce.PRODUCE_TYPE ptype)
    {
        foreach(KeyValuePair<string, NogyoItem> pair in db)
        {
            if (pair.Value.group == group && pair.Value.producetype == ptype)
                return db[pair.Key];
        }

        return null;
    }

}
