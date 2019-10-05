using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox
{
    public NogyoItemDB db { get; }
    public Dictionary<NogyoItem, int> items { get; }

    public ItemBox()
    {
        db = new NogyoItemDB();
        items = new Dictionary<NogyoItem, int>();
    }

    /*
     * アイテム数増減
     */
    public void changeItemNum(NogyoItem item, int num)
    {
        switch (items.ContainsKey(item))
        {
            case true:
                items[item] += num;
                if (items[item] == 0) items.Remove(item);
                break;
            case false:
                items.Add(item, num);
                break;
        }
    }

    

}
