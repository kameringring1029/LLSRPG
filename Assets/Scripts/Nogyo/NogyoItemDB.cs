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

    }

}
