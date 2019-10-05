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
        Debug.Log(item.name);
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
        showItems();
    }

    public void showItems()
    {
        // リストの出力
        foreach (KeyValuePair<NogyoItem, int> item in items) // Dict型のforeach http://kan-kikuchi.hatenablog.com/entry/Dictionary_foreach
        {
            Debug.Log(item.Key.name + ":" + item.Value);
        }
    }
    

}
